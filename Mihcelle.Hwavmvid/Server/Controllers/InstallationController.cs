using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Text.Json;
using System.Threading.Tasks;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstallationController : ControllerBase
    {

        public IConfiguration configuration { get; set; }
        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }

        public InstallationController(IConfiguration configuration, UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager)
        {
            this.configuration = configuration;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<bool> Get()
        {
            string defaultconnectionstring = null;
            defaultconnectionstring = configuration.GetConnectionString("DefaultConnection");
            bool framework_installed = !string.IsNullOrEmpty(defaultconnectionstring);
            return framework_installed;
        }
    }
}
