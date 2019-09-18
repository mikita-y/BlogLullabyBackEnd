using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
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
        public async Task<IEnumerable<UserViewDTO>> GetPostsAsync(UserListCriterion criterion)
        {
            if (criterion == null)
                criterion = new UserListCriterion();
            if(criterion.Username != null)
                return await Task.Run(() =>
                    _dataStore.UserProfiles.GetAll()
                    .Where(x => x.Username.Contains(criterion.Username))
                    .Paging(criterion.PageNumber, criterion.PageSize)
                    .Select(x => new UserViewDTO(x)));
            else
                return await Task.Run(() =>
                    _dataStore.UserProfiles.GetAll()
                    .Paging(criterion.PageNumber, criterion.PageSize)
                    .Select(x => new UserViewDTO(x)));
        }
    }
}
