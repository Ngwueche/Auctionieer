using Auctionieer.Domain.Models;

namespace Auctionieer.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        public IEnumerable<TEntity> GetAll();
        public Task Add(TEntity entity);
        public Task<TEntity> FindById(Guid id);


    }
}
