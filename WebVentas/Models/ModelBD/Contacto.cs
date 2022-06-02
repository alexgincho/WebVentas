using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class Contacto
    {
        public Contacto()
        {
            Proveedors = new HashSet<Proveedor>();
        }

        public int PkContacto { get; set; }
        public int? FkTipoDocumento { get; set; }
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Ubigeo { get; set; }
        public int? FkUsuarioCrea { get; set; }
        public DateTime? FechaCrea { get; set; }
        public int? FkUsuarioEdita { get; set; }
        public DateTime? FechaEdita { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TipoDocumento FkTipoDocumentoNavigation { get; set; }
        public virtual ICollection<Proveedor> Proveedors { get; set; }
    }
}
