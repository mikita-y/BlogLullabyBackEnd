using BlogLullaby.DAL.IdentityManager.Entities;
using BlogLullaby.DAL.IdentityManager.Infrastucture;
using BlogLullaby.DAL.IdentityManager.Interfaces;
using BlogLullaby.DAL.AspNetCoreIdentityManager.Infrastucture;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace BlogLullaby.DAL.AspNetCoreIdentityManager.Repositories
{
    public class AspNetCoreIdentityManager :  IIdentityManager
    {
        private UserManager<IdentityUser> _aspNetCoreIdentityUserManager;
        public AspNetCoreIdentityManager(UserManager<IdentityUser> userManager)
        {
            _aspNetCoreIdentityUserManager = userManager;
        }

        public async Task<IdentityDetails> CheckPassword(ApplicationUser appUser, string password)
        {
            var user = appUser.Map();
            var identityUser = await Task.Run(() => _aspNetCoreIdentityUserManager.Users.FirstOrDefault(x => x.UserName == user.UserName));
            if (identityUser == null)
            {
                return new IdentityDetails { IsSuccess = false, ErrorList = new Error[] { new Error { Description = "Username is incorrect." } } };
            }
            var result = await _aspNetCoreIdentityUserManager.CheckPasswordAsync(identityUser, password);
            if (result == false)
            {
                return new IdentityDetails { IsSuccess = false, ErrorList = new Error[] { new Error { Description = "Password is incorrect." } } };
            }
            return new IdentityDetails { IsSuccess = result };
        }

        public async Task<IdentityDetails> Create(ApplicationUser appUser, string password)
        {
            var user = appUser.Map();
            var result = await _aspNetCoreIdentityUserManager.CreateAsync(user, password);
            return new IdentityDetails { IsSuccess = result.Succeeded, ErrorList = result.Errors.Select(x => new Error { Code = x.Code, Description = x.Description }) };
        }

        public IQueryable<ApplicationUser> Users
        {
            get
            {
                return _aspNetCoreIdentityUserManager.Users.Select(x => x.Map());
            }
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await Task.Run(() => Users.FirstOrDefault(x => x.Id == id));
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await Task.Run(() => Users.FirstOrDefault(x => x.Email == email));
        }

        public async Task<IdentityDetails> DeleteUserAsync(ApplicationUser appUser)
        {
            var user = await _aspNetCoreIdentityUserManager.FindByEmailAsync(appUser.Email);
            var result = await _aspNetCoreIdentityUserManager.DeleteAsync(user);
            return new IdentityDetails { IsSuccess = result.Succeeded, ErrorList = result.Errors.Select(x => new Error { Code = x.Code, Description = x.Description }) };
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var user = await _aspNetCoreIdentityUserManager.FindByEmailAsync(email);
            if (user == null)
                return null;
            var token = await _aspNetCoreIdentityUserManager.GenerateEmailConfirmationTokenAsync(user);
            return token;
        }

        public async Task<IdentityDetails> ConfirmEmailAsync(string email, string code)
        {
            var user = await _aspNetCoreIdentityUserManager.FindByEmailAsync(email);
            // возможно это уже предусмотрели в identity
            if(user == null)
            return new IdentityDetails
            {
                IsSuccess = false,
                ErrorList = new Error[] 
                { new Error
                    { Code = "notFound", Description = "User not found." }
                }
            };
            var result = await _aspNetCoreIdentityUserManager.ConfirmEmailAsync(user, code);
            return new IdentityDetails { IsSuccess = result.Succeeded, ErrorList = result.Errors.Select(x => new Error { Code = x.Code, Description = x.Description }) };
        }

        

    }
}
