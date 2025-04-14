using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.PromotionService.Controllers
{
    public class PromotionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
