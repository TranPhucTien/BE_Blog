using Blog.Core.Helpers;
using Blog.Models;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface IPostRepository : IRepository<Post>
{
    Task<Post> AddAsync(Post post);
    
    Task<Post?> UpdateAsync(int id, UpdatePostDto updatePostDto);
    
    Task<Post?> DeleteAsync(int id);
    
    Task<List<Post>> GetAllFilterAsync(PostQueryObject queryObject);
}