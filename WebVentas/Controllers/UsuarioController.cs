using Microsoft.AspNetCore.Mvc;
using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.Services;
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
        public IActionResult MantenimientoPersonal(int id = 0)
        {
            Usuario entity = null;
            ViewBag.Roles = _Rols.GetAllRoles();
            if (id != 0) entity = _User.Get(id);
            return PartialView("_MantenimientoPersonal", entity ?? new Usuario());
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

        [HttpPost]
        public IActionResult Update([FromBody] Usuario entity)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                if (ModelState.IsValid)
                {
                    if (entity.PkUsuario != 0)
                    {
                        //var ValidateDni = _sPer.ValidarDniPersonal(entity.Dni);
                        // if (ValidateDni)
                        //{
                        //  throw new Exception("Error. Datos ya Registrados.");
                        //}
                        rpta.Data = _User.Update(entity);
                        rpta.Message = "Success.";

                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else { return BadRequest(); }

            }
            catch (Exception ex)
            {
                rpta.Message = ex.Message;
                rpta.Data = null;
            }
            return Ok(rpta);
        }

        [HttpPost]
        public IActionResult DesactivePersonal([FromBody] int id)
        {
            ResponseData rpta = new ResponseData();
            try
            {
                var DeletePerson = _User.Delete(id);
                if (DeletePerson)
                {
                    rpta.Data = true;
                    rpta.Message = "Se Desactivo Personal Correctamente!";
                }
                else { throw new Exception("Error. No se pudo desactivar el Personal."); }
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.Message = ex.Message;
            }
            return Ok(rpta);
        }

        [HttpGet]
        public IActionResult GetAllPersonal()
        {
            ResponseData rpta = new ResponseData();
            try
            {
                rpta.Data = _User.GetAll();
                rpta.Message = "Success.";
            }
            catch (Exception ex)
            {
                rpta.Data = null;
                rpta.Message = "Error";
            }
            return Ok(rpta);
        }
    }
}
