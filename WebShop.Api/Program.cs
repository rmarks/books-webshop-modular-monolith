using FastEndpoints;
using FastEndpoints.Security;
using System.Reflection;
using WebShop.Books;
using WebShop.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
    .AddAuthenticationJwtBearer(o => o.SigningKey = builder.Configuration["Auth:JwtSecret"]!)
    .AddAuthorization();

List<Assembly> mediatRAssemblies = [];
builder.Services
    .AddBooksModuleServices(builder.Configuration, mediatRAssemblies)
    .AddUserServices(builder.Configuration);

builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()));

var app = builder.Build();

app.UseAuthentication()
    .UseAuthorization();

app.UseFastEndpoints();

app.Run();
