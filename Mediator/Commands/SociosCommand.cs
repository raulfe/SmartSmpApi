using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.KycValidacion;
using SmartBusinessAPI.Entities.ValidationStatus;
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

        public async Task<SociosR> getSocioByIdInfo(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socios s INNER JOIN public.socio_info i ON i.socio = s.socio WHERE s.socio = @sc";
                    var data = await connection.QueryFirstAsync<SociosR>(script, new { sc = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }

        }

        public async Task<SocioInfo> getInfoById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socio_info WHERE socio = @pr";
                    var data = await connection.QueryFirstAsync<SocioInfo>(script, new { pr = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<IEnumerable<KycSociosList>> getSociosValidacion()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT a.socio,b.estatus,a.nombre,a.apellido_pat,a.apellido_mat,a.email,b.fecha_kyc  FROM public.socio_validacion b INNER JOIN public.socios a on a.socio = b.socio";
                    var data = await connection.QueryAsync<KycSociosList>(script);
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found {e.Message}");
                return null;
            }

        }

        public async Task<IEnumerable<SocioValidacion>> getValidationesById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socio_validacion WHERE socio = @socio ORDER BY validacion DESC";
                    var data = await connection.QueryAsync<SocioValidacion>(script, new { prospecto = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<SocioMeta> getSocioByEmailMeta(string email)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT b.socio,b.estatus,b.estatus_kyc,b.resultado_kyc FROM public.socios a INNER JOIN public.socio_validacion b ON a.socio = b.socio WHERE a.email = @pr";
                    var data = await connection.QueryFirstAsync<SocioMeta>(script, new { pr = email });
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

        public async Task<SocioData> getSocioByEmailAuth(string email)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socios a INNER JOIN public.socio_info b ON a.socio = b.socio WHERE email = @sc";
                    var data = await connection.QueryFirstAsync<SocioData>(script, new { sc = email });
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
                    var script = "SELECT * FROM public.socio_validacion WHERE socio = @socio ORDER BY validacion DESC LIMIT 1";
                    var data = await connection.QueryFirstAsync<SocioValidacionR>(script, new { socio = id });
                    return data;
                }
            }
            catch (Exception)
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
            catch (Exception)
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
                            fecha_validacion = socioValida.Fecha_Validacion,
                            estatus_kyc = socioValida.Estatus_Kyc,
                            fecha_empresa = socioValida.Fecha_Empresa,
                            fecha_kyc = socioValida.Fecha_Kyc,
                            resultado_kyc = socioValida.Resultado_Kyc,
                            observaciones = socioValida.Observaciones,
                            validado_por = socioValida.Validado_Por,
                            autorizado_por = socioValida.Autorizado_Por,
                            payload = socioValida.Payload,
                            id_validation = socioValida.Id_Validation,
                            id_related = socioValida.Id_Related,
                            fecha_insert = socioValida.Fecha_Insert,
                            fecha_update = socioValida.Fecha_Update
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
                    var data = await connection.ExecuteAsync(script, new
                    {
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
