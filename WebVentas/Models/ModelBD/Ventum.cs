using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace WebVentas.Models.ModelBD
{
    public partial class Ventum
    {
        public Ventum()
        {
            DetalleVenta = new HashSet<DetalleVentum>();
        }

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

        [JsonIgnore]
        public virtual Usuario FkUsuarioNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
