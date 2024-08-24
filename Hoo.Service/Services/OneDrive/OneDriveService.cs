using Hoo.Service.Authetication;
using Microsoft.Graph;

namespace Hoo.Service.Services.OneDrive
{
    public class OneDriveService : IOneDriveService
    {
        private readonly GraphServiceClient _graphClientClient;
        private readonly string _driveId;

        public OneDriveService(IConfiguration configuration)
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

