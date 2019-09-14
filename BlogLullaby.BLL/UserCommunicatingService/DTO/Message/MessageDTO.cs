using BlogLullaby.BLL.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public class MessageDTO : DTOobject
    {
        [Required]
        public string Body { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string DialogId { get; set; }
        [Required(ErrorMessage = "Not correct message sender.")]
        public UserViewDTO Owner { get; set; }

    }
}
