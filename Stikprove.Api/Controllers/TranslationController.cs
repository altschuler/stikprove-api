using System;
using System.Linq;
using Stikprove.Api.Models;

namespace Stikprove.Api.Controllers
{
    public class TranslationController : AbstractApiController, ITranslationController
    {
        public Translation Get(String id)
        {
            int dbId;
            if (int.TryParse(id, out dbId))
                return this.RepositoryContext.TranslationRepository.GetAll().Single(t => t.Id == dbId);
            
            return this.RepositoryContext.TranslationRepository.GetAll().Single(t => t.TranslationId == id);
        }
    }
}
