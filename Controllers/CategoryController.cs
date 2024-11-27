using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBuoi7.Data;
using ProjectBuoi7.Models;

namespace ProjectBuoi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly SQLDbcontext _context;

        public CategoryController(SQLDbcontext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy danh sách tất cả danh mục, bao gồm thông tin cha (nếu có).
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _context.Categories
                                               .Include(c => c.ParentCategory) // Include thông tin cha
                                               .ToListAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving categories.", details = ex.Message });
            }
        }

        /// <summary>
        /// Lấy danh mục theo ID, bao gồm thông tin cha.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var category = await _context.Categories
                                             .Include(c => c.ParentCategory) // Include thông tin cha
                                             .FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the category.", details = ex.Message });
            }
        }

        /// <summary>
        /// Tạo mới danh mục.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category newCategory)
        {
            if (newCategory == null || string.IsNullOrWhiteSpace(newCategory.Name))
            {
                return BadRequest(new { message = "Category data is invalid." });
            }

            try
            {
                // Kiểm tra ParentCategoryId (nếu có)
                if (newCategory.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _context.Categories.FindAsync(newCategory.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        return BadRequest(new { message = "Parent category not found." });
                    }
                }

                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(Get), new { id = newCategory.Id }, newCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the category.", details = ex.Message });
            }
        }

        /// <summary>
        /// Cập nhật danh mục.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Category updatedCategory)
        {
            if (updatedCategory == null || string.IsNullOrWhiteSpace(updatedCategory.Name))
            {
                return BadRequest(new { message = "Category data is invalid." });
            }

            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                // Kiểm tra ParentCategoryId (nếu có)
                if (updatedCategory.ParentCategoryId.HasValue)
                {
                    var parentCategory = await _context.Categories.FindAsync(updatedCategory.ParentCategoryId.Value);
                    if (parentCategory == null)
                    {
                        return BadRequest(new { message = "Parent category not found." });
                    }
                }

                // Cập nhật dữ liệu
                category.Name = updatedCategory.Name;
                category.ParentCategoryId = updatedCategory.ParentCategoryId;

                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the category.", details = ex.Message });
            }
        }

        /// <summary>
        /// Xóa danh mục.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                if (category == null)
                {
                    return NotFound(new { message = $"Category with ID {id} not found." });
                }

                // Kiểm tra nếu danh mục có con
                var hasChildren = await _context.Categories.AnyAsync(c => c.ParentCategoryId == id);
                if (hasChildren)
                {
                    return BadRequest(new { message = "Cannot delete a category that has child categories." });
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the category.", details = ex.Message });
            }
        }
    }
}
