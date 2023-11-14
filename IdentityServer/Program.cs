using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using static Infrastructure.DependencyInjection;
using static IdentityServer.Config;
using ApplicationCore.Utils;

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration.GetOrThrow("SecretKey");

builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryIdentityResources(GetIdentityResources())
    .AddInMemoryApiScopes(GetApiScopes())
    .AddInMemoryApiResources(GetApiResources())
    .AddInMemoryClients(GetClients(secretKey))
    .AddAspNetIdentity<User>();

var app = builder.Build();

app.UseIdentityServer();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
    await seeder.SeedAsync();
}

app.Run();
