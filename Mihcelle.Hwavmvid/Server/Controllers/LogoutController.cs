using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {

        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }

        public LogoutController(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [HttpGet]
        [Authorize]
        public async Task Get()
        {
            await this.signinmanager.SignOutAsync();
        }
    }
}
