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
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Services.GoogleDrive
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private DriveService _driveService { get; }

        private readonly ILogger<SourceController> _logger;
        private readonly IGoogleDriveRepository _googleDriveRepository;

        public GoogleDriveService(IConfiguration configuration, ILogger<SourceController> logger, IGoogleDriveRepository googleDriveRepository)
        {
            _logger = logger;
            _googleDriveRepository = googleDriveRepository;

            _driveService = InitializeDriveService(configuration);
        }

        public async Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync()
        {
            return await _googleDriveRepository.GetFilesAsync();
        }

        public async Task SyncRemoteAsync()
        {
            foreach (var file in GetGoogleFiles())
            {
                _googleDriveRepository.AddFileAsync(new GoogleDriveFileItem
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

        private DriveService InitializeDriveService(IConfiguration configuration)
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = configuration.GetValue<string>("AccessToken"),
                RefreshToken = configuration.GetValue<string>("RefreshToken"),
            };

            var applicationName = configuration.GetValue<string>("ApplicationName");
            var username = configuration.GetValue<string>("Username");

            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = configuration.GetValue<string>("ClientId"),
                    ClientSecret = configuration.GetValue<string>("ClientSecret")
                },
                Scopes = new[] { DriveService.ScopeConstants.Drive },
                DataStore = new FileDataStore(applicationName)
            });


            var credential = new UserCredential(apiCodeFlow, username, tokenResponse);

            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
        }

        private IEnumerable<Google.Apis.Drive.v3.Data.File> GetGoogleFiles()
        {
            var fileList = _driveService.Files.List();
            fileList.Q = $"mimeType!='application/vnd.google-apps.folder'";
            fileList.Fields = "nextPageToken, files(id, name, size, mimeType)";

            var result = new List<Google.Apis.Drive.v3.Data.File>();
            string pageToken = null;
            do
            {
                fileList.PageToken = pageToken;
                
                var filesResult = fileList.Execute();
                var files = filesResult.Files;

                pageToken = filesResult.NextPageToken;
                result.AddRange(files);
                _logger.LogInformation(files.Count.ToString());
            } while (pageToken != null);

            return result;
        }
    }
}
