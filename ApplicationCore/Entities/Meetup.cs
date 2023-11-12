namespace ApplicationCore.Entities;

public class Meetup
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Agenda { get; set; } = string.Empty;

    public string Organizer { get; set; } = string.Empty;

    public string Speaker { get; set; } = string.Empty;

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Location { get; set; } = string.Empty;
}
