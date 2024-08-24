using Hoo.Service.Models;
using Hoo.Service.Repository.WebFiles;
using Hoo.Service.Services.OneDrive;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.WebFiles;

namespace HooService.Common
{
    public class HooFileProviderService : IFileProviderService
    {
        private readonly IGoogleDriveService _googleDriveService;
        private readonly IOneDriveService _oneDriveProvider;
        private readonly IWebFileService _webFileService;

        private readonly ILogger<HooFileProviderService> _logger;

        public HooFileProviderService(
            ILogger<HooFileProviderService> logger, 
            IGoogleDriveService googleDriveService,
            IOneDriveService oneDriveProvider,
            IWebFileService webFileService
            )
        {
            _logger = logger;

            _googleDriveService = googleDriveService;
            _oneDriveProvider = oneDriveProvider;
            _webFileService = webFileService;
        }

        public async Task<IEnumerable<FileItem>> GetFiles()
        {
            var result = new List<FileItem>();

            /*
            result.AddRange(await _webFileRepository.GetFilesAsync());
            */

            return result;
        }
    }
}
