using Blog.Models.DTOs.Account;
using Blog.Models.Entities;

namespace Blog.Models.Mappers;

public static class UserMapper
{
    public static UserDto ToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email
        };
    }
    
    public static User ToEntity(this NewUserDto newUserDto)
    {
        return new User
        {
            UserName = newUserDto.UserName,
            Email = newUserDto.Email
        };
    }
}