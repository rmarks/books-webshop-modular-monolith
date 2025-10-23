using WebShop.Books;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

app.UseHttpsRedirection();

app.MapBookEndpoints();

app.Run();
