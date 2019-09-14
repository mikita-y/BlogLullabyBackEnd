using BlogLullaby.BLL.UserProfileService.DTO;
using BlogLullaby.BLL.Infrastructure;
using System.Threading.Tasks;
using BlogLullaby.DAL.DataStore.Interfaces;
using System;

namespace BlogLullaby.BLL.UserProfileService
{
    public class UserProfileService : IUserProfileService
    {
        private IDataStore _dataStore;
        public UserProfileService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<UserProfileDTO> GetProfileByNameAsync(string userName)
        {
            var userProfile = await _dataStore.UserProfiles.GetByNameAsync(userName);
            return userProfile.MapToDTO();
        }

        public async Task<OperationDetails> UpdateProfileAsync(UserProfileDTO updatedProfile)
        {
            if (!updatedProfile.IsValid())
                return updatedProfile.GetValidateError();
            var userProfile = await _dataStore.UserProfiles.GetByIdAsync(updatedProfile.Id);
            if (userProfile == null)
                return new OperationDetails(false, new string[] { "User profile not found." });
            var entity = updatedProfile.MapToEntity(userProfile);
            var result = await _dataStore.UserProfiles.UpdateAsync(entity);
            if(result.IsSuccess == false)
                return new OperationDetails(false, new string[] { "Execute not success." });
            return new OperationDetails(true);
        }
    }
}
