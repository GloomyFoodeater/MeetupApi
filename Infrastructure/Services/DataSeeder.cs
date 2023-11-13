using ApplicationCore.DTO.Meetup;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

/// <summary>
/// Implementation to re-create and fill database
/// with the help of injected services
/// </summary>
internal class DataSeeder : IDataSeeder
{
    private readonly ApplicationContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly MeetupService _meetupService;

    public DataSeeder(ApplicationContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        MeetupService meetupService)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _meetupService = meetupService;
    }

    private async Task SeedRole(string name)
    {
        if (await _roleManager.FindByNameAsync(name) != null) return;
        var role = new IdentityRole { Name = name };
        await _roleManager.CreateAsync(role);
    }

    private async Task SeedUserAsync(string userName, string password, string role)
    {
        if (await _userManager.FindByNameAsync(userName) != null) return;
        var user = new User { UserName = userName };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded) await _userManager.AddToRoleAsync(user, role);
    }

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
    /// Drop and create database and fill with users and other entities
    /// </summary>
    /// <returns>Asynchronous seed operation</returns>
    public async Task SeedAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        await SeedRole("admin");

        await SeedUserAsync("admin1", "_Testpassword123", "admin");
        await SeedUserAsync("admin2", "tE-stpass1234", "admin");
        await SeedUserAsync("admin3", "Root22_", "admin");

        await SeedMeetUp("First", "John Doe", "Hosh Snow");
        await SeedMeetUp("PARTY!", "Bob Dog", "Child Fork");
        await SeedMeetUp("Another one~~", "Sob Sob", "Hop Hop");
    }
}