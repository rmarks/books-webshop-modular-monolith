using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using WebShop.Users.Data;

namespace WebShop.Users.Endpoints.User;

internal record ListUsersResponse(IEnumerable<UserDto> Users);
internal record UserDto(string? UserName, string? Email, string? FullName);

internal class ListUsersEndpoint(UsersDbContext dbContext) : EndpointWithoutRequest<ListUsersResponse>
{
    private readonly UsersDbContext _dbContext = dbContext;

    public override void Configure()
    {
        Get("/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await _dbContext.ApplicationUsers
            .AsNoTracking()
            .Select(au => new UserDto(au.UserName, au.Email, au.FullName))
            .ToListAsync();

        await Send.OkAsync(new ListUsersResponse(users));
    }
}