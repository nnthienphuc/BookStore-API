using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Common.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
