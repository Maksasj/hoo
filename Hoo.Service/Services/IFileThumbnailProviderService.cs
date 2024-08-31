using Hoo.Service.Models;

namespace Hoo.Service.Services
{
    public interface IFileThumbnailProviderService
    {
        Task<FileThumbnailModel> GetFileThumbnailAsync(Guid fileId);
    }
}
