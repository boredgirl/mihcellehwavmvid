using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Client;

namespace Mihcelle.Hwavmvid.Client.Authentication
{
    public class Authenticationbase : ComponentBase
    {
        [Inject] public Applicationprovider? applicationprovider { get; set; }

    }
}
