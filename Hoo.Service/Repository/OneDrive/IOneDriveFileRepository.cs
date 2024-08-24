using Hoo.Service.Models;

namespace Hoo.Service.Repository.OneDrive
{
    public interface IOneDriveFileRepository
    {
        Task<bool> AddFileAsync(OneDriveFileItem item);

        Task<IEnumerable<OneDriveFileItem>> GetFilesAsync();

        Task<bool> DeleteAllFilesAsync();
    }
}
