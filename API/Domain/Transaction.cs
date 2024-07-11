namespace API.Domain;

public class Transaction
{
    public Guid Id { get; private set; }
    public Account Account { get; private set; } = null!;
    public Guid AccountId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid CategoryId { get; private set; }
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }

    public Transaction(Account account, Category category, DateOnly date, decimal amount)
    {
        Id = Guid.NewGuid();
        Account = account;
        Category = category;
        Date = date;
        Amount = amount;
    }

    private Transaction() { }
}
