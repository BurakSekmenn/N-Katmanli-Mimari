using NLayer.Core.DTOs;
using NLayer.Core.Entity;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IProductServices:IService<Product>
    {
        Task<CustomeResponseDto<List<ProductWithCategoryDto>>> GetProductWithCategories();
    }
}
