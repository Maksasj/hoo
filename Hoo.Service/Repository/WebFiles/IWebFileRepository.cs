using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public interface IWebFileRepository
    {
        Task<bool> AddFileAsync(WebFileItem item);
        
        Task<WebFileItem> GetFileAsync(Guid fileId);

        Task<IEnumerable<WebFileItem>> GetFilesAsync();
    }
}