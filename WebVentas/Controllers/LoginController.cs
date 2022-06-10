using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using WebVentas.Models.Common;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;
using WebVentas.Models.ModelVista.response;

namespace WebVentas.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioService _User;
        private readonly IRolService _Rols;

        private string Token = "";

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
        [HttpPost]
        public IActionResult Authenticacion([FromBody] LoginRequest auth)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                var user = _User.Auth(auth);
                if(user != null)
                {
                    Usuario usuario = new Usuario();
                    usuario = user.Usuario;
                    Token = user.Token;
                    SessionHelper.SetObjectAsJson(HttpContext.Session, "Usuario",usuario);
                    HttpContext.Session.SetString("token",Token);
                    rpta.Success = true; rpta.Message = "Ok"; rpta.Data = user;
                }
                else
                {
                    rpta.Success = false;
                    rpta.Message = "Usuario o Contraseña incorrecta.";
                    rpta.Data = null;
                    return BadRequest(rpta);
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(rpta);
        }
    }
}
