using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories.IRepository;

public interface IPostTagRepository
{
    Task<List<PostTag>> DeleteAllByPostIdAsync(int postId);
}