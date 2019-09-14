using AutoMapper;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.DAL.DataStore.Entities;

namespace BlogLullaby.BLL.PostPreviewListService.DTO
{
    public static class PostPreViewDTOMapper
    {
        public static PostPreviewDTO GetDTO(this Post post)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Post, PostPreviewDTO>()
                .ForMember("Author", opt => opt.MapFrom(x => new UserViewDTO(x.UserProfile)));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<Post, PostPreviewDTO>(post);
        }
    }
}
