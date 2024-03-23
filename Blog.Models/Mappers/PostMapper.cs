using Blog.Models.DTOs;
using Blog.Models.DTOs.Post;
using Blog.Models.Entities;

namespace Blog.Models.Mappers;

public static class PostMapper
{
    public static PostDto ToDto(this Post post)
    {
        return new PostDto
        {
            Id = post.Id,
            Title = post.Title,
            Desc = post.Desc,
            Content = post.Content,
            AuthorId = post.AuthorId,
            Views = post.Views,
            Bookmarks = post.Bookmarks,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            PublishedAt = post.PublishedAt
        };
    }
    
    public static Post ToEntity(this CreatePostDto createPostDto)
    {
        return new Post
        {
            Title = createPostDto.Title,
            Desc = createPostDto.Desc,
            Content = createPostDto.Content,
            AuthorId = createPostDto.AuthorId,
            PublishedAt = createPostDto.PublishedAt
        };
    }
}