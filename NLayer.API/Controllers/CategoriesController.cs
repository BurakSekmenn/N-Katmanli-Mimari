using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Entity;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
 
    public class CategoriesController : CustomBaseController
    {

        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        //api/categories/getsinglecatergorybywithproducts/5
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleCatergoryByWithProductsAsync(int categoryId)
        {
            return CreateActionResult(await _categoryService.GetSingleCatergoryByWithProductsAsync(categoryId));
        }
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = await _categoryService.AddAsync(_mapper.Map<Category>(categoryDto));
            var categoryDtos = _mapper.Map<CategoryDto>(category);
            return CreateActionResult(category != null ? CustomeResponseDto<CategoryDto>.Success(categoryDtos, 201) : CustomeResponseDto<CategoryDto>.Fail("Hata", 201));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            var categorydto = _mapper.Map<CategoryDto>(categories);
            return CreateActionResult(CustomeResponseDto<CategoryDto>.Success(categorydto, 200));
        }
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _categoryService.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(products.ToList());
            return CreateActionResult(CustomeResponseDto<List<CategoryDto>>.Success(categoryDtos, 200));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var categories = await _categoryService.GetByIdAsync(id);
            await _categoryService.RemoveAsync(categories);
            var categoryDtos = _mapper.Map<CategoryDto>(categories);
            return CreateActionResult(CustomeResponseDto<CategoryDto>.Success(categoryDtos, 200));
        }


        [HttpPut]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await _categoryService.UpdateAsync(_mapper.Map<Category>(categoryDto));
            var categoriesDto = _mapper.Map<CategoryDto>(categoryDto);
            return CreateActionResult(CustomeResponseDto<CategoryDto>.Success(categoriesDto, 200));
        }




    }
}
