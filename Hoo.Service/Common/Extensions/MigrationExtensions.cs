using Microsoft.EntityFrameworkCore;
using Hoo.Service.Data;

namespace Hoo.Service.Common.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using HooDbContext dbContext = scope.ServiceProvider.GetRequiredService<HooDbContext>();

        dbContext.Database.Migrate();
    }
}
