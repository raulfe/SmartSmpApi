using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly ILoginRepository _repository;

        public LoginController(ILogger<LoginController> logger, ILoginRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize]
        [HttpGet("{email}")]
        public async Task<IActionResult> validateEmail(string email, int padre)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.emailValidation(email, padre);
            var response = new object();
            switch (data)
            {
                case 0:
                    response = new
                    {
                        Status = 200,
                        Response = "/Login",
                        Email = email,
                        Details = "El usuario ya es un socio"
                    };

                    break;
                case 1:
                    response = new
                    {
                        Status = 200,
                        Response = "/Registro",
                        Email = email,
                        Details = "El usuario esta interesado pero no ah completado el registro"
                    };
                    break;
                case 2:
                    response = new
                    {
                        Status = 200,
                        Response = "/Login",
                        Email = email,
                        Details = "El usuario ya es un  prospecto"
                    };
                    break;
                case 3:
                    response = new
                    {
                        Status = 200,
                        Response = "/Registro",
                        Email = email,
                        Details = "Interesado creado satisfactoriamente"
                    };
                    break;
                default:
                    response = new
                    {
                        Status = 200,
                        Response = "/Login",
                        Email = email,
                        Details = "Tipo no valido"
                    };
                    break;
            }
            return Ok(response);
        }
    }
}
