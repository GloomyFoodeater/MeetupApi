namespace ApplicationCore.DTO.Meetup;

/// <summary>
/// Data transfer object for updating database queries.
/// Null values do not affect update
/// </summary>
public record MeetupUpdateDto(
    string? Name,
    string? Description,
    string? Agenda,
    string? Organizer,
    string? Speaker,
    DateTime? StartDateTime,
    DateTime? EndDateTime,
    string? Location);
