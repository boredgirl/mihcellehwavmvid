using Microsoft.AspNetCore.Components;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client.Pages
{
    public class Indexbase : Mainlayoutbase
    {

        [Parameter] public string? page { get; set; }

        public Applicationpage? _contextpage { get; set; }

        protected override async Task OnParametersSetAsync()
        {

            this.page = page ?? "index";

            try
            {
                var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                this._contextpage = await client.GetFromJsonAsync<Applicationpage>(string.Concat("api/page/", this.page));
            }
            catch (Exception exception) { Console.WriteLine(exception.Message); }

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

    }
}
