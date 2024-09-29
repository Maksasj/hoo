using Hoo.Service.Common;
using Hoo.Service.Models;
using Hoo.Service.Services;
using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.OneDrive;
using Hoo.Service.Services.WebFiles;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hoo.Service.Controllers
{
    [ApiController]
    public class FileController
    {
        private readonly ILogger<FileController> _logger;

        private readonly IFileProviderService _fileProviderService;
        private readonly IFileThumbnailProviderService _fileThumbnailProviderService;

        public FileController(ILogger<FileController> logger, IFileProviderService fileProviderService, IFileThumbnailProviderService fileThumbnailProviderService)
        {
            _logger = logger;
            _fileProviderService = fileProviderService;
            _fileThumbnailProviderService = fileThumbnailProviderService;
        }

        [HttpGet]
        [Route("GetFiles")]
        public async Task<IActionResult> GetFiles(int pageIndex = 0, int itemsPerPage = 100)
        {
            if (pageIndex < 0 || itemsPerPage < 0)
                return new BadRequestObjectResult("Invalid page index or items per page");

            var files = (await _fileProviderService.GetFilesAsync())
                .Skip(pageIndex * itemsPerPage)
                .Take(itemsPerPage);
            
            return new OkObjectResult(new FileItemPageResponseModel
            {
                PageIndex = pageIndex,
                ItemCount = files.Count(),
                Files = files,
            });
        }

        [HttpGet]
        [Route("GetFileCount")]
        public async Task<IActionResult> GetFileCount()
        {
            return new OkObjectResult((await _fileProviderService.GetFilesAsync()).LongCount());
        }

        [HttpGet]
        [Route("GetFileThumbnail")]
        public async Task<IActionResult> GetFileThumbnail(Guid fileId)
        {
            var item = await _fileThumbnailProviderService.GetFileThumbnailAsync(fileId);

            if (item == null)
                return new BadRequestObjectResult("File not found");

            return new OkObjectResult(new FileThumbnailResponseModel
            {
                FileId = item.FileId,
                ThumbnailUrl = item.ThumbnailUrl
            });
        }
    }
}
