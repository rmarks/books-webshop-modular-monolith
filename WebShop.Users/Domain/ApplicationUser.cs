using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;

namespace WebShop.Users.Domain;

public class ApplicationUser : IdentityUser
{
    private readonly List<CartItem> _cartItems = new();

    public string? FullName { get; set; }
    public IReadOnlyCollection<CartItem> CartItems => _cartItems.AsReadOnly();

    public void AddItemToCart(CartItem item)
    {
        Guard.Against.Null(item);

        var existingItem = _cartItems.SingleOrDefault(c => c.BookId == item.BookId);
        if (existingItem is not null)
        {
            existingItem.UpdateQuantity(existingItem.Quantity + item.Quantity);
            return;
        }

        _cartItems.Add(item);
    }
}
