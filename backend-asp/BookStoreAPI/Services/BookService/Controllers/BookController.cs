using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.BookService.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
