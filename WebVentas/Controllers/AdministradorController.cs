using Microsoft.AspNetCore.Mvc;

namespace WebVentas.Controllers
{
    public class AdministradorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Colaboradores()
        {
            return View();
        }
    }
}
