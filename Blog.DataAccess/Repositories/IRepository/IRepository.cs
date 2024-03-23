using System.Linq.Expressions;

namespace Blog.DataAccess.Repositories.IRepository;

public interface IRepository<T> where T : class
{
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool isTracking = true);
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

    IEnumerable<T> Skip(int numberToSkip);
    IEnumerable<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);
    IEnumerable<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);
}