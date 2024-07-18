using API.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    public readonly ExpenseContext Context;
    private readonly UserManager<IdentityUser> UserManager;
    private readonly RoleManager<IdentityRole> RoleManager;

    public DatabaseFixture()
    {
        var services = new ServiceCollection();

        services.AddLogging();

        services.AddDbContext<ExpenseContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ExpenseContext>()
            .AddDefaultTokenProviders();

        var serviceProvider = services.BuildServiceProvider();
        UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        Context = serviceProvider.GetRequiredService<ExpenseContext>();

        SeedDatabaseAsync().Wait();
    }

    private async Task SeedDatabaseAsync()
    {
        var seeder = new TestDatabaseSeeder(Context, UserManager, RoleManager);
        await seeder.SeedDataAsync();
    }

    public void Dispose()
    {
        Context.Database.EnsureDeleted();
        Context.Dispose();
    }
}
