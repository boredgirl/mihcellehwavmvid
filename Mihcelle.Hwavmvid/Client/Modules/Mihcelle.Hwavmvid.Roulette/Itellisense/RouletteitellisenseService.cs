using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.Roulette.Itellisense
{

    public class RouletteitellisenseService : IDisposable
    {

        private IJSObjectReference javascriptfile;
        private IJSRuntime jsruntime;

        public string ContextGameId { get; set; }
        public int? ContextGameValue { get; set; } = 0;

        public RouletteitellisenseService(IJSRuntime jsRuntime)
        {
            this.jsruntime = jsRuntime;
        }
        public async Task InitRouletteService()
        {
            this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>("import", "/roulette/rouletteitellisensejsinterop.js");
        }
        public async Task<string> Prompt(string message)
        {
            return await this.javascriptfile.InvokeAsync<string>("showPrompt", message);
        }
        public void Dispose()
        {
            if (javascriptfile != null)
                this.javascriptfile.DisposeAsync();
        }

    }
}