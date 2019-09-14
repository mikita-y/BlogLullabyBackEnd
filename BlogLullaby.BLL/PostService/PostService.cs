using System.Threading.Tasks;
using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.PostService.DTO;
using BlogLullaby.DAL.DataStore.Interfaces;

namespace BlogLullaby.BLL.PostService
{
    public class PostService : IPostService
    {
        private IDataStore _dataStore;
        public PostService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<OperationDetails> CreatePostAsync(PostDTO postDTO)
        {
            var profile = await _dataStore.UserProfiles.GetByNameAsync(postDTO.Author.Username);
            if (profile == null)
                return new OperationDetails(false, new string[] { "User profile not find." });
            var post = postDTO.MapToEntity(profile);
                var newPost = await _dataStore.Posts.CreateAsync(post);
                if (newPost == null)
                    return new OperationDetails(false, new string[] { "Post not created." });
            return new OperationDetails(true, new string[] { $"{newPost.Id}" });
        }

        public async Task<OperationDetails> DeletePostByIdAsync(int postId)
        {
            var detail = await _dataStore.Posts.DeleteByIdAsync(postId);
            if(detail.IsSuccess == false)
                return new OperationDetails(false, new string[] { "Execute not success." });
            return new OperationDetails(true);
        }

        public async Task<PostDTO> GetPostByIdAsync(int id)
        {
            var post = await _dataStore.Posts.GetByIdAsync(id);
            if (post == null)
                return null;
            var postDTO = post.MapToDTO();
            return postDTO;
        }

        public async Task<OperationDetails> AddVisitToPostByIdAsync(int id)
        {
            var post = await _dataStore.Posts.GetByIdAsync(id);
            if(post == null)
                return new OperationDetails(false, new string[] { "Post not found." });
            post.Visits++;
            var detail = await _dataStore.Posts.UpdateAsync(post);
            if(detail.IsSuccess == false)
                return new OperationDetails(false, new string[] { "Execute not success." });
            return new OperationDetails(true);
        }

        public Task<OperationDetails> UpdatePostAsync(PostDTO postDTO)
        {
            throw new System.NotImplementedException();
        }
    }
}
