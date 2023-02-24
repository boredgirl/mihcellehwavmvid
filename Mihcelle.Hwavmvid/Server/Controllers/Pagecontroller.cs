using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Pagecontroller : ControllerBase
    {

        public IWebHostEnvironment iwebhostenvironment { get; set; }
        public Applicationdbcontext applicationdbcontext { get; set; }

        public Pagecontroller(IWebHostEnvironment iwebhostenvironment, Applicationdbcontext applicationdbcontext)
        {
            this.iwebhostenvironment = iwebhostenvironment;
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpGet("{urlpath}")]
        public async Task<Applicationpage> Get(string urlpath)
        {
            var page = await this.applicationdbcontext.Applicationpages.FirstOrDefaultAsync(item => item.Urlpath == urlpath);
            return page;
        }

    }
}
