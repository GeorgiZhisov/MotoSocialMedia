using Microsoft.EntityFrameworkCore;
using MotoGrid.Data;
using MotoGrid.Data.Seeding;
using Moq;

namespace MotoGrid.Tests.Data;

public class SeedingTests
{
    private static ApplicationDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options);

    [Fact]
    public async Task ListingCategoriesSeeder_EmptyDatabase_AddsCategories()
    {
        await using var db = CreateContext();
        var serviceProvider = new Mock<IServiceProvider>();

        await new ListingCategoriesSeeder().SeedAsync(db, serviceProvider.Object);

        Assert.Equal(4, await db.ListingCategories.CountAsync());
        Assert.Single(await db.ListingCategories.Where(c => c.IsVehicle).ToListAsync());
    }

    [Fact]
    public async Task ListingCategoriesSeeder_RunTwice_DoesNotDuplicate()
    {
        await using var db = CreateContext();
        var serviceProvider = new Mock<IServiceProvider>();
        var seeder = new ListingCategoriesSeeder();

        await seeder.SeedAsync(db, serviceProvider.Object);
        await seeder.SeedAsync(db, serviceProvider.Object);

        Assert.Equal(4, await db.ListingCategories.CountAsync());
    }

    [Fact]
    public async Task ApplicationDbContextSeeder_NullContext_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(
            () => new ApplicationDbContextSeeder().SeedAsync(null!, new Mock<IServiceProvider>().Object));
    }
}
