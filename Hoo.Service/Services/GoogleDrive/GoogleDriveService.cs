using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Hoo.Service.Controllers;
using Hoo.Service.Repository.GoogleDrive;
using Hoo.Service.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Hoo.Service.Services.GoogleDrive.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hoo.Service.Services.GoogleDrive
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private IGoogleDriveClient _driveClient { get; set; }

        private readonly ILogger<SourceController> _logger;
        private readonly IGoogleDriveRepository _googleDriveRepository;

        public GoogleDriveService(IConfiguration configuration, ILogger<SourceController> logger, IGoogleDriveRepository googleDriveRepository)
        {
            _logger = logger;
            _googleDriveRepository = googleDriveRepository;

            _driveClient = new GoogleDriveClient(logger, configuration);
        }

        public async Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync()
        {
            return await _googleDriveRepository.GetFilesAsync();
        }

        public async Task SyncRemoteAsync()
        {
            foreach (var file in _driveClient.GetFiles())
            {
                if (_googleDriveRepository.HasFile(file.Id))
                    continue;

                // Todo, we probably can bulk add files to database
                await _googleDriveRepository.AddFileAsync(new GoogleDriveFileItem
                {
                    Id = Guid.NewGuid(),
                    GoogleId = file.Id,
                    Name = file.Name
                });
            }
        }

        public async Task<IActionResult> ClearCacheAsync()
        {
            await _googleDriveRepository.DeleteAllFilesAsync();

            return new OkResult();
        }
    }
}
