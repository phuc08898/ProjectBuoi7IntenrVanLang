using Microsoft.EntityFrameworkCore;
using ProjectBuoi7.Models; // Import namespace chứa các models (lớp mô hình)

namespace ProjectBuoi7.Data
{
    public class SQLDbcontext : DbContext
    {
        // Constructor để truyền DbContextOptions
        public SQLDbcontext(DbContextOptions<SQLDbcontext> dbContextOptions) : base(dbContextOptions)
        {
        }

        // Các DbSet đại diện cho bảng trong cơ sở dữ liệu
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ tự tham chiếu (self-referencing relationship)
            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Không cho phép xóa cha nếu còn con
        }
    }
}
