namespace Ucrs.Data.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Common.Models;
    using Ucrs.Data.Common.Repositories;

    public class InMemoryRepository<T, TKey> : IRepository<T>
        where T : class, IIdentifyable<TKey>
    {
        protected readonly IList<T> DatabaseStore;

        public InMemoryRepository()
        {
            this.DatabaseStore = new List<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.DatabaseStore.AsQueryable();
        }

        public void Add(T entity)
        {
            this.DatabaseStore.Add(entity);
        }

        public void Update(T entity)
        {
            this.Delete(entity);
            this.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            this.DatabaseStore.Remove(entity);
        }

        public T GetById(params object[] id)
        {
            return this.DatabaseStore.FirstOrDefault(x => id.Any(y => x.Id.Equals(y)));
        }

        public Task<T> GetByIdAsync(params object[] id)
        {
            var entity = this.DatabaseStore.FirstOrDefault(x => id.Any(y => x.Id.Equals(y)));
            return Task.FromResult(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }

        public void Dispose()
        {
        }
    }
}
