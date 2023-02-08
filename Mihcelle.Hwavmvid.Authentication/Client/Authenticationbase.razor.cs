using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mihcelle.Hwavmvid.Authentication.Client
{
    public class Authenticationbase : ComponentBase
    {
        [Inject] public Authenticationprovider? authenticationprovider { get; set; }
        public Authenticationcontext? Context { get; set; }

    }
}
