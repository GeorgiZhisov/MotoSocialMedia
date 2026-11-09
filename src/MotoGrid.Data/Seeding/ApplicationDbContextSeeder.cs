namespace MotoGrid.Data.Seeding;

/// <summary>
/// Runs all seeders in the right order (roles must exist before the administrator).
/// </summary>
public class ApplicationDbContextSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var seeders = new List<ISeeder>
        {
            new RolesSeeder(),
            new ListingCategoriesSeeder(),
            new AdminUserSeeder(),
        };

        foreach (var seeder in seeders)
        {
            await seeder.SeedAsync(dbContext, serviceProvider);
        }
    }
}
