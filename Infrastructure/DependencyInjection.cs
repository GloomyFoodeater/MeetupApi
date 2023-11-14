using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using static ApplicationCore.Utils.ErrorUtils;

namespace Infrastructure;

/// <summary>
/// Injector of internal infrastructure services
/// </summary>
public static class DependencyInjection
{

    /// <summary>
    /// Configure infrastructure services for application and add them into given collection
    /// </summary>
    /// <param name="services">Services to append infrastructure into</param>
    /// <param name="configuration">Configuration for infrastructure services</param>
    /// <returns>Same object as services</returns>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var authority = configuration.GetOrThrow("Authority");
        var audience = configuration.GetOrThrow("Audience");

        services.AddTransient<IMeetupRepository, MeetupRepository>();
        services.AddDbContext<ApplicationContext>(options =>
        {
            var connectionStr = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionStr, builder =>
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });
        services.AddTransient<IDataSeeder, MeetupSeeder>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.Authority = authority;
                 options.RequireHttpsMetadata = false;
                 options.Audience = audience;
             });

        return services;
    }

    /// <summary>
    /// Configure infrastructure services for identity server and add them into given collection
    /// </summary>
    /// <param name="services">Services to append infrastructure into</param>
    /// <param name="configuration">Configuration for infrastructure services</param>
    /// <returns>Same object as services</returns>
    /// <exception cref="NullReferenceException">Secret key was not set</exception>
    public static IServiceCollection AddIdentityInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
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
        services.AddTransient<IDataSeeder, IdentitySeeder>();

        return services;
    }
}
