using API.Features.Accounts;
using API.Features.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Transactions;

public class Transaction
{
    public Guid Id { get; private set; }
    public Account Account { get; private set; } = null!;
    public Guid AccountId { get; private set; }
    public Category Category { get; private set; } = null!;
    public Guid CategoryId { get; private set; }
    public string Description { get; private set; } = null!;
    public DateOnly Date { get; private set; }
    public decimal Amount { get; private set; }

    public Transaction(Account account, Category category, string description, DateOnly date, decimal amount)
    {
        Id = Guid.NewGuid();
        Account = account;
        Category = category;
        Description = description;
        Date = date;
        Amount = amount;
    }

    private Transaction() { }
}

public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasIndex(t => new { t.AccountId, t.CategoryId, t.Description, t.Date, t.Amount })
            .IsUnique();
    }
}
