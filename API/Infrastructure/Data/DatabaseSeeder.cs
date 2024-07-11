using Microsoft.AspNetCore.Identity;

namespace API.Infrastructure.Data;

public class DatabaseSeeder
{
    private readonly ExpenseContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DatabaseSeeder(ExpenseContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task SeedData()
    {
        if (_context.Transactions.Any())
            return;

        await UserSeeder.SeedAsync(_userManager);
        await AccountSeeder.SeedAsync(_context);
        await CategorySeeder.SeedAsync(_context);
    }
}
