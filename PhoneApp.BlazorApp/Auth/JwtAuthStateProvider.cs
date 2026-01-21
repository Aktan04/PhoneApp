using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace PhoneApp.BlazorApp.Auth;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;

    public JwtAuthStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");

        if (string.IsNullOrWhiteSpace(token))
        {
            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var identity = new ClaimsIdentity(
            ParseClaimsFromJwt(token),
            authenticationType: "jwt");

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task NotifyUserAuthentication(string token)
    {
        await _localStorage.SetItemAsync("accessToken", token);

        var identity = new ClaimsIdentity(
            ParseClaimsFromJwt(token),
            "jwt");

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(
                    new ClaimsPrincipal(identity))));
    }

    public async Task NotifyUserLogout()
    {
        await _localStorage.RemoveItemAsync("accessToken");

        NotifyAuthenticationStateChanged(
            Task.FromResult(
                new AuthenticationState(
                    new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);

        var keyValuePairs = System.Text.Json.JsonSerializer
            .Deserialize<Dictionary<string, object>>(jsonBytes)!;

        var claims = new List<Claim>();

        foreach (var kvp in keyValuePairs)
        {
            if (kvp.Value is System.Text.Json.JsonElement element &&
                element.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    claims.Add(new Claim(
                        ClaimTypes.Role,
                        item.ToString()!
                    ));
                }
            }
            else
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
            }
        }

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = base64.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}