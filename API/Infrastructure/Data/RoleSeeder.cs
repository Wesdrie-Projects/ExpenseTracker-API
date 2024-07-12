using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data;

public class RoleSeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityRole Admin { get; set; } = null!;
    public IdentityRole Consumer { get; set; } = null!;

    public RoleSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;

        Admin = new("Admin");
        Consumer = new("Consumer");
    }

    public async Task SeedAsync()
    {
        foreach (var role in new[] { Admin, Consumer })
        {
            var existingRole = await _roleManager.FindByNameAsync(role.Name);

            if (existingRole != null)
                return;

            await _roleManager.CreateAsync(role);
        }
    }
}