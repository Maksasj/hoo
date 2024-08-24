using Hoo.Service.Authetication;
using Hoo.Service.Models;
using Hoo.Service.Repository.OneDrive;
using Hoo.Service.Repository.WebFiles;
using Hoo.Service.Services.WebFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace Hoo.Service.Services.OneDrive
{
    public class OneDriveService : IOneDriveService
    {
        private readonly string _driveId;
        private readonly GraphServiceClient _graphClientClient;

        private readonly ILogger<OneDriveService> _logger;
        private readonly IOneDriveFileRepository _oneDriveFileRepository;

        public OneDriveService(IConfiguration configuration, ILogger<OneDriveService> logger, IOneDriveFileRepository oneDriveFileRepository)
        {
            _driveId = configuration.GetValue<string>("DriveId");
            _graphClientClient = new GraphServiceClient(new SimpleAuthProvider(configuration.GetValue<string>("BearerToken")));

            _logger = logger;
            _oneDriveFileRepository = oneDriveFileRepository;
        }

        public async Task<IEnumerable<OneDriveFileItem>> GetFilesAsync()
        {
            return await _oneDriveFileRepository.GetFilesAsync();
        }

        public async Task SyncRemoteAsync()
        {
            var result = await _graphClientClient.Drives[_driveId].Items["root"].Children.GetAsync();

            foreach (var item in result.Value)
            {
                if (item.File != null)
                {
                    _oneDriveFileRepository.AddFileAsync(new OneDriveFileItem
                    {
                        Id = Guid.NewGuid(),
                        OneDriveId = item.Id,
                        Name = item.Name
                    });
                }
            }
        }

        public async Task<IActionResult> ClearCacheAsync()
        {
            await _oneDriveFileRepository.DeleteAllFilesAsync();

            return new OkResult();
        }
    }
}

