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

namespace Mihcelle.Hwavmvid.Client
{
    public class MainLayoutBase : LayoutComponentBase, IDisposable
    {

        [Inject] public Applicationprovider applicationprovider { get; set; }
        [Inject] public NavigationManager navigationmanager { get; set; }
        [Inject] public AuthenticationStateProvider authenticationstateprovider { get; set; }
        [Inject] public IConfiguration Configuration { get; set; }

        public AuthenticationState? _context { get; set; }
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
                this.StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task Updatemainlayout()
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
