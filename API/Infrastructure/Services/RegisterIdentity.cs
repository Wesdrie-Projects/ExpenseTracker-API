using API.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Infrastructure.Services;

public static class RegisterIdentity
{
    public static void RegisterIdentityUserAndRole(this WebApplicationBuilder builder)
    {
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ExpenseContext>();
    }

    public static void RegisterJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:7285/",
                    ValidAudience = "local-dev",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Ecx8yjzg1I7BmkJnW3+fbKvZT6pFV+2Uc5RqM+1zBF0a"))
                };
            });
    }
}
