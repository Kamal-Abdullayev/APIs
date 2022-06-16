using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProjectX.Entities.DAL;
using ProjectX.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using ProjectX.Extension;
using System.Threading.Tasks;
using ProjectX.Dto;
using ProjectX.Helpers;

namespace ProjectX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Product()
        {
            List<ReturnProduct> productList = _context.Products.Select(p => new ReturnProduct
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
                CategoryName = p.Category.Name,
                Image = $"https://localhost:44365/assets/product/{p.Image}"
            }).ToList();
            return StatusCode(200, productList);
        }

        [HttpGet("{id}")]
        public IActionResult Product(int? id)
        {
            if (id == null) return NotFound();
            ReturnProduct product = _context.Products.Include(p => p.Category)
                .Select(p=> new ReturnProduct{
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive,
                CategoryName = p.Category.Name,
                Image = $"https://localhost:44365/assets/product/{p.Image}"
                })
                .FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();

            return StatusCode(200, product);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto productCreated)
        {
            //ImageUrl Errors
            if (!productCreated.ImageUrl.IsImage())
            {
                return BadRequest("Please upoad only image format!");
            }
            int fileSize = 5;
            if (productCreated.ImageUrl.ValidSize(fileSize))
            {
                return BadRequest($"Image size more than {fileSize}MB!");
            }

            string imagePath = await productCreated.ImageUrl.SaveImage(_env, "assets/product");

            Product newProduct = new Product();
            newProduct.Name = productCreated.Name;
            newProduct.Price = productCreated.Price;
            newProduct.IsActive = productCreated.IsActive;
            newProduct.CategoryId = productCreated.CategoryId;
            newProduct.Image = imagePath;

            _context.Products.Add(newProduct);
            _context.SaveChanges();
            return StatusCode(201, $"{newProduct.Name} created");
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductCreateDto product, int? id)
        {
            if (id == null) return NotFound();
            Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (dbProduct == null) return NotFound();

            //ImageUrl Errors
            if (!product.ImageUrl.IsImage())
            {
                return BadRequest("Please upoad only image format!");
            }
            int fileSize = 5;
            if (product.ImageUrl.ValidSize(fileSize))
            {
                return BadRequest($"Image size more than {fileSize}MB!");
            }

            dbProduct.Name = product.Name;
            dbProduct.Price = product.Price;
            dbProduct.IsActive = product.IsActive;
            dbProduct.CategoryId = product.CategoryId;

            DeleteImage.DeleteImg(_env, "assets/product", dbProduct.Image);
            string imagePath = await product.ImageUrl.SaveImage(_env, "assets/product", product.ImageUrl.FileName);
            dbProduct.Image = imagePath;

            _context.SaveChanges();
            return StatusCode(200, dbProduct);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProductField([FromForm] bool status, int? id)
        {
            if (id == null) return NoContent();
            Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (dbProduct == null) return NotFound();

            dbProduct.IsActive = status;

            _context.SaveChanges();
            return StatusCode(200, dbProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null) return NotFound();
            Product product = _context.Products.FirstOrDefault(e => e.Id == id);
            if (product == null) return NotFound();

            DeleteImage.DeleteImg(_env, "assets/product", product.Image);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return StatusCode(204);
        }
    }
}
