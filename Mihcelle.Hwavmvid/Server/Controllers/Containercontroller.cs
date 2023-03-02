using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mihcelle.Hwavmvid.Server.Data;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class Containercontroller : ControllerBase
    {

        public Applicationdbcontext applicationdbcontext { get; set; }

        public Containercontroller(Applicationdbcontext applicationdbcontext) 
        {
            this.applicationdbcontext = applicationdbcontext;
        }

        [AllowAnonymous]
        [HttpGet("{pageid}")]
        public async Task<Applicationcontainer?> Get(string pageid)
        {

            var container = await this.applicationdbcontext.Applicationcontainers.FirstOrDefaultAsync(item => item.Pageid == pageid);
            if (container != null)
            {
                return container;
            }

            return null;
        }

    }
}
