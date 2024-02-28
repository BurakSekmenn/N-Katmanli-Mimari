using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Entity;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        
        private readonly IProductServices _productservice;

        public ProductsController(IMapper mapper, IProductServices productServices = null, IProductServices productservice = null)
        {

            _mapper = mapper;
            _productservice = productservice;
        }


        [HttpGet("GetProductWithCategories")]
        public async Task<IActionResult> GetProductWithCategories()
        {
          
            return CreateActionResult(await _productservice.GetProductWithCategories());
        }




        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _productservice.GetAllAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomeResponseDto<List<ProductDto>>.Success(productDtos,200 ));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productservice.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomeResponseDto<ProductDto>.Success(productDto, 200));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _productservice.AddAsync(_mapper.Map<Product>(productDto));
            var productDtos = _mapper.Map<ProductDto>(product);
            return CreateActionResult(CustomeResponseDto<ProductDto>.Success(productDtos, 201));
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            await _productservice.UpdateAsync(_mapper.Map<Product>(productDto));
            return CreateActionResult(CustomeResponseDto<ProductDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var products = await _productservice.GetByIdAsync(id);
            await _productservice.RemoveAsync(products);
            var productDtos = _mapper.Map<ProductDto>(products);
            return CreateActionResult(CustomeResponseDto<ProductDto>.Success(productDtos, 201));
        }




    }
}
