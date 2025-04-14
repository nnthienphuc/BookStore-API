using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.AuthService.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
