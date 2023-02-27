using Mihcelle.Hwavmvid.Client.Pages;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client.Container
{
    public class Containerbase : Indexbase
    {

        public Applicationcontainer? _contextcontainer { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            if (this._contextpage != null && this._contextcontainer == null)
            {

                try
                {
                    var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                    this._contextcontainer = await client.GetFromJsonAsync<Applicationcontainer>(string.Concat("api/container/", this._contextpage.Id));
                }
                catch (Exception exception) {
                    Console.WriteLine(exception.Message);
                }
            }

            await base.OnAfterRenderAsync(firstRender);
        }

    }
}
