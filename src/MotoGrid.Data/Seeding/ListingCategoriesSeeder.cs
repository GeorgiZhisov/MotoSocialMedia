using Microsoft.EntityFrameworkCore;
using MotoGrid.Data.Models;

namespace MotoGrid.Data.Seeding;

public class ListingCategoriesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        if (await dbContext.ListingCategories.AnyAsync())
        {
            return;
        }

        var categories = new[]
        {
            new ListingCategory { Name = "Мотоциклети", IsVehicle = true },
            new ListingCategory { Name = "Части" },
            new ListingCategory { Name = "Екипировка" },
            new ListingCategory { Name = "Аксесоари" },
        };

        await dbContext.ListingCategories.AddRangeAsync(categories);
        await dbContext.SaveChangesAsync();
    }
}
