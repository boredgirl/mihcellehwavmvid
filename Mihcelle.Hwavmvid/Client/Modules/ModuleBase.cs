using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mihcelle.Hwavmvid.Client.Pages;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;

namespace Mihcelle.Hwavmvid.Client.Modules
{
    public class Modulebase : ComponentBase
    {

        [Inject] public IHttpClientFactory ihttpclientfactory { get; set; }
        [Inject] public NavigationManager navigationmanager { get; set; }

        [Parameter] public string Moduleid { get; set; }
        [Parameter] public Type Componenttype { get; set; }

        public async Task Deletemodule(string moduleid)
        {
            var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
            await client.DeleteAsync(string.Concat("api/module/", moduleid));
            this.navigationmanager.NavigateTo(this.navigationmanager.Uri, true);
        }

    }
}
