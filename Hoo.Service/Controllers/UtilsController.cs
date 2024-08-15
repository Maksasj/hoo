using Microsoft.AspNetCore.Mvc;

namespace HooService.Controllers
{
    [ApiController]
    public class UtilsController : ControllerBase
    {
        private readonly ILogger<UtilsController> _logger;

        public UtilsController(ILogger<UtilsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Ping")]
        public async Task<IActionResult> Ping()
        {
            return Ok();
        }

        [HttpGet]
        [Route("Health")]
        public async Task<IActionResult> GetHealth()
        {
            return Ok();
        }
    }
}
