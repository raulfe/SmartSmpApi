using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IProductosCommand
    {
        Task<IEnumerable<ProductosR>> getPlan();
        Task<ProductosR> getPlanByCode(string code);
        Task<int> insertNewPlan(ProductosDTO prod);
        Task<int> insertProductoPais(ProductoPais pais, int producto);
        Task<int> insertSmartPack(PaquetesDTO smartpack, int plan);
        Task<SmartPack> getSmartPack(string nombre);
        Task<int> insertNewAdicional(Adicionales adicional, int smart);
        Task<int> updatePlanAddSmartpack(int plan, int smart);
        Task<Productos> getPlanById(int id);
        Task<IEnumerable<ProductoPais>> getPaisByPlan(int id);
        Task<Paquetes> getSmartPackById(int id);
        Task<IEnumerable<AdicionalesR>> getAdicionales(int id);
    }
}