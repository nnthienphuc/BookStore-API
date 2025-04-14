using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.OrderService.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
