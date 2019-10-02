using System.Collections.Generic;
using System.Threading.Tasks;
using BlogLullaby.BLL.UserCommunicatingService;
using BlogLullaby.WEB_API.Infrastructure;
using BlogLullaby.WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogLullaby.WEB_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IUserCommunicatingService _communicatingService;

        public MessageController(IUserCommunicatingService service)
        {
            _communicatingService = service;
        }

        // GET: api/Message
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Message
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

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

        // PUT: api/Message/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id)
        {
            var username = await HttpContext.GetUserNameAsync();
            if (username == null)
                return BadRequest(new string[] { "Error with claim!!!" });
            await _communicatingService.ReadMessageAsync(id, username);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
