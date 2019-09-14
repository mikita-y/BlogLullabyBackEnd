using BlogLullaby.DAL.SqlServerDataStore.Context;
using BlogLullaby.DAL.SqlServerDataStore.Repositories;

namespace BlogLullaby.DAL.DataStore.Repositories
{
    public class SqlServerDataStore: EFDataStore<SqlServerContext>
    {
        public SqlServerDataStore(SqlServerContext context)
            : base(context)
        {
        }
    }
}
