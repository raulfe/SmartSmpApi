using SmartBusinessAPI.Entities.ValidationStatus;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface ILoginRepository
    {
        Task<int> emailValidation(string email, int padre);
        JwtSecurityToken decodeToken(string token);
        Task<object> getValidation(string email);
        Task<object> getMetaValidation(string email);
    }
}
