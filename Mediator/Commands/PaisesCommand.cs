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
    public class PaisesCommand : IPaisesCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaisesCommand> _logger;
        public PaisesCommand(IConfiguration configuration, ILogger<PaisesCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<IEnumerable<Paises>> getPaises()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.paises";
                    var data = await connection.QueryAsync<Paises>(script);
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public async Task<IEnumerable<Paises>> getPaisesByName(string name)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.paises WHERE nombre ILIKE @pais||'%'";
                    var data = await connection.QueryAsync<Paises>(script, new { pais = name });
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
