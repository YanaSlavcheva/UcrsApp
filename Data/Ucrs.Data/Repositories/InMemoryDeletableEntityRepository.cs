namespace Ucrs.Data.Repositories
{
    using System;
    using System.Linq;
    using Ucrs.Data.Common.Models;
    using Ucrs.Data.Common.Repositories;

    public class InMemoryDeletableEntityRepository<T, TKey> : InMemoryRepository<T, TKey>, IDeletableEntityRepository<T>
        where T : class, IIdentifyable<TKey>, IDeletableEntity
    {
        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }

        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }

        public override void Delete(T entity)
        {
            var databaseEntity = this.DatabaseStore.FirstOrDefault(x => x.Id.Equals(entity.Id));
            if (databaseEntity != null)
            {
                databaseEntity.IsDeleted = true;
                databaseEntity.DeletedOn = DateTime.Now;
            }
        }

        public void HardDelete(T entity)
        {
            base.Delete(entity);
        }

        public void Undelete(T entity)
        {
            var databaseEntity = this.DatabaseStore.FirstOrDefault(x => x.Id.Equals(entity.Id));
            if (databaseEntity != null)
            {
                databaseEntity.IsDeleted = false;
                databaseEntity.DeletedOn = null;
            }
        }
    }
}