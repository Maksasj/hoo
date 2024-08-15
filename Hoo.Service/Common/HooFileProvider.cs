using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Azure.Identity;
using Azure.Core;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.Graph.Reports.GetPrinterArchivedPrintJobsWithPrinterIdWithStartDateTimeWithEndDateTime;
using HooService.Repository.GoogleDrive;
using HooService.Repository.OneDrive;

namespace HooService.Common
{
    public class HooFileProvider : IFileProvider
    {
        private readonly IGoogleDriveProvider _googleDriveProvider;
        private readonly IOneDriveProvider _oneDriveProvider;
        private readonly ILogger<HooFileProvider> _logger;

        public HooFileProvider(ILogger<HooFileProvider> logger, IGoogleDriveProvider googleDriveProvider, IOneDriveProvider oneDriveProvider)
        {
            _logger = logger;

            _googleDriveProvider = googleDriveProvider;
            _oneDriveProvider = oneDriveProvider;
        }

        public async Task<IActionResult> GetFile(string fileSourceId, string fileAccessString)
        {
            return new OkResult();
        }

        public async Task<IActionResult> GetSource(string sourceId)
        {
            return new OkResult();
        }
    }
}
