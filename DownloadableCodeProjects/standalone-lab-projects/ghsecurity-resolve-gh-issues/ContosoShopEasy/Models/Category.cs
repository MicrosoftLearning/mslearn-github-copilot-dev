namespace ContosoShopEasy.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public List<Category> SubCategories { get; set; }
        public List<Product> Products { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

        public Category()
        {
            Name = string.Empty;
            Description = string.Empty;
            SubCategories = new List<Category>();
            Products = new List<Product>();
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }

        public Category(int id, string name, string description, int? parentCategoryId = null)
        {
            Id = id;
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
            SubCategories = new List<Category>();
            Products = new List<Product>();
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
        }
    }
}