using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Blog.Core.Helpers;
using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    private readonly ApplicationDbContext _db;

    public PostRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    public async Task<Post> AddAsync(Post post)
    {
        await _db.Posts.AddAsync(post);
        await _db.SaveChangesAsync();

        return post;
    }

    public async Task<Post?> UpdateAsync(int id, Post post)
    {
        var existing = await _db.Posts.Include(a => a.Author).FirstOrDefaultAsync(p => p.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Title = post.Title;
        existing.Content = post.Content;
        existing.Desc = post.Desc;
        existing.UpdatedAt = post.UpdatedAt;
        existing.PublishedAt = post.PublishedAt;
        
        await _db.SaveChangesAsync();

        return existing;
    }

    public async Task<Post?> DeleteAsync(int id)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return null;
        }

        _db.Posts.Remove(post);
        await _db.SaveChangesAsync();
        
        return post;
    }

    public async Task<List<Post>> GetAllFilterAsync(PostQueryObject queryObject)
    {
        var posts = _db.Posts.Include(a => a.Author).AsQueryable();
        
        if (!string.IsNullOrEmpty(queryObject.AuthorId))
        {
            posts = posts.Where(p => p.AuthorId == queryObject.AuthorId);
        }

        if (queryObject.TagId.HasValue)
        {
            // TODO: Update this query to filter by tag id
            // posts = posts.Where(p => p.Tags.Any(t => t.Id == queryObject.TagId));
        }
        
        if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            var sortExpression = $"{queryObject.SortBy} {(queryObject.IsDecsending ? "descending" : "ascending")}";
            posts = posts.OrderBy(sortExpression);
        }

        var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
        
        return await posts.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
    }

    public override async Task<Post?> GetFirstOrDefaultAsync(Expression<Func<Post, bool>> filter, string? includeProperties = null, bool isTracking = true)
    {
        return await _db.Posts.Include(a => a.Author).Where(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<Post>> GetAllPostsUserFilterAsync(string userId, PostUserQueryObject queryObject)
    {
        var posts = _db.Posts.Include(a => a.Author).AsQueryable();
        
        posts = posts.Where(p => p.AuthorId == userId);
        
        if (queryObject.TagId.HasValue)
        {
            // TODO: Update this query to filter by tag id
            // posts = posts.Where(p => p.Tags.Any(t => t.Id == queryObject.TagId));
        }

        if (!string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            var sortExpression = $"{queryObject.SortBy} {(queryObject.IsDecsending ? "descending" : "ascending")}";
            posts = posts.OrderBy(sortExpression);
        }

        var skipNumber = (queryObject.PageNumber - 1) * queryObject.PageSize;
        
        return await posts.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
    }
}