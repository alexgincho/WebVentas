using Microsoft.AspNetCore.Mvc;
using WebVentas.Models.Interfaces;

namespace WebVentas.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioService _User;
        private readonly IRolService _Rols;

        public LoginController(IUsuarioService user, IRolService role)
        {
            _User = user;
            _Rols = role;
        }
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
