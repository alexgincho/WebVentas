using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int PkCategoria { get; set; }
        public string Descripcion { get; set; }
        public int? FkUsuarioCrea { get; set; }
        public DateTime? FechaCrea { get; set; }
        public int? FkUsuarioEdita { get; set; }
        public DateTime? FechaEdita { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
