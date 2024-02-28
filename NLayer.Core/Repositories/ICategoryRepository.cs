using NLayer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository:IGenericRepository<Category> // ICategoryRepository is not a class, it's an interface
    {
        Task<Category> GetSingleCatergoryByWithProductsAsync(int categoryId); // GetSingleCatergoryByWithProducts bir method
    }
}
