using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;
using WebVentas.Models.ModelVista.response;

namespace WebVentas.Models.Interfaces
{
    public interface IUsuarioService
    {
        public UsuarioResponse Auth(LoginRequest user);
        public Usuario Crear(Usuario user);
    }
}
