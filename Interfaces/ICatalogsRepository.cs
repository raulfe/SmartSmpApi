using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface ICatalogsRepository
    {
        Task<IEnumerable<Paises>> getPaises();
        Task<IEnumerable<Paises>> getPaisesByName(string name);
        Task<IEnumerable<Models.Enum>> getMedioContacto();
        Task<IEnumerable<Models.Enum>> getGeneral(string categoria);
        Task<object> getPaisesGroup();
    }
}
