using Azure.Identity;
using HooService.Repository.GoogleDrive;
using Microsoft.Graph;

namespace HooService.Repository.OneDrive
{
    public class OneSourceDrive : IOneSourceDrive
    {
        private readonly ILogger<OneSourceDrive> _logger;
        private readonly GraphServiceClient _graphClientClient;

        public OneSourceDrive(ILogger<OneSourceDrive> logger, IConfiguration configuration)
        {
            _logger = logger;
            // _graphClientClient = CreateGraphClient("****", "****");
        }
        public async void Do()
        {
            var count = await _graphClientClient.Applications.Count.GetAsync();
            _logger.LogInformation(count.ToString());
        }

        private GraphServiceClient CreateGraphClient(string tenantId, string clientId)
        {
            string[] scopes = { "User.Read" };

            var options = new InteractiveBrowserCredentialOptions()
            {
                ClientId = clientId,
                TenantId = tenantId
            };

            var credential = new InteractiveBrowserCredential(options);

            return new GraphServiceClient(credential, scopes);
        }
    }
}
