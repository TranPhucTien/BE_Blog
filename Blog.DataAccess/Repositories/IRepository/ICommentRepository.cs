using Blog.Core.Helpers;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface ICommentRepository : IRepository<Comment>
{
    Task<Comment> AddAsync(Comment comment);

    Task<Comment?> UpdateAsync(int id, Comment comment);

    Task<Comment?> DeleteAsync(int id);

    Task<List<Comment>> GetAllFilterAsync(CommentQueryObject queryObject);

    // Task<List<Post>> GetAllPostsUserFilterAsync(string userId, PostUserQueryObject queryObject);
}