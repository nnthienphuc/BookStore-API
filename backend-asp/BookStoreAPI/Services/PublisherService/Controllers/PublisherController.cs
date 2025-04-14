using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.PublisherService.Controllers
{
    public class PublisherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
