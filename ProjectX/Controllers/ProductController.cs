using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectX.Entities.DAL;
using ProjectX.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Product()
        {
            List<Product> products = _context.Products.ToList();
            return StatusCode(200, products);
        }

        [HttpGet("{id}")]
        public IActionResult Product(int? id)
        {
            if (id == null) return NotFound();
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            return StatusCode(200, product);
        }

        [HttpPost("create/")]
        public IActionResult CreateProduct(Product product)
        {
            if (product == null) return NotFound();

            Product newProduct = new Product();

            newProduct.Name = product.Name;
            newProduct.Price = product.Price;

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return StatusCode(201, $"{newProduct.Name} created");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null) return NotFound();
            Product product = _context.Products.FirstOrDefault(e => e.Id == id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();
            return StatusCode(204);
        }
    }
}
