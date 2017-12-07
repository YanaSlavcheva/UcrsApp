namespace Ucrs.Data.Common.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Ucrs.Data.Common.Models;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDeletableEntity
    {
        IQueryable<TEntity> AllWithDeleted();

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
