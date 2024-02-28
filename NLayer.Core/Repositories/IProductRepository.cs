using NLayer.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product> // IProductRepository is not a class, it's an interface
    {
        Task<List<Product>> GetProductsWithCategory(); // GetProductssWithCategory bir method


    }
}
