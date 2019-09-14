using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.UserCommunicatingService;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using BlogLullaby.WEB_API.Infrastructure;
using BlogLullaby.WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogLullaby.WEB_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicatingController : ControllerBase
    {
        private IUserCommunicatingService _communicatingService;

        public CommunicatingController(IUserCommunicatingService service)
        {
            _communicatingService = service;
        }
        // GET: api/Communicating
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var criterion = new DialogCriterion() { PageNumber = 0, PageSize = 10 };
            var username = HttpContext.User.Claims.Where(x => x.Type == "username").SingleOrDefault().Value;
            if (username == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            var dialogList = await _communicatingService.GetDialogListByUserNameAsync(username, criterion);
            return Ok(dialogList);
        }

        // GET: api/Communicating/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var dialog = await _communicatingService.GetDialogByIdAsync(id);
            if(dialog == null)
                return NotFound(new string[] { "Dialog not found." });
            return Ok(dialog);

        }

        // POST: api/Communicating
        [HttpPost]
        [Route("sendmessage")]
        public async Task<IActionResult> SendMessage(CreatingMessageModel message)
        {
            var username = await HttpContext.GetUserNameAsync();
            if (username == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            var messageDto = message.MapToDTO(username);
            var result = await _communicatingService.AddMessageToDialogAsync(messageDto);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Post(DialogCreatingDTO dialog)
        {
            var username = await HttpContext.GetUserNameAsync();
            if (username == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            var list = new List<string>(dialog.Members);
            list.Add(username);
            dialog.Members = list;
            var result = await _communicatingService.CreateDialogAsync(dialog);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            return Ok();
        }


        // PUT: api/Communicating/5
        [HttpPut]
        public async Task<IActionResult> Put(DialogUpdatingModel dialog)
        {
            var result = await _communicatingService.AddMemberToDialogAsync(dialog.Id, dialog.AddingMember);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
