using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.WEB_API.Models
{
    public class CreatingMessageModel
    {
        [Required]
        public string Body { get; set; }
        [Required]
        public string DialogId { get; set; }

        public MessageDTO MapToDTO(string username)
        {
            var dto = new MessageDTO
            {
                Id = Guid.NewGuid().ToString(),
                Body = this.Body,
                Date = DateTime.Now,
                DialogId = this.DialogId,
                Sender = new UserViewDTO
                {
                    Username = username
                }
            };
            return dto;
        }
    }
}
