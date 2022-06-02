using System.Collections.Generic;
using WebVentas.Models.ModelBD;

namespace WebVentas.Models.Interfaces
{
    public interface IProductoService
    {
        public Producto GetById(int id);
        public List<Producto> GetAllProductos();
        public List<Producto> GetAllProductosCategoria(int id);
    }
}
