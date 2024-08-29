using Hoo.Service.Models;

namespace Hoo.Service.Repository.GoogleDrive
{
    public interface IGoogleDriveRepository
    {
        Task<bool> AddFileAsync(GoogleDriveFileItem item);

        Task<GoogleDriveFileItem> GetFileAsync(Guid fileId);
        
        Task<bool> DeleteFileAsync(Guid fileId);

        bool HasFile(Guid fileId);

        bool HasGoogleFile(string GoogleId);

        Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync();

        Task<bool> DeleteFilesAsync(IEnumerable<GoogleDriveFileItem> files);
    }
}
