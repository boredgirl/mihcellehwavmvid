using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mihcelle.Hwavmvid.Client
{
    public class Applicationauthenticationstateprovider : AuthenticationStateProvider
    {

        private HttpClient _httpclient { get; set; }
        public Applicationauthenticationstateprovider(HttpClient httpclient)
        {
            this._httpclient = httpclient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
            try
            {
                var serializeOptions = new JsonSerializerOptions();
                serializeOptions.WriteIndented = false;
                serializeOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                serializeOptions.AllowTrailingCommas = true;
                serializeOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
                serializeOptions.DefaultBufferSize = 4096;
                serializeOptions.MaxDepth = 41;
                serializeOptions.ReferenceHandler = ReferenceHandler.Preserve;
                serializeOptions.PropertyNamingPolicy = null;

                var claimsdic = await this._httpclient.GetFromJsonAsync<List<KeyValuePair<string, string>>>("Applicationauthenticationstate");
                //var items = JsonSerializer.Deserialize<List<Claim>>(claims, serializeOptions);

                var claimslist = new List<Claim>();
                foreach (var dicitem in claimsdic)
                {
                    claimslist.Add(new Claim(dicitem.Key, dicitem.Value));
                }

                var authenticationstate = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claimslist, "mihcelle.hwavmvid")));
                return authenticationstate;
            }
            catch(Exception exception)
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine(exception.Message);
                Console.WriteLine("----------------------------------------------------");
            }

            var anonymousid = new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(anonymousid));
        }

    }
}
