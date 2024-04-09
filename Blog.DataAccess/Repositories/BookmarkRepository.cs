using System.Linq.Dynamic.Core;
using Blog.Core.Helpers;
using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories;

public class BookmarkRepository(ApplicationDbContext db) : Repository<BookMark>(db), IBookmarkRepository
{
    public async Task<BookMark> AddAsync(BookMark bookMark)
    {
        await db.Bookmarks.AddAsync(bookMark);
        await db.SaveChangesAsync();

        return bookMark;

    }

    public async Task<BookMark?> DeleteAsync(int bookmarkId)
    {
        var bookmark = await db.Bookmarks.FirstOrDefaultAsync(p => p.Id == bookmarkId);

        if (bookmark == null)
        {
            return null;
        }

        db.Bookmarks.Remove(bookmark);
        await db.SaveChangesAsync();

        return bookmark;
    }

    public async Task<List<BookMark>> GetAllFilterAsync(string userId, BookmarkUserQueryObject query)
    {
        var bookmarks = db.Bookmarks.Include(b => b.Post).AsQueryable();

        if (userId != null)
        {
            bookmarks = bookmarks.Where(o => o.UserId == userId);
        }

        if (string.IsNullOrWhiteSpace(query.SortBy))
        {
            return await bookmarks.ToListAsync();
        }

        var sortExpression = $"{query.SortBy} {(query.IsDecsending ? "descending" : "ascending")}";
        bookmarks = bookmarks.OrderBy(sortExpression);

        return await bookmarks.ToListAsync();
    }
}