using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;
using HooService.Common;

namespace HooService.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileProvider _fileProvider;

        public FileController(ILogger<FileController> logger, IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        [Route("GetFiles")]
        public async Task<WebFileItem[]> GetFiles()
        {
            return await _fileProvider.GetFiles();
        }

        [HttpPost]
        [Route("AddWebFile")]
        public async Task<IActionResult> AddWebFile(Uri fileUri)
        {
            await _fileProvider.AddWebFile(fileUri);

            return Ok();
        }
    }
}
