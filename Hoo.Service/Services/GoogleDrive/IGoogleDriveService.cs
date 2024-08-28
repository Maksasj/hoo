using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Services.GoogleDrive
{
    public interface IGoogleDriveService
    {
        Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync();
        
        Task<FileThumbnailItem> GetFileThumbnailAsync(Guid fileId);

        Task SyncRemoteAsync();

        Task<IActionResult> ClearCacheAsync();
    }
}
