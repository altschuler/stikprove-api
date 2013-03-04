using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stikprove.Api.Security;

namespace Stikprove.Api.Controllers
{
    [TokenAuth]
    public class PingController : ApiController
    {
        public HttpResponseMessage Post()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
