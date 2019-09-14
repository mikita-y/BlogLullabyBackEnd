using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    public class MessageRepository : EFRepository<Message, string>
    {
        public MessageRepository(DbContext context)
            : base(context)
        { }

        public override IQueryable<Message> GetAll()
        {
            return 
                context.Set<Message>()
                .Include(x => x.UserProfile);
        }
    }
}
