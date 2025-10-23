using FastEndpoints;

namespace WebShop.Books.BookEndpoints;

public class ListBooksResponse
{
    public IEnumerable<BookDto> Books { get; set; } = default!;
}

public record BookDto(string Title, string Author);

internal class ListBooksEndpoint : EndpointWithoutRequest<ListBooksResponse>
{
    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(new ListBooksResponse
        {
            Books = [
                new BookDto(".NET Core in Action", "Dustin Metzgar"),
                new BookDto("ASP.NET Core in Action", "Andrew Lock"),
                new BookDto("Blazor in Action", "Chris Sainty")
            ]
        });
    }
}
