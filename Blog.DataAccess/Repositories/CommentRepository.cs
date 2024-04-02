using System.Linq.Dynamic.Core;
using Blog.Core.Helpers;
using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    private readonly ApplicationDbContext _db;

    public CommentRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        int rightValue = 1;

        if (comment.ParentId != null)
        {
            var parentComment = _db.Comments.FirstOrDefault(p => p.Id == comment.ParentId);

            if (parentComment == null)
            {
                throw new Exception("Parent comment not found");
            }

            rightValue = parentComment.Right;

            // update many
            var comments = _db.Comments.Where(p => p.PostId == parentComment.PostId && p.Right >= rightValue).ToList();
            foreach (var item in comments)
            {
                item.Right += 2;
            }

            // update many left
            comments = _db.Comments.Where(p => p.PostId == parentComment.PostId && p.Left > rightValue).ToList();
            foreach (var item in comments)
            {
                item.Left += 2;
            }
        }
        else
        {
            var post = _db.Posts.Include(post => post.Comments).FirstOrDefault(p => p.Id == comment.PostId);

            rightValue = post.Comments.Count > 0 ? post.Comments.Max(p => p.Right) : 1;
        }

        comment.Left = rightValue;
        comment.Right = rightValue + 1;

        await _db.Comments.AddAsync(comment);
        await _db.SaveChangesAsync();

        return comment;
    }

    public Task<Comment?> UpdateAsync(int id, Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task<Comment?> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Comment>> GetAllFilterAsync(CommentQueryObject queryObject)
    {
        var comments = _db.Comments.Include(a => a.User)
            .Where(p => p.PostId == queryObject.PostId);


        if (string.IsNullOrWhiteSpace(queryObject.SortBy))
        {
            var rs = await comments.ToListAsync();
            return rs;
        }

        var sortExpression = $"{queryObject.SortBy} {(queryObject.IsDecsending ? "descending" : "ascending")}";
        comments = comments.OrderBy(sortExpression);

        return await comments.ToListAsync();
    }
}