using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Entities.Prospectos;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Mediator.Commands
{
    public class ProspectosCommand : IProspectosCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProspectosCommand> _logger;
        public ProspectosCommand(IConfiguration configuration, ILogger<ProspectosCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<Prospectos> getProspectoById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.prospectos WHERE prospecto = @pr";
                    var data = await connection.QueryFirstAsync<Prospectos>(script, new { pr = id });
                    return data;

                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception : {e.Message}");
                return null;
            }


            

        }

        public async Task<Prospectos> getProspectoByEmail(string email)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.prospectos WHERE email = @pr";
                    var data = await connection.QueryFirstAsync<Prospectos>(script, new { pr = email });
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }
            

        }

        public async Task<Validacion> getVerified(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.prospecto_validacion WHERE prospecto = @pr ORDER BY fecha_insert LIMIT 1";
                    var data = await connection.QueryFirstAsync<Validacion>(script, new { pr = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<ProspectoInfo> getInfoById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.prospecto_info WHERE prospecto = @pr";
                    var data = await connection.QueryFirstAsync<ProspectoInfo>(script, new { pr = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<ProspectoValidacionR> getValidationById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.prospecto_validacion WHERE prospecto = @prospecto ORDER BY fecha_update DESC LIMIT 1";
                    var data = await connection.QueryFirstAsync<ProspectoValidacionR>(script, new { prospecto = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<ProspectoValidacionR> getProspectoValidacionbyValidacion(int validacionId)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.prospecto_validacion WHERE validacionId = @validacion ";
                    var data = await connection.QueryFirstAsync<ProspectoValidacionR>(script, new { validacion = validacionId });
                    return data;
                }

            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<int> insertInteresado(Prospectos prospecto)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.prospectos (email,padre,tipo,email_verified) " +
                        "VALUES (@email,@padre,@tipo,@email_verified)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            email = prospecto.Email,
                            padre = prospecto.Padre,
                            tipo = prospecto.Tipo,
                            email_verified = prospecto.Email_Verified
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public async Task<int> insertValidacion(ProspectoValidacion validacion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.prospecto_validacion (prospecto,estatus,estatus_kyc,fecha_kyc,resultado_kyc,validado_por,fecha_insert,fecha_update,payload) " +
                        "VALUES (@prospecto,@estatus,@estatus_kyc,@fecha_kyc,@resultado_kyc,@validado_por,@fecha_insert,@fecha_update,@payload)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = validacion.Prospecto,
                            estatus = validacion.Estatus,
                            estatus_kyc = validacion.Estatus_Kyc,
                            fecha_kyc = validacion.Fecha_Kyc,
                            resultado_kyc = validacion.Resultado_Kyc,
                            validado_por = validacion.Validado_Por,
                            fecha_insert = validacion.Fecha_Insert,
                            fecha_update = validacion.Fecha_Update,
                            payload = validacion.Payload
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public async Task<int> insertDocument(ProspectoDocumentacion documento)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.prospecto_documentacion (validacion,prospecto,tipo,filename,fecha_insert,fecha_update) " +
                        "VALUES (@validacion,@prospecto,@tipo,@filename,@fecha_insert,@fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            validacion = documento.Validacion,
                            prospecto = documento.Prospecto,
                            tipo = documento.Tipo,
                            filename = documento.Filename,
                            fecha_insert = documento.Fecha_Insert,
                            fecha_update = documento.Fecha_Update
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public async Task<int> updateProspectoInfoVerify(ProspectoInfo info)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.prospecto_info SET direccion = @direccion,fecha_update = @fecha_update,fecha_insert = @fecha_insert,genero = @genero,fecha_nacimiento = @fecha_nacimiento,foto =  @foto, cve_identidad = @cve_identidad,int_emprendimiento = @int_emprendimiento,int_networking = @int_networking,int_ahorro = @int_ahorro,int_formacion = @int_formacion,medio_contacto = @medio_contacto,pais = @pais,tel_celular = @tel_celular WHERE prospecto = @prospecto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = info.Prospecto,
                            pais = info.Pais,
                            tel_celular = info.Tel_Celular,
                            medio_contacto = info.Medio_Contacto,
                            int_formacion = info.Int_Formacion,
                            int_ahorro = info.Int_Ahorro,
                            int_networking = info.Int_Networking,
                            int_emprendimiento = info.Int_Emprendimiento,
                            foto = info.Foto,
                            cve_identidad = info.Cve_Identidad,
                            fecha_nacimiento = info.Fecha_Nacimiento,
                            genero = info.Genero,
                            fecha_insert = info.Fecha_Insert,
                            fecha_update = info.Fecha_Update,
                            direccion = info.Direccion
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

        public async Task<int> updateProspectoValidation(ProspectoValidacion validation)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.prospecto_validacion SET  estatus = @estatus,fecha_validacion = @fecha_validacion, estatus_kyc = @estatus_kyc, fecha_kyc = @fecha_kyc,resultado_kyc = @resultado_kyc,validado_por = @validado_por, fecha_insert = @fecha_insert,fecha_update = @fecha_update,payload = @payload WHERE prospecto = @prospecto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = validation.Prospecto,
                            estatus = validation.Estatus,
                            fecha_validacion = DateTime.Now,
                            estatus_kyc = validation.Estatus_Kyc,
                            fecha_kyc = validation.Fecha_Kyc,
                            resultado_kyc = validation.Resultado_Kyc,
                            validado_por = validation.Validado_Por,
                            fecha_insert = validation.Fecha_Insert,
                            fecha_update = DateTime.Now,
                            payload = validation.Payload
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

        public async Task<int> updateProspectoValidationInfo(int validation,int prospecto)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.prospectos SET  validacion = @validacion WHERE prospecto = @prospecto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = prospecto,
                            validacion = validation,
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

        public async Task<int> updateProspectoValidationValue(int prospecto,int validacion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.prospectos SET  validacion = @validacion WHERE prospecto = @prospecto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = prospecto,
                            validacion = validacion
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

        public async Task<int> deteleProspectoDocumentos(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "DELETE FROM public.prospecto_documentacion WHERE prospecto = @prospecto";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = id
                        });
                    return data;

                }
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        public async Task<Prospectos_documentacion> getDocumentById(int id)
        {
            try 
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"))) 
                {
                    var script = "SELECT e.nombre,d. * FROM public.enum e INNER JOIN public.prospecto_documentacion d ON d.tipo = e.codigo INNER JOIN public.prospectos f ON f.prospecto = d.prospecto WHERE (f.prospecto = @pr) and(e.categoria = 'TipoDocumento')";
                    var data = await connection.QueryFirstAsync<Prospectos_documentacion>(script, new { pr = id });
                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public async Task<int> insertProspectoValidacionComplete(ProspectoValidacion prospectoValida)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "INSERT INTO public.prospecto_validacion (prospecto, estatus, fecha_validacion, estatus_kyc, fecha_empresa, resultado_kyc, observaciones, validado_por, autorizado_por, payload, id_validation, id_related, fecha_insert, fecha_update)" +
                        "VALUES (@socio, @estatus, @fecha_validacion, @estatus_kyc, @fecha_empresa, @resultado_kyc, @observaciones, @validado_por, @autorizado_por, @payload, @id_validation, @id_related, @fecha_insert, @fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            prospecto = prospectoValida.Prospecto,
                            estatus = prospectoValida.Estatus,
                            fecha_validacion = prospectoValida.Fecha_Validacion,
                            estatus_kyc = prospectoValida.Estatus_Kyc,
                            fecha_empresa = prospectoValida.Fecha_Empresa,
                            fecha_kyc = prospectoValida.Fecha_Kyc,
                            resultado_kyc = prospectoValida.Resultado_Kyc,
                            observaciones = prospectoValida.Observaciones,
                            validado_por = prospectoValida.Validado_Por,
                            autorizado_por = prospectoValida.Autorizado_Por,
                            payload = prospectoValida.Payload,
                            id_validation = prospectoValida.Id_Validation,
                            id_related = prospectoValida.Id_Related,
                            fecha_insert = prospectoValida.Fecha_Insert,
                            fecha_update = prospectoValida.Fecha_Update
                        });
                    return data;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }


    }
}


