using AutoMapper;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.PostService.DTO
{
    public static class PostDTOMapper
    {
        public static Post MapToEntity(this PostDTO postDTO, UserProfile profile)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PostDTO, Post>()
                .ForMember("UserProfile", opt => opt.MapFrom(x => profile))
                .ForMember("BodyBlocks", opt => opt.MapFrom(y => 
                    y.BodyBlocks.Select(x => new PostBodyBlock()
                    {
                        Position = x.Position,
                        Content = x.Content,
                        BlockType = x.BlockType == "text" ? PostBodyBlockType.Text : PostBodyBlockType.Image
                    })
                ));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<PostDTO, Post>(postDTO);
        }

        public static PostDTO MapToDTO(this Post post)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostDTO>()
                .ForMember("Author", opt => opt.MapFrom(x => new UserViewDTO(x.UserProfile)))
                .ForMember("BodyBlocks", opt => opt.MapFrom(x => 
                    x.BodyBlocks
                        .Select(y => new PostBodyBlockDTO()
                        {
                            Position = y.Position,
                            Content = y.Content,
                            BlockType = y.BlockType == PostBodyBlockType.Text ? "text" : "image"
                        })
                        .OrderBy(y => y.Position)
                ));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Post, PostDTO>(post);
        }
    }
}
