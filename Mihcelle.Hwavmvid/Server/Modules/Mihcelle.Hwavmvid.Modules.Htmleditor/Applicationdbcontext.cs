using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Modules.Htmleditor
{

    public class Applicationdbcontext : Mihcelle.Hwavmvid.Server.Data.Applicationdbcontext
    {

        public DbSet<Applicationhtmleditor> Applicationhtmleditors { get; set; }

        public Applicationdbcontext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try { base.OnModelCreating(builder); } catch { }
        }

    }
}
