namespace ApplicationCore.DTO.Meetup;

/// <summary>
/// Data transfer object for adding database queries
/// </summary>
public record MeetupAddDto(
    string Name,
    string Description,
    string Agenda,
    string Organizer,
    string Speaker,
    DateTime StartDateTime,
    DateTime EndDateTime,
    string Location);
