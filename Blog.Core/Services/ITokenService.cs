using Blog.Models.Entities;

namespace Blog.Core.Services;

public interface ITokenService
{
    string CreateToken(User user);
}