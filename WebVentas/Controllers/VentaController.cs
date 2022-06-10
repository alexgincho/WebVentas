using Microsoft.AspNetCore.Mvc;
using WebVentas.Models.Common;
using WebVentas.Models.ModelBD;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Authorization;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelVista.response;
using Newtonsoft.Json;

namespace WebVentas.Controllers
{
    public class VentaController : Controller
    {
        private readonly IVentaService _ventaService;
        private readonly IUsuarioService _Uservice;
        public VentaController(IUsuarioService user, IVentaService vent)
        {
            _Uservice = user;
            _ventaService = vent;
        }
        public IActionResult Index()
        {
            var idUSer = SessionHelper.GetObjectFromJson<Usuario>(HttpContext.Session, "Usuario");
            if(idUSer == null)
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult MantenimientoVenta(Ventum venta)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    if (venta.PkVenta != 0)
                    {
                        // Update
                    }
                    else
                    {
                        var vent = _ventaService.CreateVenta(venta);
                        if (vent != null)
                        {
                            rpta.Success = true;
                            rpta.Message = "Venta Realizada";
                            rpta.Data = vent;
                        }
                        else { throw new Exception("Ocurrio un Error."); }
                    }
                }
                else
                {
                    throw new Exception("Ingresa los datos correctos");
                }
            }
            catch (Exception ex)
            {
                rpta.Success = false;
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            //return Ok(JsonConvert.SerializeObject(rpta));
            return Ok(rpta);
        }
    }
}
