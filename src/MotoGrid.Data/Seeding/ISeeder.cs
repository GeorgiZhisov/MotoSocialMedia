namespace MotoGrid.Data.Seeding;

/// <summary>
/// Puts initial data in the database. Each seeder must be safe to run many times.
/// </summary>
public interface ISeeder
{
    Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider);
}
