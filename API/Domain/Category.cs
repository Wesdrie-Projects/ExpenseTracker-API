using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace API.Domain;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;

    public ICollection<Transaction> Transactions { get; private set; } = new HashSet<Transaction>();

    public Category(string name)
    {
        Name = name;
    }

    private Category() { }
}

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasMany(c => c.Transactions)
            .WithOne(t => t.Category)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
