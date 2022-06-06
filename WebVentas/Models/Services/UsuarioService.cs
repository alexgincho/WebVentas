using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;
using System.Linq;

namespace WebVentas.Models.Services
{
    public class UsuarioService : IUsuarioService
    {
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
