using BlogLullaby.DAL.IdentityManager.Entities;
using BlogLullaby.DAL.IdentityManager.Infrastucture;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.DAL.IdentityManager.Interfaces
{
    public interface IIdentityManager
    {
        IQueryable<ApplicationUser> Users { get; }
        Task<IdentityDetails> Create(ApplicationUser user, string password);
        Task<IdentityDetails> CheckPassword(ApplicationUser user, string password);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IdentityDetails> DeleteUserAsync(ApplicationUser user);
        Task<string> GenerateEmailConfirmationTokenAsync(string email);
        Task<IdentityDetails> ConfirmEmailAsync(string email, string code);
    }
}
