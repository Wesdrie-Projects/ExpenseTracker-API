using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data;

public class UserSeeder
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityUser Admin { get; set; } = null!;
    public IdentityUser John { get; set; } = null!;

    public UserSeeder(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;

        Admin = new()
        {
            UserName = "admin@example.com",
            Email = "admin@example.com",
            EmailConfirmed = true
        };

        John = new()
        {
            UserName = "john@example.com",
            Email = "john@example.com",
            EmailConfirmed = true
        };
    }

    public async Task SeedAsync()
    {
        foreach (var user in new[] { Admin, John })
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);

            if (existingUser != null)
                return;

            await _userManager.CreateAsync(user, "!Password1");

            // TODO: Address How Users Are Seeded Their Roles.
            if (user.UserName == Admin.UserName)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            else if (user.UserName == John.UserName)
            {
                await _userManager.AddToRoleAsync(user, "Consumer");
            }
        }
    }
}