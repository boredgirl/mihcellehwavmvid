using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Mihcelle.Hwavmvid.Shared.Models;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using Mihcelle.Hwavmvid.Server.Data;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Shared.Constants;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstallationController : ControllerBase
    {

        public IWebHostEnvironment iwebhostenvironment { get; set; }
        public IConfiguration configuration { get; set; }
        public UserManager<Applicationuser> usermanager { get; set; }
        public SignInManager<Applicationuser> signinmanager { get; set; }
        public RoleManager<IdentityRole> rolemanager { get; set; }
        public Applicationdbcontext context { get; set; }

        public InstallationController(IWebHostEnvironment environment, IConfiguration configuration, UserManager<Applicationuser> usermanager, SignInManager<Applicationuser> signinmanager, RoleManager<IdentityRole> rolemanager, Applicationdbcontext context)
        {
            this.iwebhostenvironment = environment;
            this.configuration = configuration;
            this.usermanager = usermanager;
            this.signinmanager = signinmanager;
            this.rolemanager = rolemanager;
            this.context = context;
        }

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        [HttpGet]
        public async Task<bool> Get()
        {
            string defaultconnectionstring = null;
            defaultconnectionstring = this.configuration.GetConnectionString("DefaultConnection");
            bool framework_installed = !string.IsNullOrEmpty(defaultconnectionstring);
            return framework_installed;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task Post([FromBody] Installationmodel model)
        {
            var connectionstring = $"Data Source={model.Sqlserverinstance};Initial Catalog={model.Databasename};User ID={model.Databaseowner};Password={model.Databaseownerpassword};Encrypt=true;TrustServerCertificate=true;";
            this.Updatedconnectionstring(connectionstring);

            this.context.Database.SetConnectionString(connectionstring);
            await this.context.Database.EnsureCreatedAsync();
            await this.context.Database.MigrateAsync();

            var applicationuser = new Applicationuser();
            applicationuser.UserName = model.Hostusername;
            applicationuser.Email = model.Hostusername + "@mihcelle.hwavmvid.com";
            applicationuser.PasswordHash = model.Hostpassword;
            applicationuser.EmailConfirmed = true;
            applicationuser.TwoFactorEnabled = false;
            applicationuser.LockoutEnabled = true;

            var createuserresult = await this.usermanager.CreateAsync(applicationuser, model.Hostpassword);
            if (createuserresult.Succeeded)
            {
                if (!await this.rolemanager.RoleExistsAsync(Authentication.Administratorrole))
                {
                    await this.rolemanager.CreateAsync(new IdentityRole(Authentication.Userrole));
                }

                var addtoroleresult = await usermanager.AddToRoleAsync(applicationuser, Authentication.Userrole);
                if (!addtoroleresult.Succeeded)
                {
                    throw new HubException("Failed to add user to role..");
                }
            }
        }

        public void Updatedconnectionstring(string connectionstring)
        {
            var jsonconfig = System.IO.File.ReadAllText(string.Concat(iwebhostenvironment.ContentRootPath, "\\", "appsettings.json"));
            var deserializedconfig = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonconfig);
            if (deserializedconfig != null)
            {
                deserializedconfig["ConnectionStrings"] = new { DefaultConnection = connectionstring };
                var updatedConfigJson = JsonSerializer.Serialize(deserializedconfig, new JsonSerializerOptions{ WriteIndented = true });
                System.IO.File.WriteAllText("appsettings.json", updatedConfigJson);
            }
        }

    }
}
