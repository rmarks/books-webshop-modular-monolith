using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebShop.Users.Data;
using WebShop.Users.Domain;

namespace WebShop.Users.CartEndpoints;

public record AddCartItemRequest(int BookId, int Quantity);

internal class AddCartItemEndpoint(UsersDbContext dbContext) : Endpoint<AddCartItemRequest>
{
    private readonly UsersDbContext _dbContext = dbContext;

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

        var newCartItem = new CartItem(req.BookId, "description", req.Quantity, 0m);
        user.AddItemToCart(newCartItem);
        await _dbContext.SaveChangesAsync();

        await Send.OkAsync();
    }
}
