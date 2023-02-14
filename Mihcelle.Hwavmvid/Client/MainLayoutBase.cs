using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Http;
using Mihcelle.Hwavmvid.Client.Authentication;
using Microsoft.Extensions.Configuration;
using Mihcelle.Hwavmvid.Client.Installation;
using System.Text.Json;

namespace Mihcelle.Hwavmvid.Client
{
    public class Mainlayoutbase : LayoutComponentBase, IDisposable
    {

        [Inject] public Applicationprovider applicationprovider { get; set; }
        [Inject] public NavigationManager navigationmanager { get; set; }
        [Inject] public AuthenticationStateProvider authenticationstateprovider { get; set; }
        [Inject] public IConfiguration Configuration { get; set; }
        [Inject] public IHttpClientFactory ihttpclientfactory { get; set; }

        public AuthenticationState? _context { get; set; } = null;
        public bool framework_installed { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this._context = await this.authenticationstateprovider.GetAuthenticationStateAsync();
                await this.applicationprovider.Establishapplicationconnection();

                this.StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public JsonSerializerOptions jsonserializeroptions { get; set; } = new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
            AllowTrailingCommas = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
            DefaultBufferSize = 4096,
            MaxDepth = 41,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = null,
        };

        protected async Task Updatemainlayout()
        {
            await InvokeAsync(() =>
            {
                this.StateHasChanged();
            });
        }

        public void Dispose()
        {

        }

    }
}
