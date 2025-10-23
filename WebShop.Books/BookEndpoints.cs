using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace WebShop.Books;

public static class BookEndpoints
{
    public static void MapBookEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/books", () => new[]
        {
                new BookDto(".NET Core in Action", "Dustin Metzgar"),
                new BookDto("ASP.NET Core in Action", "Andrew Lock"),
                new BookDto("Blazor in Action", "Chris Sainty")

        });
    }
}

internal record BookDto(string Title, string Author);