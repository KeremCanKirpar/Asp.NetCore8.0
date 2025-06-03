using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Dto;
using ProductAPI.Models;

namespace ProductAPI.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductsContext _context;

    public ProductsController(ProductsContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var _products = await _context.Product.Select(p => ProductToDto(p)).ToListAsync();
        if (_products == null || !_products.Any())
        {
            return NotFound();
        }

        return Ok(_products);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var p = await _context.Product.Where(i => i.ProductId == id).Select(p => ProductToDto(p)).FirstOrDefaultAsync();

        if (p == null)
        {
            return NotFound();
        }
        return Ok(p);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product entity)
    {
        _context.Product.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProduct), new { id = entity.ProductId }, entity);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int? id, Product entity)
    {
        if (id != entity.ProductId)
        {
            return BadRequest("ürün idileri uyuşmuyor");
        }

        var product = await _context.Product.FirstOrDefaultAsync(i => i.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        product.ProductName = entity.ProductName;
        product.Price = entity.Price;
        product.IsActive = entity.IsActive;

        try
        {

            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {

            return StatusCode(StatusCodes.Status500InternalServerError, $"Ürün güncellenemedi: {ex.Message}");
        }

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int? id)
    {
        try
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FirstOrDefaultAsync(i => i.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Product.Remove(product);

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Ürün silinemedi: {ex.Message}");
        }

        return NoContent();
    }


    private static ProductDto ProductToDto(Product p)
    {
        var entity = new ProductDto();
        if(p != null)
        {
            entity.ProductId = p.ProductId;
            entity.ProductName = p.ProductName;
            entity.Price = p.Price;
        }
        return entity;
    }
}
