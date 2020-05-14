using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Join.Data;
using Join.Data.Services;
using Join.Dto;
using Join.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Join.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private IProductosRepositorio _productosRepositorio;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductosRepositorio productosRepositorio, 
            IMapper mapper,
            ILogger<ProductosController> logger)
        {
            _productosRepositorio = productosRepositorio;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            try
            {
                var producto = await _productosRepositorio.ObtenerProductosAsync();
                return _mapper.Map<List<ProductoDto>>(producto);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error al {nameof(Get)}: ${ex.Message}");
                return BadRequest();
            }
        }

        // GET: api/Productos
        [HttpGet("{id}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {
            var producto = await _productosRepositorio.ObtenerProductoAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductoDto>(producto);
        }

        // Post: api/Productos/5
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Post(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);
                var nuevoProducto = await _productosRepositorio.Agregar(producto);
                if (nuevoProducto == null)
                {
                    return BadRequest();
                }

                var nuevoProductoDto = _mapper.Map<ProductoDto>(nuevoProducto);
                return CreatedAtAction(nameof(Get), new {id = nuevoProductoDto.Id}, nuevoProductoDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al agregar en {nameof(Post)}:: ${ex.Message}");
                return BadRequest();
            }
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody] ProductoDto productoDto)
        {
            if (productoDto == null)
                return NotFound();

            var producto = _mapper.Map<Producto>(productoDto);

            var resultado = await _productosRepositorio.Actualizar(producto);
            if (!resultado)
                return BadRequest();

            return productoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _productosRepositorio.Eliminar(id);
                if (!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al {nameof(Delete)}:: id ${id}, ${ex.Message}");
                return BadRequest();
            }
        }
        


        
    }
}
