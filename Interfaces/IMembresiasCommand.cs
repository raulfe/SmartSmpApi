using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IMembresiasCommand
    {
        Task<IEnumerable<Membresias>> getMembresias();
        Task<int> insertNewMembership(Membresias membership);
        Task<int> insertMembresiaPais(MembresiaPais pais, int membresia);
        Task<Membresias> getMembresia(string nombre);
        Task<Membresias> getMembresiaById(int membresia);
    }
}
