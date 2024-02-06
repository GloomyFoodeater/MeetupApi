using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// Implementation of <see cref="IMeetupRepository"/> by wrapping 
/// <see cref="ApplicationContext.Meetups"/> methods
/// </summary>
internal class MeetupRepository : IMeetupRepository
{
    private readonly ApplicationContext _context;

    public MeetupRepository(ApplicationContext context) => _context = context;

    public async Task<List<Meetup>> GetAllAsync() => await _context.Meetups.ToListAsync();

    public async Task<Meetup?> GetAsync(int id) => await _context.Meetups.FindAsync(id);

    public void Add(Meetup meetup) => _context.Meetups.Add(meetup);

    public void Update(Meetup meetup) => _context.Meetups.Update(meetup);

    public void Remove(Meetup meetup) => _context.Meetups.Remove(meetup);

    public async Task Commit() => await _context.SaveChangesAsync();
}
