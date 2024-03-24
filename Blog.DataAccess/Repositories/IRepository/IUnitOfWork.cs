namespace Blog.DataAccess.Repositories.IRepository;

public interface IUnitOfWork
{
    IPostRepository PostRepository { get; }
    ITagRepository TagRepository { get; }
}