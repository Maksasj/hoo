using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Graph.Reports.GetPrinterArchivedPrintJobsWithPrinterIdWithStartDateTimeWithEndDateTime;
using HooService.Repository;

namespace HooService.Controllers
{
    [ApiController]
    public class HooController : ControllerBase
    {
        private readonly ILogger<HooController> _logger;
        private readonly GoogleDriveRepository _gooleDrive;

        public HooController(ILogger<HooController> logger, GoogleDriveRepository gooleDrive)
        {
            _logger = logger;
            _gooleDrive = gooleDrive;
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
        [Route("Get")]
        public async Task<IActionResult> Get()
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

            return Ok();
        }

        [HttpGet]
        [Route("GetOther")]
        public async Task<IActionResult> GetOther()
        {
            foreach (var file in _gooleDrive.GetFiles("root"))
            {
                Console.WriteLine(file.Name);
            }

            return Ok();
        }
    }
}
