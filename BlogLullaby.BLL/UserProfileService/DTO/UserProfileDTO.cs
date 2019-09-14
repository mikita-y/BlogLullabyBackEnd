using BlogLullaby.BLL.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.UserProfileService.DTO
{
    public class UserProfileDTO: DTOobject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя не установлено")]
        [StringLength(30,MinimumLength = 2, ErrorMessage = "asd")]
        public string Username { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string LastName { get; set; }
        public string PhotoUrl { get; set; }
        public string AvatarUrl { get; set; }
        [StringLength(50)]
        public string Specialization { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public DateTime LastVisit { get; set; }
    }
}
