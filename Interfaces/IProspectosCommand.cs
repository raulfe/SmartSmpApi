using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Models;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IProspectosCommand
    {
        Task<Prospectos> getProspectoById(int id);
        Task<Prospectos> getProspectoByEmail(string email);
        Task<int> insertInteresado(Prospectos prospecto);
        Task<int> updateProspectoInfoVerify(ProspectoInfo info);
        Task<int> insertDocument(ProspectoDocumentacion documento);
        Task<int> insertValidacion(ProspectoValidacion validacion);
        Task<int> updateProspectoValidation(ProspectoValidacion validation);
        Task<int> updateProspectoValidationValue(int prospecto, int validacion);
        Task<ProspectoValidacionR> getValidationById(int id);
        Task<int> deteleProspectoDocumentos(int id);
        Task<ProspectoInfo> getInfoById(int id);
        Task<int> updateProspectoValidationInfo(int validation, int prospecto);
        Task<Validacion> getVerified(int id);
    }
}
