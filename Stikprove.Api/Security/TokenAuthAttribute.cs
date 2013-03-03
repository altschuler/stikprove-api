using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using Stikprove.Api.Data;

namespace Stikprove.Api.Security
{
    public class TokenAuthAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (!authHeader.Scheme.Equals("token", StringComparison.OrdinalIgnoreCase))
                return false;

            var token = authHeader.Parameter;

            if (!AccessTokenCredentials.TryParse(token))
                return false;

            var creds = AccessTokenCredentials.Parse(token);

            using (var repo = new RepositoryContext())
            {
                var user = repo.UserRepository.GetAll().SingleOrDefault(u => u.Email == creds.Name);
                if (user == null || user.AccessToken == null)
                    return false;

                // Validate expiry of the token
                if (user.AccessTokenExpiry < DateTime.Now)
                {
                    user.AccessToken = null;
                    user.AccessTokenExpiry = null;

                    repo.Save();

                    return false;
                }

                return user.AccessToken == creds.TokenValue;
            }
        }
    }
}