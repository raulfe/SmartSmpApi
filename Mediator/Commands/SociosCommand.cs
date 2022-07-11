using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.DTOs;
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

        public async Task<IEnumerable<KycSociosList>> getSociosValidacion()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT a.socio,b.estatus,a.nombre,a.apellido_pat,a.apellido_mat,a.email,b.fecha_update FROM public.socio_validacion b INNER JOIN public.socios a on a.socio = b.socio";
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
        public async Task<Validacion> getVerified(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socio_validacion WHERE socio = @sc ORDER BY fecha_insert LIMIT 1";
                    var data = await connection.QueryFirstAsync<Validacion>(script, new { sc = id });
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
                    var script = "SELECT * FROM public.socio_info WHERE socio = @sc";
                    var data = await connection.QueryFirstAsync<SocioInfo>(script, new { sc = id });
                    return data;

                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public async Task<SocioValidacion> getValidationById(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.socio_validacion WHERE socio = @socio ORDER BY validacion DESC LIMIT 1";
                    var data = await connection.QueryFirstAsync<SocioValidacion>(script, new { socio = id });
                    return data;

                }
            }
            catch (Exception e)
            {

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
                    var data = await connection.QueryAsync<SocioValidacion>(script, new { socio = id });
                    return data;

                }
            }
            catch (Exception e)
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
    }
}
