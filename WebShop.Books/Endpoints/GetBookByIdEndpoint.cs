using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebShop.Books.Data;

namespace WebShop.Books.Endpoints;

public record GetBookByIdRequest(int Id);

internal class GetBookByIdEndpoint(BooksDbContext dbContext) : Endpoint<GetBookByIdRequest, BookDto>
{
    private readonly BooksDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
    {
        var bookDto = await _dbContext.Books
            .AsNoTracking()
            .Where(b => b.Id == req.Id)
            .Select(b => new BookDto(b.Id, b.Title, b.Author, b.Price))
            .FirstOrDefaultAsync();

        if (bookDto is null)
        {
            await Send.NotFoundAsync();
            return;
        }

        await Send.OkAsync(bookDto);
    }
}
