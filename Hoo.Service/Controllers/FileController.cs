using Hoo.Service.Models;
using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.OneDrive;
using Hoo.Service.Services.WebFiles;
using HooService.Common;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Hoo.Service.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;
        private readonly IFileProviderService _fileProviderService;

        public FileController(ILogger<FileController> logger, IFileProviderService fileProviderService)
        {
            _logger = logger;
            _fileProviderService = fileProviderService;
        }

        [HttpGet]
        [Route("GetFiles")]
        public async Task<FileItemPageResponseModel> GetFiles(int pageIndex = 0, int itemsPerPage = 100)
        {
            var files = (await _fileProviderService.GetFilesAsync())
                .Skip(pageIndex * itemsPerPage)
                .Take(itemsPerPage)
                .ToArray();
            
            return new FileItemPageResponseModel
            {
                PageIndex = pageIndex,
                ItemCount = files.Length,
                Files = files,
            };
        }

        [HttpGet]
        [Route("GetFileCount")]
        public async Task<long> GetFileCount()
        {
            return (await _fileProviderService.GetFilesAsync()).LongCount();
        }
    }
}
