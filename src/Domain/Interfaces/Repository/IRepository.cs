using System.Collections.Generic;

namespace Domain.Interfaces.Repository
{
    public interface IRepository<TEntity>
    {
        public void Save(TEntity entity);
        public ICollection<TEntity> List();
        public TEntity FindById(long id);
        public void Update(TEntity entity);
        public void Remove(TEntity entity);
    }
}
