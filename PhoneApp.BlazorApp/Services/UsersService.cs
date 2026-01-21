using System.Net;
using System.Net.Http.Json;
using PhoneApp.BlazorApp.Infrastructure;
using PhoneApp.BlazorApp.Models;

namespace PhoneApp.BlazorApp.Services;

public class UsersService
{
    private readonly HttpClient _http;

    public UsersService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<UserDto>> GetAllAsync()
    {
        return await _http.GetFromJsonAsync<List<UserDto>>("api/users")
               ?? new List<UserDto>();
    }
    
    public async Task<UserDto> GetMeAsync()
    {
        var response = await _http.GetAsync("api/users/me");

        if (response.StatusCode == HttpStatusCode.Unauthorized)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserDto>();
    }

    public async Task CreateAsync(UserCreateRequest request)
    {
        var response = await _http.PostAsJsonAsync("api/users", request);
        if (!response.IsSuccessStatusCode)
        {
            var error = await ApiErrorParser.ParseAsync(response);
            throw new ApplicationException(error?.Message ?? "Ошибка создания пользователя");
        }
    }

    public async Task UpdateAsync(int id, UpdateUserRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/users/{id}", request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await ApiErrorParser.ParseAsync(response);
            throw new ApplicationException(error?.Message ?? "Ошибка обновления пользователя");
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/users/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var error = await ApiErrorParser.ParseAsync(response);
            throw new ApplicationException(error?.Message ?? "Ошибка удаления пользователя");
        }
    }
}