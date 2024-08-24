using Microsoft.EntityFrameworkCore;

namespace Hoo.Service.Data
{
    public class HooDbContext : DbContext
    {
        public HooDbContext(DbContextOptions<HooDbContext> options) : base(options)
        {

        }
    }
}
