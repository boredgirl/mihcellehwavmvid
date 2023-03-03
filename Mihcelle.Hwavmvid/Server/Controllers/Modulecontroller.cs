using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Modules.Htmleditor;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Modulecontroller : ControllerBase
    {

        public Applicationdbcontext applicationdbcontext { get; set; }
        public Modulecontroller(Applicationdbcontext applicationdbcontext)
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task Post([FromBody] Applicationmodule module)
        {
            await this.applicationdbcontext.Applicationmodules.AddAsync(module);
            await this.applicationdbcontext.SaveChangesAsync();
        }

    }
}
