using Microsoft.AspNetCore.Mvc;
using ProjectX.Data.Entities;
using ProjectX.Dto;
using ProjectX.Entities.DAL;
using System.Collections.Generic;
using System.Linq;

namespace ProjectX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            List<Category> categories = _context.Categories.ToList();
            return StatusCode(200, categories);
        }

        [HttpGet("{id}")]
        public IActionResult Category(int? id)
        {
            if (id == null) return NotFound();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            return StatusCode(200, category);
        }

        [HttpPost("")]
        public IActionResult CreateCategory([FromForm] Category category)
        {
            if (category == null) return NotFound();
            bool isExistCategory = _context.Categories.Any(p => p.Name == category.Name);
            if (isExistCategory) return BadRequest("This Category alredy exist!");
            Category newCategory = new Category();

            newCategory.Name = category.Name;
            newCategory.IsActive = category.IsActive;

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return StatusCode(201, $"{newCategory.Name} is created!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromForm] Category category, int? id)
        {
            if (id == null) return NotFound();
            if (category == null) return NotFound();

            Category categoryWithId = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryWithId == null) return NotFound();

            Category categoryForName = _context.Categories.FirstOrDefault(c => c.Name == category.Name);

            if(categoryForName != null)
            {
                if (categoryForName.Name != categoryWithId.Name)
                {
                    return BadRequest("This Category alredy exist!");
                }
            }

            categoryWithId.Name = category.Name;
            categoryWithId.IsActive = category.IsActive;

            _context.SaveChanges();
            return StatusCode(201, categoryWithId);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateCategoryStatus([FromForm] bool isActive, int? id)
        {
            if (id == null) return NotFound();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            category.IsActive = isActive;
            _context.SaveChanges();
            return StatusCode(200, category);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null) return NotFound();
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return StatusCode(204);
        }


    }
}
