using System.Linq.Dynamic.Core;
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

    public async Task<Post?> UpdateAsync(int id, UpdatePostDto updatePostDto)
    {
        var post = await _db.Posts.FirstOrDefaultAsync(p => p.Id == id);

        if (post == null)
        {
            return null;
        }

        post.Title = updatePostDto.Title;
        post.Content = updatePostDto.Content;
        post.Desc = updatePostDto.Desc;
        post.UpdatedAt = DateTime.Now;
        post.PublishedAt = updatePostDto.PublishedAt;
        
        await _db.SaveChangesAsync();

        return post;
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
        var posts = _db.Posts.AsQueryable();
        
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
}