using Hoo.Service.Data;
using Hoo.Service.Models;
using Hoo.Service.Repository.GoogleDrive;

namespace Hoo.Service.Repository.OneDrive
{
    public class OneDriveRepository : IOneDriveRepository
    {
        private readonly ILogger<OneDriveRepository> _logger;
        private readonly HooDbContext _context;

        public OneDriveRepository(ILogger<OneDriveRepository> logger, HooDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<bool> AddFileAsync(OneDriveFileItem item)
        {
            _context.OneDriveFiles.Add(item);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }

        public async Task<IEnumerable<OneDriveFileItem>> GetFilesAsync()
        {
            return _context.OneDriveFiles.ToArray();
        }

        public async Task<bool> DeleteAllFilesAsync()
        {
            _context.OneDriveFiles.RemoveRange(_context.OneDriveFiles);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }
    }
}
