using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserCommunicatingService.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
