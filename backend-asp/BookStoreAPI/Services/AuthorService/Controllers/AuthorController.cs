using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.AuthorService.Controllers
{
    public class AuthorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
