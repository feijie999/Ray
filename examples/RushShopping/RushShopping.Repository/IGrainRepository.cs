using System;
using System.Threading.Tasks;

namespace RushShopping.Repository
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    public interface IGrainRepository<TEntity, in TPrimaryKey> : IDisposable where TEntity : class, IEntity<TPrimaryKey>
    {
        TEntity FirstOrDefault(TPrimaryKey id);

        Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id);

        void Insert(TEntity entity);

        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TPrimaryKey entity);

        void Commit();

        Task CommitAsync();
    }
}