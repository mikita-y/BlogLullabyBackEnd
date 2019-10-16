using System.Threading.Tasks;
using BlogLullaby.BLL.PostPreviewListService;
using Microsoft.AspNetCore.Mvc;

namespace BlogLullaby.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostListController : Controller
    {

        private IPostPreviewListService _postListService;

        public PostListController(IPostPreviewListService service)
        {
            _postListService = service;
        }

        // GET: api/PostList
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _postListService.GetPostsAsync(null);
            if (list == null)
                return BadRequest("Some Errors on server.");
            return Ok(list);
        }

        // Post: api/PostList
        [HttpPost]
        public async Task<IActionResult> Post(PostListCriterion criterion)
        {
            var list = await _postListService.GetPostsAsync(criterion);
            if (list == null)
                return BadRequest();
            return Ok(list);
        }
    }
}
