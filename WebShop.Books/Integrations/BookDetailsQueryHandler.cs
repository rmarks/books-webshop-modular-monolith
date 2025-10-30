using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WebShop.Books.Contracts;
using WebShop.Books.Data;

namespace WebShop.Books.Integrations;

internal class BookDetailsQueryHandler(BooksDbContext dbContext) : IRequestHandler<BookDetailsQuery, Result<BookDetailsResponse>>
{
    private readonly BooksDbContext _dbContext = dbContext;

    public async Task<Result<BookDetailsResponse>> Handle(BookDetailsQuery request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == request.BookId);

        if (book is null) return Result.NotFound();

        var response = new BookDetailsResponse(book.Id, book.Title, book.Author, book.Price);

        return response;
    }
}
