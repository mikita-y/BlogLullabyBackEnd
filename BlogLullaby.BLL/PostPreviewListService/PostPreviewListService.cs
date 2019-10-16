using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.PostPreviewListService.DTO;
using BlogLullaby.DAL.DataStore.Interfaces;

namespace BlogLullaby.BLL.PostPreviewListService
{
    public class PostPreviewListService : IPostPreviewListService
    {
        private IDataStore _dataStore;
        public PostPreviewListService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<IEnumerable<PostPreviewDTO>> GetPostsAsync(PostListCriterion criterion)
        {
            if (criterion == null)
                criterion = new PostListCriterion();
            return await Task.Run(() =>
            _dataStore.Posts.GetAll()
                .Filtering(criterion)
                .Sorting(criterion)
                .Paging(criterion.PageNumber, criterion.PageSize)
                .Select(x => x.GetDTO()));
        }
    }
}
