using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProjectBuoi7.Models;

namespace ProjectBuoi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private static List<Category> categories = new List<Category>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category newCategory)
        {
            categories.Add(newCategory);
            return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category updatedCategory)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = updatedCategory.Name;
            category.ParentCategoryId = updatedCategory.ParentCategoryId;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            categories.Remove(category);
            return NoContent();
        }
    }
}
