using System.Web.Http;
using Stikprove.Api.Data;
using Stikprove.Api.Security;

namespace Stikprove.Api.Controllers
{
    public abstract class AbstractApiController : ApiController
    {
        protected RepositoryContext RepositoryContext;

        protected AbstractApiController()
        {
            this.RepositoryContext = new RepositoryContext();
        }

        protected override void Dispose(bool disposing)
        {
            this.RepositoryContext.Dispose();
            base.Dispose(disposing);
        }
    }
}
