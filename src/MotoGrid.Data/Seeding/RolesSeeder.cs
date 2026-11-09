using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MotoGrid.Common.Constants;

namespace MotoGrid.Data.Seeding;

public class RolesSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = [RoleConstants.Administrator, RoleConstants.Moderator, RoleConstants.User];

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
