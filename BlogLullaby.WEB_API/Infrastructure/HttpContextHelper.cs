using Microsoft.AspNetCore.Http;
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
            await ((OnlineRefresher)onlineRefresher).RefreshAsync(name);
            return name;
        }
    }
}
