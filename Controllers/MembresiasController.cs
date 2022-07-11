using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.NuevaMembresia;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiasController : ControllerBase
    {
        private readonly ILogger<MembresiasController> _logger;
        private readonly IMembresiasRepository _repository;

        public MembresiasController(ILogger<MembresiasController> logger, IMembresiasRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> getMembresias([FromBody] MembresiasFilter filter)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.GetMembresias(filter);
            return Ok(data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getMembresiaById(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.GetMembresiaById(id);
            return Ok(data);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> insertNewMembresia(NewMembership membre)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.processNewMembership(membre);
            var response = new
            {
                Status = 200,
                Response = $"Mebresia procesada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> updateMembresia(updateMembership membreUpdate)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateMembresia(membreUpdate);
            var response = new
            {
                Status = 200,
                Response = "Mebresia Actualizada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);
        }

        [Authorize]
        [HttpPut("Status/{status}/{membership}")]
        public async Task<IActionResult> updateMembresiaStatus(bool status, int membership)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateMembresiaStatus(status,membership);
            var response = new
            {
                Status = 200,
                Response = "Estatus Actualizado",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);
        }
    }
}
