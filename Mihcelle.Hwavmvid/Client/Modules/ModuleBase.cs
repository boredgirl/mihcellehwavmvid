using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Mihcelle.Hwavmvid.Client.Pages;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace Mihcelle.Hwavmvid.Client.Modules
{
    public class Modulebase : ComponentBase
    {


        [Inject] public IServiceProvider iserviceprovider { get; set; }
        [Inject] public IHttpClientFactory ihttpclientfactory { get; set; }
        [Inject] public NavigationManager navigationmanager { get; set; }

        [Parameter] public string Moduleid { get; set; }
        [Parameter] public Type Componenttype { get; set; }

        protected Moduleservice<Modulepreferences> moduleservice { get; set; }
        protected Dictionary<string, object> servpara { get; set; }

        protected override Task OnInitializedAsync()
        {
            
            Modulepreferences modulepreferences = new Modulepreferences();
            this.moduleservice = new Moduleservice<Modulepreferences>();
            this.moduleservice.Preferences = new Modulepreferences();
            this.moduleservice.Preferences.ModuleId = this.Moduleid;

            this.servpara = new Dictionary<string, object>();
            servpara.Add("Moduleparams", this.moduleservice);

            return base.OnInitializedAsync();
        }

        public async Task Deletemodule(string moduleid)
        {
            var client = this.ihttpclientfactory?.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
            await client.DeleteAsync(string.Concat("api/module/", moduleid));
            this.navigationmanager.NavigateTo(this.navigationmanager.Uri, true);
        }

    }
}
