using BlogLullaby.BLL.PostPreviewListService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.PostPreviewListService
{
    public interface IPostPreviewListService
    {
        Task<IEnumerable<PostPreviewDTO>> GetPostsAsync(PostListCriterion criterion);
    }
}
