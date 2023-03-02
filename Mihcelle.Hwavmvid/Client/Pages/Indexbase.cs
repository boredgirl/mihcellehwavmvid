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

        protected override async Task OnParametersSetAsync()
        {

            this._contextpageurlpath = _contextpageurlpath ?? "mihcellehwavmvid_techonologies";

            try
            {
                var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                this.applicationprovider._contextpage = await client.GetFromJsonAsync<Applicationpage>(string.Concat("api/page/", this._contextpageurlpath));
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }

            await base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await Task.Delay(1400).ContinueWith(async (task) =>
                {
                    await this.applicationprovider.Initpackagemoduledraganddrop();
                    this.StateHasChanged();
                });
            }

            await base.OnAfterRenderAsync(firstRender);
        }

    }
}
