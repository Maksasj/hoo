using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Services.WebFiles
{
    public interface IWebFileService
    {
        Task<bool> AddFileAsync(Uri fileUri);

        Task<FileThumbnailItem> GetFileThumbnailAsync(Guid fileId);

        Task<IEnumerable<WebFileItem>> GetFilesAsync();

        Task SyncRemote();
    }
}
