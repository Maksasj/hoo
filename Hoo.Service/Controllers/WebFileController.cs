using Hoo.Service.Models;
using Hoo.Service.Services.WebFiles;
using Microsoft.AspNetCore.Mvc;
using HooService.Common;

namespace HooService.Controllers
{
    [ApiController]
    public class WebFileController : ControllerBase
    {
        private readonly ILogger<WebFileController> _logger;
        private readonly IWebFileService _webFileService;

        public WebFileController(ILogger<WebFileController> logger, IWebFileService webFileService)
        {
            _logger = logger;
            _webFileService = webFileService;
        }

        [HttpGet]
        [Route("GetWebFiles")]
        public async Task<WebFileItem[]> GetWebFiles()
        {
            return (await _webFileService.GetFilesAsync()).ToArray();
        }

        [HttpPost]
        [Route("AddWebFile")]
        public async Task<IActionResult> AddWebFile(
            [FromForm] 
            Uri fileUri
            )
        {
            await _webFileService.AddFileAsync(fileUri);

            return Ok();
        }
    }
}
