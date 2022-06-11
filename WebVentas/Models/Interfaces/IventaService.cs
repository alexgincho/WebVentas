using System.Collections.Generic;
using WebVentas.Models.ModelBD;

namespace WebVentas.Models.Interfaces
{
    public interface IVentaService
    {
        public Ventum CreateVenta(Ventum venta);
        public List<Ventum> GetVentaList(int id);
        public List<DetalleVentum> GetDetalleList(int id);
    }
}
