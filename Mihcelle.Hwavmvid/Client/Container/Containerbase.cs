using Mihcelle.Hwavmvid.Client.Pages;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client.Container
{
    public class Containerbase : Indexbase, IDisposable
    {

        public Applicationcontainer? _contextcontainer { get; set; }

        protected override async Task OnInitializedAsync()
        {

            this.applicationprovider._oncontextpagechanged += async () => await this.Contextpagechanged();
            await base.OnInitializedAsync();
        }

        protected async Task Contextpagechanged()
        {

            if (this.applicationprovider?._contextpage != null && this._contextcontainer == null ||
                this.applicationprovider?._contextpage != null && this._contextcontainer != null && this.applicationprovider?._contextpage.Id != this._contextcontainer.Pageid)
            {

                try
                {
                    var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                    this._contextcontainer = await client.GetFromJsonAsync<Applicationcontainer>(string.Concat("api/container/", this.applicationprovider?._contextpage.Id));
                    this.StateHasChanged();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        public void Dispose()
        {
            this.applicationprovider._oncontextpagechanged -= async () => await this.Contextpagechanged();
        }

    }
}
