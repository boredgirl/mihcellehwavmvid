using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;
using Mihcelle.Hwavmvid.Client.Container;

namespace Mihcelle.Hwavmvid.Client.Pages
{
    public class Indexbase : Mainlayoutbase, IDisposable
    {


        [Parameter] 
        public string? _contextpageurlpath { get; set; }

        private const string frontpage = "mihcellehwavmvid_techonologies";

        protected override async Task OnInitializedAsync()
        {
            this.navigationmanager.LocationChanged += async (obj, ev) => await this.Getcontextpage();
            await base.OnInitializedAsync();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(this._contextpageurlpath))
            {
                this._contextpageurlpath = frontpage;
                await this.Getcontextpage();
            }                

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(1400).ContinueWith((task) =>
                {
                    this.StateHasChanged();
                });
            }           

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task Getcontextpage()
        {
            try
            {
                if (!string.IsNullOrEmpty(this._contextpageurlpath))
                {
                    var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                    this.applicationprovider._contextpage = await client.GetFromJsonAsync<Applicationpage>(string.Concat("api/page/", this._contextpageurlpath));
                    this.StateHasChanged();
                    this.applicationprovider._contextpagechanged();
                }
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }
        }

        public void Dispose()
        {
            this.navigationmanager.LocationChanged -= async (obj, ev) => await this.Getcontextpage();
        }

    }
}
