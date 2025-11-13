using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebShop.Books.Data;

namespace WebShop.Books.Endpoints;

public record DeleteBookRequest(int Id);

internal class DeleteBookEndpoint(BooksDbContext dbContext) : Endpoint<DeleteBookRequest>
{
    private readonly BooksDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Delete("/books/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteBookRequest req, CancellationToken ct)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == req.Id);

        if (book is null)
        {
            await Send.NotFoundAsync();
            return;
        }

        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();

        await Send.NoContentAsync();
    }
}