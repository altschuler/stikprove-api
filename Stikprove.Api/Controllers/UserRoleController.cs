using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stikprove.Api.Models;
using Stikprove.Api.Models.Dto;
using Stikprove.Api.Security;

namespace Stikprove.Api.Controllers
{
    [TokenAuth]
    public class UserRoleController : AbstractApiController
    {
        public IEnumerable<UserRoleDto> GetUserRoles()
        {
            return this.RepositoryContext.RepositoryFor<UserRole>().GetAll().AsEnumerable().Select(UserRoleDto.Create);
        }

        public UserRoleDto GetUserRole(int id)
        {
            var role = this.RepositoryContext.RepositoryFor<UserRole>().GetById(id);
            if (role == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return UserRoleDto.Create(role);
        }

        protected override void Dispose(bool disposing)
        {
            this.RepositoryContext.Dispose();
            base.Dispose(disposing);
        }
    }
}