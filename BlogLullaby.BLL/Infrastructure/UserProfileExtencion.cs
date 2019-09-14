using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.DAL.DataStore.Interfaces;
using BlogLullaby.DAL.DataStore.Entities;

namespace BlogLullaby.BLL.Infrastructure
{
    public static class UserProfileExtencion
    {
        public static async Task<UserProfile> GetByNameAsync(this IRepository<UserProfile, int> repository, string name)
        {
            return await Task.Run(() => repository.GetAll().FirstOrDefault(x => x.Username == name));
        }
    }
}
