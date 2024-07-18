using API.Features.Transactions;
using API.Infrastructure.Data;
using Tests.Features.Accounts;
using Tests.Features.Categories;

namespace Tests.Features.Transactions;

public class TestTransactionSeeder
{
    public Transaction Default { get; set; } = null!;
    public Transaction NoAssociatedData { get; set; } = null!;

    public TestTransactionSeeder(TestAccountSeeder testAccountSeeder, TestCategorySeeder testCategorySeeder)
    {
        Default = new(testAccountSeeder.Default,
            testCategorySeeder.Default,
            "Default",
            DateOnly.FromDateTime(DateTime.Now.AddDays(-25)),
            1000.00m);
        NoAssociatedData = new(testAccountSeeder.Default,
            testCategorySeeder.Default,
            "No Associated Data",
            DateOnly.FromDateTime(DateTime.Now.AddDays(-25)),
            1000.00m);
    }

    public void Seed(ExpenseContext context)
    {
        context.Transactions.Add(Default);
        context.Transactions.Add(NoAssociatedData);
    }
}
