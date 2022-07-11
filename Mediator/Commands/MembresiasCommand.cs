using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Mediator.Commands
{
    public class MembresiasCommand : IMembresiasCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MembresiasCommand> _logger;

        public MembresiasCommand(IConfiguration configuration, ILogger<MembresiasCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IEnumerable<Membresias>> getMembresias()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.membresias";
                    var data = await connection.QueryAsync<Membresias>(script);
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found:{e.Message}");
                return null;
            }


        }

        public async Task<int> insertNewMembership(Membresias membership)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.membresias (producto, tipo, nombre, precio, moneda, vigencia, fecha_ini, fecha_fin, pago_btc, pago_tdc, descripcion,activo,es_borrador)" +
                                 "VALUES (@producto, @tipo, @nombre, @precio, @moneda, @vigencia, @fecha_ini, @fecha_fin, @pago_btc, @pago_tdc, @descripcion, @activo, @es_borrador)";

                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            producto = membership.Producto,
                            tipo = membership.Tipo,
                            nombre = membership.Nombre,
                            precio = membership.Precio,
                            moneda = membership.Moneda,
                            vigencia = membership.Vigencia,
                            fecha_ini = membership.Fecha_ini,
                            fecha_fin = membership.Fecha_fin,
                            pago_btc = membership.Pago_btc,
                            pago_tdc = membership.Pago_tdc,
                            descripcion = membership.Descripcion,
                            activo = membership.Activo,
                            es_borrador = membership.Es_borrador

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

        public async Task<int> insertMembresiaPais(MembresiaPais pais, int membresia)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.membresia_paises ( membresia , pais ) " +
                        "VALUES ( @membresia, @pais )";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            membresia = membresia,
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

        public async Task<Membresias> getMembresia(string nombre)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT smart_pack FROM public.membresias WHERE nombre = @nombre";
                    var data = await connection.QueryFirstAsync<Membresias>(script, new { nombre = nombre });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<MembresiaPais>> getMembresiaPais(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT smart_pack FROM public.membresia_paises WHERE membresia = @membresia";
                    var data = await connection.QueryAsync<MembresiaPais>(script, new { membresia = id });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<Membresias> getMembresiaById(int membresia)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.membresias WHERE membresia = @membresia";
                    var data = await connection.QueryFirstAsync<Membresias>(script, new { membresia = membresia });
                    return data;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<int> updateMembership(Membresias membership) 
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.mebresias SET producto = @producto, tipo = @tipo, nombre = @nombre, precio = @precio, moneda = @moneda, vigencia = @vigencia, fecha_ini = @fecha_ini, fecha_fin = @fecha_fin, pago_btc = @pago_btc, pago_tdc = @pago_tdc, descripcion = @descripcion, activo = @activo, es_borrador = @es_borrador WHERE membresia = @membresia";
                    var data = await connection.ExecuteAsync(script,
                        new { 
                            membresia = membership.Membresia,
                            producto = membership.Producto,
                            tipo = membership.Tipo,
                            nombre = membership.Nombre,
                            precio = membership.Precio,
                            moneda = membership.Moneda,
                            vigencia = membership.Vigencia,
                            fecha_ini = membership.Fecha_ini,
                            fecha_fin = membership.Fecha_fin,
                            pago_btc = membership.Pago_btc,
                            pago_tdc = membership.Pago_tdc,
                            descripcion = membership.Descripcion,
                            activo = membership.Activo,
                            es_borrador = membership.Es_borrador

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

        public async Task<int> updateMembershipStatus(bool status,int membership)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.mebresias SET activo = @activo WHERE membresia = @membresia";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            membresia = membership,
                            activo = status

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

        public async Task<int> deleteMembresiaPaises(int membresia)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM public.membresia_paises WHERE membresia = @membresia";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            membresia = membresia
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
