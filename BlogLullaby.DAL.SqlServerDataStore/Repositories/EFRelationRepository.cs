using BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails;
using BlogLullaby.DAL.DataStore.Interfaces;
using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    /*
    public class EFRelationRepository<Entity, Key1, Key2> : IRelationRepository<Entity, Key1, Key2> where Entity : EntityWithCompositeKey<Key1, Key2>, new()
    {
        private readonly DbContext context;

        public EFRelationRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<DataStoreOperationDetails> DeleteByCompositeKeyAsync(Key1 key1, Key2 key2)
        {
            var details = new DataStoreOperationDetails();
            var entity = await context.Set<Entity>().FindAsync(key1, key2);
            if (entity == null)
            {
                details.AddDataStoreError(new DataStoreError().NotExist());
                return details;
            }
            await Task.Run(() =>
            {
                context.Set<Entity>().Remove(entity);
                context.SaveChanges();
            });
            return details;
        }

        public async Task<IQueryable<Entity>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return context.Set<Entity>();
            }); 
        }

        public async Task<Entity> CreateByCompositeKeyAsync(Key1 key1, Key2 key2)
        {
            var entity = new Entity()
            {
                FirstKey = key1,
                SecondKey = key2
            };
            var createdEntity = await context.Set<Entity>().AddAsync(entity);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return createdEntity.Entity;
        }
    }*/
}
