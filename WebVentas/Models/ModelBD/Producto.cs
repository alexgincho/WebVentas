using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int PkProducto { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal? Cantidad { get; set; }
        public int? FkUnidad { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }
        public int? FkCategoria { get; set; }
        public int? FkProveedor { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int? FkUsuarioCrea { get; set; }
        public DateTime? FechaCrea { get; set; }
        public int? FkUsuarioEdita { get; set; }
        public DateTime? FechaEdita { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Categorium FkCategoriaNavigation { get; set; }
        public virtual Proveedor FkProveedorNavigation { get; set; }
        public virtual UnidadMedidum FkUnidadNavigation { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
