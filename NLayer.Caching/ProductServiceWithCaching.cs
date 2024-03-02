using NLayer.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Entity;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching :IProductServices
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductServices _productServices;
        private readonly IUnitOfWorks _unitOfWorks;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductServices productServices, IUnitOfWorks unitOfWorks)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _productServices = productServices;
            _unitOfWorks = unitOfWorks;



            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                _memoryCache.Set(CacheProductKey, _productServices.GetProductWithCategories().Result);
            }
        }


        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                throw new NotFoundExecption($"{typeof(Product).Name}({id}) Not Found");
            }
            
            return Task.FromResult(product);
        }

        public IQueryable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public  Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Any(expression.Compile());
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _productServices.AddAsync(entity);
            await _unitOfWorks.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entites)
        {
            await _productServices.AddRangeAsync(entites);
            await _unitOfWorks.CommitAsync();
            await CacheAllProductsAsync();
            return entites;
        }

        public async Task UpdateAsync(Product entity)
        {
            await _productServices.UpdateAsync(entity);
            await _unitOfWorks.CommitAsync();
            await CacheAllProductsAsync();


        }

        public async Task RemoveAsync(Product entity)
        {
           await _productServices.RemoveAsync(entity);
           await _unitOfWorks.CommitAsync();
           await CacheAllProductsAsync();

        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            await _productServices.RemoveRangeAsync(entities);
            await _unitOfWorks.CommitAsync();
            await CacheAllProductsAsync();
        }

        public Task<CustomeResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategories()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productWithCategory = _mapper.Map<List<ProductWithCategoryDto>>(products);

            return Task.FromResult( CustomeResponseDto<List<ProductWithCategoryDto>>.Success(productWithCategory, 200));


    
        }



        public async Task CacheAllProductsAsync()
        {
           await _memoryCache.Set(CacheProductKey, _productServices.GetAll().ToListAsync());
        }


    }
}
