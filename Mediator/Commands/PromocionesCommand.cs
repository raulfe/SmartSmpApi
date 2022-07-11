using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Mediator.Commands
{
    public class PromocionesCommand : IPromocionesCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnumsCommand> _logger;
        public PromocionesCommand(IConfiguration configuration, ILogger<EnumsCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Promociones>> getPromociones()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.promociones";
                    var data = await connection.QueryAsync<Promociones>(script);
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<Promociones> getPromocionByName(string name)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.promociones WHERE nombre = @nombre";
                    var data = await connection.QueryFirstAsync<Promociones>(script,new {nombre = name});
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<Promociones> getPromocionById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.promociones WHERE promocion = @promocion";
                    var data = await connection.QueryFirstAsync<Promociones>(script, new { promocion = id });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<PromocionProductos>> getPromocionProdById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.promo_productos WHERE promocion = @promocion";
                    var data = await connection.QueryAsync<PromocionProductos>(script, new { promocion = id });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<PromocionMembresias>> getPromocionMembershipById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.promo_membresias WHERE promocion = @promocion";
                    var data = await connection.QueryAsync<PromocionMembresias>(script, new { promocion = id });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<PromocionPaises>> getPromocionPaisById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.promocion_paises WHERE promocion = @promocion";
                    var data = await connection.QueryAsync<PromocionPaises>(script, new { promocion = id });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<int> insertNewPromo(Promociones promo)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.promociones (codigo,nombre,descripcion,tiraje_limitado,monto_limitado,activo,es_borrador,fecha_ini,fecha_fin,fecha_insert,fecha_update) " +
                        "VALUES (@codigo,@nombre,@descripcion,@tiraje_limitado,@monto_limitado,@activo,@es_borrador,@fecha_ini,@fecha_fin,@fecha_insert,@fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            codigo = promo.Codigo,
                            nombre = promo.Nombre,
                            descripcion = promo.Descripcion,
                            tiraje_limitado = promo.Tiraje_limitado,
                            monto_limitado = promo.Monto_limitado,
                            activo = promo.Activo,
                            es_borrador = promo.Es_borrador,
                            fecha_ini = promo.Fecha_ini,
                            fecha_fin = promo.Fecha_fin,
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

        public async Task<int> insertPromoPaises(PromocionPaises paises,int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.promocion_paises (promocion,pais) " +
                        "VALUES (@promocion,@pais)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            promocion = promocion,
                            pais = paises.Pais
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

        public async Task<int> insertPromoProductos(PromocionProductos prods, int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.promo_productos (producto,promocion,monto,rendimiento,comision,plazo_comision) " +
                        "VALUES (@producto,@promocion,@monto,@rendimiento,@comision,@plazo_comision)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            producto = prods.Producto,
                            promocion = promocion,
                            monto = prods.Monto,
                            rendimiento = prods.Rendimiento,
                            comision = prods.Comision,
                            plazo_comision = prods.Plazo_comision
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

        public async Task<int> insertPromoMembresias(PromocionMembresias prods, int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.promo_membresias (membresia,promocion,precio) " +
                        "VALUES (@membresia,@promocion,@precio)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            membresia = prods.Membresia,
                            promocion = promocion,
                            precio = prods.Precio
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

        public async Task<int> updatePromocion(bool status, int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.promociones SET activo = @activo WHERE promocion = @promocion";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            activo = status,
                            promocion = promocion
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

        public async Task<int> updatePromocionIn(Promociones promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.promociones SET premio = @premio, insignia = @insignia,codigo = @codigo,nombre = @nombre,descripcion = @descripcion,rendimiento_extra = @rendimiento_extra,tiraje_limitado = @tiraje_limitado,monto_limitado = @monto_limitado,exclusivo_rangos = @exclusivo_rangos,descuento_porcentaje = @descuento_porcentaje,insignias_and = @insignias_and,ventas_conteo = @ventas_conteo,ventas_monto = @ventas_monto,observaciones = @observaciones,activo = @activo,es_borrador = @es_borrador,fecha_ini = @fecha_ini,fecha_fin = @fecha_fin,fecha_update = @fecha_update, WHERE promocion = @promocion";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            promocion = promocion.Promocion,
                            premio = promocion.Premio,
                            insignia = promocion.Insignia,
                            codigo = promocion.Codigo,
                            nombre = promocion.Nombre,
                            descripcion = promocion.Descripcion,
                            rendimiento_extra = promocion.Rendimiento_extra,
                            tiraje_limitado = promocion.Tiraje_limitado,
                            monto_limitado = promocion.Monto_limitado,
                            exclusivo_rangos = promocion.Exclusivo_rangos,
                            descuento_porcentaje = promocion.Descuento_porcentaje,
                            insignias_and = promocion.Insignias_and,
                            ventas_conteo = promocion.Ventas_conteo,
                            ventas_monto = promocion.Ventas_monto,
                            observaciones = promocion.Observaciones,
                            activo = promocion.Activo,
                            es_borrador = promocion.Es_borrador,
                            fecha_ini = promocion.Fecha_ini,
                            fecha_fin = promocion.Fecha_fin,
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

        public async Task<int> deletePromoPaises(int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM promocion_paises WHERE promocion = @promocion";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            promocion = promocion

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

        public async Task<int> deletePromoPlanes(int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM promo_productos WHERE promocion = @promocion";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            promocion = promocion

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

        public async Task<int> deletePromoMembresias(int promocion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM promo_membresias WHERE promocion = @promocion";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            promocion = promocion

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
