using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IPaisesCommand
    {
        Task<IEnumerable<Paises>> getPaises();
        Task<IEnumerable<Paises>> getPaisesByName(string name);
    }
}
