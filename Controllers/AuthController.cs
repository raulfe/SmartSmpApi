using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartBusinessAPI.Exceptions;
using SmartBusinessAPI.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;
        private readonly ILoginRepository _repository;
        private readonly IAuth0Repository _auth;

        public AuthController(ILogger<AuthController> logger,ILoginRepository repository, IConfiguration configuration, IAuth0Repository auth)
        {
            _logger = logger;
            _repository = repository;
            _configuration = configuration;
            _auth = auth;
        }

        [Authorize]
        [HttpGet("Decode/{token}")]
        public IActionResult decodeToken(string token)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = _repository.decodeToken(token);
            return Ok(data);
        }

        [HttpGet("Internal/Services/{secret}")]
        public IActionResult authInternalServices(string secret)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            if(secret == _configuration["Authentication:SecretKey"])
            {
                var token = GenerateToken();
                var response = new
                {
                    Status = 200,
                    Company = "SmartBusinessCorp",
                    Type = "Public",
                    Token = token
                };
                return Ok(response);
            }
            else
            {
                throw new BusinessException("Secreto invalido");
            }
        }

        [HttpGet("ResendEmailVerification/{id}")]
        public async Task<IActionResult> resendEmail(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _auth.ReSendValidation(id);
            var response = new
            {
                Results = "Verificacion re-enviada",
                Data = data,
                Fecha = DateTime.Now,
                Company = "SmartBusinessCorp"
            };
            return Ok(response);
        }
        private string GenerateToken()
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,"SmartBusinessCorp"),
                new Claim(ClaimTypes.Role,"Administration")
            };

            var payload = new JwtPayload
                (
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claims,
                    DateTime.Now,
                    DateTime.UtcNow.AddMinutes(50)
                );
            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
