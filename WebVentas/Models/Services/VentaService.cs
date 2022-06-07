using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;

namespace WebVentas.Models.Services
{
    public class VentaService : IVentaService
    {
        public Ventum CreateVenta(Ventum venta)
        {
            Ventum result = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }
    }
}
