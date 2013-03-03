using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Helpers;
using Stikprove.Api.Data;
using Stikprove.Api.Models;
using Stikprove.Api.Security;

namespace Stikprove.Api.Modules
{
    public class BasicAuthHttpModule : IHttpModule
    {
        private const string Realm = "Stikprove";

        public void Init(HttpApplication context)
        {
            // Register event handlers
            context.AuthenticateRequest += this.OnApplicationAuthenticateRequest;
            context.EndRequest += this.OnApplicationEndRequest;
        }

        private void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            return;
            var request = HttpContext.Current.Request;
            // TODO fix haxx
            if (!request.Path.StartsWith("/api/"))
                return;

            if (request.Path.StartsWith("/api/login"))
                return;
            
            using (var repo = new RepositoryContext())
            {
                User user = null;
                if (request.Path.StartsWith("/api/login"))
                {
                    // TODO bypass and generate access token
                    var credentials = this.GetAuthenticationHeader(request);
                    user = repo.UserRepository.GetAll().SingleOrDefault(u => u.Email == credentials.Name);

                    if (user != null && this.AuthenticateUser(user, credentials))
                    {
                        var accessToken = AuthUtils.GenerateAccessToken();

                        // TODO generate access token and return
                    }
                }

                var token = this.GetAuthenticationHeader(request);
                if (this.AuthenticateUser(user, token))
                {
                    // TODO enable roles
                    var identity = new GenericIdentity(token.Name);
                    this.SetPrincipal(new GenericPrincipal(identity, null));
                }
            }
        }

        private BasicAuthCredentials GetAuthenticationHeader(HttpRequest request)
        {
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                {
                    return BasicAuthCredentials.Parse(authHeaderVal.Parameter);
                }
            }

            return null;
        }


        private AccessTokenCredentials GetTokenHeader(HttpRequest request)
        {
            var tokenHeader = request.Headers["X-Access-Token"];
            if (tokenHeader != null)
            {
                return AccessTokenCredentials.Parse(tokenHeader);
            }

            return null;
        }

        private void OnApplicationEndRequest(object sender, EventArgs e)
        {
            // If the request was unauthorized, add the WWW-Authenticate header to the response.
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                response.Headers.Add("X-Authentication-Required", string.Format("Basic realm=\"{0}\"", Realm));
            }
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private bool AuthenticateUser(User user, BasicAuthCredentials credentials)
        {
            return Crypto.VerifyHashedPassword(user.Password, credentials.Password + user.Salt);
        }

        public void Dispose()
        {
        }
    }
}