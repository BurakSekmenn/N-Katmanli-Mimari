using NLayer.Core.DTOs;
using NLayer.Core.Entity;

namespace NLayer.Core.Services
{
    public interface IProductServices:IService<Product>
    {
        Task<List<ProductWithCategoryDto>> GetProductWithCategories();
    }
}
