using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserProfileService.DTO;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.UserProfileService
{
    public interface IUserProfileService
    {
        Task<OperationDetails> UpdateProfileAsync(UserProfileDTO updatedProfile);
        Task<UserProfileDTO> GetProfileByNameAsync(string userName);
    }
}
