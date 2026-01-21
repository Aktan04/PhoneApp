using System.Net.Http.Json;
using PhoneApp.BlazorApp.Infrastructure;
using PhoneApp.BlazorApp.Models;

namespace PhoneApp.BlazorApp.Services;

public class PhonesService
{
    private readonly HttpClient _http;

    public PhonesService(HttpClient http)
    {
        _http = http;
    }

    public Task<List<PhoneDto>> GetMyPhonesAsync()
        => _http.GetFromJsonAsync<List<PhoneDto>>("api/phones");

    public Task CreateMyPhoneAsync(CreatePhoneRequest dto)
        => _http.PostAsJsonAsync("api/phones", dto);

    public async Task UpdateAsync(int phoneId, UpdatePhoneRequest request)
    {
        var response = await _http.PutAsJsonAsync($"api/phones/{phoneId}", request);
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException("Ошибка обновления телефона");
    }

    public async Task DeleteAsync(int phoneId)
    {
        var response = await _http.DeleteAsync($"api/phones/{phoneId}");
        if (!response.IsSuccessStatusCode)
            throw new ApplicationException("Ошибка удаления телефона");
    }
}