using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MotoGrid.Common.Constants;
using MotoGrid.Data.Models;

namespace MotoGrid.Data.Seeding;

/// <summary>
/// Creates the first administrator. The e-mail and the password are read from
/// the configuration (User Secrets in development), never from the source code:
///   Seed:AdminEmail, Seed:AdminPassword
/// If they are not configured, no administrator is created.
/// </summary>
public class AdminUserSeeder : ISeeder
{
    public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var email = configuration["Seed:AdminEmail"];
        var password = configuration["Seed:AdminPassword"];

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return;
        }

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        if (await userManager.FindByEmailAsync(email) is not null)
        {
            return;
        }

        var admin = new ApplicationUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            DisplayName = "Администратор",
        };

        var result = await userManager.CreateAsync(admin, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException(
                string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
        }

        await userManager.AddToRoleAsync(admin, RoleConstants.Administrator);
    }
}
