using Ardalis.GuardClauses;

namespace WebShop.Users.Domain;

public class CartItem
{
    public int Id { get; private set; }
    public int BookId { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public CartItem(int bookId, string description, int quantity, decimal unitPrice)
    {
        BookId = Guard.Against.NegativeOrZero(bookId);
        Description = Guard.Against.NullOrEmpty(description);
        Quantity = Guard.Against.Negative(quantity);
        UnitPrice = Guard.Against.Negative(unitPrice);
    }

    internal void UpdateQuantity(int quantity)
    {
        Quantity = Guard.Against.Negative(quantity);
    }

    internal void UpdateDescription(string description)
    {
        Description = Guard.Against.NullOrEmpty(description);
    }

    internal void UpdateUnitPrice(decimal unitPrice)
    {
        UnitPrice = Guard.Against.Negative(unitPrice);
    }
}
