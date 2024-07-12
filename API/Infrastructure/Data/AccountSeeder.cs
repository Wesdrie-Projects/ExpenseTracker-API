using API.Domain;

namespace API.Infrastructure.Data;

public class AccountSeeder
{
    public Account Debit { get; set; } = null!;
    public Account Credit { get; set; } = null!;

    public AccountSeeder(UserSeeder userSeeder)
    {
        Debit = new(userSeeder.John,
            "Debit");
        Credit = new(userSeeder.John,
            "Credit");
    }

    public void Seed(ExpenseContext context)
    {
        context.Accounts.Add(Debit);
        context.Accounts.Add(Credit);
    }
}
