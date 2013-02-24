using System.Net;
using System.Security.Principal;

namespace Stikprove.Api.Models
{
    public partial class User : IIdentifiable, IPrincipal
    {
        public bool IsInRole(string role)
        {
            // TODO multiple roles
            return true;
        }

        public IIdentity Identity
        {
            get { return new HttpListenerBasicIdentity(this.Email, this.Password); }
        }
    }
    public partial class Translation : IIdentifiable { }
    public partial class Company : IIdentifiable { }
    public partial class UserRole : IIdentifiable { }
}