using AutoMapper;
using BlogLullaby.DAL.DataStore.Entities;
using BlogLullaby.DAL.IdentityManager.Entities;

namespace BlogLullaby.BLL.AuthenticationService.DTO
{
    public static class UserDTOMapper
    {
        public static UserProfile GetUserProfile(this RegistrationUserDTO userDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegistrationUserDTO, UserProfile>();
            });
            var mapper = config.CreateMapper();
            var user = mapper.Map<RegistrationUserDTO, UserProfile>(userDTO);
            return user;
        }

        public static ApplicationUser GetApplicationUser(this RegistrationUserDTO userDTO)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RegistrationUserDTO, ApplicationUser>().
                ForMember("Username", opt => opt.MapFrom(x => x.Email));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<RegistrationUserDTO, ApplicationUser>(userDTO);
        }
    }
}
