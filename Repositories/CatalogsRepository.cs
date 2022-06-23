using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class CatalogsRepository : ICatalogsRepository
    {
        private readonly ILogger<CatalogsRepository> _logger;
        private readonly IPaisesCommand _paises;
        private readonly IEnumsCommand _enums;

        public CatalogsRepository(ILogger<CatalogsRepository> logger, IPaisesCommand paises, IEnumsCommand enums)
        {
            _logger = logger;
            _paises = paises;
            _enums = enums;
        }
        public async Task<IEnumerable<Models.Enum>> getGeneral(string categoria)
        {
            try
            {
                var data = await _enums.getGeneral(categoria);
                if(data.Count() == 0 || data == null)
                {
                    throw new BusinessException("La categoria no existe");
                }
                return data;
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
        }

        public async Task<IEnumerable<Paises>> getPaises()
        {
            try
            {
                var data = await _paises.getPaises();
                return data;
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
        }

        public async Task<object> getPaisesGroup()
        {
            try
            {
                var data = await _paises.getPaises();
                var america = new List<Paises>();
                var africa = new List<Paises>();
                var asia = new List<Paises>();
                var europa = new List<Paises>();
                var oceania = new List<Paises>();
                foreach (var i in data)
                {
                    switch (i.Continente)
                    {
                        case 1:
                            africa.Add(i);
                            break;
                        case 2:
                            america.Add(i);
                            break;
                        case 3:
                            asia.Add(i);
                            break;
                        case 4:
                            europa.Add(i);
                            break;
                        case 5:
                            oceania.Add(i);
                            break;
                        default:
                            break;
                    }
                }
                var masterObj = new
                {
                    america,
                    africa,
                    asia,
                    europa,
                    oceania
                };
                return masterObj;
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
        }

        public async Task<IEnumerable<Paises>> getPaisesByName(string name)
        {
            try
            {
                var data = await _paises.getPaisesByName(name);
                return data;
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
        }

        public async Task<IEnumerable<Models.Enum>> getMedioContacto()
        {
            try
            {
                var data = await _enums.getMedioContacto();
                return data;
            }
            catch (Exception e)
            {

                _logger.LogError($"Exception found: {e.Message}");
                throw new BusinessException(e.Message);
            }
        }

    }
}
