using Ardalis.Result;
using FastEndpoints;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebShop.Books.Contracts;
using WebShop.Users.Data;
using WebShop.Users.Domain;

namespace WebShop.Users.Endpoints.Cart;

public record AddCartItemRequest(int BookId, int Quantity);

internal class AddCartItemEndpoint : Endpoint<AddCartItemRequest>
{
    private readonly UsersDbContext _dbContext;
    private readonly IMediator _mediator;

    public AddCartItemEndpoint(UsersDbContext dbContext,
                               IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(AddCartItemRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var user = await _dbContext.ApplicationUsers
            .Include(a => a.CartItems)
            .SingleOrDefaultAsync(a => a.Email == emailAddress);

        if (user is null)
        {
            await Send.UnauthorizedAsync();
            return;
        }

        // getting book details from Books Module
        var query = new BookDetailsQuery(req.BookId);
        var result = await _mediator.Send(query);

        if (result.Status == ResultStatus.NotFound)
        {
            await Send.NotFoundAsync();
            return;
        }

        var bookDetails = result.Value;

        string description = $"{bookDetails.Title} by {bookDetails.Author}";

        var newCartItem = new CartItem(req.BookId, description, req.Quantity, bookDetails.Price);
        user.AddItemToCart(newCartItem);
        await _dbContext.SaveChangesAsync();

        await Send.OkAsync();
    }
}
