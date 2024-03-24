using Blog.DataAccess.Data;
using Blog.DataAccess.Repositories.IRepository;
using Blog.Models.Entities;

namespace Blog.DataAccess.Repositories;

public class TagRepository : Repository<Tag>, ITagRepository
{
    private readonly ApplicationDbContext _db;
    
    public TagRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
}