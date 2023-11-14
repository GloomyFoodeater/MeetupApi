namespace ApplicationCore.Interfaces.Services;

/// <summary>
/// Interface to fill database with data
/// </summary>
public interface IDataSeeder
{
    /// <summary>
    /// Fill database with initial data
    /// </summary>
    /// <returns>Asynchronous seed operation</returns>
    Task SeedAsync();
}
