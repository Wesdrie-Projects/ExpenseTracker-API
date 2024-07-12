using API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Services;

public static class RegisterDatabase
{
    public static void RegisterPostgres(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ExpenseContext>(options =>
            options.UseNpgsql(
                builder.Configuration.GetConnectionString("DefaultConnection")));
    }
}
