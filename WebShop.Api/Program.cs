using FastEndpoints;
using WebShop.Books;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();

builder.Services.AddBookServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseFastEndpoints();

app.Run();
