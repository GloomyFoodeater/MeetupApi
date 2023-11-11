namespace ApplicationCore.Entities;

public class Meetup
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public int Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public string? Agenda { get; set; }

    public string Organizer { get; set; }

    public string Speaker { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public string Location { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
