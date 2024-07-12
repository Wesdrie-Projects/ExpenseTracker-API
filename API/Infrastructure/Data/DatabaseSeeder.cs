using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data;

public class DatabaseSeeder
{
    private readonly ExpenseContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DatabaseSeeder(ExpenseContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedDataAsync()
    {
        var roleSeeder = new RoleSeeder(_roleManager);
        await roleSeeder.SeedAsync();

        var userSeeder = new UserSeeder(_userManager);
        await userSeeder.SeedAsync();

        var accountSeeder = new AccountSeeder(userSeeder);
        accountSeeder.Seed(_context);

        var categorySeeder = new CategorySeeder(userSeeder);
        categorySeeder.Seed(_context);

        var transactionSeeder = new TransactionSeeder(accountSeeder, categorySeeder);
        transactionSeeder.Seed(_context);

        await _context.SaveChangesAsync();
    }
}
