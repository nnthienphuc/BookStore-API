using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.StaffService.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
