using Microsoft.Extensions.Configuration;

namespace ApplicationCore.Utils;

public static class ErrorUtils
{
    public static object ToErrorObject(Exception e) => new { Error = e.InnerException?.Message ?? e.Message };

    public static string GetOrThrow(this IConfiguration configuration, string key) =>
        configuration[key] ?? throw new NullReferenceException($"{key} was not set");

}
