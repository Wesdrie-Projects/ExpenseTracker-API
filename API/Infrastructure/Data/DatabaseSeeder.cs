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

    public async Task SeedDataAsync()
    {
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
