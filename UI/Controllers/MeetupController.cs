using ApplicationCore.DTO.Meetup;
using ApplicationCore.Entities;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers;

/// <summary>
/// Controller for CRUD operations on meetups
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class MeetupController : ControllerBase
{
    private readonly MeetupService _service;

    public MeetupController(MeetupService service) => _service = service;

    // EF Core uses inner exception for messages from database
    private static object ToErrorObject(Exception e) => new { Error = e.InnerException?.Message ?? e.Message };

    /// <summary>
    /// Endpoint for 'GET /api/<see cref="MeetupController"/>'
    /// </summary>
    /// <returns>
    /// 200 for success,
    /// 500 for unexpected errors
    /// </returns>
    [HttpGet]
    public async Task<ActionResult<List<Meetup>>> GetAsync()
    {
        try
        {
            var meetups = await _service.GetAllAsync();
            return Ok(new { meetups });
        }
        catch (Exception e)
        {
            return BadRequest(ToErrorObject(e));
        }
    }

    /// <summary>
    /// Endpoint for 'GET /api/<see cref="MeetupController"/>/{id}'
    /// </summary>
    /// <param name="id">Id of meetup to get</param>
    /// <returns>
    /// 200 with found object for success,
    /// 404 for non-existent meetup,
    /// 500 for unexpected errors
    /// </returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Meetup>> GetAsync(int id)
    {
        try
        {
            var meetup = await _service.GetAsync(id);
            if (meetup == null)
                return NotFound(new { Error = $"Meetup with id='{id}' did not exist" });
            return Ok(meetup);
        }
        catch (Exception e)
        {
            return BadRequest(ToErrorObject(e));
        }
    }

    /// <summary>
    /// Endpoint for 'POST /api/<see cref="MeetupController"/>'
    /// </summary>
    /// <param name="data">All fields to create new entity</param>
    /// <returns>
    /// 201 with created object and location header for success, 
    /// 500 for unexpected errors
    /// </returns>
    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] MeetupAddDto data)
    {
        try
        {
            var added = await _service.AddAsync(data);
            var uri = $"{Request.Path}/{added.Id}";
            return Created(uri, added);
        }
        catch (Exception e)
        {
            return BadRequest(ToErrorObject(e));
        }
    }

    /// <summary>
    /// Endpoint for 'PUT /api/<see cref="MeetupController"/>/{id}'
    /// </summary>
    /// <param name="id">Id of meetup to update</param>
    /// <param name="data">Data to modify</param>
    /// <returns>
    /// 200 with modified object for success,
    /// 404 for non-existent meetup,
    /// 500 for unexpected errors
    /// </returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> PutAsync(int id, [FromBody] MeetupUpdateDto data)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, data);
            return Ok(updated);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(ToErrorObject(e));
        }
        catch (Exception e)
        {
            return BadRequest(ToErrorObject(e));
        }
    }

    /// <summary>
    /// Endpoint for 'DELETE /api/<see cref="MeetupController"/>/{id}'
    /// </summary>
    /// <param name="id">Id of meetup to delete</param>
    /// <returns>
    /// 204 for success,
    /// 500 for unexpected errors
    /// </returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        try
        {
            await _service.RemoveAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(ToErrorObject(e));
        }
    }
}
