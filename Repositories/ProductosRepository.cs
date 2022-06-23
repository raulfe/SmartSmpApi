using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities.NuevosProducto;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly ILogger<ProductosRepository> _logger;
        private readonly IProductosCommand _productos;
        public ProductosRepository(ILogger<ProductosRepository> logger, IProductosCommand productos)
        {
            _logger = logger;
            _productos = productos;
        }

        public async Task<IEnumerable<ProductosR>> getPlan(int? id)
        {
            var data = await _productos.getPlan();
            if(data.Count() == 0 || data == null)
            {
                throw new BusinessException("No hay planes en el inventario");
            }
            if(id != null)
            {
                data = data.Where(x => x.Tipo == id).ToList();
            }
            return data;
        }

        public async Task<object> getPlanById(int id)
        {
            var data = await _productos.getPlanById(id);
            if(data == null)
            {
                throw new BusinessException("El Plan no existe");
            }
            if(data.Tipo == 1)
            {
                var paises = await _productos.getPaisByPlan(id);
                var smartpack = await _productos.getSmartPackById(data.Smart_Pack);
                var adicionales = await _productos.getAdicionales(data.Smart_Pack);
                var result = new
                {
                    Plan = data,
                    SmartPack = smartpack,
                    Paises = paises,
                    Adicionales = adicionales
                };
                return result;
            }else if(data.Tipo == 2)
            {
                var result = new
                {
                    Plan = data,
                    Tipo = "Plan adicional"
                };
                return result;
            }
            else
            {
                throw new BusinessException("Plan no identificado / Tipo no valido");
            }
        }
        public async Task<bool> processNewPlan(NewProduct prod)
        {
            var codValidation = await _productos.getPlanByCode(prod.Plan.Codigo);
            if(codValidation != null)
            {
                _logger.LogError("El codigo ya ha sido usado para otro Plan");
                throw new BusinessException("El codigo ya ha sido usado para otro Plan");
            }
            var smartValidation = await _productos.getSmartPack(prod.Smartpack.Nombre);
            if (smartValidation != null)
            {
                _logger.LogError("El nombre ya ha sido usado para otro SmartPack");
                throw new BusinessException("El nombre ya ha sido usado para otro SmartPack");
            }
            if(prod.Paises.Count() == 0)
            {
                _logger.LogError("El plan deben contener al menos un pais donde aplicarse");
                throw new BusinessException("El plan deben contener al menos un pais donde aplicarse");

            }
            if(prod.Plan.Tipo == 1)
            {
                var resInsert = await _productos.insertNewPlan(prod.Plan);
                if (resInsert == 0)
                {
                    _logger.LogError("El producto no pudo ser creado");
                    throw new BusinessException("El producto no pudo ser creado");
                }

                _logger.LogInformation("Producto creado satisfactoriamente");
                var newCode = await _productos.getPlanByCode(prod.Plan.Codigo);
                var idPlan = newCode.Producto;
                foreach (var pais in prod.Paises)
                {
                    var resPais = await _productos.insertProductoPais(pais, idPlan);
                    if (resPais == 0)
                    {
                        _logger.LogError($"Error al insertar el producto {idPlan} con el pais {pais.Pais}");
                        continue;
                    }
                }
                var resSmart = await _productos.insertSmartPack(prod.Smartpack, idPlan);
                if (resSmart > 0)
                {
                    var smartNombre = await _productos.getSmartPack(prod.Smartpack.Nombre);
                    var updatePlan = await _productos.updatePlanAddSmartpack(idPlan,smartNombre.Smart_Pack);
                    if(updatePlan == 0)
                    {
                        _logger.LogError($"Error al actualizar el Plan previamente creado");
                    }
                    foreach (var adicional in prod.Adicionales)
                    {
                        var resAdicional = await _productos.insertNewAdicional(adicional, smartNombre.Smart_Pack);
                        if (resAdicional == 0)
                        {
                            _logger.LogError($"Error al agregar el adicional {adicional.Adicional} para el smartpack {smartNombre.Smart_Pack}");
                            continue;
                        }
                    }
                }
            }
            else if(prod.Plan.Tipo == 2)
            {
                var resInsert = await _productos.insertNewPlan(prod.Plan);
                if (resInsert == 0)
                {
                    _logger.LogError("El adicional no pudo ser creado");
                    throw new BusinessException("El adicional no pudo ser creado");
                }

                _logger.LogInformation("Adicional creado satisfactoriamente");
                var newCode = await _productos.getPlanByCode(prod.Plan.Codigo);
                var idPlan = newCode.Producto;
                foreach (var pais in prod.Paises)
                {
                    var resPais = await _productos.insertProductoPais(pais, idPlan);
                    if (resPais == 0)
                    {
                        _logger.LogError($"Error al insertar el producto {idPlan} con el pais {pais.Pais}");
                        continue;
                    }
                }
            }
            
            return true;
        }
    }
}
