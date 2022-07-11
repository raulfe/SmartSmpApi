using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities.NuevaPromocion;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromocionesController : ControllerBase
    {
        private readonly ILogger<PromocionesController> _logger;
        private readonly IPromocionesRepository _repository;

        public PromocionesController(ILogger<PromocionesController> logger, IPromocionesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [Authorize]
        [HttpGet("List")]
        public async Task<IActionResult> getPromociones()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getPromociones();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> getPromocionById(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getPromocionByIDCustom(id);
            return Ok(data);
        }

        [Authorize]
        [HttpPost("Plan")]
        public async Task<IActionResult> insertNewPromoPlan(NewPromocion promo)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.processNewPromoProduct(promo);
            var response = new
            {
                Status = 200,
                Response = $"Promocion procesada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        [Authorize]
        [HttpPost("Membresia")]
        public async Task<IActionResult> insertNewPromoMembresia(NewPromocionMembresia promo)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.processNewPromoMembresia(promo);
            var response = new
            {
                Status = 200,
                Response = $"Promocion procesada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        [Authorize]
        [HttpPut("Plan")]
        public async Task<IActionResult> updatePromoProduct(NewPromocion promo)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updatePromoProduct(promo);
            var response = new
            {
                Status = 200,
                Response = $"Promocion actualizada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        [Authorize]
        [HttpPut("Membresia")]
        public async Task<IActionResult> updatePromoMembresia(NewPromocionMembresia promo)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updatePromoMembresias(promo);
            var response = new
            {
                Status = 200,
                Response = $"Promocion actualizada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        [Authorize]
        [HttpPut("Status/{status}/{id}")]
        public async Task<IActionResult> insertNewPromoPlan(bool status, int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateStatus(status, id);
            var response = new
            {
                Status = 200,
                Response = $"Promocion actualizada",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }
    }
}
