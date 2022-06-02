using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class Proveedor
    {
        public Proveedor()
        {
            Productos = new HashSet<Producto>();
        }

        public int PkProveedor { get; set; }
        public int? FkTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string RazonSocial { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int? FkContacto { get; set; }
        public DateTime? FechaCrea { get; set; }
        public int? FkUsuarioCrea { get; set; }
        public DateTime? FechaEdita { get; set; }
        public int? FkUsuarioEdita { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Contacto FkContactoNavigation { get; set; }
        public virtual TipoDocumento FkTipoDocumentoNavigation { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}
