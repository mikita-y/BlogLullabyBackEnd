using AutoMapper;
using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.UserListService.DTO
{
    public static class UserProfilePreviewDTOMapper
    {
        public static UserProfilePreviewDTO MapToDTO(this UserProfile entity)
        {
            if (entity == null)
                return null;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserProfile, UserProfilePreviewDTO>()
                .ForMember("FullName", opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"))
                .ForMember("TotalVisits", opt => opt.MapFrom(x => x.Posts.Sum(y => y.Visits)));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<UserProfile, UserProfilePreviewDTO>(entity);
        }

    }
}
