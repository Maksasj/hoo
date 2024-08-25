using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Hoo.Service.Controllers;
using Hoo.Service.Repository.GoogleDrive;

namespace Hoo.Service.Services.GoogleDrive.Client
{
    public class GoogleDriveClient : IGoogleDriveClient
    {
        private readonly ILogger<SourceController> _logger;
        private DriveService _driveService { get; }


        public GoogleDriveClient(ILogger<SourceController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _driveService = InitializeDriveService(configuration);
        }
        
        public IGoogleDriveFileCollection GetFiles()
        {
            return new GoogleDriveFileCollection(_driveService);
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
    }
}
