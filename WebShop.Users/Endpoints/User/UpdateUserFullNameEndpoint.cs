using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebShop.Users.Data;

namespace WebShop.Users.Endpoints.User;

internal record UpdateUserFullNameRequest(string FullName);

internal class UpdateUserFullNameEndpoint(UsersDbContext dbContext) : Endpoint<UpdateUserFullNameRequest>
{
    private readonly UsersDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Post("/users/FullName");
        Claims("EmailAddress");
    }

    public override async Task HandleAsync(UpdateUserFullNameRequest req, CancellationToken ct)
    {
        var emailAddress = User.FindFirstValue("EmailAddress");

        var user = await _dbContext.ApplicationUsers
            .SingleOrDefaultAsync(au => au.Email == emailAddress);

        if (user is null)
        {
            await Send.UnauthorizedAsync();
            return;
        }

        user.UpdateFullName(req.FullName);
        await _dbContext.SaveChangesAsync();

        await Send.OkAsync();
    }
}