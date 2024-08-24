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

        [HttpPost]
        [Route("AddWebFile")]
        public async Task<IActionResult> UploadFile(Uri fileUri)
        {
            return Ok();
        }

        /*
        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile(string fileSourceId, string fileAccessString, IFormFile file)
        {
            using (var stream = new FileStream(fileAccessString, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("DeleteFile")]
        public async Task<IActionResult> DeleteFile(string fileSourceId, string fileAccessString)
        {
            return Ok();
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileSourceId, string fileAccessString)
        {
            var content = new FileStream("C:\\Programming\\hoo\\README.md", FileMode.Open, FileAccess.Read, FileShare.Read);
            var response = File(content, "application/octet-stream");
            return response;
        }
        */
    }
}
