using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.OneDrive;
using Hoo.Service.Services.WebFiles;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Controllers
{
    [ApiController]
    public class SourceController
    {
        private readonly IGoogleDriveService _googleDriveService;
        private readonly IOneDriveService _oneDriveService;
        private readonly IWebFileService _webFileService;

        private readonly ILogger<SourceController> _logger;

        public SourceController(
            ILogger<SourceController> logger, 
            IGoogleDriveService googleDriveService, 
            IOneDriveService oneDriveService, 
            IWebFileService webFileService
            )
        {
            _googleDriveService = googleDriveService;
            _oneDriveService = oneDriveService;
            _webFileService = webFileService;

            _logger = logger;
        }

        [HttpPatch]
        [Route("SyncSources")]
        public async Task<IActionResult> SyncSources()
        {
            await _googleDriveService.SyncRemoteAsync();
            // await _oneDriveService.SyncRemoteAsync();
            // await _webFileService.SyncRemote();

            return new OkResult();
        }

        [HttpDelete]
        [Route("ClearCache")]
        public async Task<IActionResult> ClearCache()
        {
            await _googleDriveService.ClearCacheAsync();
            await _oneDriveService.ClearCacheAsync();

            return new OkResult();
        }
    }
}
