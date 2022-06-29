using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Entities;
using SmartBusinessAPI.Entities.NuevosProducto;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ILogger<ProductosController> _logger;
        private readonly IProductosRepository _repository;

        public ProductosController(ILogger<ProductosController> logger, IProductosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        //[Authorize]
        [HttpGet("Planes/{tipo}")]
        public async Task<IActionResult> getPlan(int tipo)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getPlan(tipo);
            return Ok(data);
        }

        //[Authorize]
        [HttpGet("Planes/General")]
        public async Task<IActionResult> getPlans()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getPlans();
            return Ok(data);
        }

        //[Authorize]
        [HttpGet("Plan/{id}")]
        public async Task<IActionResult> getPlanById(int id)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.getPlanById(id);
            return Ok(data);
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> insertNewPlan(NewProduct prod)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.processNewPlan(prod);
            var response = new
            {
                Status = 200,
                Response = $"Plan procesado",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        //[Authorize]
        [HttpPut("Status")]
        public async Task<IActionResult> updateStatus(StatusProducto prod)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updateStatusProducto(prod);
            var response = new
            {
                Status = 200,
                Response = $"Plan actualizado",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);

        }

        //[Authorize]
        [HttpPut]
        public async Task<IActionResult> updatePlan(UpdateProduct prod)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _repository.updatePlan(prod);
            var response = new
            {
                Status = 200,
                Response = $"Plan actualizado",
                Details = "Smart Business API",
                Results = data
            };
            return Ok(response);
        }

    }
}
