using Hoo.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Hoo.Service.Data
{
    public class WebFileDbContext : DbContext
    {
        public DbSet<WebFileItem> Files { get; set; }

        public WebFileDbContext(DbContextOptions<WebFileDbContext> options) : base(options)
        {
        }
    }
}
