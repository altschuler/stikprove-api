using System.Data.Objects;
using Stikprove.Api.Models;

namespace Stikprove.Api.Data
{
    public class RepositoryContext : IRepositoryContext
    {
        private readonly StikproveEntities Entities;

        public IRepository<User> UserRepository { get; private set; }
        public IRepository<Translation> TranslationRepository { get; private set; }
        public IRepository<Company> CompanyRepository { get; private set; }

        public RepositoryContext()
        {
            this.Entities = new StikproveEntities();

            this.UserRepository = new Repository<User>(this.Entities.User, this.Entities.ObjectStateManager);
            this.TranslationRepository = new Repository<Translation>(this.Entities.Translation, this.Entities.ObjectStateManager);
            this.CompanyRepository = this.RepositoryFor<Company>();
        }

        public IRepository<T> RepositoryFor<T>() where T : class, IIdentifiable
        {
            var type = this.Entities.GetType();    
            var info = type.GetProperty(typeof(T).Name);    
            var value = (ObjectSet<T>) info.GetValue(this.Entities, null);
            return new Repository<T>(value, this.Entities.ObjectStateManager);
        }
    
        public void Save()
        {
            this.Entities.SaveChanges();
        }

        public void Dispose()
        {
            this.Entities.Dispose();
        }
    }
}