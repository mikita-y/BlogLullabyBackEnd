using BlogLullaby.DAL.DataStore.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.Infrastructure
{
    public class UserViewDTO
    {
        [Required]
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime LastVisit { get; set; }


        public UserViewDTO()
        { }
        public UserViewDTO(UserProfile userProfile)
        {
            if(userProfile != null)
            {
                Username = userProfile.Username;
                AvatarUrl = userProfile.AvatarUrl;
                LastVisit = userProfile.LastVisit;
            }
        }
    }
}
