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

        [Authorize]
        [HttpPost]
        public async Task Post([FromBody] Applicationmodule module)
        {
            await this.applicationdbcontext.Applicationmodules.AddAsync(module);
            await this.applicationdbcontext.SaveChangesAsync();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            var module = await this.applicationdbcontext.Applicationmodules.FirstOrDefaultAsync(item => item.Id == id);
            if (module != null)
            {
                module.Containercolumnid = string.Empty;
                this.applicationdbcontext.Applicationmodules.Update(module);
                await this.applicationdbcontext.SaveChangesAsync();
            }
        }

    }
}
