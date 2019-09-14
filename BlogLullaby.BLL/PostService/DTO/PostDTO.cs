using BlogLullaby.BLL.Infrastructure;
using System;
using System.Collections.Generic;

namespace BlogLullaby.BLL.PostService.DTO
{
    public class PostDTO: DTOobject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<PostBodyBlockDTO> BodyBlocks { get; set; }
        public string MainImageUrl { get; set; }
        public DateTime Date { get; set; }
        public int Visits { get; set; }
        public UserViewDTO Author { get; set; }
    }

    public class PostBodyBlockDTO
    {
        public int Position { get; set; }
        public string BlockType { get; set; }
        public string Content { get; set; }
    }
}