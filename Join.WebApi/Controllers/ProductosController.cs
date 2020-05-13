using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Join.Data;
using Join.Data.Services;
using Join.Models;

namespace Join.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IProductosRepositio _productosRepositio;

        public ProductosController(IProductosRepositio productosRepositio)
        {
            _productosRepositio = productosRepositio;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProducto()
        {
            try
            {
                return await _productosRepositio.ObtenerProductosAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // GET: api/Productos
        [HttpGet("{id}", Name = "GetProducto")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _productosRepositio.ObtenerProductoAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return producto;
        }

        // Post: api/Productos/5
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                var nuevoProducto = await _productosRepositio.AgregarProducto(producto);
                if (nuevoProducto == null)
                {
                    return BadRequest();
                }

                return CreatedAtAction(nameof(GetProducto), new {id = nuevoProducto.Id}, producto);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PutProducto(int id, [FromBody] Producto producto)
        {
            if (producto == null)
                return NotFound();

            var resultado = await _productosRepositio.Actualizar(producto);
            if (!resultado)
                return BadRequest();

            return producto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                var resultado = await _productosRepositio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        


        // DELETE: api/Productos/5


        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Producto>> DeleteProducto(int id)
        //{
        //    var producto = await _context.Productos.FindAsync(id);
        //    if (producto == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Productos.Remove(producto);
        //    await _context.SaveChangesAsync();

        //    return producto;
        //}

        //private bool ProductoExists(int id)
        //{
        //    return _context.Productos.Any(e => e.Id == id);
        //}
    }
}
