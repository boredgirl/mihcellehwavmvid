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

        public UserManager<IdentityUser> usermanager { get; set; }
        public SignInManager<IdentityUser> signinmanager { get; set; }

        public LogoutController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task Get()
        {
            await this.signinmanager.SignOutAsync();
        }
    }
}
