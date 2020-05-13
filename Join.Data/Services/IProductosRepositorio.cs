using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Join.Models;

namespace Join.Data.Services
{
    public interface IProductosRepositio
    {
        Task<List<Producto>> ObtenerProductosAsync();
        Task<Producto> ObtenerProductoAsync(int id);
        Task<Producto> AgregarProducto(Producto producto);
        Task<bool> Actualizar(Producto producto);
        Task<bool> Eliminar(int id);
    }
}
