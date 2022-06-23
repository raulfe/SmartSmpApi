using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class SociosRepository : ISociosRepository
    {
        private readonly ISociosCommand _socios;
        private readonly ILogger<SociosRepository> _logger;

        public SociosRepository(ISociosCommand socios, ILogger<SociosRepository> logger)
        {
            _logger = logger;
            _socios = socios;
        }

        public async Task<Socios> getSocioByID(int id)
        {
            try
            {
                var data = await _socios.getSocioById(id);
                return data;
            }
            catch (Exception e)
            {

                throw new BusinessException($"Exception found: {e.Message}"); 
            }
            
        }

        public async Task<SociosR> getSocioByPosition(int id)
        {
            try
            {
                var data = await _socios.getSocioByPosition(id);
                return data;
            }
            catch (Exception e)
            {

                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<CountType> getSociosCount()
        {
            try
            {
                var data = await _socios.getSociosCount();
                return data;
            }
            catch (Exception e)
            {

                throw new BusinessException($"Exception found: {e.Message}");
            }
        }
    }
}
