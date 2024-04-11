using Blog.Core.Helpers;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface IBookmarkRepository : IRepository<BookMark>
{
    Task<BookMark> AddAsync(BookMark bookMark);

    Task<BookMark?> DeleteAsync(string userId, int postId);

    Task<List<BookMark>> DeleteAllByPostIdAsync(int postId);

    Task<List<BookMark>> GetAllFilterAsync(string? userId, BookmarkUserQueryObject query);
}