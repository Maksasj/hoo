using Azure.Identity;
using Google.Apis.Auth.OAuth2;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.Graph;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace HooService.Repository.OneDrive
{
    public class OneDriveSource : IOneDriveSource
    {
        private readonly GraphServiceClient _graphClientClient;
        private readonly string _driveId;

        public OneDriveSource(IConfiguration configuration)
        {
            _graphClientClient = new GraphServiceClient(new SimpleAuthProvider(configuration.GetValue<string>("BearerToken")));
            _driveId = configuration.GetValue<string>("DriveId");
        }

        public async void Do()
        {
            var result = await _graphClientClient.Drives[_driveId].Items["root"].Children.GetAsync();
            foreach (var item in result.Value)
            {
                // _logger.LogInformation(item.Name);
            }
        }
    }
}

