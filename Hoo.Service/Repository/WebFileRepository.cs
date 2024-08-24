using Hoo.Service.Data;
using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository
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

        public async Task<bool> AddWebFile(Uri fileUri)
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
