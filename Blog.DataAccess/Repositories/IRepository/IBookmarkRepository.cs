using Blog.Core.Helpers;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface IBookmarkRepository : IRepository<BookMark>
{
    Task<BookMark> AddAsync(BookMark bookMark);

    Task<BookMark?> DeleteAsync(int bookmarkId);

    Task<List<BookMark>> GetAllFilterAsync(string userId, BookmarkUserQueryObject query);
}