﻿using BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails;
using BlogLullaby.DAL.DataStore.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace BlogLullaby.DAL.DataStore.Interfaces
{
    public interface IRelationRepository<TEntity, Key1, Key2>  where TEntity: EntityWithCompositeKey<Key1, Key2>
    {
        IQueryable<TEntity> GetAll();
        TEntity Find(Key1 key1, Key2 key2);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<TEntity> FindAsync(Key1 key1, Key2 key2);
        Task<TEntity> CreateByCompositeKeyAsync(Key1 key1, Key2 key2);
        Task<DataStoreOperationDetails> DeleteByCompositeKeyAsync(Key1 key1, Key2 key2);
    }
}
