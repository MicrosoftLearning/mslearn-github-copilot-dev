using System.ComponentModel.DataAnnotations;

namespace ContosoOnlineStore
{
    public class OrderItem
    {
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be positive")]
        public int ProductId { get; set; }

        [Range(1, 1000, ErrorMessage = "Quantity must be between 1 and 1000")]
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => UnitPrice * Quantity;

        public OrderItem(int productId, int quantity, decimal unitPrice = 0)
        {
            if (productId <= 0)
                throw new ArgumentException("Product ID must be positive", nameof(productId));

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive", nameof(quantity));

            if (quantity > 1000)
                throw new ArgumentException("Quantity cannot exceed 1000", nameof(quantity));

            if (unitPrice < 0)
                throw new ArgumentException("Unit price cannot be negative", nameof(unitPrice));

            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public override string ToString()
        {
            return $"OrderItem[ProductId={ProductId}, Quantity={Quantity}, UnitPrice={UnitPrice:C}, Total={TotalPrice:C}]";
        }
    }
}
