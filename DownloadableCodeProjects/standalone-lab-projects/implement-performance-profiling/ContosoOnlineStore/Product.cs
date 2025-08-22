using System.ComponentModel.DataAnnotations;

namespace ContosoOnlineStore
{
    public class Product
    {
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be positive")]
        public int Id { get; }

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Product name must be between 1 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s\-_\.]+$", ErrorMessage = "Product name contains invalid characters")]
        public string Name { get; }

        [Range(0.01, 100000.00, ErrorMessage = "Product price must be between $0.01 and $100,000.00")]
        public decimal Price { get; }

        [Range(0, int.MaxValue, ErrorMessage = "Initial stock cannot be negative")]
        public int InitialStock { get; }

        public string Category { get; }

        public string Description { get; }

        public DateTime CreatedAt { get; }

        public Product(int id, string name, decimal price, int initialStock, string category = "General", string description = "")
        {
            if (id <= 0)
                throw new ArgumentException("Product ID must be positive", nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));

            if (price < 0.01m)
                throw new ArgumentException("Product price must be at least $0.01", nameof(price));

            if (initialStock < 0)
                throw new ArgumentException("Initial stock cannot be negative", nameof(initialStock));

            Id = id;
            Name = name.Trim();
            Price = price;
            InitialStock = initialStock;
            Category = category.Trim();
            Description = description.Trim();
            CreatedAt = DateTime.UtcNow;
        }

        public override string ToString()
        {
            return $"Product[{Id}]: {Name} - {Price:C}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Product other && Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
