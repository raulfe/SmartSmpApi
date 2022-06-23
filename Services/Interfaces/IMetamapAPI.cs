using SmartBusinessAPI.Entities.Verification;
using SmartBusinessAPI.Models.Metamap;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Services.Interfaces
{
    public interface IMetamapAPI
    {
        Task<Auth> extractToken();
        Task<VerificationObject> getMetamapDataById(string id, string token);
        Task<VerificationObject> getMetamapDataByUrl(string url, string token);
    }
}
