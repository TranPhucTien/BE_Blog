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
            Author = post.Author.ToDto(),
            Views = post.Views,
            Bookmarks = post.Bookmarks,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            PublishedAt = post.PublishedAt
        };
    }
    
    public static Post ToPostFromCreate(this CreatePostDto postDto, string authorId)
    {
        return new Post
        {
            Title = postDto.Title,
            Desc = postDto.Desc,
            Content = postDto.Content,
            AuthorId = authorId,
            PublishedAt = postDto.PublishedAt
        };
    }
}