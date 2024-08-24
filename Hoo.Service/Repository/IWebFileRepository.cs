using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository
{
    public interface IWebFileRepository
    {
        Task<bool> AddWebFile(Uri fileUri);
    }
}