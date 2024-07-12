using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data;

public class UserSeeder
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityUser John { get; set; } = null!;

    public UserSeeder(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;

        John = new()
        {
            UserName = "john@example.com",
            Email = "john@example.com",
            EmailConfirmed = true
        };
    }

    public async Task SeedAsync()
    {
        var createUser = await _userManager.CreateAsync(John, "!Password1");

        if (createUser.Succeeded)
            throw new Exception("Cannot create user.");
    }
}