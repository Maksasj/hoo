using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Graph.Reports.GetPrinterArchivedPrintJobsWithPrinterIdWithStartDateTimeWithEndDateTime;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Graph.Reports.GetPrinterArchivedPrintJobsWithPrinterIdWithStartDateTimeWithEndDateTime;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using HooService.Common;

namespace HooService.Controllers
{
    [ApiController]
    public class HooController : ControllerBase
    {
        private readonly ILogger<HooController> _logger;
        private readonly IFileProvider _fileProvider;

        public HooController(ILogger<HooController> logger, IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        [HttpGet]
        [Route("GetFile")]
        public async Task<IActionResult> GetFile(string fileSourceId, string fileId)
        {
            return await _fileProvider.GetFile(fileSourceId, fileId);
        }

        [HttpGet]
        [Route("GetSource")]
        public async Task<IActionResult> GetSource(string sourceId)
        {
            return await _fileProvider.GetSource(sourceId);
        }
    }
}
