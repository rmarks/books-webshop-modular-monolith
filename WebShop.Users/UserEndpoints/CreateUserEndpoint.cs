using FastEndpoints;
using Microsoft.AspNetCore.Identity;

namespace WebShop.Users.UserEndpoints;

public record CreateUserRequest(string Email, string Password);

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
            UserName = req.Email
        };

        await _userManager.CreateAsync(newUser, req.Password);

        await Send.OkAsync();
    }
}
