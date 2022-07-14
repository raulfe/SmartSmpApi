using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.KycValidacion;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IProspectosRepository
    {
        Task<Prospectos_documentacion> getDocumentById(int id);
        Task<bool> updateProspectoValidacion(Validacionupdate prospectoValidacion);
    }
}
