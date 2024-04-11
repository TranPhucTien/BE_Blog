using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Blog.Core.Helpers;
using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    private readonly ApplicationDbContext _db;
    private static Random rng = new Random();

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
        var existing = await _db.Posts
            .Include(a => a.Author)
            .Include(p => p.PostTags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existing == null)
        {
            return null;
        }

        existing.Title = post.Title;
        existing.Content = post.Content;
        existing.Desc = post.Desc;
        existing.PostTags = post.PostTags;
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
        var posts = _db.Posts
            .Include(a => a.Author)
            .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
            .AsQueryable();
        
        if (!string.IsNullOrEmpty(queryObject.AuthorId))
        {
            posts = posts.Where(p => p.AuthorId == queryObject.AuthorId);
        }

        if (queryObject.TagId.HasValue)
        {
            posts = posts.Where(p => p.PostTags.Any(pt => pt.TagId == queryObject.TagId));
        }

        if (string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            var rs = await posts.ToListAsync();
            rs = rs.OrderBy(_ => rng.Next()).ToList();
            
            return rs;
        }
        
        var sortExpression = $"{queryObject.SortBy} {(queryObject.IsDecsending ? "descending" : "ascending")}";
        posts = posts.OrderBy(sortExpression);

        return await posts.ToListAsync();
    }

    public override async Task<Post?> GetFirstOrDefaultAsync(Expression<Func<Post, bool>> filter, string? includeProperties = null, bool isTracking = true)
    {
        return await _db.Posts.Include(a => a.Author)
            .Include(a => a.PostTags).ThenInclude(a => a.Tag)
            .Where(filter).FirstOrDefaultAsync();
    }
    
    public async Task<List<Post>> GetAllPostsUserFilterAsync(string userId, PostUserQueryObject queryObject)
    {
        var posts = _db.Posts.Include(a => a.Author)
            .Include(a => a.PostTags).ThenInclude(a => a.Tag)
            .AsQueryable();
        
        posts = posts.Where(p => p.AuthorId == userId);
        
        if (queryObject.TagId.HasValue)
        {
            posts = posts.Where(p => p.PostTags.Any(pt => pt.TagId == queryObject.TagId));
        }

        if (string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            return await posts.ToListAsync();
        }
        
        var sortExpression = $"{queryObject.SortBy} {(queryObject.IsDecsending ? "descending" : "ascending")}";
        posts = posts.OrderBy(sortExpression);

        return await posts.ToListAsync();
    }
}