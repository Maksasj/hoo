using Hoo.Service.Data;
using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public class WebFileRepository : IWebFileRepository
    {
        private readonly ILogger<WebFileRepository> _logger;
        private readonly HooDbContext _context;

        public WebFileRepository(ILogger<WebFileRepository> logger, HooDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<WebFileItem> GetFileAsync(Guid fileId)
        {
            var query = _context.WebFiles.Where(file => file.Id.Equals(fileId));

            if (!query.Any())
                return null;

            return query.First();
        }

        public async Task<IEnumerable<WebFileItem>> GetFilesAsync()
        {
            return _context.WebFiles.ToArray();
        }

        public async Task<bool> AddFileAsync(WebFileItem item)
        {
            _context.WebFiles.Add(item);
            
            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }
    }
}
