namespace WebShop.Books.Domain;

internal class Book
{
    public int Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Author { get; private set; } = string.Empty;
    public decimal UnitPrice { get; private set; }

    public Book(int id, string title, string author, decimal unitPrice)
    {
        Id = id;
        Title = title;
        Author = author;
        UnitPrice = unitPrice;
    }
}
