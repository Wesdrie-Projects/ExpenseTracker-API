using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Data;

public static class RegisterDatabaseService
{
    public static void AddPostgresDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ExpenseContext>(options =>
            options.UseNpgsql(connectionString));
    }
}
