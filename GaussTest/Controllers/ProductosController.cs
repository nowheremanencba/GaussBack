using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GaussTest.Models;

namespace GaussTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly GaussTestContext _context;

        public ProductosController(GaussTestContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {  
            try
            { 
                return await _context.Productos.Include(o => o.Marca).ToListAsync(); 
            }
            catch (Exception  ex)
            {
                ObjectResult o = new ObjectResult( ex.StackTrace);
                o.StatusCode = 500;
                return o;
            }
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            try
            {
                var producto = await _context.Productos.FindAsync(id);
            
                if (producto == null)
                {
                    return NotFound();
                }
                var marca = await _context.Marca.FindAsync(producto.MarcaID);
                producto.Marca = marca;
                return producto;
            }
            catch (Exception ex)
            {
                ObjectResult o = new ObjectResult(ex.StackTrace);
                o.StatusCode = 500;
                return o;
            }

        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if(VerificarPrecioMayorCosto (producto.Precio, producto.Costo ))
            {
                ObjectResult o = new ObjectResult("El precio es mayor que el costo");
                o.StatusCode = 500;
                return o;
            }

            if (id != producto.id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                if (VerificarPrecioMayorCosto(producto.Precio, producto.Costo))
                {
                    ObjectResult o = new ObjectResult("El precio es mayor que el costo");
                    o.StatusCode = 500;
                    return o;
                }

                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ObjectResult o = new ObjectResult(ex.StackTrace);
                o.StatusCode = 500;
                return o;
            }
            return CreatedAtAction("GetProducto", new { id = producto.id }, producto);
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Producto>> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            try
            {   
                if (producto == null)
                {
                    return NotFound();
                } 
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                ObjectResult o = new ObjectResult(ex.StackTrace);
                o.StatusCode = 500;
                return o;
            }
            return producto;
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.id == id);
        }
        private bool VerificarPrecioMayorCosto(decimal precio, decimal costo)
        { 
            if (precio> costo)  {
                return false; 
            }
            else{
                return true;
            }
        }
    }
}
