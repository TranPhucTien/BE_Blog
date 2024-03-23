using Blog.Models.DTOs.Tag;
using Blog.Models.Entities;

namespace Blog.Models.Mappers;

public static class TagMapper
{
    public static TagDto ToDto(this Tag tag)
    {
        return new TagDto
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
    
    
    public static Tag ToEntity(this TagDto tagDto)
    {
        return new Tag
        {
            Name = tagDto.Name
        };
    }
}