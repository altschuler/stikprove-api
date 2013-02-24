using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Objects;
using Stikprove.Api.Models;

namespace Stikprove.Api.Data
{
    public class Repository<T> : IRepository<T> where T : class, IIdentifiable
    {
        protected ObjectSet<T> Entities;
        private readonly ObjectStateManager ObjectStateManager;

        public Repository(ObjectSet<T> entities, ObjectStateManager objectStateManager)
        {
            this.Entities = entities;
            this.ObjectStateManager = objectStateManager;
        }

        public IQueryable<T> GetAll()
        {
            return this.Entities.AsQueryable();
        }

        public T GetById(int entityId)
        {
            return this.Entities.SingleOrDefault(e => e.Id == entityId);
        }

        public IEnumerable<T> Get(Func<T, bool> lambda)
        {
            return this.Entities.Where(lambda);
        }

        public void Add(IEnumerable<T> entities)
        {
            foreach (var entity in entities.ToList())
                this.Add(entity);
        }

        public void Add(T entity)
        {
            this.Entities.AddObject(entity);
        }

        public void Delete(T entity)
        {
            this.Entities.DeleteObject(entity);
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities.ToList())
                this.Delete(entity);
        }

        public void Delete(Func<T,bool> lambda)
        {
            var entities = this.Entities.Where(lambda);
            foreach (var entity in entities.ToList())
                this.Delete(entity);
        }

        public void Update(int id, T entity)
        {
            this.Entities.Attach(entity);
            this.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
    }
}
