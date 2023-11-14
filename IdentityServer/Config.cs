using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer;

/// <summary>
/// Static class to get identity resources, clients, api scropes and resources
/// </summary>
public static class Config
{
    private const string roleScope = "roles";
    private const string audience = "meetupApi";

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource> { new IdentityResources.OpenId() };

    public static List<Client> GetClients(string key) =>
        new()
        {
           new ()
           {
                ClientId = "MeetupApi",
                ClientSecrets = new [] { new Secret(key.Sha512()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, roleScope }
            }
        };

    public static List<ApiScope> GetApiScopes() =>
       new() { new(roleScope) { UserClaims = { "role" } } };

    public static List<ApiResource> GetApiResources() =>
        new() { new(audience) { Scopes = { roleScope } } };
}
