using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mihcelle.Hwavmvid.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Sitecontroller : ControllerBase
    {

        [AllowAnonymous]
        [HttpPost("brandmark")]
        public async Task<IActionResult> Postbrandmark([FromForm] IFormFileCollection files)
        {


            return new OkObjectResult(new { Message = "File upload succeeded." });
        }

        [AllowAnonymous]
        [HttpPost("favicon")]
        public async Task<IActionResult> Postfavicon([FromForm] IFormFileCollection files)
        {


            return new OkObjectResult(new { Message = "File upload succeeded." });
        }

    }
}
