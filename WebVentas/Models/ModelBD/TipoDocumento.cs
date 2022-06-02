using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class TipoDocumento
    {
        public TipoDocumento()
        {
            Contactos = new HashSet<Contacto>();
            Proveedors = new HashSet<Proveedor>();
        }

        public int PkTipoDocumento { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
