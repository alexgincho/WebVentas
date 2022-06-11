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
        public IActionResult Producto()
        {
            return View();
        }
        public IActionResult PartialProducto(int id)
        {
            ViewBag.Categorias = _sPro.GetAllCategorias();
            return PartialView("_ProductoCreate");
        }
        [HttpPost]
        public IActionResult CreateProducto([FromBody] Producto producto)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                var prod = _sPro.Crear(producto);
                if(prod != null)
                {
                    rpta.Success = true;
                    rpta.Message = "Exito";
                    rpta.Data = prod;
                }
            }
            catch (Exception ex)
            {
                rpta.Success = false;
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            return Ok(rpta);
        }
    }
}
