using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.PostService.DTO
{
    public static class PostDTOMapper
    {
        public static Post MapToEntity(this PostDTO postDTO, UserProfile profile)
        {
            return new Post()
            {
                Title = postDTO.Title,
                BodyBlocks = postDTO.BodyBlocks.Select(x => new PostBodyBlock()
                {
                    Position = x.Position,
                    Content = x.Content,
                    BlockType = x.BlockType == "text" ? PostBodyBlockType.Text : PostBodyBlockType.Image
                }).ToArray(),
                MainImageUrl = postDTO.MainImageUrl,
                Visits = postDTO.Visits,
                UserProfile = profile,
                Date = postDTO.Date
            };
        }

        public static PostDTO MapToDTO(this Post post)
        {
            return new PostDTO()
            {
                Title = post.Title,
                BodyBlocks = post?.BodyBlocks?
                .Select(x => new PostBodyBlockDTO()
                {
                    Position = x.Position,
                    Content = x.Content,
                    BlockType = x.BlockType == PostBodyBlockType.Text ? "text" : "image"
                })
                .OrderBy(x => x.Position),
                MainImageUrl = post.MainImageUrl,
                Visits = post.Visits,
                Author = new UserViewDTO(post.UserProfile),
                Date = post.Date
            };
        }
    }
}
