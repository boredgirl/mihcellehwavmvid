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

builder.Services.AddHttpClient("Mihcelle.Hwavmvid.ServerAPI.Unauthenticated",
    client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "base"));

builder.Services.AddTransient<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient("Mihcelle.Hwavmvid.ServerAPI", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
}).AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();


// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Mihcelle.Hwavmvid.ServerAPI"));



await builder.Build().RunAsync();
