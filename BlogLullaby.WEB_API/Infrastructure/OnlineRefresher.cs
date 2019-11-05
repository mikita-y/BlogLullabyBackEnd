using BlogLullaby.BLL.UserProfileService;
using System;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Infrastructure
{
    public class OnlineRefresher
    {
        private IUserProfileService _userProfileService;

        public OnlineRefresher(IUserProfileService service)
        {
            _userProfileService = service;
        }

        public async Task RefreshAsync(string username)
        {
            var user = await _userProfileService.GetProfileByNameAsync(username);
            if(user != null)
            {
                user.LastVisit = DateTime.Now;
                var a = await _userProfileService.UpdateProfileAsync(user);
            }
        }
    }
}
