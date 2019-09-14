using BlogLullaby.DAL.DataStore.Entities;
using BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.DAL.DataStore.Interfaces
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IQueryable<TEntity> GetAll();
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<TEntity> CreateAsync(TEntity item);
        Task<DataStoreOperationDetails> UpdateAsync(TEntity item);
        Task<DataStoreOperationDetails> DeleteByIdAsync(TKey id);
    }
}
