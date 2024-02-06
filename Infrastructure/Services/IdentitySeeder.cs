using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

/// <summary>
/// Seeder for identity entities
/// </summary>
internal class IdentitySeeder : IDataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentitySeeder(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    private async Task SeedRole(string name)
    {
        if (await _roleManager.FindByNameAsync(name) != null) return;
        var role = new IdentityRole { Name = name };
        await _roleManager.CreateAsync(role);
    }

    private async Task SeedUserAsync(string userName, string password, string[] roles)
    {
        if (await _userManager.FindByNameAsync(userName) != null) return;
        var user = new User { UserName = userName };
        var result = await _userManager.CreateAsync(user, password);

        // Does not append roles into existing users
        if (result.Succeeded)
            foreach (var role in roles)
                await _userManager.AddToRoleAsync(user, role);
    }

    /// <summary>
    /// Fill database with identity information (users, roles)
    /// </summary>
    /// <returns><inheritdoc/></returns>
    public async Task SeedAsync()
    {
        await SeedRole("user");
        await SeedRole("admin");

        await SeedUserAsync("user1", "Groot23_+", new[] { "user" });

        await SeedUserAsync("admin1", "_Testpassword123", new[] { "user", "admin" });
        await SeedUserAsync("admin2", "tE-stpass1234", new[] { "user", "admin" });
        await SeedUserAsync("admin3", "Root22_", new[] { "user", "admin" });
    }
}
