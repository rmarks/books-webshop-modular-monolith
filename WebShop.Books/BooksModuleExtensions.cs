using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebShop.Books.Data;

namespace WebShop.Books;

public static class BooksModuleExtensions
{
    public static IServiceCollection AddBooksModuleServices(this IServiceCollection services,
                                                            IConfiguration config,
                                                            List<Assembly> mediatRAssemblies)
    {
        var connectionString = config.GetConnectionString("BooksConnectionString");
        services.AddDbContext<BooksDbContext>(options => options.UseSqlServer(connectionString));

        mediatRAssemblies.Add(typeof(BooksModuleExtensions).Assembly);

        return services;
    }
}
