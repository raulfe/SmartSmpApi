using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.NuevaMembresia;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IMembresiasRepository
    {
        Task<IEnumerable<Membresias>> GetMembresias(MembresiasFilter filter);
        Task<bool> processNewMembership(NewMembership membresia);
        Task<NewMembership> GetMembresiaById(int id);
        Task<bool> updateMembresia(updateMembership updateMembership);
        Task<bool> updateMembresiaStatus(bool status, int membership);
    }
}
