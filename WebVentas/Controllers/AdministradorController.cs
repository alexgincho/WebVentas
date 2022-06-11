using Microsoft.AspNetCore.Mvc;
using WebVentas.Models.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebVentas.Models.ModelBD;

using WebVentas.Models.ModelVista.response;

namespace WebVentas.Controllers
{
    public class AdministradorController : Controller
    {
        private IProductoService _sPro;
        public AdministradorController(IProductoService sPro)
        {

            this._sPro = sPro;
        }

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
