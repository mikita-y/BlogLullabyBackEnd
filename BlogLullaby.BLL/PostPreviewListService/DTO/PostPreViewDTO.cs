using BlogLullaby.BLL.Infrastructure;
using System;

namespace BlogLullaby.BLL.PostPreviewListService.DTO
{
    public class PostPreviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string MainImageUrl { get; set; }
        public DateTime Date { get; set; }
        public int Visits { get; set; }
        public UserViewDTO Author { get; set; }
    }
}
