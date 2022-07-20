using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
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
        [HttpGet("/Socio/Documentacion/{id}")]
        public async Task<IActionResult> getDocumentById(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getDocumentById(id);
            return Ok(data);
        }


        [Authorize]
        [HttpPut("/Socios/Validacion")]
        public async Task<IActionResult> updateSocioValidacion(Validacionupdate socioValidacion)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateSocioValidacion(socioValidacion);
            var response = new
            {
                Status = 200,
                Response = $"Socio (validacion) actualizado con éxito",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);
        }

        //[Authorize]
        [HttpPost("List")]
        public async Task<IActionResult> getSocioSearch(SocioSearch socioSearch)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSocioSearch(socioSearch);
            return Ok(data);
        }

        //[Authorize]
        [HttpPost("Productos")]
        public async Task<IActionResult> getSocioProductSearch(SocioProductSearch socioProduct)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSocioProductSearch(socioProduct);
            return Ok(data);
        }

        //[Authorize]
        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> getSocioDetail(int id )
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
           _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSocioDetail(id);
            return Ok(data);
        }

        //[Authorize]
        [HttpGet("Detail/History/{id}")]
        public async Task<IActionResult> getSocioHistory(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getSocioHistory(id);
            return Ok(data);
        }

        //[Authorize]
        [HttpPut("/Status/{id}/")]
        public async Task<IActionResult> updateStatusSocio(int id, int status)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateStatusSocio(id, status);
            var response = new
            {
                Status = 200,
                Response = $"Estatus de Socio actualizado con éxito",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);
        }




    }
}
