using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Services.CustomerSevice.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
