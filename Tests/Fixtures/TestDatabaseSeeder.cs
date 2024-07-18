using API.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Tests.Features.Accounts;
using Tests.Features.Categories;
using Tests.Features.Transactions;

namespace Tests.Fixtures;

internal class TestDatabaseSeeder
{
    private readonly ExpenseContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TestDatabaseSeeder(ExpenseContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task SeedDataAsync()
    {
        var testRoleSeeder = new TestRoleSeeder(_roleManager);
        await testRoleSeeder.SeedAsync();

        var testUserSeeder = new TestUserSeeder(_userManager);
        await testUserSeeder.SeedAsync();

        var testAccountSeeder = new TestAccountSeeder(testUserSeeder);
        testAccountSeeder.Seed(_context);

        var testCategorySeeder = new TestCategorySeeder(testUserSeeder);
        testCategorySeeder.Seed(_context);

        var testTransactionSeeder = new TestTransactionSeeder(testAccountSeeder, testCategorySeeder);
        testTransactionSeeder.Seed(_context);

        await _context.SaveChangesAsync();
    }
}
