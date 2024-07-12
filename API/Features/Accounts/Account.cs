using API.Features.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Accounts;

public class Account
{
    public Guid Id { get; private set; }
    public IdentityUser User { get; private set; } = null!;
    public string UserId { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public ICollection<Transaction> Transactions { get; private set; } = new HashSet<Transaction>();

    public Account(IdentityUser user, string name)
    {
        Id = Guid.NewGuid();
        User = user;
        Name = name;
    }
    private Account() { }
}

public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(a => new { a.UserId, a.Name })
            .IsUnique();

        builder.HasMany(a => a.Transactions)
            .WithOne(t => t.Account)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
