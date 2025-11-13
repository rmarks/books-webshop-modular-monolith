using Ardalis.GuardClauses;

namespace WebShop.Books.Domain;

internal class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    public Book(string title, string author, decimal price, int id = 0)
    {
        Title = Guard.Against.NullOrEmpty(title);
        Author = Guard.Against.NullOrEmpty(author);
        Price = Guard.Against.Negative(price);

        if (id != 0)
        {
            Id = Guard.Against.Negative(id);
        }
    }

    public void UpdatePrice(decimal newPrice)
    {
        Price = Guard.Against.Negative(newPrice);
    }
}
