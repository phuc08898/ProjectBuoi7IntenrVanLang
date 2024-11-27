using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBuoi7.Data;
using ProjectBuoi7.Models;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;
using Xceed.Words.NET;
using Xceed.Document.NET;

namespace ProjectBuoi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly SQLDbcontext _context;

        public ArticleController(SQLDbcontext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả bài viết
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _context.Articles.ToListAsync();
            return Ok(articles);
        }

        // Lấy thông tin một bài viết theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // Thêm một bài viết mới
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Article newArticle)
        {
            _context.Articles.Add(newArticle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = newArticle.Id }, newArticle);
        }

        // Cập nhật một bài viết
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Article updatedArticle)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            article.Title = updatedArticle.Title;
            article.Content = updatedArticle.Content;
            article.CategoryId = updatedArticle.CategoryId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Xóa một bài viết
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Xuất một bài viết ra Word
        [HttpGet("export-word/{id}")]
        public async Task<IActionResult> ExportToWord(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound("Article not found.");
            }

            try
            {
                // Tạo tài liệu Word
                using (var doc = DocX.Create("Article.docx"))
                {
                    doc.InsertParagraph($"Title: {article.Title}")
                       .FontSize(16)
                       .Bold()
                       .Alignment = Alignment.center;

                    doc.InsertParagraph($"Category ID: {article.CategoryId}")
                       .FontSize(12)
                       .Italic()
                       .Alignment = Alignment.left;

                    doc.InsertParagraph("Content:")
                       .FontSize(14)
                       .Bold()
                       .Alignment = Alignment.left;

                    doc.InsertParagraph(article.Content)
                       .FontSize(12)
                       .Alignment = Alignment.left;

                    var stream = new MemoryStream();
                    doc.SaveAs(stream);
                    stream.Position = 0;

                    string wordFileName = $"Article-{article.Id}-{DateTime.Now:yyyyMMddHHmmssfff}.docx";
                    return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", wordFileName);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
