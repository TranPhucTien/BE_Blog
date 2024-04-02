using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;

namespace Blog.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IPostRepository PostRepository { get; }
    public ITagRepository TagRepository { get; }
    public ICommentRepository CommentRepository { get; }
    
    public UnitOfWork(ApplicationDbContext db)
    {
        TagRepository = new TagRepository(db);
        PostRepository = new PostRepository(db);
        CommentRepository = new CommentRepository(db);
    }
}