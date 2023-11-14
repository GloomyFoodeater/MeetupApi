using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces.Repositories;

/// <summary>
/// Repository class for meetup table
/// </summary>
public interface IMeetupRepository
{
    /// <summary>
    /// Get list of all meetups
    /// </summary>
    /// <returns>Asynchronous get operation resulting in list of meetups</returns>
    Task<List<Meetup>> GetAllAsync();

    /// <summary>
    /// Get meetup by given id
    /// </summary>
    /// <param name="id">Id of meetup to get</param>
    /// <returns>Asynchronous get operation resulting in found meetup or null</returns>
    Task<Meetup?> GetAsync(int id);

    /// <summary>
    /// Mark meetup as added
    /// </summary>
    /// <param name="meetup">Meetup to add</param>
    void Add(Meetup meetup);

    /// <summary>
    /// Mark meetup as updated
    /// </summary>
    /// <param name="meetup">Meetup to update</param>
    void Update(Meetup meetup);

    /// <summary>
    /// Mark meetup as removed
    /// </summary>
    /// <param name="meetup">Meetup to remove</param>
    void Remove(Meetup meetup);

    /// <summary>
    /// Commit changes in repository
    /// </summary>
    /// <returns>Asynchronous commit operation</returns>
    Task Commit();
}
