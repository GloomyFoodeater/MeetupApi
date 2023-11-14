using ApplicationCore.DTO.Meetup;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;

namespace Infrastructure.Services;

/// <summary>
/// Seeder for meetup entities
/// </summary>
internal class MeetupSeeder : IDataSeeder
{
    private readonly MeetupService _meetupService;

    public MeetupSeeder(MeetupService meetupService) => _meetupService = meetupService;

    private async Task SeedMeetUp(string name, string organizer, string speaker)
    {
        var description = name + " desc";
        var agenda = "1.Sleep\n2.Do nothing\n3.Yawn";

        var random = new Random();
        var StartDateTime = DateTime.Now;
        var EndDateTime = DateTime.Now + new TimeSpan(random.Next(24), random.Next(60), random.Next(60));
        var location = "Place " + Guid.NewGuid();

        var data = new MeetupAddDto(name, description, agenda, organizer, speaker, StartDateTime, EndDateTime, location);
        await _meetupService.AddAsync(data);
    }

    /// <summary>
    /// Fill database with sample meetups
    /// </summary>
    /// <returns><inheritdoc/></returns>
    public async Task SeedAsync()
    {
        if (_meetupService.GetAllAsync().Result.Any()) return;

        await SeedMeetUp("First", "John Doe", "Hosh Snow");
        await SeedMeetUp("PARTY!", "Bob Dog", "Child Fork");
        await SeedMeetUp("Another one~~", "Sob Sob", "Hop Hop");
    }
}