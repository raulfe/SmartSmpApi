using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KycController : ControllerBase
    {
        private readonly ILogger<KycController> _logger;
        private readonly IKycRepository _repository;

        public KycController(ILogger<KycController> logger, IKycRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize]
        [HttpGet("Prospectos/List")]
        public async Task<IActionResult> getProspectoValidacionList()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getListProspectoValidacion();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("Prospectos/{id}")]
        public async Task<IActionResult> getProspectoValidacionByID(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getProspectoValidacionInfo(id);
            return Ok(data);
        }

        [Authorize]
        [HttpGet("Socios/List")]
        public async Task<IActionResult> getSociosValidacionList()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getListSociosValidacion();
            return Ok(data);
        }
    }
}
