using ContosoShopEasy.Models;

namespace ContosoShopEasy.Data
{
    public class OrderRepository
    {
        private static List<Order> _orders = new List<Order>();
        private static int _nextOrderId = 1;

        static OrderRepository()
        {
            InitializeOrders();
        }

        private static void InitializeOrders()
        {
            // Pre-populate with some sample orders for demo purposes
            var sampleOrders = new List<Order>
            {
                new Order(1, 1, "ORD-20241029-0001-001")
                {
                    OrderDate = DateTime.UtcNow.AddDays(-5),
                    Status = OrderStatus.Delivered,
                    SubTotal = 429.99m,
                    TaxAmount = 34.40m,
                    ShippingCost = 0m,
                    TotalAmount = 464.39m,
                    ShippedDate = DateTime.UtcNow.AddDays(-3),
                    DeliveredDate = DateTime.UtcNow.AddDays(-1),
                    TrackingNumber = "TRK202410290001001",
                    ShippingAddress = new Address("123 Main St", "Anytown", "CA", "12345", "USA"),
                    BillingAddress = new Address("123 Main St", "Anytown", "CA", "12345", "USA"),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem(1, 1, 29, 1, 429.99m) // Dyson Supersonic Hair Dryer
                    }
                },
                new Order(2, 2, "ORD-20241028-0002-001")
                {
                    OrderDate = DateTime.UtcNow.AddDays(-3),
                    Status = OrderStatus.Shipped,
                    SubTotal = 1899.99m,
                    TaxAmount = 152.00m,
                    ShippingCost = 0m,
                    TotalAmount = 2051.99m,
                    ShippedDate = DateTime.UtcNow.AddDays(-1),
                    TrackingNumber = "TRK202410280002001",
                    ShippingAddress = new Address("456 Oak Ave", "Springfield", "TX", "67890", "USA"),
                    BillingAddress = new Address("456 Oak Ave", "Springfield", "TX", "67890", "USA"),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem(2, 2, 2, 1, 1899.99m) // Dell XPS 13
                    }
                },
                new Order(3, 1, "ORD-20241027-0001-002")
                {
                    OrderDate = DateTime.UtcNow.AddDays(-2),
                    Status = OrderStatus.Processing,
                    SubTotal = 579.97m,
                    TaxAmount = 46.40m,
                    ShippingCost = 0m,
                    TotalAmount = 626.37m,
                    ShippingAddress = new Address("123 Main St", "Anytown", "CA", "12345", "USA"),
                    BillingAddress = new Address("123 Main St", "Anytown", "CA", "12345", "USA"),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem(3, 3, 9, 1, 399.99m), // Sony WH-1000XM5
                        new OrderItem(4, 3, 26, 4, 39.99m)  // YETI Rambler 30oz (4 units)
                    }
                },
                new Order(4, 4, "ORD-20241026-0004-001")
                {
                    OrderDate = DateTime.UtcNow.AddDays(-1),
                    Status = OrderStatus.Pending,
                    SubTotal = 89.97m,
                    TaxAmount = 7.20m,
                    ShippingCost = 9.99m,
                    TotalAmount = 107.16m,
                    ShippingAddress = new Address("321 Pine St", "Riverside", "FL", "33101", "USA"),
                    BillingAddress = new Address("321 Pine St", "Riverside", "FL", "33101", "USA"),
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem(5, 4, 17, 1, 79.99m), // Levi's 511 Slim Jeans
                        new OrderItem(6, 4, 35, 1, 19.99m)  // AUKEY Car Charger
                    }
                }
            };

            _orders.AddRange(sampleOrders);
            _nextOrderId = _orders.Count + 1;
        }

        public List<Order> GetAllOrders()
        {
            return _orders.ToList();
        }

        public Order? GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public Order? GetOrderByNumber(string orderNumber)
        {
            if (string.IsNullOrEmpty(orderNumber))
                return null;

            return _orders.FirstOrDefault(o => o.OrderNumber.Equals(orderNumber, StringComparison.OrdinalIgnoreCase));
        }

        public List<Order> GetOrdersByUserId(int userId)
        {
            return _orders.Where(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToList();
        }

        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            return _orders.Where(o => o.Status == status).OrderByDescending(o => o.OrderDate).ToList();
        }

        public void AddOrder(Order order)
        {
            order.Id = _nextOrderId++;
            order.OrderDate = DateTime.UtcNow;
            _orders.Add(order);
        }

        public bool UpdateOrder(Order order)
        {
            var existingOrder = GetOrderById(order.Id);
            if (existingOrder != null)
            {
                var index = _orders.IndexOf(existingOrder);
                _orders[index] = order;
                return true;
            }
            return false;
        }

        public bool DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            if (order != null && order.Status == OrderStatus.Pending)
            {
                _orders.Remove(order);
                return true;
            }
            return false;
        }

        public List<Order> GetRecentOrders(int count = 10)
        {
            return _orders.OrderByDescending(o => o.OrderDate).Take(count).ToList();
        }

        public List<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate)
        {
            return _orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate)
                          .OrderByDescending(o => o.OrderDate)
                          .ToList();
        }

        public decimal GetTotalRevenue(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _orders.Where(o => o.Status != OrderStatus.Cancelled);
            
            if (startDate.HasValue)
                query = query.Where(o => o.OrderDate >= startDate.Value);
            
            if (endDate.HasValue)
                query = query.Where(o => o.OrderDate <= endDate.Value);
            
            return query.Sum(o => o.TotalAmount);
        }

        public int GetOrderCount(OrderStatus? status = null)
        {
            if (status.HasValue)
                return _orders.Count(o => o.Status == status.Value);
            
            return _orders.Count;
        }

        public Dictionary<OrderStatus, int> GetOrderStatusCounts()
        {
            return _orders.GroupBy(o => o.Status)
                         .ToDictionary(g => g.Key, g => g.Count());
        }

        public int GetNextOrderId()
        {
            return _nextOrderId;
        }

        // Security vulnerability: Method to get all order details including payment info
        public List<object> GetAllOrderDetailsWithPayments()
        {
            return _orders.Select(o => new
            {
                OrderId = o.Id,
                OrderNumber = o.OrderNumber,
                UserId = o.UserId,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                PaymentInfo = o.PaymentInfo,
                ShippingAddress = o.ShippingAddress?.ToString(),
                BillingAddress = o.BillingAddress?.ToString()
            }).Cast<object>().ToList();
        }
    }
}