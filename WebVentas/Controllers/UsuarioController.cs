using Microsoft.AspNetCore.Mvc;
using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.response;

namespace WebVentas.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _User;
        private readonly IRolService _Rols;

        public UsuarioController(IUsuarioService user , IRolService role)
        {
            _User = user;
            _Rols = role;   
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MantenimientoUsuario([FromBody] Usuario user)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    if(user.PkUsuario != 0)
                    {
                        // Editar
                    }
                    else
                    {
                        var usuario = _User.Crear(user);
                        if (usuario != null)
                        {
                            rpta.Success = true;
                            rpta.Message = "Ok";
                            rpta.Data = usuario;
                        }
                        else { throw new Exception(); }
                    }
                }
            }
            catch(Exception ex)
            {
                rpta.Success = false;
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            return Ok(rpta);
        }
    }
}
