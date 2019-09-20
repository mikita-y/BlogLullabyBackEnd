using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.UserListService.DTO;
using BlogLullaby.DAL.DataStore.Interfaces;

namespace BlogLullaby.BLL.UserListService
{
    public class UserListService : IUserListService
    {
        private IDataStore _dataStore;
        public UserListService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<IEnumerable<UserProfilePreviewDTO>> GetUsersAsync(UserListCriterion criterion)
        {
            if (criterion == null)
                criterion = new UserListCriterion();
            return await Task.Run(() => 
                _dataStore.UserProfiles.GetAll()
                .Filtering(criterion)
                .Sorting(criterion)
                .Paging(criterion.PageNumber, criterion.PageSize)
                .Select(x => x.MapToDTO()));
        }
    }
}
