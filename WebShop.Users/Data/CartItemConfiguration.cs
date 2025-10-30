using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebShop.Users.Domain;

namespace WebShop.Users.Data;

internal class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(205);
        
        builder.Property(c => c.UnitPrice)
            .HasPrecision(9, 2);
    }
}
