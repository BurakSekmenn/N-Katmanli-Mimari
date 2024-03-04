using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;

namespace NLayer.WebNProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _productServices.GetProductWithCategories());
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
    }
}
