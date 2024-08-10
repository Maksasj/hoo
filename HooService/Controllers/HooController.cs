using Microsoft.AspNetCore.Mvc;

namespace HooService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HooController : ControllerBase
    {
        private readonly ILogger<HooController> _logger;

        public HooController(ILogger<HooController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {

        }
    }
}
