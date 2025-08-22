using System.ComponentModel.DataAnnotations;

namespace ContosoOnlineStore
{
    public class Order
    {
        [Range(1, int.MaxValue)]
        public int OrderId { get; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string ShippingAddress { get; set; } = string.Empty;

        public List<OrderItem> Items { get; }

        public DateTime OrderDate { get; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        private static int _nextOrderId = 1;
        private static readonly object _orderIdLock = new object();

        public Order()
        {
            lock (_orderIdLock)
            {
                OrderId = _nextOrderId++;
            }
            Items = new List<OrderItem>();
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Pending;
        }

        public Order(string customerEmail, string shippingAddress) : this()
        {
            CustomerEmail = customerEmail?.Trim() ?? throw new ArgumentNullException(nameof(customerEmail));
            ShippingAddress = shippingAddress?.Trim() ?? throw new ArgumentNullException(nameof(shippingAddress));
        }

        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (Items.Count >= 50)
                throw new InvalidOperationException("Cannot add more than 50 items to an order");

            // Check if item already exists and combine quantities
            var existingItem = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                Items.Add(item);
            }
        }

        public bool RemoveItem(int productId)
        {
            return Items.RemoveAll(i => i.ProductId == productId) > 0;
        }

        public int GetTotalItemCount()
        {
            return Items.Sum(i => i.Quantity);
        }
    }

    public enum OrderStatus
    {
        Pending = 0,
        Processing = 1,
        Shipped = 2,
        Delivered = 3,
        Cancelled = 4,
        Returned = 5
    }
}
