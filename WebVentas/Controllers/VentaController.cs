using Microsoft.AspNetCore.Mvc;
using WebVentas.Models.ModelBD;

namespace WebVentas.Controllers
{
    public class VentaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult MantenimientoVenta(Ventum venta)
        {
            return Ok(venta);
        }
    }
}
