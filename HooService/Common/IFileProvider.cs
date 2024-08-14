using Microsoft.AspNetCore.Mvc;

namespace HooService.Common
{
    public interface IFileProvider
    {
        Task<IActionResult> GetFile(string sourceId, string fileId);
        Task<IActionResult> GetSource(string sourceId);
    }
}
