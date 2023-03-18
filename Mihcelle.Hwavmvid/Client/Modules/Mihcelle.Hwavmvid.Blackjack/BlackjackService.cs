using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Modules.Blackjack
{

    public class BlackjackService : IDisposable
    {

        public IJSObjectReference javascriptfile { get; set; }
        public IJSRuntime jsruntime { get; set; }

        public BlackjackService(IJSRuntime jsRuntime)
        {
            this.jsruntime = jsRuntime;
        }

        public async Task InitBlackjackService()
        {
            if (this.javascriptfile == null)
                this.javascriptfile = await this.jsruntime.InvokeAsync<IJSObjectReference>("import", "/blackjack/blackjackjsinterop.js");
        }

        public void Dispose()
        {
            if (javascriptfile != null)
                this.javascriptfile.DisposeAsync();
        }

    }
}