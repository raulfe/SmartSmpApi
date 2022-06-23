using SmartBusinessAPI.Entities;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface IAuth0Repository
    {
        Task<bool> ReSendValidation(int id);
    }
}
