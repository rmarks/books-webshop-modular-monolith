using FastEndpoints;
using FastEndpoints.Security;
using WebShop.Books;
using WebShop.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(o => o.SigningKey = builder.Configuration["Auth:JwtSecret"]!)
    .AddAuthorization();

builder.Services
    .AddBookServices(builder.Configuration)
    .AddUserServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints();

app.Run();
