using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebShop.Books.Data;

namespace WebShop.Books.BookEndpoints;

public class ListBooksResponse
{
    public IEnumerable<BookDto> Books { get; set; } = default!;
}

internal class ListBooksEndpoint(BooksDbContext dbContext) : EndpointWithoutRequest<ListBooksResponse>
{
    private readonly BooksDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var books = await _dbContext.Books
            .AsNoTracking()
            .Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price))
            .ToListAsync();

        await Send.OkAsync(new ListBooksResponse
        {
            Books = books
        });
    }
}
