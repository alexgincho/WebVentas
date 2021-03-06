using Microsoft.AspNetCore.Mvc;
using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.response;

namespace WebVentas.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _Ps;
        public ProductoController(IProductoService _producto)
        {
            _Ps = _producto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ver(int id)
        {
            Producto product = null;
            if (id != 0)
            {
                product = new Producto();
                product = _Ps.GetById(id);
            }
            return View(product);
        }
        public IActionResult VerDetalle(int id)
        {
            Producto product = null;
            if (id != 0)
            {
                product = new Producto();
                product = _Ps.GetById(id);
            }
            return PartialView("_VerDetalleProducto", product);
        }
        public IActionResult AgregarCarritoCompra(int id, int? cantidad = 0)
        {
            Producto product = null;
            if (id != 0)
            {
                product = new Producto();
                product = _Ps.GetById(id);
                if(cantidad == 0) { product.Cantidad = cantidad; }
                else { product.Cantidad = cantidad; }
            }
            return PartialView("_AgregarCarrito", product);
        }
        public IActionResult Carrito()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProducto(int id)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    var product = _Ps.GetById(id);
                    if(product.PkProducto != 0)
                    {
                        rpta.Success = true; rpta.Message = "Ok"; rpta.Data = product;
                    }
                    else { throw new Exception(); }
                }
                else { throw new Exception(); }
            }
            catch (Exception ex)
            {
                rpta.Success = false;
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            return Ok(rpta);
        }

        [HttpGet]
        public IActionResult GetProductos()
        {
            ResponseData rpta= new ResponseData();
            try
            {
                var products = _Ps.GetAllProductos();
                if(products.Count > 0)
                {
                    rpta.Success = true; rpta.Message = "Ok"; rpta.Data = products;
                }
                else { throw new Exception(); }
            }
            catch(Exception ex)
            {
                rpta.Success = false; rpta.Message = ex.Message; rpta.Data = null;
            }
            return Ok(rpta);
        }

        // ProductoAdmin
        public IActionResult ProductoAdmin()
        {
            return View();
        }
        public IActionResult MantenimientoProductos(int id = 0)
        {
            Producto entity = null;
            //ViewBag.Categoria = _sCat.GetAll();
            if (id != 0) entity = _Ps.GetById(id);
            return PartialView("_MantenimientoProducto", entity ?? new Producto());
        }
        [HttpGet]
        public IActionResult GetAllProductoP()
        {
            ResponseData rpta = new ResponseData();
            try
            {
                rpta.Data = _Ps.GetAllProductos();
                rpta.State = 200;
                rpta.Message = "Success.";
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.State = 400;
                rpta.Message = "Error";

            }
            return Ok(rpta);
        }
    }
}
