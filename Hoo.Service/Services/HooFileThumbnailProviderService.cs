using System.Diagnostics;
using Hoo.Service.Models;
using Hoo.Service.Repository.GoogleDrive;
using Hoo.Service.Repository.OneDrive;
using Hoo.Service.Repository.WebFiles;
using Hoo.Service.Services.GoogleDrive;
using Hoo.Service.Services.OneDrive;
using Hoo.Service.Services.WebFiles;
using HooService.Common;

namespace Hoo.Service.Services
{
    public class HooFileThumbnailProviderService : IFileThumbnailProviderService
    {
        private readonly IGoogleDriveRepository _googleDriveRepository;
        private readonly IOneDriveRepository _oneDriveRepository;
        private readonly IWebFileRepository _webFileRepository;

        private readonly ILogger<HooFileThumbnailProviderService> _logger;

        public HooFileThumbnailProviderService(
            ILogger<HooFileThumbnailProviderService> logger,
            IGoogleDriveRepository googleDriveRepository,
            IOneDriveRepository oneDriveRepository,
            IWebFileRepository webFileRepository
        )
        {
            _logger = logger;

            _googleDriveRepository = googleDriveRepository;
            _oneDriveRepository = oneDriveRepository;
            _webFileRepository = webFileRepository;
        }

        public async Task<FileThumbnailModel> GetFileThumbnailAsync(Guid fileId)
        {
            // Google Drive file
            if (_googleDriveRepository.HasFile(fileId))
            {
                var file = await _googleDriveRepository.GetFileAsync(fileId);

                return new FileThumbnailModel
                {
                    FileId = file.Id,
                    ThumbnailUrl = file.ThumbnailUri
                };
            }

            // Web file
            if (_webFileRepository.HasFile(fileId))
            {
                var file = await _webFileRepository.GetFileAsync(fileId);

                return new FileThumbnailModel
                {
                    FileId = file.Id,
                    ThumbnailUrl = file.AccessUri.ToString()
                };
            }

            throw new UnreachableException("File is not contained in any repository");
        }
    }
}
