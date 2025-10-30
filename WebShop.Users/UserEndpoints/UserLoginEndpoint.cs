using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using WebShop.Users.Domain;

namespace WebShop.Users.UserEndpoints;

public record UserLoginRequest(string Email, string Password);

internal class UserLoginEndpoint(UserManager<ApplicationUser> userManager) : Endpoint<UserLoginRequest>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    public override void Configure()
    {
        Post("/users/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UserLoginRequest req, CancellationToken ct)
    {
        var user = await _userManager.FindByEmailAsync(req.Email);

        if (user is null)
        {
            await Send.UnauthorizedAsync();
            return;
        }

        var loginSuccessful = await _userManager.CheckPasswordAsync(user, req.Password);

        if (!loginSuccessful)
        {
            await Send.UnauthorizedAsync();
            return;
        }

        var jwtSecret = Config["Auth:JwtSecret"]!;

        var token = JwtBearer.CreateToken(options =>
        {
            options.SigningKey = jwtSecret;
            options.User["EmailAddress"] = user.Email!;
        });

        await Send.OkAsync(token);
    }
}