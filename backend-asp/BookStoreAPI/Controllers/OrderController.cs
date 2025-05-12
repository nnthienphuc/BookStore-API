using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
