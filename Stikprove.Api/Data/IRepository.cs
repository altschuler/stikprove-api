using System;
using System.Collections.Generic;
using System.Linq;

namespace Stikprove.Api.Data
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(int id, T entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        void Delete(Func<T, bool> lambda);

        T GetById(int id);
        IQueryable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> lambda);
    }
}
