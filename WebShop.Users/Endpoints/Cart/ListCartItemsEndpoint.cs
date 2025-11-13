using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebShop.Users.Data;

namespace WebShop.Users.Endpoints.Cart;

public record ListCartItemsResponse(IEnumerable<CartItemDto> CartItems);

public record CartItemDto(int Id, int BookId, string Description, int Quantity, decimal UnitPrice);

internal class ListCartItemsEndpoint(UsersDbContext dbContext) : EndpointWithoutRequest<ListCartItemsResponse>
{
    private readonly UsersDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/cart");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(CancellationToken ct)
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

        var cartItems = user.CartItems
            .Select(c => new CartItemDto(c.Id, c.BookId, c.Description, c.Quantity, c.UnitPrice))
            .ToList();

        await Send.OkAsync(new ListCartItemsResponse(cartItems));
    }
}