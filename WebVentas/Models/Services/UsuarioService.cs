using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebVentas.Models.Common;
using Microsoft.Extensions.Options;
using WebVentas.Models.ModelVista.response;
using System.Collections.Generic;

namespace WebVentas.Models.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppSettings _appSettings;
        public UsuarioService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Usuario Crear(Usuario user)
        {
            Usuario result = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    result = new Usuario();
                    result.FkTipoDocumento = user.FkTipoDocumento;
                    result.NroDocumento = user.NroDocumento;
                    result.Nombre = user.Nombre;
                    result.ApellidoPaterno = user.ApellidoPaterno;
                    result.ApellidoMaterno = user.ApellidoMaterno;
                    result.Direccion = user.Direccion;
                    result.Telefono = user.Telefono;
                    result.Email = user.Email;
                    result.Password = user.Password;
                    result.FkRol = user.FkRol;
                    result.Ubigeo = user.Ubigeo;
                    result.FechaCrea = DateTime.Now;
                    result.FkUsuarioCrea = 1;

                    db.Usuarios.Add(result);
                    db.SaveChanges();
                    if(result.PkUsuario == 0 || result == null)
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public List<Usuario> GetAll()
        {
            List<Usuario> result = null;
            string error = "";
            try
            {
                using (var db = new BDVentasContext())
                {
                    var lst = db.Usuarios.ToList().OrderByDescending(p => p.PkUsuario).ToList();
                    //var lst = db.Personaladms.Join(
                    //            db.Roles, p => p.FkRol, 
                    //            r => r.PkErol, 
                    //            (p, r) => 
                    //            new 
                    //            { 
                    //                p.Nombre,
                    //                r.Descripcion
                    //            });
                    //var personales = lst.ToList();
                    if (lst.Count() > 0)
                    {
                        result = lst;
                    }
                    else { throw new Exception("Error. No hay Personal Administrativo registrado"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Usuario Get(int id)
        {
            Usuario result = null;
            string error = "";
            try
            {
                using (var db = new BDVentasContext())
                {
                    var obj = db.Usuarios.Where(u => u.PkUsuario == id);
                    var usuario = obj.FirstOrDefault();
                    if (usuario != null)
                    {
                        result = usuario;
                    }
                    else { throw new Exception("Usuaio no Existe."); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }
        public bool Delete(int id)
        {
            bool result = false;
            string error = "";
            try
            {
                using (var db = new BDVentasContext())
                {
                    var obj = db.Usuarios.Find(id);
                    if (obj != null)
                    {
                        obj.IsDeleted = true;
                        obj.FechaEdita = DateTime.Now;
                        db.SaveChanges();
                        result = true;
                    }
                    else { throw new Exception("Error. Personal Adm no encontrado."); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public Usuario Update(Usuario entity)
        {
            Usuario result = null;
            string error = "";
            try
            {
                using (var db = new BDVentasContext())
                {
                    var obj = db.Usuarios.Find(entity.PkUsuario);
                    if (obj != null)
                    {
                        obj.NroDocumento = entity.NroDocumento; // Validar Dni no repetidos
                        obj.Nombre = entity.Nombre;
                        obj.ApellidoPaterno = entity.ApellidoPaterno;
                        obj.ApellidoMaterno = entity.ApellidoMaterno;
                        obj.Direccion = entity.Direccion;
                        obj.Telefono = entity.Telefono;
                        obj.Email = entity.Email; // Validar Email no repetidos
                        db.SaveChanges();
                        result = entity;
                    }
                    else { throw new Exception("Error. Datos no Actualizados"); }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return result;
        }

        public UsuarioResponse Auth(LoginRequest user)
        {
            UsuarioResponse result = null;
            try
            {
                using (var db = new BDVentasContext())
                {

                    var usuario = db.Usuarios.Where(u=>u.Email == user.Email && u.Password == user.Password && u.IsDeleted == false).FirstOrDefault();
                    if (usuario == null) { return null; }
                    else
                    {
                        result = new UsuarioResponse();
                        result.Usuario = usuario;
                        result.Email = usuario.Email;
                        result.Token = GetToken(usuario);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.PkUsuario.ToString()),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
