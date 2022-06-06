using WebVentas.Models.ModelBD;
using WebVentas.Models.ModelVista.request;

namespace WebVentas.Models.Interfaces
{
    public interface IUsuarioService
    {
        public Usuario Login(LoginRequest user);
    }
}
