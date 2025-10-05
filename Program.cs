using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorGitHubTest;
using BlazorGitHubTest.Services;
using Supabase;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Wczytaj konfigurację Supabase
var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var config = await http.GetFromJsonAsync<Dictionary<string, string>>("appsettings.json");

var supabaseUrl = config!["SupabaseUrl"];
var supabaseKey = config!["SupabaseKey"];

// Zarejestruj Supabase Client
builder.Services.AddScoped<Client>(_ => 
    new Client(supabaseUrl, supabaseKey, new SupabaseOptions
    {
        AutoRefreshToken = true,
        AutoConnectRealtime = true
    })
);

// Zarejestruj SupabaseService
builder.Services.AddScoped<SupabaseService>();

await builder.Build().RunAsync();
