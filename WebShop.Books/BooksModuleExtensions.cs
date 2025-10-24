using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Books.Data;

namespace WebShop.Books;

public static class BooksModuleExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services,
                                                     IConfiguration config)
    {
        var connectionString = config.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BooksDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }
}
