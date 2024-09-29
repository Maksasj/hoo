using Hoo.Service.Models;
using Hoo.Service.Repository.WebFiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hoo.Service.Services.WebFiles
{
    public class WebFileService : IWebFileService
    {
        private readonly ILogger<WebFileService> _logger;
        private readonly IWebFileRepository _webFileRepository;

        public WebFileService(ILogger<WebFileService> logger, IWebFileRepository webFileRepository)
        {
            _logger = logger;
            _webFileRepository = webFileRepository;
        }

        public async Task<IEnumerable<WebFileItem>> GetFilesAsync()
        {
            return await _webFileRepository.GetFilesAsync();
        }

        public async Task<bool> AddFileAsync(Uri fileUri)
        {
            return await _webFileRepository.AddFileAsync(new WebFileItem {
                Id = Guid.NewGuid(),
                AccessUri = fileUri,
                CreatedDate = DateTimeOffset.Now,
                LastModificationDate = DateTimeOffset.Now
            });
        }

        public async Task SyncRemote()
        {
            throw new NotImplementedException();
        }
    }
}
