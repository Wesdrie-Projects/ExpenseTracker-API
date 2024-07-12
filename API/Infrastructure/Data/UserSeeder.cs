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
        try
        {
            var createUser = await _userManager.CreateAsync(John, "!Password1");
        }
        catch (Exception ex)
        {
            throw new Exception($"Cannot Create User: {ex.Message}");
        }
    }
}