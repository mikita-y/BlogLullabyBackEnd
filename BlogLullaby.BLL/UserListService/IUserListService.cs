using BlogLullaby.BLL.UserListService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.UserListService
{
    public interface IUserListService
    {
        Task<IEnumerable<UserProfilePreviewDTO>> GetUsersAsync(UserListCriterion criterion);
    }
}
