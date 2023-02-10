using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Http;
using Mihcelle.Hwavmvid.Client.Authentication;

namespace Mihcelle.Hwavmvid.Client
{
    public class MainLayoutBase : LayoutComponentBase
    {

        [Inject] public Applicationprovider applicationprovider { get; set; }
        [Inject] public NavigationManager navigationmanager { get; set; }
        [Inject] public AuthenticationStateProvider authenticationstateprovider { get; set; }

        public AuthenticationState? _context { get; set; }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this._context = await this.authenticationstateprovider.GetAuthenticationStateAsync();
                this.StateHasChanged();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

    }
}
