using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Identity;
using Mihcelle.Hwavmvid.Client;
using Mihcelle.Hwavmvid.Shared.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Mihcelle.Hwavmvid.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Mihcelle.Hwavmvid.ServerAPI"));

// mihcelle.hwavmvid
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, Applicationauthenticationstateprovider>();

// mihcelle.hwavmvid
builder.Services.AddScoped<Applicationservice, Applicationservice>();

await builder.Build().RunAsync();
