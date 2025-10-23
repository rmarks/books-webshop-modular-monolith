using Microsoft.Extensions.DependencyInjection;

namespace WebShop.Books;

public static class BooksModuleExtensions
{
    public static IServiceCollection AddBookServices(this IServiceCollection services)
    {
        return services;
    }
}
