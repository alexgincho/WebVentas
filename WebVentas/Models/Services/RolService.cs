using System;
using System.Collections.Generic;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using System.Linq;
namespace WebVentas.Models.Services
{
    public class RolService : IRolService
    {
        public List<Rol> GetAllRoles()
        {
            List<Rol> roles = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    var List = db.Rols.Where(r => r.PkRol != 2).ToList();
                    if(List.Count > 0) { roles = List; }
                    else { throw new Exception(); }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return roles;
        }
    }
}
