using System.Linq.Expressions;
using Blog.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Blog.DataAccess.Repositories.IRepository;

namespace Blog.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    internal readonly DbSet<T> DbSet;
    private static readonly char[] Separator = new char[] { ',' };

    public Repository(ApplicationDbContext db)
    {
        DbSet = db.Set<T>();
    }

    public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool isTracking = true)
    {
        IQueryable<T> query = DbSet.AsNoTracking();

        if (isTracking)
        {
            query = DbSet;
        }

        query = query.Where(filter);
        
        if (includeProperties == null)
        {
            return await query.FirstOrDefaultAsync();
        }
        
        foreach (var includeProperty in includeProperties.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        
        return await query.FirstOrDefaultAsync();
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
    {
        IQueryable<T> query = DbSet;
        
        if (filter != null)
        {
            query = query.Where(filter);
        }
        
        if (includeProperties == null)
        {
            return await query.ToListAsync();
        }
        
        foreach (var includeProperty in includeProperties.Split(Separator, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        
        return await query.ToListAsync();
    }

    public IEnumerable<T> Skip(int numberToSkip)
    {
        IQueryable<T> query = DbSet;
        
        return query.Skip(numberToSkip);
    }
    
    public IEnumerable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        IQueryable<T> query = DbSet;
        
        return query.OrderBy(keySelector);
    }
    
    public IEnumerable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        IQueryable<T> query = DbSet;
        
        return query.OrderByDescending(keySelector);
    }
    
    public void RemoveRange(IEnumerable<T> entity)
    {
        DbSet.RemoveRange(entity);
    }
    
}