using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;
using Mihcelle.Hwavmvid.Client.Container;

namespace Mihcelle.Hwavmvid.Client.Pages
{
    public class Indexbase : Mainlayoutbase
    {

        [Parameter] 
        public string? _contextpageurlpath { get; set; }

        private const string frontpage = "mihcellehwavmvid_techonologies";

        protected override async Task OnParametersSetAsync()
        {
            this._contextpageurlpath = _contextpageurlpath ?? frontpage;

            if (this.applicationprovider._contextpage != null && this.applicationprovider._contextpage.Urlpath != this._contextpageurlpath)
                await this.Getcontextpage(this._contextpageurlpath);

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

        private async Task Getcontextpage(string _contextpageurlpath)
        {
            try
            {
                await InvokeAsync(async () =>
                {
                    var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                    this.applicationprovider._contextpage = await client.GetFromJsonAsync<Applicationpage>(string.Concat("api/page/", _contextpageurlpath));
                    this.applicationprovider._contextpagechanged();
                    this.StateHasChanged();
                });  
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }
        }

    }
}
