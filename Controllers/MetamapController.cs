using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartBusinessAPI.Entities.Verification;
using SmartBusinessAPI.Entities.Webhook;
using SmartBusinessAPI.Interfaces;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetamapController : ControllerBase
    {
        private readonly ILogger<MetamapController> _logger;
        private readonly IMetamapRepository _repository;

        public MetamapController(ILogger<MetamapController> logger, IMetamapRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost("Sync")]
        public async Task<IActionResult> syncMetamap([FromBody] WebHookObject webHook)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var payload = JsonConvert.SerializeObject(webHook);
            _logger.LogInformation(payload);
            if(webHook.eventName == "verification_completed" || webHook.eventName == "verification_updated")
            {
                var url = webHook.resource;
                var data = await _repository.syncVerification(url);
                if (data == true)
                {
                    var response = new
                    {
                        Status = 200,
                        Response = $"El usuario ha sido sincronizado por completo",
                        Details = "Smart Business API"
                    };
                    return Ok(response);
                }
                else
                {
                    var response = new
                    {
                        Status = 200,
                        Response = $"El sistema de verificación fallo",
                        Details = "Smart Business API"
                    };
                    return Ok(response);
                }
            }
            else
            {
                var response = new
                {
                    Status = 200,
                    Response = $"Step aun no completado",
                    Details = "Smart Business API"
                };
                return Ok(response);
            }
            
        }

        

        
    }
}
