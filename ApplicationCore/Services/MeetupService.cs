using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApplicationCore.Services;

/// <summary>
/// Service for CRUD operations on meetups in the database
/// </summary>
public class MeetupService
{
    private readonly IApplicationDbContext _context;

    public MeetupService(IApplicationDbContext context) => _context = context;

    /// <summary>
    /// Get list of all meetups
    /// </summary>
    /// <returns>Asynchronous get operation resulting in list of meetups</returns>
    public async Task<List<Meetup>> GetAllAsync() => await _context.Meetups.ToListAsync();

    /// <summary>
    /// Get meetup by given id
    /// </summary>
    /// <param name="id">Id of meetup to get</param>
    /// <returns>Asynchronous get operation resulting in found meetup or null</returns>
    public async Task<Meetup?> GetByIdAsync(int id) => await _context.Meetups.FindAsync(id);

    // TODO: Pass dto
    /// <summary>
    /// Add new meetup
    /// </summary>
    /// <param name="data">Data to create meetup</param>
    /// <returns>Asynchronous add operation resulting in id of added meetup</returns>
    public async Task<int> AddAsync(Meetup data)
    {
        _context.Meetups.Add(data);
        await _context.SaveChangesAsync();
        return data.Id;
    }

    // TODO: Pass dto
    /// <summary>
    /// Update existing meetup
    /// </summary>
    /// <returns>Asynchronous update operation</returns>
    /// <param name="data">Data to update meetup</param>
    public async Task UpdateAsync(Meetup data)
    {
        _context.Meetups.Update(data);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove existing meetup
    /// </summary>
    /// <param name="id">Id of meetup to remove</param>
    /// <returns>Asynchronous remove operation</returns>
    /// <exception cref="KeyNotFoundException">Thrown if there was no entity with given id</exception>
    public async Task RemoveAsync(int id)
    {
        var meetup = await _context.Meetups.FindAsync(id)
            ?? throw new KeyNotFoundException($"Meetup with id='{id}' was not found");
        _context.Meetups.Remove(meetup);
    }
}
