using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.KycValidacion;
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
            catch (Exception e)
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

            if (prospectoValidacion.Autorizado == null)
            {
                _logger.LogError("El socio no cuenta con persona que autorice el proceso");
                throw new BusinessException("El socio no cuenta con persona que autorice el proceso");
            }

            var resultKyc = "";
            switch (prospectoValidacion.Estatus)
            {
                case 1:
                    resultKyc = "verified";
                    break;
                case 3:
                    resultKyc = "rejected";
                    break;
                case 2:
                    resultKyc = "reviewNeeded";
                    break;
            }

            var validation = new ProspectoValidacion()
            {
                Prospecto = validacion.Prospecto,
                Estatus = prospectoValidacion.Estatus,
                Fecha_Validacion = validacion.Fecha_Validacion,
                Estatus_Kyc = prospectoValidacion.Estatus,
                Fecha_Kyc = DateTime.Now,
                Fecha_Empresa = validacion.Fecha_Empresa,
                Resultado_Kyc = resultKyc,
                Observaciones = prospectoValidacion.Observaciones,
                Validado_Por = validacion.Validado_Por,
                Autorizado_Por = prospectoValidacion.Autorizado,
                Payload = new JsonParameter(JsonConvert.SerializeObject(validacion.Payload)),
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
