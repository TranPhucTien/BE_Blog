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

    public async Task<BookMark?> DeleteAsync(string userId, int postId)
    {
        var bookmark = await db.Bookmarks.FirstOrDefaultAsync(p => p.UserId == userId && p.PostId == postId);

        if (bookmark == null)
        {
            return null;
        }

        db.Bookmarks.Remove(bookmark);
        await db.SaveChangesAsync();

        return bookmark;
    }

    public async Task<List<BookMark>> DeleteAllByPostIdAsync(int postId)
    {
        var bookmarks = await db.Bookmarks.Where(p => p.PostId == postId).ToListAsync();

        if (bookmarks.Count == 0)
        {
            return new List<BookMark>();
        }

        db.Bookmarks.RemoveRange(bookmarks);
        await db.SaveChangesAsync();

        return bookmarks;
    }

    public async Task<List<BookMark>> GetAllFilterAsync(string? userId, BookmarkUserQueryObject query)
    {
        var bookmarks = db.Bookmarks
            .Include(b => b.Post)
            .ThenInclude(p => p.PostTags)
            .ThenInclude(pt => pt.Tag)
            .Include(b => b.Post)
            .ThenInclude(p => p.Author)
            .AsQueryable();

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