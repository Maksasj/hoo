using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public interface IWebFileRepository
    {
        Task<IEnumerable<WebFileItem>> GetFilesAsync();

        Task<bool> AddWebFileAsync(Uri fileUri);
    }
}