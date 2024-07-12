using API.Features.Categories;

namespace API.Infrastructure.Data;

public class CategorySeeder
{
    public Category Income { get; set; } = null!;
    public Category Interest { get; set; } = null!;
    public Category Debt { get; set; } = null!;
    public Category Bills { get; set; } = null!;
    public Category Entertainment { get; set; } = null!;
    public Category Groceries { get; set; } = null!;
    public Category Transport { get; set; } = null!;
    public Category Unexpected { get; set; } = null!;

    public CategorySeeder(UserSeeder userSeeder)
    {
        Income = new(userSeeder.John,
            "Income");
        Interest = new(userSeeder.John, 
            "Interest");
        Debt = new(userSeeder.John,
            "Debt");
        Bills = new(userSeeder.John,
            "Bills");
        Entertainment = new(userSeeder.John,
            "Entertainment");
        Groceries = new(userSeeder.John,
            "Groceries");
        Transport = new(userSeeder.John,
            "Transport");
        Unexpected = new(userSeeder.John,
            "Unexpected");
    }

    public void Seed(ExpenseContext context)
    {
        context.Categories.Add(Income);
        context.Categories.Add(Interest);
        context.Categories.Add(Debt);
        context.Categories.Add(Bills);
        context.Categories.Add(Entertainment);
        context.Categories.Add(Groceries);
        context.Categories.Add(Transport);
        context.Categories.Add(Unexpected);
    }
}
