using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.SociosDocument;
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

        public async Task<int> updateSocioValidacion(SocioValidacion socioValida)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"))) 
                {
                    var script = "UPDATE  public.socio_validacion SET estatus = @estatus, estatus_kyc = @estatus_kyc, fecha_validacion = @fecha_validacion, autorizado_por = @autorizado_por, fecha_update = @fecha_update)";
                    var data = await connection.ExecuteAsync(script,
                        new
                        {
                            estatus = socioValida.Estatus,
                            estatus_kyc = socioValida.EstatusKyc,
                            fecha_validacion = socioValida.FechaValidacion,
                            autorizado_por = socioValida.AutorizadoPor,
                            fecha_update = socioValida.FechaUpdate
                        });
                    return data;
                }
            }
            catch (Exception e) 
            {
                _logger.LogError($"Exception found : {e.Message} ");
                return 0;
            }
        }


        public async Task<SocioValidacion> getLastValidation( int validacion)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "select * from public.socio_validacion where validacion =  @sc order by fecha_validacion DESC LIMIT 1";
                    var data = await connection.QueryFirstAsync<SocioValidacion>(script, new { sc = validacion });
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }

        }


        public async Task <DataSocioDocumentacion> getDataSocioDocument(int socio) 
        {
            try 
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default"))) 
                {
                    var script = "select * from  public.socios aINNER JOIN socio_documentacion b ON a.validacion = b.validacion AND b.socio = a.socio ";

                    var data = await connection.QueryFirstAsync<DataSocioDocumentacion> (script, new { sc = socio });
                    return data;
                }
            }
            catch (Exception) 
            {
                return null;
            }
        }




    }
}
