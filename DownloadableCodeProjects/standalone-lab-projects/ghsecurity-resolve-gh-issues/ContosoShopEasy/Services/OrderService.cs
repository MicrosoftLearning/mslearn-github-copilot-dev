using ContosoShopEasy.Models;
using ContosoShopEasy.Data;

namespace ContosoShopEasy.Services
{
    public class OrderService
    {
        private readonly OrderRepository _orderRepository;
        private readonly ProductService _productService;
        private readonly UserService _userService;

        public OrderService(OrderRepository orderRepository, ProductService productService, UserService userService)
        {
            _orderRepository = orderRepository;
            _productService = productService;
            _userService = userService;
        }

        public Order CreateOrder(int userId, List<OrderItem> items)
        {
            var user = _userService.GetUser(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {userId} not found");
            }

            // Generate order number - Security vulnerability: predictable order numbers
            string orderNumber = GenerateOrderNumber(userId);

            var order = new Order
            {
                Id = _orderRepository.GetNextOrderId(),
                UserId = userId,
                OrderNumber = orderNumber,
                OrderDate = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                OrderItems = new List<OrderItem>()
            };

            decimal subtotal = 0;
            foreach (var item in items)
            {
                var product = _productService.GetProductById(item.ProductId);
                if (product == null)
                {
                    Console.WriteLine($"[WARNING] Product {item.ProductId} not found, skipping item");
                    continue;
                }

                if (!_productService.IsProductInStock(item.ProductId, item.Quantity))
                {
                    Console.WriteLine($"[WARNING] Insufficient stock for product {product.Name}");
                    continue;
                }

                var orderItem = new OrderItem
                {
                    Id = order.OrderItems.Count + 1,
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Product = product,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * item.Quantity
                };

                order.OrderItems.Add(orderItem);
                subtotal += orderItem.TotalPrice;

                // Update stock
                _productService.UpdateStock(item.ProductId, -item.Quantity);
            }

            // Calculate totals
            order.SubTotal = subtotal;
            order.TaxAmount = subtotal * 0.08m; // 8% tax
            order.ShippingCost = CalculateShippingCost(subtotal);
            order.TotalAmount = order.SubTotal + order.TaxAmount + order.ShippingCost;

            _orderRepository.AddOrder(order);

            Console.WriteLine($"[INFO] Order created: {orderNumber} for user {user.Username}");
            Console.WriteLine($"[DEBUG] Order total: ${order.TotalAmount}");

            return order;
        }

        public Order? GetOrder(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public Order? GetOrderByNumber(string orderNumber)
        {
            return _orderRepository.GetOrderByNumber(orderNumber);
        }

        public List<Order> GetUserOrders(int userId)
        {
            return _orderRepository.GetOrdersByUserId(userId);
        }

        public bool UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order != null)
            {
                order.Status = status;
                
                if (status == OrderStatus.Shipped)
                {
                    order.ShippedDate = DateTime.UtcNow;
                    order.TrackingNumber = GenerateTrackingNumber(order.OrderNumber);
                }
                else if (status == OrderStatus.Delivered)
                {
                    order.DeliveredDate = DateTime.UtcNow;
                }

                Console.WriteLine($"[INFO] Order {order.OrderNumber} status updated to {status}");
                return true;
            }
            return false;
        }

        public bool CancelOrder(int orderId)
        {
            var order = _orderRepository.GetOrderById(orderId);
            if (order != null && order.Status == OrderStatus.Pending)
            {
                // Restore stock
                foreach (var item in order.OrderItems)
                {
                    _productService.UpdateStock(item.ProductId, item.Quantity);
                }

                order.Status = OrderStatus.Cancelled;
                Console.WriteLine($"[INFO] Order {order.OrderNumber} has been cancelled");
                return true;
            }
            return false;
        }

        // Security vulnerability: Predictable order number generation
        private string GenerateOrderNumber(int userId)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMdd");
            string userPart = userId.ToString("D4");
            int orderCount = _orderRepository.GetOrdersByUserId(userId).Count + 1;
            
            return $"ORD-{timestamp}-{userPart}-{orderCount:D3}";
        }

        private decimal CalculateShippingCost(decimal subtotal)
        {
            if (subtotal >= 100) return 0; // Free shipping over $100
            if (subtotal >= 50) return 5.99m;
            return 9.99m;
        }

        // Security vulnerability: Predictable tracking number generation
        private string GenerateTrackingNumber(string orderNumber)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmm");
            return $"TRK{timestamp}{orderNumber.Replace("-", "").Substring(3, 6)}";
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public decimal GetTotalRevenue()
        {
            return _orderRepository.GetAllOrders()
                .Where(o => o.Status != OrderStatus.Cancelled)
                .Sum(o => o.TotalAmount);
        }
    }
}