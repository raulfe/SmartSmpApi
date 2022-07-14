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
    public class KycRepository : IKycRepository
    {
        private readonly ILogger<ProductosRepository> _logger;
        private readonly IProspectosCommand _prospectos;
        private readonly ISociosCommand _socios;
        public KycRepository(ILogger<ProductosRepository> logger, IProspectosCommand prospectos, ISociosCommand socios)
        {
            _logger = logger;
            _prospectos = prospectos;
            _socios = socios;
        }

        public async Task<IEnumerable<KycProspectosList>> getListProspectoValidacion()
        {
            var data = await _prospectos.getProspectoValidacion();
            if(data.Count() == 0 || data == null)
            {
                _logger.LogError("No hay validaciones de prospectos en la db");
                throw new BusinessException("No hay validaciones de prospectos en la db");
            }
            return data;
        }

        public async Task<IEnumerable<KycSociosList>> getListSociosValidacion()
        {
            var data = await _socios.getSociosValidacion();
            if (data.Count() == 0 || data == null)
            {
                _logger.LogError("No hay validaciones de prospectos en la db");
                throw new BusinessException("No hay validaciones de prospectos en la db");
            }
            return data;
        }

        public async Task<object> getProspectoValidacionInfo(int id)
        {
            var historico = await _prospectos.getValidationesById(id);
            if(historico.Count() == 0 || historico == null)
            {
                _logger.LogError("El prospecto no tiene historico de validaciones");
            }
            var prospecto = await _prospectos.getProspectoById(id);
            if(prospecto == null)
            {
                _logger.LogError("El prospecto no existe");
                throw new BusinessException("El prospecto no existe");
            }
            var info = await _prospectos.getInfoById(id);
            var response = new
            {
                Prospecto = prospecto,
                Informacion = info,
                Historico = historico
            };
            return response;
        }

        public async Task<object> getSocioValidacionInfo(int id)
        {
            var historico = await _socios.getValidationesById(id);
            if (historico.Count() == 0 || historico == null)
            {
                _logger.LogError("El socio no tiene historico de validaciones");
            }
            var socio = await _socios.getSocioById(id);
            if (socio == null)
            {
                _logger.LogError("El prospecto no existe");
                throw new BusinessException("El prospecto no existe");
            }
            var info = await _socios.getInfoById(id);
            var response = new
            {
                Socio = socio,
                Informacion = info,
                Historico = historico
            };
            return response;
        }
    }
}
