namespace ProjectBuoi7.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; } // Nullable for root categories
        public Category ParentCategory { get; set; } // Navigation property
    }
}
