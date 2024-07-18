using Microsoft.AspNetCore.Identity;

namespace Tests.Fixtures;

public class TestUserSeeder
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityUser Default { get; set; } = null!;

    public TestUserSeeder(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;

        Default = new()
        {
            UserName = "default@example.com",
            Email = "default@example.com",
            EmailConfirmed = true
        };
    }

    public async Task SeedAsync()
    {
        foreach (var user in new[] { Default })
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);

            if (existingUser != null)
                return;

            await _userManager.CreateAsync(user, "!Password1");

            // TODO: Address How Users Are Seeded Their Roles.
            if (user.UserName == Default.UserName)
                await _userManager.AddToRoleAsync(user, "Admin");
        }
    }
}
