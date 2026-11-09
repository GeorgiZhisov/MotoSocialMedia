using Microsoft.EntityFrameworkCore;
using MotoGrid.Data;
using MotoGrid.Data.Seeding;

namespace MotoGrid.Web.Infrastructure;

public static class WebApplicationExtensions
{
    /// <summary>
    /// Applies the pending EF Core migrations and seeds the initial data
    /// (roles, listing categories, administrator).
    /// </summary>
    public static async Task MigrateAndSeedAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ApplicationDbContext>();
        var logger = services.GetRequiredService<ILogger<ApplicationDbContextSeeder>>();

        if (!dbContext.Database.GetMigrations().Any())
        {
            logger.LogWarning("No migrations found. Run 'Add-Migration InitialCreate' to create the database schema.");
            return;
        }

        await dbContext.Database.MigrateAsync();
        await new ApplicationDbContextSeeder().SeedAsync(dbContext, services);
    }
}
