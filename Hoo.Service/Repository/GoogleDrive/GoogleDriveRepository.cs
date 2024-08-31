using Hoo.Service.Data;
using Hoo.Service.Models;
using Hoo.Service.Repository.WebFiles;

namespace Hoo.Service.Repository.GoogleDrive
{
    public class GoogleDriveRepository : IGoogleDriveRepository
    {
        private readonly HooDbContext _context;

        public GoogleDriveRepository(HooDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFileAsync(GoogleDriveFileItem item)
        {
            _context.GoogleDriveFiles.Add(item);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }

        public async Task<GoogleDriveFileItem> GetFileAsync(Guid fileId)
        {
            var query = _context.GoogleDriveFiles.Where(file => file.Id.Equals(fileId));

            if (!query.Any())
                return null;

            return query.First();
        }

        public async Task<bool> DeleteFileAsync(Guid fileId)
        {
            var file = await GetFileAsync(fileId);

            if (file == null)
                return false;

            _context.GoogleDriveFiles.RemoveRange(file);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }

        public bool HasFile(Guid fileId)
        {
            var query = _context.GoogleDriveFiles.Where(file => file.Id.Equals(fileId));

            return query.Any();
        }

        public bool HasGoogleFile(string GoogleId)
        {
            var query = _context.GoogleDriveFiles.Where(file => file.GoogleId.Equals(GoogleId));

            return query.Any();
        }

        public async Task<IEnumerable<GoogleDriveFileItem>> GetFilesAsync()
        {
            return _context.GoogleDriveFiles;
        }

        public async Task<bool> DeleteFilesAsync(IEnumerable<GoogleDriveFileItem> files)
        {
            _context.GoogleDriveFiles.RemoveRange(files);

            var saveResult = await _context.SaveChangesAsync();

            return !(saveResult == 1);
        }
    }
}
