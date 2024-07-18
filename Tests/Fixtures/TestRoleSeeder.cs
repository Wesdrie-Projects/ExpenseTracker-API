using Microsoft.AspNetCore.Identity;

namespace Tests.Fixtures;

public class TestRoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityRole Admin { get; set; } = null!;

    public TestRoleSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;

        Admin = new("Admin");
    }

    public async Task SeedAsync()
    {
        foreach (var role in new[] { Admin })
        {
            var existingRole = await _roleManager.FindByNameAsync(role.Name);

            if (existingRole != null)
                return;

            await _roleManager.CreateAsync(role);
        }
    }
}