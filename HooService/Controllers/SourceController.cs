using Microsoft.AspNetCore.Mvc;
using HooService.Common;

namespace HooService.Controllers
{
    [ApiController]
    public class SourceController : ControllerBase
    {
        private readonly ILogger<SourceController> _logger;
        private readonly IFileProvider _fileProvider;

        public SourceController(ILogger<SourceController> logger, IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetFile(string fileSourceId, string fileAccessString)
        {
            return await _fileProvider.GetFile(fileSourceId, fileAccessString);
        }

        [HttpGet]
        [Route("GetSource")]
        public async Task<IActionResult> GetSource(string sourceId)
        {
            return await _fileProvider.GetSource(sourceId);
        }

        [HttpGet]
        [Route("GetFiles")]
        public async Task<IActionResult> GetFiles()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetSources")]
        public async Task<IActionResult> GetSources()
        {
            return Ok();
        }
    }
}
