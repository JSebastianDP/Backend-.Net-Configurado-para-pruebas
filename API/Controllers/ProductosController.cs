// API/Controllers/ProductosController.cs
using Domain.Entities;
using Infrastructure.Persistence;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    // GET api/productos
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var productos = await _context.Productos.ToListAsync();
        return Ok(productos);
    }

    // GET api/productos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
            return NotFound();
        return Ok(producto);
    }

    // POST api/productos
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductoDto dto)
    {
        var producto = new Producto
        {
            Nombre = dto.Nombre,
            Precio = dto.Precio
        };

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    // PUT api/productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductoDto dto)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
            return NotFound();

        producto.Nombre = dto.Nombre;
        producto.Precio = dto.Precio;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE api/productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
            return NotFound();

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
