using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.Prospectos;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class ProspectosRepository : IProspectosRepository
    {
        private readonly IProspectosCommand _prospectos;

        private readonly ILogger<ProspectosRepository> _logger;


        public ProspectosRepository(IProspectosCommand prospectos, ILogger<ProspectosRepository> logger) 
        {
            _logger = logger;
            _prospectos = prospectos;
        }

        public async Task<Prospectos_documentacion> getDocumentById(int id) 
        {
            try 
            {
                var data = await _prospectos.getDocumentById(id);
                return data;
            }
            catch ( Exception e)
            {
                throw new BusinessException($"Exception found: {e.Message}");
            }

        }



        public async Task<bool> updateProspectoValidacion(Validacionupdate prospectoValidacion)
        {
            var prosp = await _prospectos.getProspectoById(prospectoValidacion.Id);
            if (prosp == null)
            {
                _logger.LogError("El prospecto buscado no existe");
                throw new BusinessException("El prospecto buscado no existe");
            }

            var validacion = await _prospectos.getProspectoValidacionbyValidacion(prosp.Validacion);
            if (validacion == null)
            {
                _logger.LogError("El socio no cuenta con Id de validacion existente");
                throw new BusinessException("El socio no cuenta con Id de validacion existente");
            }

            //Comente esta parte porque no se si es necesario validar si esta Auroizado_por y de ser así, si me enviaran ese dato para el Update
            //if (validacion.AutorizadoPor == null) 
            //{
            //    _logger.LogError("El prospecto no cuenta con persona que autorice el proceso");
            //    throw new BusinessException("El prospecto no cuenta con persona que autorice el proceso");
            //}
            var validation = new ProspectoValidacion()
            {
                Prospecto = validacion.Prospecto,
                Estatus = prospectoValidacion.Estatus,
                Fecha_Validacion = validacion.Fecha_Validacion,
                Estatus_Kyc = validacion.Estatus_Kyc,
                Fecha_Kyc = DateTime.Now,
                Fecha_Empresa = validacion.Fecha_Empresa,
                Resultado_Kyc = validacion.Resultado_Kyc,
                Observaciones = prospectoValidacion.Observaciones,
                Validado_Por = validacion.Validado_Por,
                Autorizado_Por = validacion.Autorizado_Por,
                Payload = new JsonParameter(JsonConvert.SerializeObject(validacion)),
                Id_Validation = validacion.Id_Validation,
                Id_Related = validacion.Id_Related,
                Fecha_Insert = DateTime.Now,
                Fecha_Update = DateTime.Now,
            };
            var validationResponse = await _prospectos.insertProspectoValidacionComplete(validation);
            if (validationResponse == 0)
            {
                throw new BusinessException("Lo sentimos hubo un problema al registrar esta nueva validacion");
            }
            var lastValidacion = await _prospectos.getValidationById(validacion.Prospecto);
            if (lastValidacion == null)
            {
                _logger.LogError("No pudimos encontrar la ultima validacion del prospecto");
                throw new BusinessException("No pudimos encontrar la ultima validacion del prospecto");

            }
            var resUpdate = await _prospectos.updateProspectoValidationInfo(lastValidacion.Validacion, lastValidacion.Prospecto);
            if (resUpdate == 0)
            {
                _logger.LogError("La Validacion del Socio no pudo ser actualizada");
                throw new BusinessException("La Validacion del Socio no pudo ser actualizada");
            }
            return true;
        }






    }
}
