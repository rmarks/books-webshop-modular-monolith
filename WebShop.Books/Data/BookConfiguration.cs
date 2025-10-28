using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebShop.Books.Domain;

namespace WebShop.Books.Data;

internal class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Author)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(b => b.Price)
            .HasPrecision(9,2);

        builder.HasData(
            new Book(1, ".NET Core in Action", "Dustin Metzgar", 19m),
            new Book(2, "ASP.NET Core in Action", "Andrew Lock", 21m),
            new Book(3, "Blazor in Action", "Chris Sainty", 20m));
    }
}
