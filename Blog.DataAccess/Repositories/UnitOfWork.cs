using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;

namespace Blog.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public IPostRepository PostRepository { get; }
    
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        PostRepository = new PostRepository(_db);
    }
}