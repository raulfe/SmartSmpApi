using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IEnumsCommand
    {
        Task<IEnumerable<Models.Enum>> getMedioContacto();
        Task<IEnumerable<Models.Enum>> getGeneral(string categoria);
    }
}
