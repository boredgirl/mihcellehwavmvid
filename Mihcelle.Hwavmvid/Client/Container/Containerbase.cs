using Mihcelle.Hwavmvid.Client.Pages;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client.Container
{
    public class Containerbase : Indexbase, IDisposable
    {

        protected override async Task OnInitializedAsync()
        {

            this.applicationprovider._oncontextpagechanged += async () => await this.Contextpagechanged();
            await base.OnInitializedAsync();
        }

        protected async Task Contextpagechanged()
        {

            if (this.applicationprovider?._contextpage != null && this.applicationprovider._contextcontainer == null ||
                this.applicationprovider?._contextpage != null && this.applicationprovider._contextcontainer != null && this.applicationprovider?._contextpage.Id != this.applicationprovider?._contextcontainer.Pageid)
            {

                try
                {

                    var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                    this.applicationprovider._contextcontainer = await client.GetFromJsonAsync<Applicationcontainer>(string.Concat("api/container/", this.applicationprovider?._contextpage.Id));

                    if (this.applicationprovider._contextcontainer != null)
                    {
                        this.applicationprovider._contextcontainercolumns = await client.GetFromJsonAsync<List<Applicationcontainercolumn>>(string.Concat("api/containercolumns/", this.applicationprovider?._contextcontainer.Id));
                    }

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
