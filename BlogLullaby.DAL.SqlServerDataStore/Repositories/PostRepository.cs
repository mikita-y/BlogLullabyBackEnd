using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    public class PostRepository: EFRepository<Post, int>
    {
        public PostRepository(DbContext context) 
            :base(context)
        {}

        public override IQueryable<Post> GetAll()
        {
            return
                context.Set<Post>()
                .Include(x => x.BodyBlocks)
                .Include(x => x.UserProfile);
        }
    }
}
