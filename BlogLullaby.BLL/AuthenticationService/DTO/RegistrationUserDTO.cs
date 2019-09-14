using BlogLullaby.BLL.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.AuthenticationService.DTO
{
    public class RegistrationUserDTO : DTOobject
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
