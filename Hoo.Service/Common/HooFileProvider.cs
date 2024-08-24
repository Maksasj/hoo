using Hoo.Service.Models;
using Hoo.Service.Repository.WebFiles;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HooService.Common
{
    public class HooFileProvider : IFileProvider
    {
        private readonly IGoogleDriveSource _googleDriveProvider;
        private readonly IOneDriveSource _oneDriveProvider;
        private readonly ILogger<HooFileProvider> _logger;

        private readonly IWebFileRepository _webFileRepository;

        public HooFileProvider(ILogger<HooFileProvider> logger, IGoogleDriveSource googleDriveProvider, IOneDriveSource oneDriveProvider, IWebFileRepository webFileRepository)
        {
            _logger = logger;

            _googleDriveProvider = googleDriveProvider;
            _oneDriveProvider = oneDriveProvider;
            _webFileRepository = webFileRepository;
        }

        public async Task<WebFileItem[]> GetFiles()
        {
            var result = new List<WebFileItem>();

            result.AddRange(await _webFileRepository.GetFilesAsync());

            return result.ToArray();
        }

        public async Task<IActionResult> AddWebFile(Uri fileUri)
        {
            await _webFileRepository.AddWebFileAsync(fileUri);

            return new OkResult();
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
