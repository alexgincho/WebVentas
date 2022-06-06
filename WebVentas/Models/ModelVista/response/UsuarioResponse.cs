using WebVentas.Models.ModelBD;

namespace WebVentas.Models.ModelVista.response
{
    public class UsuarioResponse
    {
        public Usuario Usuario { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
