using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProspectosController : ControllerBase
    {
        private readonly ILogger<ProspectosController> _logger;
        private readonly IProspectosRepository _repository;

        public ProspectosController(ILogger<ProspectosController> logger, IProspectosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }


        [Authorize]
        [HttpGet("/Prospecto/Documentacion/{id}")]
        public async Task<IActionResult> getDocumentById(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getDocumentById(id);
            return Ok(data);
        }

        [Authorize]
        [HttpPut("/Prospectos/Validacion")]
        public async Task<IActionResult> updateProspectoValidacion(Validacionupdate prospectoValidacion)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateProspectoValidacion(prospectoValidacion);
            var response = new
            {
                Status = 200,
                Response = $"Prospecto (validacion) actualizado con éxito",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);
        }



    }
}
