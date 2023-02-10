using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Mihcelle.Hwavmvid.Client;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// mihcelle.hwavmvid
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

// mihcelle.hwavmvid
builder.Services.AddScoped<AuthenticationStateProvider, Applicationauthenticationstateprovider>();
builder.Services.AddScoped<Applicationprovider, Applicationprovider>();

// mihcelle.hwavmvid
builder.Services.AddHttpClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api"));

await builder.Build().RunAsync();
