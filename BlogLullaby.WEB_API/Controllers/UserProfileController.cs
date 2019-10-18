using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.UserProfileService;
using BlogLullaby.BLL.UserProfileService.DTO;
using BlogLullaby.WEB_API.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogLullaby.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private IUserProfileService _userProfileService;
        private FileSavingHelper _fileSavingHelper;

        public UserProfileController(IUserProfileService service, FileSavingHelper fileSavingHelper)
        {
            _userProfileService = service;
            _fileSavingHelper = fileSavingHelper;
        }

        // GET: api/UserProfile/name
        [AllowAnonymous]
        [HttpGet("{username}")]
        public async Task<ActionResult> Get(string username)
        {
            var user = await _userProfileService.GetProfileByNameAsync(username);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // PUT: api/UserProfile/5
        // update text fields
        [HttpPut]
        public async Task<ActionResult> Put(UserProfileDTO profile)
        {
            var details = await _userProfileService.UpdateProfileAsync(profile);
            if (!details.IsSuccess)
                return BadRequest();
            return Ok();
        }

        [HttpPost("{property}")]
        //[Route("update-user-photo")]
        public async Task<ActionResult> UpdateUserPhoto([FromRoute]string property, [FromForm]IFormFile body)
        {
            if(property != "avatar" && property != "photo")
                return BadRequest(new string[] { "URL not correct." });
            var claimName = await HttpContext.GetUserNameAsync();
            var profile = await _userProfileService.GetProfileByNameAsync(claimName);
            if (profile == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            var photoUrl = await _fileSavingHelper.SaveFormFile(body, "userPhotos");

            if (photoUrl == null)
                return BadRequest(new string[] { "Cant save file, try again." });
            switch(property)
            {
                case "avatar":
                    await _fileSavingHelper.DeleteFileAsync(profile.AvatarUrl);
                    profile.AvatarUrl = photoUrl;
                    break;
                case "photo":
                    await _fileSavingHelper.DeleteFileAsync(profile.PhotoUrl);
                    profile.PhotoUrl = photoUrl;
                    break;
            }
            var details = await _userProfileService.UpdateProfileAsync(profile);
            if (!details.IsSuccess)
                return BadRequest(details.Descriptions);
            return Ok();
        }
    }
}
