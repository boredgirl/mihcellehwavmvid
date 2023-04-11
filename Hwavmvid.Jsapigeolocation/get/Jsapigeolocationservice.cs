using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Jsapigeolocation
{

    public class Jsapigeolocationservice : IDisposable
    {
        
        public IJSRuntime JsRuntime { get; set; }
        public IJSObjectReference Module { get; set; } = null;
        public DotNetObjectReference<Jsapigeolocationservice> DotNetObjectRef { get; set; }

        public event Action<Jsapigeolocationpositionevent> OnGeolocationpositionReceived;
        public event Action<Jsapigeolocationpermissionsevent> OnGeolocationpermisssionsChanged;
        public event Action OnUpdateUI;

        public List<Jsapigeolocationmap> Mapitems { get; set; } = new List<Jsapigeolocationmap>();

        public Jsapigeolocationservice(IJSRuntime jsRuntime)
        {
            this.JsRuntime = jsRuntime;
            this.DotNetObjectRef = DotNetObjectReference.Create(this);
        }
        public async Task Initgeolocationservice()
        {
            if (this.Module == null)
            {
                this.Module = await this.JsRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/Mihcelle.Hwavmvid.Jsapigeolocation/jsapigeolocationjsinterop.js");
            }
        }
        public async Task InitGeolocationMap(string componentid, string elementid)
        {
            var contextmap = this.Getmap(componentid);
            if (contextmap == null)
            {
                contextmap = new Jsapigeolocationmap() { Id = componentid, Item = null, Jsmapreference = null };
                contextmap.Jsmapreference = await this.Module.InvokeAsync<IJSObjectReference>("initgeolocationmap", this.DotNetObjectRef, componentid, elementid);
                this.Mapitems.Add(contextmap);
            }                
        }

        public Jsapigeolocationmap Getmap(string id)
        {
            return this.Mapitems.FirstOrDefault(item => item.Id == id);
        }
        public void Removemap(string componentid)
        {
            var map = this.Getmap(componentid);
            if (map != null)
                this.Mapitems.Remove(map);
        }

        public async Task Getgeolocationpermissions(string componentid)
        {
            var map = this.Getmap(componentid);
            if (map != null)
                await map.Jsmapreference.InvokeVoidAsync("requestpermissions");
        }
        public async Task Getgeolocation(string componentid)
        {
            var map = this.Getmap(componentid);
            if (map != null)
                await map.Jsmapreference.InvokeVoidAsync("requestcoords");
        }
        public async Task Renderbingmapposition(string componentid, double? latitude, double? longitude)
        {
            var map = this.Getmap(componentid);
            if (map != null)
                await map.Jsmapreference.InvokeVoidAsync("renderbingmapposition", latitude, longitude);
        }

        [JSInvokable("Pushcoords")]
        public async void Pushcoords(string componentid, string coords)
        {
            var map = this.Getmap(componentid);
            if (map != null)
            {
                map.Item = JsonSerializer.Deserialize<Jsapigeolocationitem>(coords);
                this.OnGeolocationpositionReceived?.Invoke(new Jsapigeolocationpositionevent() { permissionstate = map.Permissionstate, Item = map.Item });

                await this.Renderbingmapposition(componentid, map.Item.latitude, map.Item.longitude);
                this.UpdateUI();
            }
        }

        [JSInvokable("Permissionschanged")]
        public void Permissionschanged(string componentid, string permissionsstate)
        {
            var map = this.Getmap(componentid);
            if (map != null)
            {
                map.Permissionstate = permissionsstate;
            }

            this.OnGeolocationpermisssionsChanged?.Invoke(new Jsapigeolocationpermissionsevent() { Permissionsstate = permissionsstate });
        }

        public void UpdateUI()
        {
            this.OnUpdateUI?.Invoke();
        }

        public void Dispose()
        {
            if (this.Module != null)
            {
                this.Module.DisposeAsync();
            }
        }

    }
}