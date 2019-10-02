using BlogLullaby.BLL.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public class MessageDTO : DTOobject
    {
        public string Id { get; set; }
        [Required]
        public string Body { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public string DialogId { get; set; }
        [Required(ErrorMessage = "Not correct message sender.")]
        public UserViewDTO Sender { get; set; }
        public bool IsRead { get; set; }

    }
}
