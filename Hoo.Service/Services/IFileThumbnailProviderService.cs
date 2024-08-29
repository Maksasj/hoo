using Hoo.Service.Models;

namespace Hoo.Service.Services
{
    public interface IFileThumbnailProviderService
    {
        Task<FileThumbnailItem> GetFileThumbnailAsync(Guid fileId);
    }
}
