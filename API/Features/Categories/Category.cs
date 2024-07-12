using API.Features.Transactions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Features.Categories;

public class Category
{
    public Guid Id { get; private set; }
    public IdentityUser User { get; private set; } = null!;
    public string UserId { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public ICollection<Transaction> Transactions { get; private set; } = new HashSet<Transaction>();

    public Category(IdentityUser user, string name)
    {
        User = user;
        Name = name;
    }

    private Category() { }
}

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasIndex(c => new { c.UserId, c.Name })
            .IsUnique();

        builder.HasMany(c => c.Transactions)
            .WithOne(t => t.Category)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
