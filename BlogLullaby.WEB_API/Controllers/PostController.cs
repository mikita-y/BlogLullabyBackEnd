using System.Threading.Tasks;
using BlogLullaby.BLL.PostService;
using BlogLullaby.BLL.PostService.DTO;
using BlogLullaby.WEB_API.Infrastructure;
using BlogLullaby.WEB_API.ModelMappers;
using BlogLullaby.WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogLullaby.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private IPostService _postService;
        private FileSavingHelper _fileSavingHelper;

        public PostController(IPostService service, FileSavingHelper fileSavingHelper)
        {
            _postService = service;
            _fileSavingHelper = fileSavingHelper;
        }

        // GET: api/Post/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();
            await _postService.AddVisitToPostByIdAsync(id);
            return Ok(post);
        }

        // POST: api/Post
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CreatingPostModelCrutch newPost)
        {

            ///////////////////////////

            var claim = await HttpContext.GetUserNameAsync();
            var creatingPostModel = newPost.MapToCreatingPostModel();
            var postDto = creatingPostModel.MapToDTO(claim, _fileSavingHelper);

            var details = await _postService.CreatePostAsync(postDto);
            if (!details.IsSuccess)
                return BadRequest(details.Descriptions);
            var postId = ((string[])details.Descriptions)[0];
            return Ok(postId);
        }

        // PUT: api/Post
        [HttpPut]
        public async Task<IActionResult> Put(PostDTO updatedPost)
        {
            var result = await _postService.UpdatePostAsync(updatedPost);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var description = await _postService.DeletePostByIdAsync(id);
            if (!description.IsSuccess)
                return BadRequest(description.Descriptions);
            return Ok();

        }
    }
}
