using BlogLullaby.BLL.Infrastructure;
using System;
using System.Collections.Generic;

namespace BlogLullaby.BLL.UserCommunicatingService.DTO
{
    public class DialogDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
        public IEnumerable<UserViewDTO> Members { get; set; }
    }
}
