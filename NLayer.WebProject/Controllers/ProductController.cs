using Microsoft.AspNetCore.Mvc;

namespace NLayer.WebProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

       
    }
}
