using System;
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
    public class TranslationController : AbstractApiController
    {
        public TranslationDto Get(String id)
        {
            Translation result = null;
            int dbId;
            if (int.TryParse(id, out dbId))
                result = this.RepositoryContext.TranslationRepository.GetAll().SingleOrDefault(t => t.Id == dbId);

            if (result != null)
                return TranslationDto.Create(result);

            var translation = this.RepositoryContext.TranslationRepository.GetAll().SingleOrDefault(t => t.TranslationId == id);
            if (translation == null)
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));

            return TranslationDto.Create(translation);
        }
    }
}
