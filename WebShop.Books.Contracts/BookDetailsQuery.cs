using Ardalis.Result;
using MediatR;

namespace WebShop.Books.Contracts;

public record BookDetailsQuery(int BookId) : IRequest<Result<BookDetailsResponse>>;