using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Services.GoogleDrive
{
    public interface IGoogleDriveService
    {
        Task<IEnumerable<GoogleFileItem>> GetFilesAsync();

        Task SyncRemoteAsync();

        Task<IActionResult> ClearCacheAsync();
    }
}
