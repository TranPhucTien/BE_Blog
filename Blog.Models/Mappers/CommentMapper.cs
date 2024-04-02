using Blog.Models.DTOs;
using Blog.Models.DTOs.Comment;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;

namespace Blog.Models.Mappers;

public static class CommentMapper
{
    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Content = comment.Content,
            ParentId = comment.ParentId,
            PostId = comment.PostId,
            CreatedBy = comment.User.ToDto(),
            CreatedAt = comment.CreatedAt,
            UpdatedAt = comment.UpdatedAt
        };
    }

    public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, string createdBy)
    {
        return new Comment
        {
            Content = commentDto.Content,
            ParentId = commentDto.ParentId,
            PostId = commentDto.PostId,
            UserId = createdBy
        };
    }

    public static PaginationDto<List<CommentDto>> ToPaginationFromListPost(this List<Comment> list, int pageNumber, int pageSize)
    {
        var length = list.Count;

        var skipNumber = (pageNumber - 1) * pageSize;
        list = list.Skip(skipNumber).Take(pageSize).ToList();

        var commentDtos = list.Select(p => p.ToDto()).ToList();

        return new PaginationDto<List<CommentDto>>
        {
            Data = commentDtos,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = length,
            TotalPages = (int)Math.Ceiling((double)length / pageSize),
        };
    }
}