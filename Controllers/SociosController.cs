using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Interfaces;
using SmartBusinessAPI.Models;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {
        private readonly ILogger<SociosController> _logger;
        private readonly ISociosRepository _repository;

        public SociosController(ILogger<SociosController> logger, ISociosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getSocioById(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSocioByID(id);
            return Ok(data);
        }

        [Authorize]
        [HttpGet("Count")]
        public async Task<IActionResult> getSociosCount()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSociosCount();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("Position/{position}")]
        public async Task<IActionResult> getSociosByPosition(int position)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSocioByPosition(position);
            return Ok(data);
        }


        [Authorize]
        [HttpPut("SocioValidacion/{socio}")]
        public async Task<IActionResult> updateSociosValidacion(SocioValidacion socioValida, int socio)
        {
            var addres = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {addres} IP");
            var data = await _repository.updateSociosValidacion(socioValida, socio);
            return Ok(data);
        }


        [Authorize]
        [HttpGet("DocumentSocio")]
        public async Task<IActionResult> getDataSocioDocumentacion(int socio) 
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getDataSocioDocumentacion(socio);
            return Ok(data);
        }


    }
}
