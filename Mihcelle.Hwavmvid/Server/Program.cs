using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Mihcelle.Hwavmvid.Client;
using Mihcelle.Hwavmvid.Server;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// mihcelle.hwavmvid
var configbuilder = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.ContentRootPath}.json", true, true);
var config = configbuilder.Build();

// mihcelle.hwavmvid
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var installed = !string.IsNullOrEmpty(connectionString);

if (installed == false)
{
    if (string.IsNullOrEmpty(connectionString))
    {
        var configpath = string.Concat(builder.Environment.ContentRootPath, "\\wwwroot\\", "framework.json");
        var jsonconfig = System.IO.File.ReadAllText(configpath);
        var deserializedconfig = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonconfig);
        if (deserializedconfig != null)
        {
            deserializedconfig["installation"] = new { createdon = string.Empty };
            var updatedconfigfile = JsonSerializer.Serialize(deserializedconfig, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(configpath, updatedconfigfile);
        }
    }
}

builder.Services.AddDbContext<Applicationdbcontext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<Applicationuser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 2;
})
    .AddEntityFrameworkStores<Applicationdbcontext>();

if (installed == true)
{
    var identitycookiename = string.Concat("mihcelle_hwavmvid_identity_cookie_", builder.Configuration.GetSection("Installation").GetValue<string>("Createdon"));
    var authenticationbuilder = builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    });
    authenticationbuilder.AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.Unspecified;
        options.Cookie.Name = installed ? identitycookiename : "unauthenticated";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/";
    });
}

builder.Services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = false;
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                options.JsonSerializerOptions.AllowTrailingCommas = true;
                options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
                options.JsonSerializerOptions.DefaultBufferSize = 4096;
                options.JsonSerializerOptions.MaxDepth = 41;
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddCors(option =>
{
    option.AddPolicy("mihcellehwavmvidcorspolicy", (builder) =>
    {
        builder.SetIsOriginAllowedToAllowWildcardSubdomains()
               .SetIsOriginAllowed(isOriginAllowed => true)
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// mihcelle.hwavmvid
builder.Services.AddSignalR()
    .AddHubOptions<Applicationhub>(options =>
    {
        options.EnableDetailedErrors = true;
        options.KeepAliveInterval = TimeSpan.FromSeconds(15);
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        options.MaximumReceiveMessageSize = Int64.MaxValue;
        options.StreamBufferCapacity = Int32.MaxValue;
    })
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.WriteIndented = false;
        options.PayloadSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never;
        options.PayloadSerializerOptions.AllowTrailingCommas = true;
        options.PayloadSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals;
        options.PayloadSerializerOptions.DefaultBufferSize = 4096;
        options.PayloadSerializerOptions.MaxDepth = 41;
        options.PayloadSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("mihcellehwavmvidcorspolicy");

app.UseAuthentication();
app.UseAuthorization();

// mihcelle.hwavmvid
app.MapHub<Applicationhub>("/api/applicationhub", options =>
    {
        options.Transports = HttpTransportType.WebSockets | HttpTransportType.LongPolling;
        options.ApplicationMaxBufferSize = long.MaxValue;
        options.TransportMaxBufferSize = long.MaxValue;
        options.WebSockets.CloseTimeout = TimeSpan.FromSeconds(10);
        options.LongPolling.PollTimeout = TimeSpan.FromSeconds(10);
    });

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
