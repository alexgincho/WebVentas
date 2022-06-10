using System;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using System.Linq;
namespace WebVentas.Models.Services
{
    public class VentaService : IVentaService
    {
        public Ventum CreateVenta(Ventum venta)
        {
            Ventum result = null;
            using (var db = new BDVentasContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var oModel = new Ventum();
                        oModel.Codigo = GenerarCodigo();
                        oModel.FkUsuario = venta.FkUsuario;
                        oModel.FkUsuarioCrea = venta.FkUsuario;
                        oModel.Ubigeo = venta.Ubigeo;
                        oModel.Direccion = venta.Direccion;
                        oModel.Telefono = venta.Telefono;
                        oModel.IsDelivery = venta.IsDelivery;
                        oModel.Estado = 1;
                        oModel.FechaRegistro = DateTime.Now;
                        oModel.FechaEntrega = DateTime.Now;
                        oModel.Observacion = venta.Observacion;
                        oModel.Igv = (decimal) 0.18;
                        oModel.Total = venta.DetalleVenta.Sum(d => d.Cantidad * d.PrecioUnidad);
                        oModel.FechaCrea = DateTime.Now;
                        db.Venta.Add(oModel);
                        db.SaveChanges();
                        foreach(var item in venta.DetalleVenta)
                        {
                            var oDetalle = new DetalleVentum();
                            oDetalle.FkVenta = oModel.PkVenta;
                            oDetalle.FkProducto = item.FkProducto;
                            oDetalle.Cantidad =  item.Cantidad;
                            oDetalle.FkUnidad = item.FkUnidad;
                            oDetalle.PrecioUnidad = item.PrecioUnidad;
                            oDetalle.SubTotal = item.SubTotal;
                            db.DetalleVenta.Add(oDetalle);
                            db.SaveChanges();
                        }
                        if(oModel != null) { result = new Ventum(); result = oModel; }
                        else { throw new Exception(); }
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error. En la Base de Datos.");
                    }
                }
            }
            return result;
        }
        public string GenerarCodigo()
        {
            string codigo = "";
            try
            {
                using (var db = new BDVentasContext())
                {
                    var venta = db.Venta.ToList().OrderByDescending(v=>v.PkVenta);
                    if(venta.Count() > 0)
                    {
                        codigo = "F00000" + venta.Count() + 1;
                    }
                    else
                    {
                        codigo = "F000001";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return codigo;
        }
    }
}
