using BlogLullaby.DAL.DataStore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.DAL.SqlServerDataStore.Repositories
{
    public class DialogRepository : EFRepository<Dialog, string>
    {
        public DialogRepository(DbContext context)
            : base(context)
        { }

        public override IQueryable<Dialog> GetAll()
        {
            return 
                context.Set<Dialog>()
                .Include(x => x.Messages)
                .ThenInclude(x => x.UserProfile)
                .Include(x => x.DialogMembers)
                .ThenInclude(x => x.UserProfile);
        }
        
        public async override Task<Dialog> CreateAsync(Dialog dialog)
        {
            IEnumerable<DialogMember> dialogMembers = dialog.DialogMembers;
            dialog.DialogMembers = null;
            await context.Set<DialogMember>().AddRangeAsync(dialogMembers);
            var savedItem = await context.Set<Dialog>().AddAsync(dialog);
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
    }
}
