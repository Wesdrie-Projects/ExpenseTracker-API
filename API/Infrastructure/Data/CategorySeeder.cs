using API.Domain;

namespace API.Infrastructure.Data;

public class CategorySeeder
{
    public static async Task SeedAsync(ExpenseContext context)
    {
        var categories = new List<Category>
            {
                new("Income"),
                new("House Hold"),
                new("Groceries"),
                new("Savings"),
                new("Transpot"),
            };

        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();
    }
}
