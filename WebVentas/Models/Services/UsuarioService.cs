using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;
using System.Linq;

namespace WebVentas.Models.Services
{
    public class UsuarioService : IUsuarioService
    {
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

        public Usuario Login(LoginRequest user)
        {
            Usuario result = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    var usuario = db.Usuarios.Where(u=>u.Email == user.Email && u.Password == user.Password && u.IsDeleted == false).FirstOrDefault();
                    if (usuario != null)
                    {
                        result = usuario;
                    }else { throw new Exception(); }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
