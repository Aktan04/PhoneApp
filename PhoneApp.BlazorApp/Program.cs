using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PhoneApp.BlazorApp;
using PhoneApp.BlazorApp.Auth;
using PhoneApp.BlazorApp.Infrastructure;
using PhoneApp.BlazorApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// LocalStorage
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddScoped<JwtAuthStateProvider>();
// Auth + JWT
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<PhonesService>();
builder.Services.AddScoped<JwtAuthorizationMessageHandler>();

// HttpClient через IHttpClientFactory
builder.Services.AddHttpClient("ApiClient", client =>
    {
        client.BaseAddress = new Uri("https://localhost:7061/");
    })
    .AddHttpMessageHandler<JwtAuthorizationMessageHandler>();

// Делаем HttpClient доступным напрямую
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>()
        .CreateClient("ApiClient"));

await builder.Build().RunAsync();