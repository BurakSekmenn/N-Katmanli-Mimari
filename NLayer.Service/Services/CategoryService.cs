using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Entity;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;


        public CategoryService(IGenericRepository<Category> genericRepository, IUnitOfWorks unitOfWorks, ICategoryRepository categoryRepository = null, IMapper mapper = null) : base(genericRepository, unitOfWorks)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CustomeResponseDto<CategoryWithProductsDto>> GetSingleCatergoryByWithProductsAsync(int categoryId)
        {
            var category = await _categoryRepository.GetSingleCatergoryByWithProductsAsync(categoryId);
            var CategoryDto = _mapper.Map<CategoryWithProductsDto>(category);
            return  CustomeResponseDto<CategoryWithProductsDto>.Success(CategoryDto,200);
        }
    }
}
