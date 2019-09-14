using BlogLullaby.BLL.AuthenticationService.DTO;
using BlogLullaby.BLL.Infrastructure;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.AuthenticationService
{
    public interface IAuthenticationService
    {
        Task<OperationDetails> CreateUserAsync(RegistrationUserDTO userDto);
        Task<OperationDetails> AuthenticateAsync(LogInUserDTO userDto);
        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task<OperationDetails> ConfirmEmailAsync(string email, string code);
        Task<OperationDetails> ChangeUsernameAsync(string oldName, string newName);
    }
}
