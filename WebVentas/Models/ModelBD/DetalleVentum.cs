using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class DetalleVentum
    {
        public int PkDetalle { get; set; }
        public int? FkVenta { get; set; }
        public int? FkProducto { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public int? Cantidad { get; set; }
        public int? FkUnidad { get; set; }
        public decimal? SubTotal { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Producto FkProductoNavigation { get; set; }
        public virtual Ventum FkVentaNavigation { get; set; }
    }
}
