using Hoo.Service.Data;
using Hoo.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hoo.Service.Repository.WebFiles
{
    public class WebFileRepository : IWebFileRepository
    {
        private readonly HooDbContext _context;

        public WebFileRepository(HooDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFileAsync(WebFileItem item)
        {
            _context.WebFiles.Add(item);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }

        public async Task<WebFileItem> GetFileAsync(Guid fileId)
        {
            var query = _context.WebFiles.Where(file => file.Id.Equals(fileId));

            if (!query.Any())
                return null;

            return query.First();
        }

        public async Task<bool> DeleteFileAsync(Guid fileId)
        {
            var file = await GetFileAsync(fileId);

            if(fileId == null)
                return false;

            _context.WebFiles.RemoveRange(file);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }

        public bool HasFile(Guid fileId)
        {
            var query = _context.WebFiles.Where(file => file.Id.Equals(fileId));

            return query.Any();
        }

        public async Task<IEnumerable<WebFileItem>> GetFilesAsync()
        {
            return _context.WebFiles;
        }

        public async Task<bool> DeleteFilesAsync(IEnumerable<WebFileItem> files)
        {
            _context.WebFiles.RemoveRange(files);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }
    }
}
