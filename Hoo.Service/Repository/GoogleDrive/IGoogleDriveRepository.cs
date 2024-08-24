using Hoo.Service.Models;

namespace Hoo.Service.Repository.GoogleDrive
{
    public interface IGoogleDriveRepository
    {
        Task<bool> AddFileAsync(GoogleFileItem item);

        Task<IEnumerable<GoogleFileItem>> GetFilesAsync();
    }
}
