using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

/// <summary>
/// Implementation of EF core unit of work
/// </summary>
public class ApplicationContext : DbContext
{
    /// <summary>
    /// Connect to database with given options and ensure its creation
    /// </summary>
    /// <param name="options">Options to configure db context</param>
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) => Database.EnsureCreated();

    #region EF Core repositories

    public DbSet<Meetup> Meetups => Set<Meetup>();

    #endregion
}
