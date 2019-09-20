using AutoMapper;
using BlogLullaby.DAL.DataStore.Entities;
using System.Linq;

namespace BlogLullaby.BLL.UserProfileService.DTO
{
    public static class UserProfileDTOMapper
    {
        /////// using mapper
        public static UserProfileDTO MapToDTO(this UserProfile entity)
        {
            if (entity == null)
                return null;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserProfile, UserProfileDTO>()
                .ForMember("TotalVisits", opt => opt.MapFrom(x => x.Posts.Sum(y => y.Visits)));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<UserProfile, UserProfileDTO>(entity);
        }

        public static UserProfile MapToEntity(this UserProfileDTO dto)
        {
            if (dto == null)
                return null;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map<UserProfileDTO, UserProfile>(dto);
        }

        public static UserProfile MapToEntity(this UserProfileDTO dto, UserProfile profile)
        {
            if (dto == null)
                return null;
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper.Map(dto, profile);
        }
    }
}
