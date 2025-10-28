using FastEndpoints;
using WebShop.Books.Data;
using WebShop.Books.Domain;

namespace WebShop.Books.BookEndpoints;

public record CreateBookRequest(string Title, string Author, decimal Price);

internal class CreateBookEndpoint(BooksDbContext dbContext) : Endpoint<CreateBookRequest>
{
    private readonly BooksDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBookRequest req, CancellationToken ct)
    {
        var newBook = new Book(0, req.Title, req.Author, req.Price);

        await _dbContext.Books.AddAsync(newBook);
        await _dbContext.SaveChangesAsync();

        var newBookDto = new BookDto(newBook.Id, newBook.Title, newBook.Author, newBook.Price);

        await Send.CreatedAtAsync<GetBookByIdEndpoint>(new { newBookDto.Id }, newBookDto);
    }
}
