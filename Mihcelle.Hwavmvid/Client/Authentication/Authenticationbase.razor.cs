using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace Mihcelle.Hwavmvid.Client.Authentication
{
    public class Authenticationbase : MainLayoutBase
    {
        [Inject] public IHttpClientFactory httpclientfactory { get; set; }

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
