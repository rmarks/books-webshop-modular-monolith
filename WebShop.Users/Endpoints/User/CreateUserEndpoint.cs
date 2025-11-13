using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using WebShop.Users.Domain;

namespace WebShop.Users.Endpoints.User;

public record CreateUserRequest(string Email, string Password, string? FullName);

internal class CreateUserEndpoint(UserManager<ApplicationUser> userManager) : Endpoint<CreateUserRequest>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override void Configure()
    {
        Post("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest req, CancellationToken ct)
    {
        var newUser = new ApplicationUser
        {
            Email = req.Email,
            UserName = req.Email,
            FullName = req.FullName
        };

        await _userManager.CreateAsync(newUser, req.Password);

        await Send.OkAsync();
    }
}
