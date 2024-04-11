using Blog.Models.DTOs;
using Blog.Models.DTOs.Bookmark;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;

namespace Blog.Models.Mappers;

public static class BookmarkMapper
{
    public static BookmarkDto ToDto(this BookMark bookmark)
    {
        return new BookmarkDto
        {
            Post = bookmark.Post.ToDto(),
            CreatedAt = bookmark.CreatedAt,
        };
    }

    public static BookMark ToEntity(this CreateBookmarkDto bookmark, string userId)
    {
        return new BookMark
        {
            UserId = userId,
            PostId = bookmark.PostId,
        };
    }

    public static PaginationDto<List<BookmarkDto>> ToPaginationFromListPost(this List<BookMark> list, int pageNumber, int pageSize)
    {
        var length = list.Count;

        var skipNumber = (pageNumber - 1) * pageSize;
        list = list.Skip(skipNumber).Take(pageSize).ToList();

        var bookmarkDtos = list.Select(p => p.ToDto()).ToList();

        return new PaginationDto<List<BookmarkDto>>
        {
            Data = bookmarkDtos,
            CurrentPage = pageNumber,
            PageSize = pageSize,
            TotalCount = length,
            TotalPages = (int)Math.Ceiling((double)length / pageSize),
        };
    }
}