using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public interface IWebFileRepository
    {
        Task<bool> AddFileAsync(WebFileItem item);
        
        Task<WebFileItem> GetFileAsync(Guid fileId);

        Task<bool> DeleteFileAsync(Guid fileId);

        bool HasFile(Guid fileId);

        Task<IEnumerable<WebFileItem>> GetFilesAsync();

        Task<bool> DeleteFilesAsync(IEnumerable<WebFileItem> files);
    }
}