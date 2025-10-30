namespace WebShop.Books.Contracts;

public record BookDetailsResponse(int BookId, string Title, string Author, decimal Price);
