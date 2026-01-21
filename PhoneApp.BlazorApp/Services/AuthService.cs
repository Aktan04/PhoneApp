using System.Net.Http.Json;
using Blazored.LocalStorage;
using PhoneApp.BlazorApp.Auth;
using PhoneApp.BlazorApp.Infrastructure;
using PhoneApp.BlazorApp.Models;

namespace PhoneApp.BlazorApp.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly JwtAuthStateProvider _authStateProvider;

    public AuthService(
        HttpClient http,
        JwtAuthStateProvider authStateProvider)
    {
        _http = http;
        _authStateProvider = authStateProvider;
    }

    public async Task LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync(
            "api/auth/login",
            new LoginRequest
            {
                Email = email,
                Password = password
            });

        if (!response.IsSuccessStatusCode)
        {
            var error = await ApiErrorParser.ParseAsync(response);
            throw new ApplicationException(error?.Message ?? "Ошибка авторизации");
        }

        var result = await response.Content
            .ReadFromJsonAsync<TokenResponseDto>();

        if (string.IsNullOrWhiteSpace(result?.AccessToken))
            throw new ApplicationException("Токен не получен");

        await _authStateProvider.NotifyUserAuthentication(result.AccessToken);
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        var response = await _http.PostAsJsonAsync(
            "api/auth/register",
            request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await ApiErrorParser.ParseAsync(response);
            throw new ApplicationException(error?.Message ?? "Ошибка регистрации");
        }

        var result = await response.Content
            .ReadFromJsonAsync<TokenResponseDto>();

        if (string.IsNullOrWhiteSpace(result?.AccessToken))
            throw new ApplicationException("Токен не получен");

        await _authStateProvider.NotifyUserAuthentication(result.AccessToken);
    }

    public async Task LogoutAsync()
    {
        await _authStateProvider.NotifyUserLogout();
    }
}