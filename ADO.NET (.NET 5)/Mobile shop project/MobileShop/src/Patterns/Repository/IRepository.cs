using System;

namespace Patterns.Repository
{
    public interface IRepository<TEntity>
        where TEntity : IEntity
    {
        public void Create(TEntity item);

        public void Update(Int32 id, TEntity item);

        public void Delete(Int32 id);
    }
}