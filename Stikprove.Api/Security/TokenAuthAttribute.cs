﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
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
            if (authHeader == null || !authHeader.Scheme.Equals("token", StringComparison.OrdinalIgnoreCase))
                return false;

            var token = authHeader.Parameter;

            if (!AccessTokenCredentials.TryParse(token))
                return false;

            var creds = AccessTokenCredentials.Parse(token);

            using (var repo = new RepositoryContext())
            {
                var user = repo.UserRepository.GetById(creds.Id);
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

                if (user.AccessToken == creds.TokenValue)
                {
                    var identity = new GenericIdentity(user.Id.ToString());
                    IPrincipal principal = new GenericPrincipal(identity, user.Roles.Select(r => r.Name).ToArray());
                    Thread.CurrentPrincipal = principal;
                    
                    if (HttpContext.Current != null)
                        HttpContext.Current.User = principal;

                    // move on to role check
                    return base.IsAuthorized(actionContext);
                }
            }

            return false;
        }
    }
}