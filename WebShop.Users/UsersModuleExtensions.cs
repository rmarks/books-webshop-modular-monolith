using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Users.Data;
using WebShop.Users.Domain;

namespace WebShop.Users;

public static class UsersModuleExtensions
{
    public static IServiceCollection AddUserServices(this IServiceCollection services,
                                                     IConfiguration config)
    {
        var connectionString = config.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();

        return services;
    }
}
