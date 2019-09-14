using BlogLullaby.DAL.DataStore.Entities;
using BlogLullaby.DAL.DataStore.Infrastructure.OperationDetails;
using BlogLullaby.DAL.DataStore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    public class EFRepository<T,V> : IRepository<T, V> where T : Entity<V>
    {

        protected DbContext context;
        protected IQueryable<T> Entities
        {
            get
            {
                return context.Set<T>();
            }
        }

        public EFRepository(DbContext context)
        {
            this.context = context;
        }


        public virtual IQueryable<T> GetAll()
        {
            return context.Set<T>();
        }

        public virtual async Task<IQueryable<T>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public virtual T GetById(V id)
        {
            return GetAll().FirstOrDefault(x => x.Id.Equals(id));
        }

        public async virtual Task<T> GetByIdAsync(V id)
        {
            return await Task.Run(() => GetById(id));
        }

        public virtual async Task<T> CreateAsync(T item)
        {
            var savedItem = await context.Set<T>().AddAsync(item);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return null;
            }
            return savedItem.Entity;
        }

        public virtual async Task<DataStoreOperationDetails> DeleteByIdAsync(V id)
        {
            var details = new DataStoreOperationDetails();
            var item = await context.Set<T>().FindAsync(id);
            if (item == null)
            {
                details.AddDataStoreError(new DataStoreError().NotExist());
                return details;
            }
            await Task.Run(() => 
            {
                context.Set<T>().Remove(item);
                context.SaveChanges();
            });
            return details;
        }

        public virtual async Task<DataStoreOperationDetails> UpdateAsync(T item)
        {
            try
            {
                await Task.Run(() =>
                {
                    context.Entry(item).State = EntityState.Modified;
                    context.SaveChanges();
                });
            }
            catch(DbUpdateException)
            {
                return new DataStoreOperationDetails() { IsSuccess = false};
            } 
            return new DataStoreOperationDetails();
        }

    }
}
