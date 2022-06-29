using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.NuevosProducto;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IProductosRepository
    {
        Task<IEnumerable<ProductosR>> getPlan(int? id);
        Task<IEnumerable<ProductosR>> getPlans();
        Task<bool> processNewPlan(NewProduct prod);
        Task<object> getPlanById(int id);
        Task<bool> updatePlan(UpdateProduct prod);
        Task<int> updateStatusProducto(StatusProducto prod);
    }
}
