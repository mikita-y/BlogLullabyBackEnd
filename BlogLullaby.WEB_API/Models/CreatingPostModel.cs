using BlogLullaby.BLL.PostService.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace BlogLullaby.WEB_API.Models
{
    public class CreatingPostModel
    {
        public string Title { get; set; }
        public IFormFile MainImage { get; set; }
        public IEnumerable<PostBodyBlock> BodyBlocks { get; set; }
        public SubjectArea SubjectArea { get; set; }
    }

    public class PostBodyBlock
    {
        public string Type { get; set; }
        public string Text { get; set; }
        public IFormFile Image { get; set; }
    }

    public enum SubjectArea { IT, Sport, News, Entertainment, Education, Culture, Beauty, Religion, Medicine, Another }
}
