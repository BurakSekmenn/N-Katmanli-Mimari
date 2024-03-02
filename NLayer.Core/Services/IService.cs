using System.Linq.Expressions;

namespace NLayer.Core.Services;

public interface IService <T> where T : class
{
    Task<T> GetByIdAsync(int id);

    IQueryable<T> GetAll();

    Task<IEnumerable<T>> GetAllAsync();






    // Productsrepository.where(x=>x.id>5)// bundan sonra farklı işlem yaparım ama TolistAsync() ile bitirirsem veritabanına sorguyu gönderir.
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entites);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task RemoveRangeAsync(IEnumerable<T> entities);
}
