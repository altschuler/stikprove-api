using System;
using Stikprove.Api.Models;

namespace Stikprove.Api.Controllers
{
    public interface ITranslationController
    {
        Translation Get(String id);
    }
}