using BlogLullaby.BLL.UserProfileService;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Refresh(string username)
        {
            var user = await _userProfileService.GetProfileByNameAsync(username);
            user.LastVisit = DateTime.Now;
            var a = await _userProfileService.UpdateProfileAsync(user);
        }
    }
}
