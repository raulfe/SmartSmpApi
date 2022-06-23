using SmartBusinessAPI.Entities.DTOs;
using SmartBusinessAPI.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Interfaces
{
    public interface ILoginRepository
    {
        Task<int> emailValidation(string email, int padre);
        JwtSecurityToken decodeToken(string token);
    }
}
