using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.Socios;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Mediator.Commands
{
    public class SociosCommand : ISociosCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<SociosCommand> _logger;
        public SociosCommand(IConfiguration configuration, ILogger<SociosCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Socios> getSocioById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socios WHERE socio = @sc";
                    var data = await connection.QueryFirstAsync<Socios>(script, new { sc = id });
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<Socios> getSocioByEmail(string email)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socios WHERE email = @sc";
                    var data = await connection.QueryFirstAsync<Socios>(script, new { sc = email });
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public async Task<SociosR> getSocioByPosition(int position)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT si.tel_celular,si.calle,si.num_ext,si.num_int,si.colonia,si.municipio,si.ciudad,si.codigo_postal,s.* FROM public.socios s INNER JOIN public.socio_info si ON si.socio = s.socio LIMIT 1 OFFSET @sc";
                    var data = await connection.QueryFirstAsync<SociosR>(script, new { sc = position });
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<CountType> getSociosCount()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT COUNT(*) FROM public.socios";
                    var data = await connection.QueryFirstAsync<CountType>(script);
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<Socios_documentacion> getDocumentById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT e.nombre,d. * FROM public.enum e INNER JOIN public.socio_documentacion d ON d.tipo = e.codigo inner JOIN public.socios f ON f.socio = d.socio WHERE (f.socio = @soc) and(e.categoria = 'TipoDocumento')";
                    var data = await connection.QueryFirstAsync<Socios_documentacion>(script, new { soc = id });
                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<SocioValidacionR> getLastValidacionById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socio_validacion WHERE socio = @socio ORDER BY fecha_update DESC LIMIT 1";
                    var data = await connection.QueryFirstAsync<SocioValidacionR>(script, new { socio = id });
                    return data;
                }
            }
            catch (Exception )
            {
                return null;
            }
        }

        public async Task<SocioValidacionR> getSocioValidacionbyValidacion(int validacionId)
        {
            try 
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socio_validacion WHERE validacionId = @validacion ";
                    var data = await connection.QueryFirstAsync<SocioValidacionR>(script, new { validacion = validacionId });
                    return data;
                }

            }
            catch ( Exception) 
            {
                return null;
            }
        }

        public async Task<int> insertSocioValidacion(SocioValidacion socioValida) 
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"))) 
                {
                    var script = "INSERT INTO public.socio_validacion (socio, estatus, fecha_validacion, estatus_kyc, fecha_empresa, resultado_kyc, observaciones, validado_por, autorizado_por, payload, id_validation, id_related, fecha_insert, fecha_update)" +
                        "VALUES (@socio, @estatus, @fecha_validacion, @estatus_kyc, @fecha_empresa, @resultado_kyc, @observaciones, @validado_por, @autorizado_por, @payload, @id_validation, @id_related, @fecha_insert, @fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            socio = socioValida.Socio,
                            estatus = socioValida.Estatus,
                            fecha_validacion = socioValida.FechaValidacion,
                            estatus_kyc = socioValida.EstatusKyc,
                            fecha_empresa = socioValida.FechaEmpresa,
                            fecha_kyc = socioValida.FechaKyc,
                            resultado_kyc = socioValida.ResultadoKyc,
                            observaciones = socioValida.Observaciones,
                            validado_por = socioValida.ValidadoPor,
                            autorizado_por = socioValida.AutorizadoPor,
                            payload = socioValida.Payload,
                            id_validation = socioValida.IdValidation,
                            id_related = socioValida.IdRelated,
                            fecha_insert = socioValida.FechaInsert,
                            fecha_update = socioValida.FechaUpdate
                        });
                    return data;
                }
            }
            catch (Exception) 
            {
                return 0;
            }
        }
        public async Task<int> updateSocioIdValidacion(int validacion, int socio) 
        {
            try 
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"))) 
                {
                    var script = "UPDATE public.socios SET validacion = @validacion WHERE socio = @socio";
                    var data = await connection.ExecuteAsync(script, new {
                        prospecto = socio,
                        validacion = validacion,

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
