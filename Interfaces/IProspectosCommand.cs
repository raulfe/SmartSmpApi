using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Entities.KycValidacion;
using SmartBusinessAPI.Entities.ValidationStatus;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IProspectosCommand
    {
        Task<IEnumerable<KycProspectosList>> getProspectoValidacion();
        Task<Prospectos> getProspectoById(int id);
        Task<Prospectos> getProspectoByEmail(string email);
        Task<ProspectoData> getProspectoByEmailAuth(string email);
        Task<ProspectoMeta> getProspectoByEmailMeta(string email);
        Task<int> insertInteresado(Prospectos prospecto);
        Task<int> updateProspectoInfoVerify(ProspectoInfo info);
        Task<int> insertDocument(ProspectoDocumentacion documento);
        Task<int> insertValidacion(ProspectoValidacion validacion);
        Task<int> updateProspectoValidation(ProspectoValidacion validation);
        Task<int> updateProspectoValidationValue(int prospecto, int validacion);
        Task<ProspectoValidacionR> getValidationById(int id);
        Task<IEnumerable<ProspectoValidacion>> getValidationesById(int id);
        Task<int> deteleProspectoDocumentos(int id);
        Task<ProspectoInfo> getInfoById(int id);
        Task<int> updateProspectoValidationInfo(int validation, int prospecto);
        Task<Validacion> getVerified(int id);
        Task<Prospectos_documentacion> getDocumentById(int id);
        Task<int> insertProspectoValidacionComplete(ProspectoValidacion prospectoValida);
        Task<ProspectoValidacionR> getProspectoValidacionbyValidacion(int validacionId);
    }
}
