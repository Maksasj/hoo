using Hoo.Service.Data;
using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public class WebFileRepository : IWebFileRepository
    {
        private readonly ILogger<WebFileRepository> _logger;
        private readonly WebFileDbContext _context;

        public WebFileRepository(ILogger<WebFileRepository> logger, WebFileDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<WebFileItem>> GetFilesAsync()
        {
            return _context.Files.ToArray();
        }

        public async Task<bool> AddWebFileAsync(Uri fileUri)
        {
            _context.Files.Add(new WebFileItem
            {
                Id = Guid.NewGuid(),
                AccessUri = fileUri,
                CreatedDate = DateTimeOffset.Now,
                LastModificationDate = DateTimeOffset.Now
            });

            var saveResult = await _context.SaveChangesAsync();
            return !(saveResult == 1);
        }
    }
}
