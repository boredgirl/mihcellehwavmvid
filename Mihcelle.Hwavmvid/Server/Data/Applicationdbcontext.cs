using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Mihcelle.Hwavmvid.Shared.Models;

namespace Mihcelle.Hwavmvid.Server.Data
{
    public class Applicationdbcontext : IdentityDbContext<Applicationuser>
    {

        public DbSet<Applicationuser> Applicationusers { get; set; }
        public DbSet<Applicationsite> Applicationsites { get; set; }
        public DbSet<Applicationpage> Applicationpages { get; set; }

        public Applicationdbcontext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try { base.OnModelCreating(builder); } catch { }
        }

    }
}
