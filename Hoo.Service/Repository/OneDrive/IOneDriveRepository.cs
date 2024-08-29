using Hoo.Service.Models;

namespace Hoo.Service.Repository.OneDrive
{
    public interface IOneDriveRepository
    {
        Task<bool> AddFileAsync(OneDriveFileItem item);

        Task<IEnumerable<OneDriveFileItem>> GetFilesAsync();

        Task<bool> DeleteAllFilesAsync();
    }
}
