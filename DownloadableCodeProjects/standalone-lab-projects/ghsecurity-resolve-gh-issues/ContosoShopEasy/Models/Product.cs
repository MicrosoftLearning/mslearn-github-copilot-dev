namespace ContosoShopEasy.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Brand { get; set; }
        public string SKU { get; set; }
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public string ImageUrl { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }

        public Product()
        {
            Name = string.Empty;
            Description = string.Empty;
            Brand = string.Empty;
            SKU = string.Empty;
            ImageUrl = string.Empty;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        public Product(int id, string name, string description, decimal price, int categoryId, 
                      string brand, string sku, int stockQuantity, string imageUrl, double rating = 0.0, int reviewCount = 0)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CategoryId = categoryId;
            Brand = brand;
            SKU = sku;
            StockQuantity = stockQuantity;
            ImageUrl = imageUrl;
            Rating = rating;
            ReviewCount = reviewCount;
            IsActive = true;
            CreatedDate = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }
    }
}