using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Mediator.Commands
{
    public class ProductosCommand : IProductosCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductosCommand> _logger;
        public ProductosCommand(IConfiguration configuration, ILogger<ProductosCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Productos> getPlanById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.productos WHERE producto = @producto";
                    var data = await connection.QueryFirstAsync<Productos>(script,new { producto = id });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<AdicionalesR>> getAdicionales(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.paquete_productos WHERE smart_pack = @smart_pack";
                    var data = await connection.QueryAsync<AdicionalesR>(script, new { smart_pack = id });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ProductoPais>> getPaisByPlan(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.producto_paises WHERE producto = @producto";
                    var data = await connection.QueryAsync<ProductoPais>(script, new { producto = id });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ProductosR>> getPlan()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.productos";
                    var data = await connection.QueryAsync<ProductosR>(script);
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<ProductosR> getPlanByCode(string code)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.productos WHERE codigo = @codigo";
                    var data = await connection.QueryFirstAsync<ProductosR>(script,new { codigo = code});
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<int> updatePlanInicial(Productos prod)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.productos  SET tipo = @tipo,codigo = @codigo,nombre = @nombre,monto = @monto,moneda = @moneda,es_monto_abierto = @es_monto_abierto,pago_btc = @pago_btc,pago_tdc = @pago_tdc,rendimiento = @rendimiento,regla_capitalizacion = @regla_capitalizacion,comision = @comision,duracion = @duracion,es_borrador = @es_borrador,fecha_ini = @fecha_ini,fecha_fin = @fecha_fin,fecha_update = @fecha_update WHERE producto = @producto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            producto = prod.Producto,
                            tipo = prod.Tipo,
                            codigo = prod.Codigo,
                            nombre = prod.Nombre,
                            monto = prod.Monto,
                            moneda = prod.Moneda,
                            es_monto_abierto = prod.Es_monto_abierto,
                            pago_btc = prod.Pago_btc,
                            pago_tdc = prod.Pago_tdc,
                            rendimiento = prod.Rendimiento,
                            regla_capitalizacion = prod.Regla_capitalizacion,
                            comision = prod.Comision,
                            plazo_forzoso = prod.Plazo_Forzoso,
                            plazo_comision = prod.Plazo_Comision,
                            duracion = prod.Duracion,
                            activo = prod.Activo,
                            es_borrador = prod.Es_borrador,
                            fecha_ini = prod.Fecha_Ini,
                            fecha_fin = prod.Fecha_Fin,
                            fecha_update = DateTime.Now
                        });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> insertNewPlan(ProductosDTO prod)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.productos (tipo,codigo,nombre,monto,moneda,es_monto_abierto,pago_btc,pago_tdc,rendimiento,regla_capitalizacion,comision,plazo_forzoso,plazo_comision,duracion,activo,es_borrador,fecha_ini,fecha_fin,fecha_insert,fecha_update) " +
                        "VALUES (@tipo,@codigo,@nombre,@monto,@moneda,@es_monto_abierto,@pago_btc,@pago_tdc,@rendimiento,@regla_capitalizacion,@comision,@plazo_forzoso,@plazo_comision,@duracion,@activo,@es_borrador,@fecha_ini,@fecha_fin,@fecha_insert,@fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            tipo = prod.Tipo,
                            codigo = prod.Codigo,
                            nombre = prod.Nombre,
                            monto = prod.Monto,
                            moneda = prod.Moneda,
                            es_monto_abierto = prod.Es_monto_abierto,
                            pago_btc = prod.Pago_btc,
                            pago_tdc = prod.Pago_tdc,
                            rendimiento = prod.Rendimiento,
                            regla_capitalizacion = prod.Regla_capitalizacion,
                            comision = prod.Comision,
                            plazo_forzoso = prod.Plazo_Forzoso,
                            plazo_comision = prod.Plazo_Comision,
                            duracion = prod.Duracion,
                            activo = prod.Activo,
                            es_borrador = prod.Es_borrador,
                            fecha_ini = prod.Fecha_Ini,
                            fecha_fin = prod.Fecha_Fin,
                            fecha_insert = DateTime.Now,
                            fecha_update = DateTime.Now
                        });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> deleteProductoPaises(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM public.producto_paises WHERE producto = @producto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            producto = id
                        });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> insertNewAdicional(Adicionales adicional,int smart)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.paquete_productos (smart_pack,producto,rendimiento) " +
                        "VALUES (@smart_pack,@producto,@rendimiento)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            smart_pack = smart,
                            producto = adicional.Adicional,
                            rendimiento = adicional.Rendimiento
                        });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> updatePlanAddSmartpack(int plan, int smart)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.productos SET smart_pack = @smart_pack WHERE producto = @producto ";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            smart_pack = smart,
                            producto = plan
                        });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<SmartPack> getSmartPack(string nombre)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT smart_pack FROM public.paquetes WHERE nombre = @nombre";
                    var data = await connection.QueryFirstAsync<SmartPack>(script, new { nombre = nombre });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<Paquetes> getSmartPackById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.paquetes WHERE smart_pack = @smart_pack";
                    var data = await connection.QueryFirstAsync<Paquetes>(script, new { smart_pack = id });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<int> insertProductoPais(ProductoPais pais,int producto)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.producto_paises (producto,pais) " +
                        "VALUES (@producto,@pais)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            producto = producto,
                            pais = pais.Pais
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> insertSmartPack(PaquetesDTO smartpack,int plan)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.paquetes (nombre,descripcion,plan_inicial,rendimiento,fecha_ini,fecha_fin,fecha_insert,fecha_update) " +
                        "VALUES (@nombre,@descripcion,@plan_inicial,@rendimiento,@fecha_ini,@fecha_fin,@fecha_insert,@fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            nombre = smartpack.Nombre,
                            descripcion = smartpack.Descripcion,
                            plan_inicial = plan,
                            rendimiento = smartpack.Rendimiento,
                            fecha_ini = smartpack.Fecha_ini,
                            fecha_fin = smartpack.Fecha_fin,
                            fecha_insert = DateTime.Now,
                            fecha_update = DateTime.Now
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> updateSmartPack(Paquetes smartpack)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.paquetes SET descripcion = @descripcion,rendimiento = @rendimiento,fecha_ini = @fecha_ini,fecha_fin = @fecha_fin,fecha_update = @fecha_update WHERE smart_pack = @smart_pack";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            smart_pack = smartpack.Smart_pack,
                            descripcion = smartpack.Descripcion,
                            rendimiento = smartpack.Rendimiento,
                            fecha_ini = smartpack.Fecha_ini,
                            fecha_fin = smartpack.Fecha_fin,
                            fecha_update = DateTime.Now
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> updateStatusProducto(StatusProducto prod)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.productos SET activo = @activo WHERE producto = @producto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            activo = prod.Status,
                            producto = prod.Producto
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }

        public async Task<int> deletePaquetesProductos(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM public.paquete_productos WHERE smart_pack = @smart_pack";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            smart_pack = id
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                return 0;
            }
        }
    }
}
