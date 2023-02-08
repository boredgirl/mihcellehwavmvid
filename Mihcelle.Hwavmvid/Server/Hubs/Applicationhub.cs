using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Security.Claims;

namespace Mihcelle.Hwavmvid.Server.Hubs
{
    public class Applicationhub : Hub, IDisposable, IServiceProvider
    {

        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }

        public Applicationhub(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [AllowAnonymous]
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("connected to application hub..");
            Console.WriteLine("-------------------------------------");

            var user = new Applicationuser()
            {
                UserName = "host7",
                Email = "host7@mihcelle.hwavmvid.com",
                EmailConfirmed = true,
                LockoutEnabled = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
            };
            await this.usermanager.CreateAsync(user, "!P4ssword");
            await base.OnConnectedAsync();
        }

        [AllowAnonymous]
        public async Task Establishapplicationconnection()
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
