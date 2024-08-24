using Microsoft.AspNetCore.Mvc;

namespace HooService.Common
{
    public interface IFileProvider
    {
        Task<IActionResult> AddWebFile(Uri fileUri);

        Task<IActionResult> GetFile(string sourceId, string filePath);
        Task<IActionResult> GetSource(string sourceId);
    }
}
