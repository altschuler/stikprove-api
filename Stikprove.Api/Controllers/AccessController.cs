using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using Stikprove.Api.Models.Dto;
using Stikprove.Api.Security;

namespace Stikprove.Api.Controllers
{
    public class AccessController : AbstractApiController
    {
        public HttpResponseMessage PostLogin(BasicAuthCredentials credentials)
        {
            var user = this.RepositoryContext.UserRepository.GetAll().SingleOrDefault(u => u.Email == credentials.Name);
            
            if (user == null)
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid login");

            if (!Crypto.VerifyHashedPassword(user.Password, credentials.Password + user.Salt))
                return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid login");

            // Generate a new access token
            user.AccessToken = AuthUtils.GenerateAccessToken();
            user.AccessTokenExpiry = DateTime.Now.AddDays(1);

            this.RepositoryContext.Save();

            return this.Request.CreateResponse(HttpStatusCode.OK, UserDto.Create(user));
        }

        //public HttpResponseMessage PostLogout(BasicAuthCredentials credentials)
        //{
        //    var user = this.RepositoryContext.UserRepository.GetAll().SingleOrDefault(u => u.Email == credentials.Name);

        //    user.AccessToken = null;
        //    user.AccessTokenExpiry = null;

        //    this.RepositoryContext.Save();

        //    return this.Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}
