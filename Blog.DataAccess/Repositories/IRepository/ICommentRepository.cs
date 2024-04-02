using Blog.Core.Helpers;
using Blog.Models.DTOs.Comment;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface ICommentRepository : IRepository<Comment>
{
    Task<Comment> AddAsync(Comment comment);

    Task<Comment?> UpdateAsync(Comment comment, UpdateCommentDto newComment);

    Task<Comment?> DeleteAsync(Comment comment);

    Task<List<Comment>> GetAllFilterAsync(CommentQueryObject queryObject);

    // Task<List<Post>> GetAllPostsUserFilterAsync(string userId, PostUserQueryObject queryObject);
}