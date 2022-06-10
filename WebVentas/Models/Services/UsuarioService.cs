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

        public Usuario Get(int id)
        {
            Usuario user = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    var usuario = db.Usuarios.Where(u=>u.PkUsuario == id).FirstOrDefault();
                    if (usuario == null) { return null; }
                    else
                    {
                        user = new Usuario();
                        user = usuario;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return user;
        }
    }
}
