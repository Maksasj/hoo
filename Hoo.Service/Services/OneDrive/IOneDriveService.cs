using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Services.OneDrive
{
    public interface IOneDriveService
    {
        Task<IEnumerable<OneDriveFileItem>> GetFilesAsync();

        Task SyncRemoteAsync();

        Task<IActionResult> ClearCacheAsync();
    }
}
