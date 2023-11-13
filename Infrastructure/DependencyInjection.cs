using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using ApplicationCore.Services;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces.Services;
using Infrastructure.Services;

namespace Infrastructure;

/// <summary>
/// Class to inject infrastructure services
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configure infrastructure services and add them into given collection
    /// </summary>
    /// <param name="services">Services to append infrastructure into</param>
    /// <param name="configuration">Configuration for infrastructure services</param>
    /// <returns>Same object as services</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IMeetupRepository, MeetupRepository>();
        services.AddDbContext<ApplicationContext>(options =>
        {
            var connectionStr = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionStr, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationContext>();
        services.AddTransient<IDataSeeder, DataSeeder>();

        return services;
    }
}
