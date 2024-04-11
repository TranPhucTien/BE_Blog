using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories;

public class PostTagRepository(ApplicationDbContext db) : IPostTagRepository
{
    public async Task<List<PostTag>> DeleteAllByPostIdAsync(int postId)
    {
        var postTags = await db.PostTags.Where(pt => pt.PostId == postId).ToListAsync();

        db.PostTags.RemoveRange(postTags);

        await db.SaveChangesAsync();

        return postTags;
    }
}