using Blog.Core.Helpers;
using Blog.Models;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface IPostRepository : IRepository<Post>
{
    Task<Post> AddAsync(Post post);
    
    Task<Post?> UpdateAsync(int id, Post post);
    
    Task<Post?> DeleteAsync(int id);
    
    Task<List<Post>> GetAllFilterAsync(PostQueryObject queryObject);
    
    Task<List<Post>> GetAllPostsUserFilterAsync(string userId, PostUserQueryObject queryObject);
}