using ApplicationCore.DTO.Meetup;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using AutoMapper;

namespace ApplicationCore.Services;

/// <summary>
/// Service for CRUD operations on meetups in the database.
/// Wraps <see cref="IMeetupRepository"/> methods with data transfer and
/// exception throwing.
/// </summary>
public class MeetupService
{
    private readonly IMeetupRepository _repo;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Create service via repository and mapper
    /// </summary>
    /// <param name="repo">Repository to obtain data</param>
    /// <param name="mapper">Mapper to map meetup dtos with meetups</param>
    public MeetupService(IMeetupRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    private async Task<Meetup> GetOrThrow(int id)
    {
        return await _repo.GetAsync(id)
            ?? throw new KeyNotFoundException($"Meetup with id='{id}' did not exist");
    }

    /// <summary>
    /// Get list of all meetups
    /// </summary>
    /// <returns>Asynchronous get operation resulting in list of meetups</returns>
    public async Task<List<Meetup>> GetAllAsync() => await _repo.GetAllAsync();

    /// <summary>
    /// Get meetup by given id
    /// </summary>
    /// <param name="id">Id of meetup to get</param>
    /// <returns>Asynchronous get operation resulting in found meetup or null</returns>
    public async Task<Meetup?> GetAsync(int id) => await _repo.GetAsync(id);

    /// <summary>
    /// Add new meetup
    /// </summary>
    /// <param name="data">Data to add meetup</param>
    /// <returns>Asynchronous add operation resulting in added meetup</returns>
    public async Task<Meetup> AddAsync(MeetupAddDto data)
    {
        var entity = _mapper.Map<MeetupAddDto, Meetup>(data);
        _repo.Add(entity);
        await _repo.Commit();
        return entity;
    }

    /// <summary>
    /// Update existing meetup
    /// </summary>
    /// <param name="id">Id of meetup to update</param>
    /// <param name="data">Data to update meetup</param>
    /// <returns>Asynchronous update operation resulting in updated meetup</returns>
    /// <exception cref="KeyNotFoundException">Thrown if there was no entity with given id</exception>
    public async Task<Meetup> UpdateAsync(int id, MeetupUpdateDto data)
    {
        var entity = await GetOrThrow(id);
        _mapper.Map(source: data, destination: entity);
        _repo.Update(entity);
        await _repo.Commit();
        return entity;
    }

    /// <summary>
    /// Remove existing meetup
    /// </summary>
    /// <param name="id">Id of meetup to remove</param>
    /// <returns>Asynchronous remove operation</returns>
    /// <exception cref="KeyNotFoundException">Thrown if there was no entity with given id</exception>
    public async Task RemoveAsync(int id)
    {
        var entity = await GetOrThrow(id);
        _repo.Remove(entity);
        await _repo.Commit();
    }
}
