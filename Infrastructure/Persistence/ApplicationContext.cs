using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Implementation of EF core unit of work
/// </summary>
internal class ApplicationContext : IdentityDbContext<User>
{
    /// <summary>
    /// Connect to database with given options
    /// </summary>
    /// <param name="options">Options to configure context</param>
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    #region EF Core repositories

    public DbSet<Meetup> Meetups => Set<Meetup>();

    #endregion
}
