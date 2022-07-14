using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.KycValidacion;
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

        public async Task<SociosR> getSocioByIDinfo(int id)
        {
            try
            {
                var data = await _socios.getSocioByIdInfo(id);
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

        public async Task<Socios_documentacion> getDocumentById(int id)
        {
            try
            {
                var data = await _socios.getDocumentById(id);
                return data;
            }
            catch (Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }
        }

        public async Task<bool> updateSocioValidacion(Validacionupdate socioValidacion)
        {
            var socio = await _socios.getSocioById(socioValidacion.Id);
            if (socio == null)
            {
                _logger.LogError("El socio buscado no existe");
                throw new BusinessException("El socio buscado no existe");
            }

            var validacion = await _socios.getSocioValidacionbyValidacion(socio.Validacion);
            if (validacion == null)
            {
                _logger.LogError("El socio no cuenta con Id de validacion existente");
                throw new BusinessException("El socio no cuenta con Id de validacion existente");
            }

            var validation = new SocioValidacion()
            {
                Socio = validacion.Socio,
                Estatus = socioValidacion.Estatus,
                Fecha_Validacion = validacion.Fecha_Validacion,
                Estatus_Kyc = validacion.Estatus_Kyc,
                Fecha_Kyc = DateTime.Now,
                Fecha_Empresa = validacion.Fecha_Empresa,
                Resultado_Kyc = validacion.Resultado_Kyc,
                Observaciones = socioValidacion.Observaciones,
                Validado_Por = validacion.Validado_Por,
                Autorizado_Por = validacion.Autorizado_Por,
                Payload = new JsonParameter(JsonConvert.SerializeObject(validacion.Payload)),
                Id_Validation = validacion.Id_Validation,
                Id_Related = validacion.Id_Related,
                Fecha_Insert = DateTime.Now,
                Fecha_Update = DateTime.Now,
            };
            var validationResponse = await _socios.insertSocioValidacion(validation);
            if (validationResponse == 0)
            {
                throw new BusinessException("Lo sentimos hubo un problema al registrar esta nueva validacion");
            }
            var lastValidacion = await _socios.getLastValidacionById(validacion.Socio);
            if (lastValidacion == null)
            {
                _logger.LogError("No pudimos encontrar la ultima validacion del socio");
                throw new BusinessException("No pudimos encontrar la ultima validacion del socio");

            }
            var resUpdate = await _socios.updateSocioIdValidacion(lastValidacion.Validacion, lastValidacion.Socio);
            if (resUpdate == 0)
            {
                _logger.LogError("La Validacion del Socio no pudo ser actualizada");
                throw new BusinessException("La Validacion del Socio no pudo ser actualizada");
            }
            return true;
        }
    }
}
