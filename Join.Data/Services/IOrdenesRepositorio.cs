using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Join.Models;

namespace Join.Data.Services
{
    public interface IOrdenesRepositorio : IRepositorioGenerico<Orden>
    {
        Task<IEnumerable<Orden>> ObtenerTodosConDetallesAsync();
        Task<Orden> ObtenerConDetallesAsync(int id);
    }
}
