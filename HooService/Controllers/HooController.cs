using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Graph.Reports.GetPrinterArchivedPrintJobsWithPrinterIdWithStartDateTimeWithEndDateTime;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;

namespace HooService.Controllers
{
    [ApiController]
    public class HooController : ControllerBase
    {
        private readonly ILogger<HooController> _logger;

        private readonly IGoogleSourceDrive _googleGoogleSourceDrive;
        private readonly IOneSourceDrive _oneSourceDrive;

        public HooController(ILogger<HooController> logger, IGoogleSourceDrive gooleGoogleSourceDrive, IOneSourceDrive oneSourceDrive)
        {
            _logger = logger;

            _googleGoogleSourceDrive = gooleGoogleSourceDrive;
            _oneSourceDrive = oneSourceDrive;
        }

        [HttpGet]
        [Route("OneDrive")]
        public async Task<IActionResult> OneDrive()
        {
            _oneSourceDrive.Do();

            return Ok();
        }

        [HttpGet]
        [Route("GoogleDrive")]
        public async Task<IActionResult> GoogleDrive()
        {
            foreach (var file in _googleGoogleSourceDrive.GetFiles("root"))
            {
                Console.WriteLine(file.Name);
            }

            return Ok();
        }
    }
}
