using API.Features.Categories;
using API.Infrastructure.Data;
using Tests.Fixtures;

namespace Tests.Features.Categories;

public class TestCategorySeeder
{
    public Category Default { get; set; } = null!;
    public Category NoAssociatedData { get; set; } = null!;

    public TestCategorySeeder(TestUserSeeder testUserSeeder)
    {
        Default = new(testUserSeeder.Default,
            "Default");

        NoAssociatedData = new(testUserSeeder.Default,
            "No Associated Data");
    }

    public void Seed(ExpenseContext context)
    {
        context.Categories.Add(Default);
        context.Categories.Add(NoAssociatedData);
    }
}

