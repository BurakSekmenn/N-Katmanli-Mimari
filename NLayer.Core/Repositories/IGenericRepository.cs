using System.Linq.Expressions;

namespace NLayer.Core.Repositories;

public interface IGenericRepository<T> where T : class
{

    Task<T> GetByIdAsync(int id);

    IQueryable<T> GetAll(Expression<Func<T, bool>> expression);


    // Productsrepository.where(x=>x.id>5)// bundan sonra farklı işlem yaparım ama TolistAsync() ile bitirirsem veritabanına sorguyu gönderir.
    IQueryable<T> Where(Expression<Func<T,bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entites);
    void Update(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);


}
