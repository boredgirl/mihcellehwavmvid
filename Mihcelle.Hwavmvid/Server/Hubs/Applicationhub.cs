using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Mihcelle.Hwavmvid.Server.Hubs
{
    public class Applicationhub : Hub, IDisposable, IServiceProvider
    {

        [AllowAnonymous]
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        [AllowAnonymous]
        public void Establishapplicationconnection()
        {

        }

        [AllowAnonymous]
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public object? GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
