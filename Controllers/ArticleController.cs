using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using MiniExcelLibs; // Thư viện MiniExcel
using static MiniSoftware.MiniWord; // Thư viện MiniWord
using System.IO; // Để xử lý MemoryStream
using System;
using Xceed.Words.NET;
using Xceed.Document.NET;
using ProjectBuoi7.Models;

namespace ProjectBuoi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        // Bộ nhớ tạm để lưu danh sách bài viết
        private static List<Article> articles = new List<Article>();

        // Lấy danh sách tất cả bài viết
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(articles);
        }

        // Lấy thông tin một bài viết theo ID
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            return Ok(article);
        }

        // Thêm một bài viết mới
        [HttpPost]
        public IActionResult Create([FromBody] Article newArticle)
        {
            newArticle.Id = articles.Count > 0 ? articles.Max(a => a.Id) + 1 : 1; // Tạo ID tự động
            articles.Add(newArticle);
            return CreatedAtAction(nameof(Get), new { id = newArticle.Id }, newArticle);
        }

        // Cập nhật một bài viết
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Article updatedArticle)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            article.Title = updatedArticle.Title;
            article.Content = updatedArticle.Content;
            article.CategoryId = updatedArticle.CategoryId;
            return NoContent();
        }

        // Xóa một bài viết
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            articles.Remove(article);
            return NoContent();
        }

        // Xuất danh sách bài viết ra Excel
        [HttpGet("export")]
        public IActionResult ExportToMiniExcel()
        {
            var data = articles.Select(a => new
            {
                a.Id,
                a.Title,
                a.Content
            }).ToList();

            var stream = new MemoryStream();
            stream.SaveAs(data);
            stream.Position = 0;

            string excelName = $"Articles-{DateTime.Now:yyyyMMddHHmmssfff}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

        // Nhập danh sách bài viết từ Excel
        [HttpPost("import")]
        public IActionResult ImportFromMiniExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using (var stream = file.OpenReadStream())
            {
                var importedData = stream.Query<Article>().ToList();

                foreach (var article in importedData)
                {
                    article.Id = articles.Count > 0 ? articles.Max(a => a.Id) + 1 : 1;
                    articles.Add(article);
                }
            }

            return Ok(new { message = "Import successful.", count = articles.Count });
        }

        // Xuất một bài viết ra Word
        [HttpGet("export-word/{id}")]
        public IActionResult ExportToWord(int id)
        {
            var article = articles.FirstOrDefault(a => a.Id == id);
            if (article == null)
            {
                return NotFound("Article not found.");
            }

            try
            {
                // Tạo một tài liệu Word mới
                using (var doc = DocX.Create("Article.docx"))
                {
                    // Thêm tiêu đề bài viết
                    doc.InsertParagraph($"Title: {article.Title}")
                       .FontSize(16)
                       .Bold()
                       .Alignment = Alignment.center;

                    // Thêm ID của thể loại
                    doc.InsertParagraph($"Category ID: {article.CategoryId}")
                       .FontSize(12)
                       .Italic()
                       .Alignment = Alignment.left;

                    // Thêm nội dung bài viết
                    doc.InsertParagraph("Content:")
                       .FontSize(14)
                       .Bold()
                       .Alignment = Alignment.left;
                    doc.InsertParagraph(article.Content)
                       .FontSize(12)
                       .Alignment = Alignment.left;

                    // Lưu tài liệu vào MemoryStream
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
