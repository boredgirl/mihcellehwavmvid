using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Mihcelle.Hwavmvid.Server
{
    public class Applicationcookieauthenticationhandler : CookieAuthenticationHandler
    {
        public Applicationcookieauthenticationhandler(IOptionsMonitor<CookieAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        public override async Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
        {
            try { await base.SignInAsync(user, properties); } catch (Exception ex) { }
        }
        protected override async Task HandleSignInAsync(ClaimsPrincipal user, AuthenticationProperties? properties)
        {
            try { await base.HandleSignInAsync(user, properties); } catch (Exception ex) { }
        }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            try { await base.HandleChallengeAsync(properties); } catch (Exception ex) { }
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try { return await base.HandleAuthenticateAsync(); } catch (Exception ex) { return null; }
        }
    }
}
