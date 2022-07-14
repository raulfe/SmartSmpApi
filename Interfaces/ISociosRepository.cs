using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.KycValidacion;
using SmartBusinessAPI.Models;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface ISociosRepository
    {
        Task<Socios> getSocioByID(int id);
        Task<SociosR> getSocioByIDinfo(int id);
        Task<CountType> getSociosCount();
        Task<SociosR> getSocioByPosition(int id);
        Task<Socios_documentacion> getDocumentById(int id);
        Task<bool> updateSocioValidacion(Validacionupdate socioValidacion);
    }
}
