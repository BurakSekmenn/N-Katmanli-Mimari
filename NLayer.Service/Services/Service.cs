using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NLayer.Service.Exceptions;

namespace NLayer.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {

        private readonly IGenericRepository<T> _genericRepository;
        private readonly IUnitOfWorks _unitOfWorks;

        public Service(IGenericRepository<T> genericRepository, IUnitOfWorks unitOfWorks)
        {
            _genericRepository = genericRepository;
            _unitOfWorks = unitOfWorks;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _genericRepository.AddAsync(entity);
            await _unitOfWorks.CommitAsync(); // SaveChangesAsync Methodunu çağırıyoruz.
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entites)
        {
            await _genericRepository.AddRangeAsync(entites);
            await _unitOfWorks.CommitAsync(); // SaveChangesAsync Methodunu çağırıyoruz.
            return entites;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
           return await _genericRepository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _genericRepository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _genericRepository.GetByIdAsync(id);

            if (hasProduct == null)
            {
                throw new NotFoundExecption($"{typeof(T).Name}({id}) Not Found");
            }

            return hasProduct;
        }

        public async Task RemoveAsync(T entity)
        {
            _genericRepository.Remove(entity);
            await _unitOfWorks.CommitAsync(); // SaveChangesAsync Methodunu çağırıyoruz.

        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _genericRepository.RemoveRange(entities);
            await _unitOfWorks.CommitAsync(); // SaveChangesAsync Methodunu çağırıyoruz.
        }

        public async Task UpdateAsync(T entity)
        {
            _genericRepository.Update(entity);
            await _unitOfWorks.CommitAsync(); // SaveChangesAsync Methodunu çağırıyoruz.
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _genericRepository.Where(expression);
            
        }
    }
}
