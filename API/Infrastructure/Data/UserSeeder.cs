using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data;

public static class UserSeeder
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager)
    {
        var email = "user@example.com";

        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var password = "password";

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new Exception("Unable to seed user.");
    }
}