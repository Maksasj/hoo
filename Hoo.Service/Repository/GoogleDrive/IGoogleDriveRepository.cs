using Hoo.Service.Models;

namespace Hoo.Service.Repository.GoogleDrive
{
    public interface IGoogleDriveRepository
    {
        Task<bool> AddFileAsync(GoogleDriveFileItem item);

        Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync();

        Task<bool> DeleteAllFilesAsync();
    }
}
