using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mihcelle.Hwavmvid.Client;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mihcelle.Hwavmvid.Client.Authentication
{
    public class Authenticationbase : ComponentBase
    {
        [Inject] public Applicationprovider? applicationprovider { get; set; }
        [Inject] public AuthenticationStateProvider? authenticationstateprovider { get; set; }

        public JsonSerializerOptions jsonserializeroptions { get; set; } = new JsonSerializerOptions()
        {
            WriteIndented = false,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.Never,
            AllowTrailingCommas = true,
            NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowNamedFloatingPointLiterals,
            DefaultBufferSize = 4096,
            MaxDepth = 41,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles,
            PropertyNamingPolicy = null,
        };
    }
}
