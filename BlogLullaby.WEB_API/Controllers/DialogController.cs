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
    public class DialogController : ControllerBase
    {
        private IUserCommunicatingService _communicatingService;

        public DialogController(IUserCommunicatingService service)
        {
            _communicatingService = service;
        }
        // GET: api/Communicating
        [HttpGet]
        public async Task<ActionResult<(IEnumerable<DialogPreview> dialogList, int pageCount)>> Get()
        {
            var criterion = new DialogCriterion()
            {
                PageNumber = 0,
                PageSize = 10,
                Username = await HttpContext.GetUserNameAsync()
            };
            if (criterion.Username == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            var list = await _communicatingService.GetDialogListAsync(criterion);
            return Ok(list);
        }

        [HttpPost]
        [Route("create")]
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

        [HttpPost]
        public async Task<ActionResult<(IEnumerable<DialogPreview> dialogList, int pageCount)>> Post(DialogCriterion criterion)
        {
            var username = await HttpContext.GetUserNameAsync();
            if (username == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            criterion.Username = username;
            var list = await _communicatingService.GetDialogListAsync(criterion);
            return Ok(list);
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            var dialog = await _communicatingService.GetDialogByIdAsync(id);
            if(dialog == null)
                return NotFound(new string[] { "Dialog not found." });
            return Ok(dialog);

        }



        [HttpPut]
        public async Task<IActionResult> Put(DialogUpdatingModel dialog)
        {
            var result = await _communicatingService.AddMemberToDialogAsync(dialog.Id, dialog.AddingMember);
            if (!result.IsSuccess)
                return BadRequest(result.Descriptions);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {            
            return Ok();
        }*/
    }
}
