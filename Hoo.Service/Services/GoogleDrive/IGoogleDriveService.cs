using Hoo.Service.Models;

namespace Hoo.Service.Services.GoogleDrive
{
    public interface IGoogleDriveService
    {
        Task<IEnumerable<GoogleFileItem>> GetFilesAsync();

        Task SyncRemote();
    }
}
