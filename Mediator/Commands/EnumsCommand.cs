using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using SmartBusinessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Mediator.Commands
{
    public class EnumsCommand : IEnumsCommand
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EnumsCommand> _logger;
        public EnumsCommand(IConfiguration configuration, ILogger<EnumsCommand> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Models.Enum>> getMedioContacto()
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.enum WHERE categoria = 'MedioContacto'";
                    var data = await connection.QueryAsync<Models.Enum>(script);
                    return data;

                }
            }
            catch (Exception)
            {

                return null;
            }


        }

        public async Task<IEnumerable<Models.Enum>> getGeneral(string categoria)
        {
            try
            {
                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("Default")))
                {
                    var script = "SELECT * FROM public.enum WHERE categoria = @categoria";
                    var data = await connection.QueryAsync<Models.Enum>(script,new { categoria = categoria});
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
