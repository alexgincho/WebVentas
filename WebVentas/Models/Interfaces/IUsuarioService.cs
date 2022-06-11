using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;
using WebVentas.Models.ModelVista.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebVentas.Models.Interfaces
{
    public interface IUsuarioService
    {
        public UsuarioResponse Auth(LoginRequest user);
        public Usuario Crear(Usuario user);
        public List<Usuario> GetAll();
        public Usuario Get(int id);
        public Usuario Update(Usuario entity);
        public bool Delete(int id);
    }
}
