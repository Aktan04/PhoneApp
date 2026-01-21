using System.Net.Http.Json;
using PhoneApp.BlazorApp.Models;

namespace PhoneApp.BlazorApp.Infrastructure;

public static class ApiErrorParser
{
    public static async Task<ApiError?> ParseAsync(HttpResponseMessage response)
    {
        if (response.Content == null)
            return null;

        try
        {
            return await response.Content.ReadFromJsonAsync<ApiError>();
        }
        catch
        {
            return new ApiError
            {
                Message = $"HTTP {(int)response.StatusCode}"
            };
        }
    }
}