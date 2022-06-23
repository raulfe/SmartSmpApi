using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartBusinessAPI.Interfaces;
using System.Threading.Tasks;

namespace SmartBusinessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly ILogger<CatalogsController> _logger;
        private readonly ICatalogsRepository _catalogs;

        public CatalogsController(ILogger<CatalogsController> logger, ICatalogsRepository catalogs)
        {
            _logger = logger;
            _catalogs = catalogs;
        }
        [Authorize]
        [HttpGet("Paises/List")]
        public async Task<IActionResult> getPaises()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _catalogs.getPaises();
            return Ok(data);
        }
        
        [Authorize]
        [HttpGet("Paises/{name}")]
        public async Task<IActionResult> getPaises(string name)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _catalogs.getPaisesByName(name);
            return Ok(data);
        }

        [Authorize]
        [HttpGet("Paises/Grouped")]
        public async Task<IActionResult> getPaisesGroup()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _catalogs.getPaisesGroup();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("MedioContacto")]
        public async Task<IActionResult> getMedioContacto()
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _catalogs.getMedioContacto();
            return Ok(data);
        }

        [Authorize]
        [HttpGet("General/{categoria}")]
        public async Task<IActionResult> getGeneral(string categoria)
        {
            var address = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            _logger.LogInformation($"Request by {address} IP");
            var data = await _catalogs.getGeneral(categoria);
            return Ok(data);
        }
    }
}
