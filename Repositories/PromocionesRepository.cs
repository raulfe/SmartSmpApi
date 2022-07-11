using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities.NuevaPromocion;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class PromocionesRepository : IPromocionesRepository
    {
        private readonly ILogger<LoginRepository> _logger;
        private readonly IPromocionesCommand _promociones;

        public PromocionesRepository(ILogger<LoginRepository> logger, IPromocionesCommand promociones)
        {
            _logger = logger;
            _promociones = promociones;
        }

        public async Task<IEnumerable<Promociones>> getPromociones()
        {
            var data = await _promociones.getPromociones();
            if(data == null || data.Count() == 0)
            {
                _logger.LogError("No hay promociones en la db");
                throw new BusinessException("No hay promociones en la db");
            }
            return data;
        }

        public async Task<bool> updateStatus(bool status,int promocion)
        {
            var data = await _promociones.updatePromocion(status, promocion);
            if(data == 0)
            {
                throw new BusinessException("El estatus de la promocion no pudo ser actualizado");
            }
            return true;
        }

        public async Task<bool> processNewPromoProduct(NewPromocion promo)
        {
            if(promo.Paises.Count == 0)
            {
                throw new BusinessException("La promocion debe al menos contener un pais donde aplicarse");
            }
            if(promo.Planes.Count == 0)
            {
                throw new BusinessException("La promocion debe contener al menos un plan");
            }

            var resInsPromo = await _promociones.insertNewPromo(promo.Promocion);
            if(resInsPromo == 0)
            {
                throw new BusinessException("La promocion no pudo ser registrada");
            }
            else
            {
                var newPromo = await _promociones.getPromocionByName(promo.Promocion.Nombre);
                if(newPromo == null)
                {
                    throw new BusinessException("La promocion no pudo ser registrada");
                }
                foreach(var pais in promo.Paises)
                {
                    var resPais = await _promociones.insertPromoPaises(pais, newPromo.Promocion);
                    if(resPais == 0)
                    {
                        throw new BusinessException("El pais no pudo ser asignado a la promocion");
                    }
                }

                foreach(var plan in promo.Planes)
                {
                    var resPlan = await _promociones.insertPromoProductos(plan, newPromo.Promocion);
                    if (resPlan == 0)
                    {
                        throw new BusinessException("El plan no pudo ser asignado a la promocion");
                    }
                }
                return true;
            }
        }

        public async Task<bool> processNewPromoMembresia(NewPromocionMembresia promo)
        {
            if (promo.Paises.Count == 0)
            {
                throw new BusinessException("La promocion debe al menos contener un pais donde aplicarse");
            }
            if (promo.Membresias.Count == 0)
            {
                throw new BusinessException("La promocion debe contener al menos una membresia");
            }

            var resInsPromo = await _promociones.insertNewPromo(promo.Promocion);
            if (resInsPromo == 0)
            {
                throw new BusinessException("La promocion no pudo ser registrada");
            }
            else
            {
                var newPromo = await _promociones.getPromocionByName(promo.Promocion.Nombre);
                if (newPromo == null)
                {
                    throw new BusinessException("La promocion no pudo ser registrada");
                }
                foreach (var pais in promo.Paises)
                {
                    var resPais = await _promociones.insertPromoPaises(pais, newPromo.Promocion);
                    if (resPais == 0)
                    {
                        throw new BusinessException("El pais no pudo ser asignado a la promocion");
                    }
                }

                foreach (var membership in promo.Membresias)
                {
                    var resPlan = await _promociones.insertPromoMembresias(membership, newPromo.Promocion);
                    if (resPlan == 0)
                    {
                        throw new BusinessException("El plan no pudo ser asignado a la promocion");
                    }
                }
                return true;
            }
        }

        public async Task<bool> updatePromoProduct(NewPromocion promo)
        {
            var valPromo = await _promociones.getPromocionById(promo.Promocion.Promocion);
            if(valPromo == null)
            {
                _logger.LogError("La promocion no existe");
                throw new BusinessException("La promocion no existe");
            }
            if(promo.Paises.Count == 0)
            {
                _logger.LogError("La promocion debe contener al menos un pais donde aplicarse");
                throw new BusinessException("La promocion debe contener al menos un pais donde aplicarse");
            }
            if(promo.Planes.Count == 0)
            {
                _logger.LogError("La promocion debe contener al menos un plan donde aplicarse");
                throw new BusinessException("La promocion debe contener al menos un plan donde aplicarse");
            }
            var resPromo = await _promociones.updatePromocionIn(promo.Promocion);
            if(resPromo == 0)
            {
                _logger.LogError("La promocion no pudo ser actualizada");
                throw new BusinessException("La promocion no pudo ser actualizada");
            }
            var delPaises = await _promociones.deletePromoPaises(promo.Promocion.Promocion);
            foreach(var pais in promo.Paises)
            {
                var insertPais = await _promociones.insertPromoPaises(pais, promo.Promocion.Promocion);
                if(insertPais == 0)
                {
                    _logger.LogInformation("El pais no pudo ser agregado a la promocion");
                }
            }
            var delPlanes = await _promociones.deletePromoPlanes(promo.Promocion.Promocion);
            foreach(var plan in promo.Planes)
            {
                var insertPlan = await _promociones.insertPromoProductos(plan, promo.Promocion.Promocion);
                if (insertPlan == 0)
                {
                    _logger.LogInformation("El plan no pudo ser agregado a la promocion");
                }
            }
            return true;
        }

        public async Task<bool> updatePromoMembresias(NewPromocionMembresia promo)
        {
            var valPromo = await _promociones.getPromocionById(promo.Promocion.Promocion);
            if (valPromo == null)
            {
                _logger.LogError("La promocion no existe");
                throw new BusinessException("La promocion no existe");
            }
            if (promo.Paises.Count == 0)
            {
                _logger.LogError("La promocion debe contener al menos un pais donde aplicarse");
                throw new BusinessException("La promocion debe contener al menos un pais donde aplicarse");
            }
            if (promo.Membresias.Count == 0)
            {
                _logger.LogError("La promocion debe contener al menos un plan donde aplicarse");
                throw new BusinessException("La promocion debe contener al menos un plan donde aplicarse");
            }
            var resPromo = await _promociones.updatePromocionIn(promo.Promocion);
            if (resPromo == 0)
            {
                _logger.LogError("La promocion no pudo ser actualizada");
                throw new BusinessException("La promocion no pudo ser actualizada");
            }
            var delPaises = await _promociones.deletePromoPaises(promo.Promocion.Promocion);
            foreach (var pais in promo.Paises)
            {
                var insertPais = await _promociones.insertPromoPaises(pais, promo.Promocion.Promocion);
                if (insertPais == 0)
                {
                    _logger.LogInformation("El pais no pudo ser agregado a la promocion");
                }
            }
            var delMembership = await _promociones.deletePromoMembresias(promo.Promocion.Promocion);
            foreach (var membership in promo.Membresias)
            {
                var insertPlan = await _promociones.insertPromoMembresias(membership, promo.Promocion.Promocion);
                if (insertPlan == 0)
                {
                    _logger.LogInformation("El plan no pudo ser agregado a la promocion");
                }
            }
            return true;
        }

        public async Task<object> getPromocionByIDCustom(int id)
        {
            var promo = await _promociones.getPromocionById(id);
            if(promo == null)
            {
                _logger.LogError("La promocion no existe");
                throw new BusinessException("La promocion no existe");
            }
            var paises = await _promociones.getPromocionPaisById(id);
            if(paises.Count() == 0 || paises == null)
            {
                _logger.LogError("La promocion no contiene paises");
                throw new BusinessException("La promocion no contiene paises");
            }
            var planes = await _promociones.getPromocionProdById(id);
            var memberships = await _promociones.getPromocionMembershipById(id);
            if(planes.Count() == 0 || planes == null)
            {
                var response = new
                {
                    Promocion = promo,
                    Membresias = memberships,
                    Paises = paises
                };
                return response;
            }
            else
            {
                var response = new
                {
                    Promocion = promo,
                    Planes = planes,
                    Paises = paises
                };
                return response;
            }
        }
    }
}
