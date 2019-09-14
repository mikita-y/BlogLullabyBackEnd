using BlogLullaby.BLL.UserProfileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.WEB_API.Infrastructure
{
    public static class HttpContextHelper
    {
        public static async Task<string> GetUserNameAsync(this HttpContext context)
        {
            var name = context.User.Claims.Where(x => x.Type == "username").SingleOrDefault().Value;
            var onlineRefresher = context.RequestServices.GetService(typeof (OnlineRefresher));
            await ((OnlineRefresher)onlineRefresher).Refresh(name);
            return name;
        }
    }
}
