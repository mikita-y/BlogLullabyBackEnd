using BlogLullaby.BLL.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.AuthenticationService.DTO
{
    public class RegistrationUserDTO : DTOobject
    {
        [Required(ErrorMessage = "Имя не установлено")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "asd")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email не установлено")]
        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is requered")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
