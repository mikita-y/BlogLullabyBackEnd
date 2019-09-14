using BlogLullaby.BLL.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace BlogLullaby.BLL.AuthenticationService.DTO
{
    public class LogInUserDTO : DTOobject
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
