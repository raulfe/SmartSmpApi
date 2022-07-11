using SmartBusinessAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IKycRepository
    {
        Task<IEnumerable<KycProspectosList>> getListProspectoValidacion();
        Task<IEnumerable<KycSociosList>> getListSociosValidacion();
        Task<object> getProspectoValidacionInfo(int id);
        Task<object> getSocioValidacionInfo(int id);
    }
}
