using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebShop.Books.Data;

namespace WebShop.Books.BookEndpoints;

public record UpdateBookPriceRequest(int Id, decimal NewPrice);

internal class UpdateBookPriceEndpoint(BooksDbContext dbContext) : Endpoint<UpdateBookPriceRequest>
{
    private readonly BooksDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/books/{Id}/price");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateBookPriceRequest req, CancellationToken ct)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == req.Id);

        if (book is null)
        {
            await Send.NotFoundAsync();
            return;
        }

        book.UpdatePrice(req.NewPrice);

        await _dbContext.SaveChangesAsync();

        var updatedBookDto = new BookDto(book.Id, book.Title, book.Author, book.Price);

        await Send.OkAsync(updatedBookDto);
    }
}
