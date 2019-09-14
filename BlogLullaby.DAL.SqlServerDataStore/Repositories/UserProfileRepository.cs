using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    class UserProfileRepository: EFRepository<UserProfile, int>
    {
        public UserProfileRepository(DbContext context)
            : base(context)
        { }


        public override IQueryable<UserProfile> GetAll()
        {
            return context.Set<UserProfile>()
                .Include(x => x.Posts)
                .Include(x => x.DialogMembers)
                .ThenInclude(x => x.Dialog)
                .ThenInclude(x => x.Messages)
                .ThenInclude(x => x.UserProfile);
        }
    }
}
