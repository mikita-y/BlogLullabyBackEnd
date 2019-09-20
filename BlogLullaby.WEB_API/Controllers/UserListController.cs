using System.Threading.Tasks;
using BlogLullaby.BLL.UserListService;
using Microsoft.AspNetCore.Mvc;

namespace BlogLullaby.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserListController : ControllerBase
    {
        private IUserListService _userListService;

        public UserListController(IUserListService service)
        {
            _userListService = service;
        }

        // GET: api/UserList
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _userListService.GetUsersAsync(null));
        }

        // POST: api/UserList
        [HttpPost]
        public async Task<ActionResult> Post(UserListCriterion criterion)
        {
            return Ok(await _userListService.GetUsersAsync(criterion));
        }
    }
}
