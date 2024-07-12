using API.Infrastructure.Data;
using API.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterPostgres();
builder.RegisterIdentityUserAndRole();
// Still Need To Implement JWT Sign-In & Auth
builder.RegisterJwtAuthentication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ExpenseContext>();
        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

        context.Database.Migrate();
        context.Database.EnsureCreated();

        var databaseSeeder = new DatabaseSeeder(context, userManager);
        await databaseSeeder.SeedDataAsync();
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
