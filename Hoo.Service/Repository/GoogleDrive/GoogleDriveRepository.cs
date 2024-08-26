using Hoo.Service.Data;
using Hoo.Service.Models;
using Hoo.Service.Repository.WebFiles;

namespace Hoo.Service.Repository.GoogleDrive
{
    public class GoogleDriveRepository : IGoogleDriveRepository
    {
        private readonly ILogger<GoogleDriveRepository> _logger;
        private readonly HooDbContext _context;

        public GoogleDriveRepository(ILogger<GoogleDriveRepository> logger, HooDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> AddFileAsync(GoogleDriveFileItem item)
        {
            _context.GoogleDriveFiles.Add(item);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }

        public bool HasFile(string GoogleId)
        {
            return _context.GoogleDriveFiles.Where(file => file.GoogleId.Equals(GoogleId)).Any();
        }

        public async Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync()
        {
            return _context.GoogleDriveFiles.ToArray();
        }

        public async Task<bool> DeleteAllFilesAsync()
        {
            _context.GoogleDriveFiles.RemoveRange(_context.GoogleDriveFiles);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }
    }
}
