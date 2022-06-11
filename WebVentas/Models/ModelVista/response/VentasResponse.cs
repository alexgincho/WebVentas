using System;
using System.Collections.Generic;

namespace WebVentas.Models.ModelVista.response
{
    public class VentasResponse
    {
        public int PkVenta { get; set; }
        public string Codigo { get; set; }
        public int? FkUsuario { get; set; }
        public string Ubigeo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public bool IsDelivery { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Observacion { get; set; }
        public decimal? Igv { get; set; }
        public decimal? Total { get; set; }
        public int? FkUsuarioCrea { get; set; }
        public DateTime? FechaCrea { get; set; }
        public int? FkUsuarioEdita { get; set; }
        public DateTime? FechaEdita { get; set; }
        public bool? IsDeleted { get; set; }

        public UsuarioModel Usuario { get; set; }
        public List<DetalleVentaModel> DetalleVenta { get; set; }
    }

    public class DetalleVentaModel
    {
        public int PkDetalle { get; set; }
        public int? FkVenta { get; set; }
        public int? FkProducto { get; set; }
        public decimal? PrecioUnidad { get; set; }
        public int? Cantidad { get; set; }
        public int? FkUnidad { get; set; }
        public decimal? SubTotal { get; set; }
        public ProductoModel Producto { get; set; }
    }
    public class UsuarioModel
    {
        public int PkUsuario { get; set; }
        public int? FkTipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public string Email { get; set; }
    }
    public class ProductoModel
    {
        public int PkProducto { get; set; }
        public string Codigo { get; set; }
        public string NombreProducto { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal? Cantidad { get; set; }
        public int? FkUnidad { get; set; }
        public decimal? PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }
    }
}
