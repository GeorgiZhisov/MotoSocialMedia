using Microsoft.EntityFrameworkCore;
using MotoGrid.Data;
using MotoGrid.Data.Models;
using MotoGrid.Data.Models.Enums;

namespace MotoGrid.Tests.Data;

public class ApplicationDbContextTests
{
    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private static ApplicationUser CreateUser(string email = "rider@motogrid.bg") =>
        new() { UserName = email, Email = email, DisplayName = "Rider" };

    [Fact]
    public async Task SaveChanges_NewEntity_SetsCreatedOn()
    {
        await using var db = CreateContext();
        var user = CreateUser();
        db.Users.Add(user);
        var before = DateTime.UtcNow;

        var motorcycle = new Motorcycle
        {
            Make = "Honda",
            Model = "CB500F",
            Year = 2020,
            EngineCc = 471,
            Category = MotorcycleCategory.Naked,
            OwnerId = user.Id,
        };
        db.Motorcycles.Add(motorcycle);
        await db.SaveChangesAsync();

        Assert.True(motorcycle.CreatedOn >= before);
        Assert.Null(motorcycle.ModifiedOn);
    }

    [Fact]
    public async Task SaveChanges_ModifiedEntity_SetsModifiedOn()
    {
        await using var db = CreateContext();
        var user = CreateUser();
        db.Users.Add(user);
        var category = new ListingCategory { Name = "Части" };
        db.ListingCategories.Add(category);
        await db.SaveChangesAsync();

        category.Name = "Резервни части";
        await db.SaveChangesAsync();

        Assert.NotNull(category.ModifiedOn);
        Assert.True(category.ModifiedOn >= category.CreatedOn);
    }

    [Fact]
    public async Task SaveChanges_UserWithoutDisplayName_UsesPartOfEmail()
    {
        await using var db = CreateContext();
        var user = new ApplicationUser { UserName = "georgi@motogrid.bg", Email = "georgi@motogrid.bg" };
        db.Users.Add(user);

        await db.SaveChangesAsync();

        Assert.Equal("georgi", user.DisplayName);
    }

    [Fact]
    public async Task SaveChanges_UserWithDisplayName_KeepsIt()
    {
        await using var db = CreateContext();
        var user = CreateUser();

        db.Users.Add(user);
        await db.SaveChangesAsync();

        Assert.Equal("Rider", user.DisplayName);
    }

    [Fact]
    public void Model_UserForeignKeys_DoNotCascade()
    {
        using var db = CreateContext();

        var cascadingUserKeys = db.Model.GetEntityTypes()
            .Where(e => e.ClrType.Namespace == typeof(Post).Namespace)
            .SelectMany(e => e.GetForeignKeys())
            .Where(fk => fk.PrincipalEntityType.ClrType == typeof(ApplicationUser))
            .Where(fk => fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList();

        Assert.Empty(cascadingUserKeys);
    }
}
