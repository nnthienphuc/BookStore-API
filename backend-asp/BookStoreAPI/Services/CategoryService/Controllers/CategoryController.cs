using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.CategoryService.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
