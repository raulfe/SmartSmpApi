using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using SmartBusinessAPI.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Repositories
{
    public class MetamapRepository : IMetamapRepository
    {
        private readonly IMetamapAPI _api;
        private readonly ILogger<MetamapRepository> _logger;
        private readonly IProspectosCommand _prospecto;

        public MetamapRepository(IMetamapAPI api, ILogger<MetamapRepository> logger, IProspectosCommand prospecto)
        {
            _api = api;
            _logger = logger;
            _prospecto = prospecto;
        }

        public async Task<bool> syncVerification(string url)
        {
            _logger.LogInformation("Extrayendo token de metamap");
            _logger.LogInformation($"Resource {url}");
            var token = await _api.extractToken();
            var verificationObject = await _api.getMetamapDataByUrl(url, token.access_token);
            var prosp = await _prospecto.getProspectoByEmail(verificationObject.metadata.email);
            if (prosp == null)
            {
                throw new BusinessException("El prospecto no existe");
            }
            var info = await _prospecto.getInfoById(prosp.Prospecto);
            if (info == null)
            {
                throw new BusinessException("El prospecto no se ha registrado");
            }
            var sex = 2;
            if (verificationObject.documents[0].fields.sex.value == "M")
            {
                sex = 1;
            }
            var newInfo = new ProspectoInfo()
            {
                Prospecto = prosp.Prospecto,
                Pais = info.Pais,
                Tel_Celular = info.Tel_Celular,
                Medio_Contacto = info.Medio_Contacto,
                Int_Formacion = info.Int_Formacion,
                Int_Ahorro = info.Int_Ahorro,
                Int_Emprendimiento = info.Int_Emprendimiento,
                Int_Networking = info.Int_Networking,
                Foto = verificationObject.steps[0].data.sources[0].url,
                Cve_Identidad = verificationObject.documents[0].fields.curp.value,
                Fecha_Insert = info.Fecha_Insert,
                Fecha_Nacimiento = Convert.ToDateTime(verificationObject.documents[0].fields.dateOfBirth.value),
                Fecha_Update = DateTime.Now,
                Genero = sex,
                Direccion = verificationObject.documents[0].fields.address.value
            };
            var infoResponse = await _prospecto.updateProspectoInfoVerify(newInfo);
            if (infoResponse == 0)
            {
                _logger.LogError("Error al intentar actualizar la informacion del prospecto");
                throw new BusinessException("Error al intentar actualizar la informacion del prospecto");
            }
            var val = await _prospecto.getValidationById(prosp.Prospecto);
            if (val == null)
            {
                var estatus = 0;
                switch (verificationObject.identity.status)
                {
                    case "verified":
                        estatus = 1;
                        break;
                    case "rejected":
                        estatus = 3;
                        break;
                    case "reviewNeeded":
                        estatus = 2;
                        break;
                }
                var duplicated = "0";
                if(verificationObject.documents[0].steps[6].data.duplicateSignup == true)
                {
                    duplicated = verificationObject.documents[0].steps[6].data.relatedRecords[0];
                }
                var validation = new ProspectoValidacion()
                {
                    Prospecto = prosp.Prospecto,
                    Estatus = estatus,
                    Estatus_Kyc = estatus,
                    Fecha_Kyc = DateTime.Now,
                    Resultado_Kyc = verificationObject.identity.status,
                    Validado_Por = "MetamapService",
                    Fecha_Insert = DateTime.Now,
                    Fecha_Update = DateTime.Now,
                    Payload = new Entities.JsonParameter(JsonConvert.SerializeObject(verificationObject)),
                    Id_Validation = duplicated
                };
                var validationResponse = await _prospecto.insertValidacion(validation);
                if (validationResponse == 0)
                {
                    throw new BusinessException("El prospecto ya ah sido validado por el servicio de Metamap");
                }
                var valid = await _prospecto.getValidationById(prosp.Prospecto);
                var tipo = 1;
                foreach (var i in verificationObject.documents[0].photos)
                {
                    
                    var document = new ProspectoDocumentacion()
                    {
                        Prospecto = prosp.Prospecto,
                        Fecha_Insert = DateTime.Now,
                        Fecha_Update = DateTime.Now,
                        Validacion = valid.Validacion,
                        Filename = i,
                        Tipo = tipo
                    };
                    var documentResponse = await _prospecto.insertDocument(document);
                    if (documentResponse == 0)
                    {
                        throw new BusinessException("Error al intentar ingresar el documento");
                    }
                    tipo++;
                }
                var documentA = new ProspectoDocumentacion()
                {
                    Prospecto = prosp.Prospecto,
                    Fecha_Insert = DateTime.Now,
                    Fecha_Update = DateTime.Now,
                    Validacion = valid.Validacion,
                    Filename = verificationObject.documents[1].photos[0],
                    Tipo = 5
                };
                var documentResponseA = await _prospecto.insertDocument(documentA);
                if (documentResponseA == 0)
                {
                    throw new BusinessException("Error al intentar ingresar el documento");
                }
                var resLast = await _prospecto.updateProspectoValidationInfo(valid.Validacion, prosp.Prospecto);
                if(resLast == 0)
                {
                    throw new BusinessException("Error al intentar actualizar el prospecto");
                }
                return true;
            }
            else
            {
                var estatus = 0;
                switch (verificationObject.identity.status)
                {
                    case "verified":
                        estatus = 1;
                        break;
                    case "rejected":
                        estatus = 3;
                        break;
                    case "reviewNeeded":
                        estatus = 2;
                        break;
                }
                var validation = new ProspectoValidacion()
                {
                    Prospecto = prosp.Prospecto,
                    Estatus = estatus,
                    Estatus_Kyc = estatus,
                    Fecha_Kyc = val.Fecha_Kyc,
                    Resultado_Kyc = verificationObject.identity.status,
                    Validado_Por = "SmartBusiness",
                    Fecha_Insert = val.Fecha_Insert,
                    Fecha_Update = DateTime.Now,
                    Payload = new Entities.JsonParameter(JsonConvert.SerializeObject(verificationObject))
                };
                var validationResponse = await _prospecto.insertValidacion(validation);
                if (validationResponse == 0)
                {
                    throw new BusinessException("El prospecto ya ah sido validado por el servicio de Metamap");
                }
                var valid = await _prospecto.getValidationById(prosp.Prospecto);
                var delete = await _prospecto.deteleProspectoDocumentos(prosp.Prospecto);
                if(delete > 0)
                {
                    var tipo = 1;
                    foreach (var i in verificationObject.documents[0].photos)
                    {

                        var document = new ProspectoDocumentacion()
                        {
                            Prospecto = prosp.Prospecto,
                            Fecha_Insert = DateTime.Now,
                            Fecha_Update = DateTime.Now,
                            Validacion = valid.Validacion,
                            Filename = i,
                            Tipo = tipo
                        };
                        var documentResponse = await _prospecto.insertDocument(document);
                        if (documentResponse == 0)
                        {
                            throw new BusinessException("Error al intentar ingresar el documento");
                        }
                        tipo++;
                    }
                    var documentA = new ProspectoDocumentacion()
                    {
                        Prospecto = prosp.Prospecto,
                        Fecha_Insert = DateTime.Now,
                        Fecha_Update = DateTime.Now,
                        Validacion = valid.Validacion,
                        Filename = verificationObject.documents[1].photos[0],
                        Tipo = 5
                    };
                    var documentResponseA = await _prospecto.insertDocument(documentA);
                    if (documentResponseA == 0)
                    {
                        throw new BusinessException("Error al intentar ingresar el documento");
                    }
                    var resLast = await _prospecto.updateProspectoValidationInfo(valid.Validacion, prosp.Prospecto);
                    if (resLast == 0)
                    {
                        throw new BusinessException("Error al intentar actualizar el prospecto");
                    }
                }
                return true;
            }
            
        }
    }
}
