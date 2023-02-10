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

        private IHttpClientFactory _httpclientfactory { get; set; }
        public Applicationauthenticationstateprovider(IHttpClientFactory httpclientfactory)
        {
            this._httpclientfactory = httpclientfactory;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            
            try
            {
                var client = this._httpclientfactory.CreateClient("Mihcelle.Hwavmvid.ServerApi.Unauthenticated");
                var claimsdic = await client.GetFromJsonAsync<List<KeyValuePair<string, string>>>("Applicationauthenticationstate");
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
