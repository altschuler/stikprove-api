using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stikprove.Api.Models.Dto;

namespace Stikprove.Api.Controllers
{
    /// <summary>
    /// The sole purpose of this controller is to allow clients to test a login
    /// </summary>
    [Authorize]
    public class PingController : AbstractApiController
    {
        public HttpResponseMessage GetAny()
        {
            var user = this.RepositoryContext.UserRepository.GetAll().SingleOrDefault(u => u.Email == this.User.Identity.Name);
            return this.Request.CreateResponse(HttpStatusCode.OK, UserDto.Create(user));
        }
    }
}
