using SmartBusinessAPI.Entities.NuevaPromocion;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IPromocionesRepository
    {
        Task<IEnumerable<Promociones>> getPromociones();
        Task<bool> processNewPromoProduct(NewPromocion promo);
        Task<bool> updateStatus(bool status, int promocion);
        Task<bool> processNewPromoMembresia(NewPromocionMembresia promo);
        Task<bool> updatePromoProduct(NewPromocion promo);
        Task<bool> updatePromoMembresias(NewPromocionMembresia promo);
        Task<object> getPromocionByIDCustom(int id);
    }
}
