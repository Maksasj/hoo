using Hoo.Service.Data;
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
    }
}
