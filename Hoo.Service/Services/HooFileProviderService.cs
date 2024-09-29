using Hoo.Service.Models;
using Hoo.Service.Repository.WebFiles;
using Hoo.Service.Services.OneDrive;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.WebFiles;
using Microsoft.Graph.Models.Security;

namespace Hoo.Service.Common
{
    public class HooFileProviderService : IFileProviderService
    {
        private readonly IGoogleDriveService _googleDriveService;
        private readonly IOneDriveService _oneDriveService;
        private readonly IWebFileService _webFileService;

        private readonly ILogger<HooFileProviderService> _logger;

        public HooFileProviderService(
            ILogger<HooFileProviderService> logger, 
            IGoogleDriveService googleDriveService,
            IOneDriveService oneDriveService,
            IWebFileService webFileService
            )
        {
            _logger = logger;

            _googleDriveService = googleDriveService;
            _oneDriveService = oneDriveService;
            _webFileService = webFileService;
        }

        public async Task<IEnumerable<FileItemModel>> GetFilesAsync()
        {
            var result = new List<FileItemModel>();

            // Web files
            result.AddRange((await _webFileService.GetFilesAsync()).Select(file => new FileItemModel
            {
                Id = file.Id,
                SourceType = "WebFile",
                Name = file.AccessUri.ToString()
            }));

            // Google Drive files
            result.AddRange((await _googleDriveService.GetFilesAsync()).Select(file => new FileItemModel
            {
                Id = file.Id,
                SourceType = "GoogleDrive",
                Name = file.Name
            }));

            // One Drive files
            result.AddRange((await _oneDriveService.GetFilesAsync()).Select(file => new FileItemModel
            {
                Id = file.Id,
                SourceType = "OneDrive",
                Name = file.Name
            }));

            return result;
        }
    }
}
