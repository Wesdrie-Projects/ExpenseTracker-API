using API.Domain;

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

    public CategorySeeder()
    {
        Income = new("Income");
        Interest = new("Interest");
        Debt = new("Debt");
        Bills = new("Bills");
        Entertainment = new("Entertainment");
        Groceries = new("Groceries");
        Transport = new("Transport");
        Unexpected = new("Unexpected");
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
