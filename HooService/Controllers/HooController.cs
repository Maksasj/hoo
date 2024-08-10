using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;

namespace HooService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HooController : ControllerBase
    {
        private readonly ILogger<HooController> _logger;

        public HooController(ILogger<HooController> logger)
        {
            _logger = logger;
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

        [HttpGet]
        public async Task Get()
        {
            var client = CreateGraphClient("****", "****");

            _logger.LogInformation(client.ToString());

            var count = await client.Applications.Count.GetAsync();

            _logger.LogInformation(count.ToString());
            /*
            var userDriveId = drive.Id;

            var rootItem = await client.Drives[userDriveId].Root.GetAsync();
            _logger.LogInformation(rootItem.ToString());
            */
        }
    }
}
