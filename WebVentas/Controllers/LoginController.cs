using Microsoft.AspNetCore.Mvc;

namespace WebVentas.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }
    }
}
