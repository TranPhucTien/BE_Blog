using System.Linq.Dynamic.Core;
using Blog.Core.Helpers;
using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.DTOs.Comment;
using Blog.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories;

public class CommentRepository(ApplicationDbContext db) : Repository<Comment>(db), ICommentRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<Comment> AddAsync(Comment comment)
    {
        int rightValue;

        if (comment.ParentId != null)
        {
            var parentComment = _db.Comments.FirstOrDefault(p => p.Id == comment.ParentId);

            if (parentComment == null)
            {
                throw new Exception("Parent comment not found");
            }

            rightValue = parentComment.Right;

            var comments = _db.Comments.Where(p => p.PostId == parentComment.PostId && p.Right >= rightValue).ToList();
            foreach (var item in comments)
            {
                item.Right += 2;
            }

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

    public async Task<Comment?> UpdateAsync(Comment comment, UpdateCommentDto newComment)
    {
        comment.Content = newComment.Content;
        comment.UpdatedAt = DateTime.Now;

        await _db.SaveChangesAsync();

        return comment;
    }

    public async Task<Comment?> DeleteAsync(Comment comment)
    {
        var left = comment.Left;
        var right = comment.Right;

        var width = right - left + 1;

        var comments = _db.Comments.Where(p => p.Left >= left && p.Right <= right).ToList();
        foreach (var item in comments)
        {
            _db.Comments.Remove(item);
        }

        comments = _db.Comments.Where(p => p.Left > right).ToList();
        foreach (var item in comments)
        {
            item.Left -= width;
        }

        comments = _db.Comments.Where(p => p.Right > right).ToList();

        foreach (var item in comments)
        {
            item.Right -= width;
        }

        await _db.SaveChangesAsync();

        return comment;
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