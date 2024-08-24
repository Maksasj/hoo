using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using Microsoft.AspNetCore.Mvc;

namespace HooService.Common
{
    public class HooFileProvider : IFileProvider
    {
        private readonly IGoogleDriveSource _googleDriveProvider;
        private readonly IOneDriveSource _oneDriveProvider;
        private readonly ILogger<HooFileProvider> _logger;

        public HooFileProvider(ILogger<HooFileProvider> logger, IGoogleDriveSource googleDriveProvider, IOneDriveSource oneDriveProvider)
        {
            _logger = logger;

            _googleDriveProvider = googleDriveProvider;
            _oneDriveProvider = oneDriveProvider;
        }

        public async Task<IActionResult> GetFile(string fileSourceId, string filePath)
        {
            foreach (var file in _googleDriveProvider.GetFiles("root"))
            {
                _logger.LogInformation(file.Id);
            }

            return new OkResult();
        }

        public async Task<IActionResult> GetSource(string sourceId)
        {
            return new OkResult();
        }
    }
}
