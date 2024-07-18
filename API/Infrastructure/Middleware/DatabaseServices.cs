using API.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Services;

public static class DatabaseServices
{
    public static void RegisterPostgres(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ExpenseContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
    }

    public static async Task SetUpDevelopmentDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ExpenseContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        context.Database.Migrate();

        var databaseSeeder = new DatabaseSeeder(context, userManager, roleManager);
        await databaseSeeder.SeedDataAsync();
    }
}
