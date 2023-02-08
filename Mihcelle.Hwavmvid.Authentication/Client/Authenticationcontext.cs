using Microsoft.AspNetCore.Identity;
using Mihcelle.Hwavmvid.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Authentication.Client
{
    public class Authenticationcontext
    {
        public Applicationuser? Applicationuser { get; set; }
        public List<IdentityRole>? Roles { get; set; }
    }
}
