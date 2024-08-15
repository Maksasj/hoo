using Microsoft.AspNetCore.Mvc;

namespace HooService.Common
{
    public interface IFileProvider
    {
        Task<IActionResult> GetFile(string sourceId, string fileAccessString);
        Task<IActionResult> GetSource(string sourceId);
    }
}
