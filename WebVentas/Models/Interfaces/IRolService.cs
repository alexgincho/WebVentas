using System.Collections.Generic;
using WebVentas.Models.ModelBD;

namespace WebVentas.Models.Interfaces
{
    public interface IRolService
    {
        public List<Rol> GetAllRoles();
    }
}
