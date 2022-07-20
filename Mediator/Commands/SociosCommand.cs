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
                        socio = socio,
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


        public async Task<IEnumerable<ResultSearchSocios>> getSocioSearch(SocioSearch socioSearch) 
        {
            try 
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT s.socio,s.nombre,s.email,i.tel_celular,r.nombre as Nombre_Rango FROM public.socios s left JOIN public.socio_info i ON i.socio = s.socio left JOIN public.rangos r ON r.rango = s.rango WHERE s.nombre like '%"+socioSearch.nombre+ "%' AND s.socio = '" + socioSearch.id + "'  "; 
                    var data = await connection.QueryAsync<ResultSearchSocios>(script, new {
                    });
                     var dee = data.OrderByDescending(x => x.socio).Skip(socioSearch.pagina * -1).Take(100).ToList(); 
                    return dee;
                }
            }
            catch (Exception e) 
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<ResultSearchSocioProduct>> getSocioProductSearch(SocioProductSearch socioProduct)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT s.socio, s.nombre, s.email, COUNT(i.plan) AS total_planes FROM public.socios s left JOIN public.socio_productos i ON i.socio = s.socio WHERE  cast(s.socio as varchar(100)) like '%" + socioProduct.id + "%' GROUP BY(s.socio)";
                    var data = await connection.QueryAsync<ResultSearchSocioProduct>(script, new
                    {
                    });
                    var dee = data.OrderByDescending(x => x.socio).Skip(socioProduct.pagina * -1).Take(100).ToList();
                    return dee;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Exception found: {e.Message}");
                return null;
            }
          
        }



        public async Task<IEnumerable<SocioHistory>> getSocioHistory(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT s.socio,i.fecha_inicio,i.estatus,i.capital_inicial,i.capital FROM public.socios s left JOIN public.socio_productos i ON i.socio = s.socio WHERE  s.socio = @socio GROUP BY (s.socio, i.capital_inicial, i.capital, i.fecha_inicio, i.estatus)";
                    var data = await connection.QueryAsync<SocioHistory>(script, new { socio = id   });
                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<SocioDetail> getSocioDetail(int id)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT s.socio,s.nombre,s.rango,r.nombre as Nombre_Rango, SUM(i.capital_inicial) AS suma_capital FROM public.socios s left JOIN public.socio_productos i ON i.socio = s.socio left JOIN public.rangos r ON r.rango = s.rango WHERE  s.socio = @socio GROUP BY (s.socio, r.nombre) ";
                    var data = await connection.QueryFirstAsync<SocioDetail>(script, new { socio = id });
                    return data;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<int> updateStatusSocio(int id, int status)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "UPDATE public.socios SET estatus = @estatus WHERE socio = @socio";
                    var data = await connection.ExecuteAsync(script, new
                    {
                        socio = id,
                        estatus = status,
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
