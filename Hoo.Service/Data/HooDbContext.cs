using Hoo.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Hoo.Service.Data
{
    public class HooDbContext : DbContext
    {
        public DbSet<WebFileItem> WebFiles { get; set; }
        
        public DbSet<GoogleFileItem> GoogleDriveFiles { get; set; }

        public DbSet<OneDriveFileItem> OneDriveFiles { get; set; }

        public HooDbContext(DbContextOptions<HooDbContext> options) : base(options)
        {
        }
    }
}
