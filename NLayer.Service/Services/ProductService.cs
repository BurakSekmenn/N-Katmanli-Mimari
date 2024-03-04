using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Entity;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductServices
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IUnitOfWorks unitOfWork, IMapper mapper) : base(productRepository, unitOfWork)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }




        public async Task<List<ProductWithCategoryDto>> GetProductWithCategories()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productsDtos = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return productsDtos;
        }
    }
}
