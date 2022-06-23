using SmartBusinessAPI.Entities;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Services.Interfaces
{
    public interface IAuthAPI
    {
        Task<AuthR> extractToken();
        Task<bool> resendEmail(string token, int id);
    }
}
