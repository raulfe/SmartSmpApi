using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IPromocionesCommand
    {
        Task<IEnumerable<Promociones>> getPromociones();
        Task<Promociones> getPromocionByName(string name);
        Task<Promociones> getPromocionById(int id);
        Task<int> insertNewPromo(Promociones promo);
        Task<int> insertPromoPaises(PromocionPaises paises, int promocion);
        Task<int> insertPromoProductos(PromocionProductos prods, int promocion);
        Task<int> insertPromoMembresias(PromocionMembresias prods, int promocion);
        Task<int> updatePromocion(bool status, int promocion);
        Task<int> updatePromocionIn(Promociones promocion);
        Task<int> deletePromoPaises(int promocion);
        Task<int> deletePromoPlanes(int promocion);
        Task<int> deletePromoMembresias(int promocion);
        Task<IEnumerable<PromocionPaises>> getPromocionPaisById(int id);
        Task<IEnumerable<PromocionMembresias>> getPromocionMembershipById(int id);
        Task<IEnumerable<PromocionProductos>> getPromocionProdById(int id);
    }
}
