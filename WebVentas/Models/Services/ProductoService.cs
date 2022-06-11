using System;
using System.Collections.Generic;
using WebVentas.Models.Interfaces;
using WebVentas.Models.ModelBD;
using System.Linq;
namespace WebVentas.Models.Services
{
    public class ProductoService : IProductoService
    {
        public List<Producto> GetAllProductos()
        {
            List<Producto> products = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    var List = db.Productos.Join(db.Categoria, prod => prod.FkCategoria, cat => cat.PkCategoria,
                        (prod, cat) => new {
                            prod = prod,
                            cat = cat
                        }).Join(db.UnidadMedida,x=>x.prod.FkUnidad, um=> um.PkUnidad, (x, um) => new { 
                            x.prod,
                            x.cat,
                            um = um
                        }).Where(x=>x.prod.IsDeleted != true).OrderByDescending(x=>x.prod.PkProducto).ToList();
                    if(List.Count() > 0)
                    {
                        products = new List<Producto>();
                        List.ForEach(x => {
                            var oModel = new Producto(); oModel.FkCategoriaNavigation = new Categorium(); oModel.FkUnidadNavigation = new UnidadMedidum();
                            oModel.PkProducto = x.prod.PkProducto;
                            oModel.Codigo = x.prod.Codigo;
                            oModel.NombreProducto = x.prod.NombreProducto;
                            oModel.Descripcion = x.prod.Descripcion;
                            oModel.Cantidad = x.prod.Cantidad;
                            oModel.FkUnidad = x.prod.FkUnidad;
                            oModel.Imagen = x.prod.Imagen;
                            oModel.PrecioCompra = x.prod.PrecioCompra;
                            oModel.PrecioVenta = x.prod.PrecioVenta;
                            oModel.FkProveedor = x.prod.FkProveedor;
                            oModel.FechaRegistro = x.prod.FechaRegistro;
                            oModel.FechaVencimiento = x.prod.FechaVencimiento;
                            oModel.FkCategoriaNavigation.Descripcion = x.cat.Descripcion;
                            oModel.FkUnidadNavigation.Codigo = x.um.Codigo;
                            oModel.FkUnidadNavigation.Descripcion= x.um.Descripcion;

                            products.Add(oModel);
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return products;
        }

        public List<Producto> GetAllProductosCategoria(int id)
        {
            throw new System.NotImplementedException();
        }

        public Producto GetById(int id)
        {
            Producto product = null;
            try
            {
                using (var db = new BDVentasContext())
                {
                    var produ = db.Productos.Join(db.Categoria, prod => prod.FkCategoria, cat => cat.PkCategoria,
                        (prod, cat) => new {
                            prod = prod,
                            cat = cat
                        }).Join(db.UnidadMedida, x => x.prod.FkUnidad, um => um.PkUnidad, (x, um) => new {
                            x.prod,
                            x.cat,
                            um = um
                        }).Where(x => x.prod.IsDeleted != true && x.prod.PkProducto == id).FirstOrDefault();
                    if (produ != null)
                    {
                         product = new Producto(); product.FkCategoriaNavigation = new Categorium(); product.FkUnidadNavigation = new UnidadMedidum();
                         product.PkProducto = produ.prod.PkProducto;
                         product.Codigo = produ.prod.Codigo;
                         product.NombreProducto = produ.prod.NombreProducto;
                         product.Descripcion = produ.prod.Descripcion;
                         product.Cantidad = produ.prod.Cantidad;
                         product.Imagen =  produ.prod.Imagen;
                         product.FkUnidad = produ.prod.FkUnidad;
                         product.PrecioCompra = produ.prod.PrecioCompra;
                         product.PrecioVenta = produ.prod.PrecioVenta;
                         product.FkProveedor = produ.prod.FkProveedor;
                         product.FechaRegistro = produ.prod.FechaRegistro;
                         product.FechaVencimiento = produ.prod.FechaVencimiento;
                         product.FkCategoriaNavigation.Descripcion = produ.cat.Descripcion;
                         product.FkUnidadNavigation.Codigo = produ.um.Codigo;
                         product.FkUnidadNavigation.Descripcion = produ.um.Descripcion;

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }
    }
}
