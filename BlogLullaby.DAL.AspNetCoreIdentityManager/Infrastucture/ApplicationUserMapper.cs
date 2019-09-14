using AutoMapper;
using BlogLullaby.DAL.IdentityManager.Entities;
using Microsoft.AspNetCore.Identity;

namespace logLullaby.DAL.AspNetCoreIdentityManager.Infrastucture
{
    internal static class ApplicationUserMapper
    {
        public static ApplicationUser Map(this IdentityUser identityUser)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<IdentityUser, ApplicationUser>().
                ForMember("Username", opt => opt.MapFrom(x => x.Email));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<IdentityUser, ApplicationUser>(identityUser);
        }

        public static IdentityUser Map(this ApplicationUser appUser)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ApplicationUser, IdentityUser>().
                ForMember("UserName", opt => opt.MapFrom(x => x.Email));
            });
            var mapper = config.CreateMapper();
            return mapper.Map<ApplicationUser, IdentityUser>(appUser);
        }
    }
}
