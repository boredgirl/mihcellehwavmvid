using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        public UserManager<IdentityUser> usermanager { get; set; }
        public SignInManager<IdentityUser> signinmanager { get; set; }

        public LoginController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signinmanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task Post([FromBody] string helloworld)
        {

            var identityuser = await usermanager.FindByEmailAsync("host7@mihcelle.hwavmvid.com");
            if (identityuser != null)
            {
                //string password = usermanager.PasswordHasher.HashPassword(identityuser, "!P4ssword");
                var result = await signinmanager.PasswordSignInAsync(identityuser, "!P4ssword", true, false);
                if (result.Succeeded) {}
            }
        }
    }
}
