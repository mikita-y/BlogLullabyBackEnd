using BlogLullaby.BLL.Infrastructure;
using BlogLullaby.BLL.PostService.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogLullaby.BLL.PostService
{
    public interface IPostService
    {
        Task<PostDTO> GetPostByIdAsync(int id);
        Task<OperationDetails> CreatePostAsync(PostDTO postDTO);
        Task<OperationDetails> DeletePostByIdAsync(int postId);
        Task<OperationDetails> AddVisitToPostByIdAsync(int postId);
        Task<OperationDetails> UpdatePostAsync(PostDTO postDTO);
    }
}
