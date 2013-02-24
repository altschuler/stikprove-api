using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using Stikprove.Api.Data;

namespace Stikprove.Api.Modules
{
    public class BasicAuthHttpModule : IHttpModule
    {
        private const string Realm = "Stikprove";

        public void Init(HttpApplication context)
        {
            // Register event handlers
            context.AuthenticateRequest += OnApplicationAuthenticateRequest;
            context.EndRequest += OnApplicationEndRequest;
        }

        private static void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        // TODO salt/hash whatever pass
        private static bool CheckPassword(string username, string password)
        {
            using (var repo = new RepositoryContext())
            {
                var user = repo.UserRepository.GetAll().SingleOrDefault(u => u.Email == username);
                if (user == null)
                    return false;

                return user.Password == password;
            }
        }

        private static bool AuthenticateUser(string credentials)
        {
            bool validated;
            try
            {
                var encoding = Encoding.GetEncoding("UTF-8");
                credentials = encoding.GetString(Convert.FromBase64String(credentials));

                var separator = credentials.IndexOf(':');
                var name = credentials.Substring(0, separator);
                var password = credentials.Substring(separator + 1);

                validated = CheckPassword(name, password);
                if (validated)
                {
                    var identity = new GenericIdentity(name);
                    SetPrincipal(new GenericPrincipal(identity, null));
                }
            }
            catch (FormatException)
            {
                // Credentials were not formatted correctly.
                validated = false;

            }
            return validated;
        }

        private static void OnApplicationAuthenticateRequest(object sender, EventArgs e)
        {
            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader != null)
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) && authHeaderVal.Parameter != null)
                {
                    AuthenticateUser(authHeaderVal.Parameter);
                }
            }
        }

        // If the request was unauthorized, add the WWW-Authenticate header 
        // to the response.
        private static void OnApplicationEndRequest(object sender, EventArgs e)
        {
            var response = HttpContext.Current.Response;
            if (response.StatusCode == 401)
            {
                response.Headers.Add("X-Authentication-Required", string.Format("Basic realm=\"{0}\"", Realm));
            }
        }

        public void Dispose()
        {
        }
    }
}