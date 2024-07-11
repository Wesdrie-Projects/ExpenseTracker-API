using API.Domain;

namespace API.Infrastructure.Data;

public static class AccountSeeder
{
    public static async Task SeedAsync(ExpenseContext context)
    {
            var users = context.Users.ToList();

            var accounts = new List<Account>
            {
                new(users[0], "Debit Account"),
                new(users[0], "Credit Account")
            };

            await context.Accounts.AddRangeAsync(accounts);
            await context.SaveChangesAsync();
    }
}
