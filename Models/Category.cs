namespace ProjectBuoi7.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; } // Optional, cho quan hệ cha-con (nếu có)
    }
}
