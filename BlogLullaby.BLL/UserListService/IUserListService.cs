using BlogLullaby.BLL.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.UserListService
{
    public interface IUserListService
    {
        Task<IEnumerable<UserViewDTO>> GetPostsAsync(UserListCriterion criterion);
    }
}
