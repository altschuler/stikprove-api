using System;
using Stikprove.Api.Models;

namespace Stikprove.Api.Data
{
    public interface IRepositoryContext: IDisposable
    {
        IRepository<T> RepositoryFor<T>() where T : class, IIdentifiable;
        void Save();
    }
}