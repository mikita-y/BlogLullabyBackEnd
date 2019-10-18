using BlogLullaby.BLL.AuthenticationService.DTO;
using BlogLullaby.BLL.Infrastructure;
using System.Threading.Tasks;
using System.Linq;
using BlogLullaby.DAL.IdentityManager.Interfaces;
using BlogLullaby.DAL.DataStore.Interfaces;

namespace BlogLullaby.BLL.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private IIdentityManager userManager;
        private IDataStore dataStore;
        public AuthenticationService (IIdentityManager userManager, IDataStore dataStore)
        {
            this.dataStore = dataStore;
            this.userManager = userManager;
        }

        public async Task<OperationDetails> AuthenticateAsync(LogInUserDTO userDTO)
        {
            if (!userDTO.IsValid())
            {
                return userDTO.GetValidateError();
            }
            var applicationUser = await userManager.GetUserByEmailAsync(userDTO.Login);
            if (applicationUser == null)
            {
                var userProfile = await dataStore.UserProfiles.GetByNameAsync(userDTO.Login);
                if(userProfile == null)
                    return new OperationDetails(false, new string[] { "Username not found."});
                applicationUser = await userManager.GetUserByIdAsync(userProfile.IdentityUserId);
                if(applicationUser == null)
                    return new OperationDetails(false, new string[] { "Error in system logic." });
            }
            var result = await userManager.CheckPassword(applicationUser, userDTO.Password);
            if (!result.IsSuccess)
            {
                return new OperationDetails(false, result.ErrorList.Select(x => x.Description));
            }
            else
            {
                ///включение подтверждения пароля
                if(applicationUser.EmailConfirmed)
                    return new OperationDetails(true);
                else
                    return new OperationDetails(false, new string[] { "Email not confirmed." });
            }
        }

        public async Task<OperationDetails> CreateUserAsync(RegistrationUserDTO userDTO)
        {
            if (!userDTO.IsValid())
            {
                return userDTO.GetValidateError();
            }
            var dbUser = await dataStore.UserProfiles.GetByNameAsync(userDTO.UserName);
            if(dbUser != null)
                return new OperationDetails(false, new string[] { "This username used yet."});
            var applicationUser = userDTO.GetApplicationUser();
            var result = await userManager.Create(applicationUser, userDTO.Password);
            if (!result.IsSuccess)
            {
                return new OperationDetails(false, result.ErrorList.Select(x => x.Description));
            }
            var userProfile = userDTO.GetUserProfile();
            userProfile.IdentityUserId = (await userManager.GetUserByEmailAsync(applicationUser.Email)).Id;
            var createdProfile = await dataStore.UserProfiles.CreateAsync(userProfile);
            if(createdProfile == null)
            {
                var errorUser = await userManager.GetUserByEmailAsync(applicationUser.Username);
                var res = await userManager.DeleteUserAsync(errorUser);
                return new OperationDetails(false, new string[] { "Some error, try another data." });
            }
            return new OperationDetails(true);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(email);
            return token;
        }

        public async Task<OperationDetails> ConfirmEmailAsync(string email, string code)
        {
            var result = await userManager.ConfirmEmailAsync(email, code);
            return new OperationDetails(result.IsSuccess, result.ErrorList?.Select(x => x.Description));
        }

        public async Task<OperationDetails> ChangeUsernameAsync(string oldName, string newName)
        {
            var profile = await dataStore.UserProfiles.GetByNameAsync(oldName);
            profile.Username = newName;
            var result = await dataStore.UserProfiles.UpdateAsync(profile);
            if (!result.IsSuccess)
                return new OperationDetails(result.IsSuccess, result.DataStoreErrors.Select(x => x.Description));
            else
                return new OperationDetails(result.IsSuccess);
        }
    }
}
