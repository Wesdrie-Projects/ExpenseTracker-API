using API.Features.Accounts;
using API.Infrastructure.Data;
using Tests.Fixtures;

namespace Tests.Features.Accounts;

public class TestAccountSeeder
{
    public Account Default { get; set; } = null!;
    public Account NoAssociatedData { get; set; } = null!;

    public TestAccountSeeder(TestUserSeeder testUserSeeder)
    {
        Default = new(testUserSeeder.Default,
            "Default");

        NoAssociatedData = new(testUserSeeder.Default,
            "No Associated Data");
    }

    public void Seed(ExpenseContext context)
    {
        context.Accounts.Add(Default);
        context.Accounts.Add(NoAssociatedData);
    }
}
