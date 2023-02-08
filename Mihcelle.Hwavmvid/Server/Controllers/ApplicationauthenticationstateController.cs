using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationauthenticationstateController : ControllerBase
    {

        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }
        public RoleManager<IdentityRole> rolemanager { get; set; }

        public ApplicationauthenticationstateController(UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager, RoleManager<IdentityRole> rolemanager)
        {
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.rolemanager = rolemanager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<List<KeyValuePair<string, string>>> Get()
        {

            var identityuser = await usermanager.FindByEmailAsync("host7@mihcelle.hwavmvid.com");
            if (identityuser != null)
            {
                if (!await this.rolemanager.RoleExistsAsync("Administrator"))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole("Administrator"));
                    await usermanager.AddToRoleAsync(identityuser, "Administrator");
                }

                //string password = usermanager.PasswordHasher.HashPassword(identityuser, "!P4ssword");
                var result = await signinmanager.PasswordSignInAsync(identityuser, "!P4ssword", true, false);
                if (result.Succeeded)
                {                   

                    ClaimsIdentity claimsidentity = new ClaimsIdentity(HttpContext.User.Claims.ToList(), "requireauthenticateduser");
                    var claimsprincipal = new ClaimsPrincipal(claimsidentity);
                    var authenticationstate = new AuthenticationState(claimsprincipal);

                    var serializeOptions = new JsonSerializerOptions();
                    serializeOptions.WriteIndented = false;
                    serializeOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                    serializeOptions.AllowTrailingCommas = true;
                    serializeOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
                    serializeOptions.DefaultBufferSize = 4096;
                    serializeOptions.MaxDepth = 41;
                    serializeOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    serializeOptions.PropertyNamingPolicy = null;

                    List<KeyValuePair<string, string>> claimslist = new();
                    foreach (var claim in HttpContext.User.Claims)
                    {
                        claimslist.Add(new KeyValuePair<string, string>(claim.Type, claim.Value));
                    }

                    //var json = JsonSerializer.Serialize(claimslist);
                    return claimslist;
                }
            }

            return null;
        }
    }
}
