using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Interfaces;

/// <summary>
/// UOW interface for database access
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Meetups table
    /// </summary>
    DbSet<Meetup> Meetups { get; }

    /// <summary>
    /// Commit transaction in database
    /// </summary>
    /// <returns>Asynchronous save operation resulting in number of affected rows</returns>
    Task<int> SaveChangesAsync();
}
