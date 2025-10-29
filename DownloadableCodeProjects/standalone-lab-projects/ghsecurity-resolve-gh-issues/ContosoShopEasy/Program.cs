using ContosoShopEasy.Models;
using ContosoShopEasy.Services;
using ContosoShopEasy.Data;
using ContosoShopEasy.Security;

namespace ContosoShopEasy
{
    class Program
    {
        // Dependency injection setup (manual for simplicity in this educational project)
        private static ProductRepository _productRepository = new ProductRepository();
        private static UserRepository _userRepository = new UserRepository();
        private static OrderRepository _orderRepository = new OrderRepository();
        
        private static ProductService _productService = new ProductService(_productRepository);
        private static UserService _userService = new UserService(_userRepository);
        private static PaymentService _paymentService = new PaymentService(_orderRepository);
        private static OrderService _orderService = new OrderService(_orderRepository, _productService, _userService);
        private static SecurityValidator _securityValidator = new SecurityValidator();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Welcome to ContosoShopEasy E-Commerce Platform ===");
            Console.WriteLine("This application demonstrates a realistic e-commerce system with");
            Console.WriteLine("intentional security vulnerabilities for educational purposes.");
            Console.WriteLine();

            // Run security audit first (shows vulnerabilities)
            _securityValidator.RunSecurityAudit();
            Console.WriteLine();

            // Demonstrate the e-commerce workflow
            DemonstrateECommerceWorkflow();

            Console.WriteLine();
            Console.WriteLine("=== End of ContosoShopEasy Demo ===");
            Console.WriteLine("Application completed successfully!");
            Console.WriteLine("Total products in catalog: " + _productService.GetAllProducts().Count);
            Console.WriteLine("Total registered users: " + _userService.GetAllUsers().Count);
            Console.WriteLine("Total revenue: $" + _orderService.GetTotalRevenue().ToString("F2"));
        }

        static void DemonstrateECommerceWorkflow()
        {
            Console.WriteLine("=== E-Commerce Workflow Demonstration ===");
            
            // 1. Product Browsing and Search
            Console.WriteLine("\n1. PRODUCT SEARCH AND BROWSING");
            Console.WriteLine("Searching for products...");
            DemonstrateProductSearch();

            // 2. User Registration (with vulnerabilities)
            Console.WriteLine("\n2. USER REGISTRATION");
            Console.WriteLine("Registering new users...");
            DemonstrateUserRegistration();

            // 3. User Login (with vulnerabilities)
            Console.WriteLine("\n3. USER LOGIN");
            Console.WriteLine("Demonstrating user login...");
            DemonstrateUserLogin();

            // 4. Shopping Cart and Order Creation
            Console.WriteLine("\n4. SHOPPING CART & ORDER CREATION");
            Console.WriteLine("Creating sample orders...");
            DemonstrateOrderCreation();

            // 5. Payment Processing (with vulnerabilities)
            Console.WriteLine("\n5. PAYMENT PROCESSING");
            Console.WriteLine("Processing payments...");
            DemonstratePaymentProcessing();

            // 6. Order Management
            Console.WriteLine("\n6. ORDER MANAGEMENT");
            Console.WriteLine("Managing order statuses...");
            DemonstrateOrderManagement();
        }

        static void DemonstrateProductSearch()
        {
            // Vulnerable search - demonstrates SQL injection risk
            string[] searchTerms = { "laptop", "phone", "'; DROP TABLE Products; --", "headphones" };
            
            foreach (string searchTerm in searchTerms)
            {
                Console.WriteLine($"Searching for: '{searchTerm}'");
                var results = _productService.SearchProducts(searchTerm);
                Console.WriteLine($"Found {results.Count} products");
                
                if (results.Count > 0 && !searchTerm.Contains("DROP"))
                {
                    var firstResult = results.First();
                    Console.WriteLine($"  -> {firstResult.Name} - ${firstResult.Price} ({firstResult.Brand})");
                }
                Console.WriteLine();
            }

            // Show featured products
            Console.WriteLine("Featured Products:");
            var featured = _productService.GetFeaturedProducts(3);
            foreach (var product in featured)
            {
                Console.WriteLine($"  -> {product.Name} - ${product.Price} (Rating: {product.Rating}/5.0)");
            }
        }

        static void DemonstrateUserRegistration()
        {
            // Register new users with various security issues
            var newUsers = new[]
            {
                new { Username = "testuser1", Email = "test1@email.com", Password = "weak", FirstName = "Test", LastName = "User1" },
                new { Username = "admin'; DROP TABLE Users; --", Email = "hacker@evil.com", Password = "password123", FirstName = "Hacker", LastName = "McHackface" },
                new { Username = "normaluser", Email = "normal@email.com", Password = "mypassword", FirstName = "Normal", LastName = "Person" }
            };

            foreach (var userData in newUsers)
            {
                Console.WriteLine($"Registering user: {userData.Username}");
                
                // Vulnerable validation
                bool isValidInput = _securityValidator.ValidateInput(userData.Username, "Username");
                bool isValidEmail = _securityValidator.ValidateEmail(userData.Email);
                bool isValidPassword = _securityValidator.ValidatePasswordStrength(userData.Password);
                
                if (isValidInput && isValidEmail && isValidPassword)
                {
                    bool success = _userService.RegisterUser(userData.Username, userData.Email, userData.Password, userData.FirstName, userData.LastName);
                    Console.WriteLine(success ? "Registration successful!" : "Registration failed!");
                }
                Console.WriteLine();
            }
        }

        static void DemonstrateUserLogin()
        {
            // Demonstrate login attempts (including admin backdoor)
            var loginAttempts = new[]
            {
                new { Username = "john_doe", Password = "hello" },
                new { Username = "admin", Password = "password" },
                new { Username = "testuser1", Password = "weak" },
                new { Username = "nonexistent", Password = "whatever" }
            };

            foreach (var attempt in loginAttempts)
            {
                Console.WriteLine($"Login attempt: {attempt.Username}");
                
                // Check if admin user (vulnerable hardcoded check)
                if (_securityValidator.IsAdminUser(attempt.Username, attempt.Password))
                {
                    Console.WriteLine("ADMIN ACCESS GRANTED!");
                }
                else
                {
                    var user = _userService.LoginUser(attempt.Username, attempt.Password);
                    if (user != null)
                    {
                        string sessionToken = _securityValidator.GenerateSessionToken(user.Username);
                        Console.WriteLine($"Login successful! Session token: {sessionToken}");
                    }
                    else
                    {
                        Console.WriteLine("Login failed!");
                    }
                }
                Console.WriteLine();
            }
        }

        static void DemonstrateOrderCreation()
        {
            // Create sample orders for existing users
            var user = _userService.GetUserByUsername("john_doe");
            if (user != null)
            {
                Console.WriteLine($"Creating order for user: {user.Username}");
                
                // Create order items
                var orderItems = new List<OrderItem>
                {
                    new OrderItem { ProductId = 1, Quantity = 1 }, // MacBook Pro
                    new OrderItem { ProductId = 9, Quantity = 1 }  // Sony Headphones
                };

                var order = _orderService.CreateOrder(user.Id, orderItems);
                Console.WriteLine($"Order created: {order.OrderNumber}");
                Console.WriteLine($"Order total: ${order.TotalAmount:F2}");
                Console.WriteLine($"Items in order: {order.OrderItems.Count}");
                
                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"  -> {item.Product?.Name} x{item.Quantity} = ${item.TotalPrice:F2}");
                }
            }
        }

        static void DemonstratePaymentProcessing()
        {
            // Process payments with various security vulnerabilities
            var paymentData = new[]
            {
                new { CardNumber = "4532015112830366", CardHolder = "John Doe", Expiry = "12/26", CVV = "123", Amount = 2949.98m },
                new { CardNumber = "5555555555554444", CardHolder = "Jane Smith", Expiry = "08/25", CVV = "456", Amount = 399.99m },
                new { CardNumber = "4111111111111111", CardHolder = "Test User", Expiry = "01/24", CVV = "789", Amount = 199.99m }
            };

            foreach (var payment in paymentData)
            {
                Console.WriteLine($"Processing payment for {payment.CardHolder}");
                
                // Vulnerable credit card validation
                bool isValidCard = _securityValidator.ValidateCreditCard(payment.CardNumber);
                
                if (isValidCard)
                {
                    bool success = _paymentService.ProcessPayment(
                        payment.CardNumber,
                        payment.CardHolder,
                        payment.Expiry,
                        payment.CVV,
                        payment.Amount
                    );
                    
                    Console.WriteLine(success ? "Payment successful!" : "Payment failed!");
                }
                else
                {
                    Console.WriteLine("Invalid credit card format!");
                }
                Console.WriteLine();
            }
        }

        static void DemonstrateOrderManagement()
        {
            // Show order status updates
            Console.WriteLine("Recent Orders:");
            var recentOrders = _orderRepository.GetRecentOrders(3);
            
            foreach (var order in recentOrders)
            {
                Console.WriteLine($"Order {order.OrderNumber}: {order.Status} - ${order.TotalAmount:F2}");
                
                if (order.Status == OrderStatus.Processing)
                {
                    Console.WriteLine($"  -> Updating order {order.OrderNumber} to Shipped");
                    _orderService.UpdateOrderStatus(order.Id, OrderStatus.Shipped);
                }
            }

            // Show order statistics
            Console.WriteLine("\nOrder Statistics:");
            var statusCounts = _orderRepository.GetOrderStatusCounts();
            foreach (var status in statusCounts)
            {
                Console.WriteLine($"  {status.Key}: {status.Value} orders");
            }

            decimal totalRevenue = _orderService.GetTotalRevenue();
            Console.WriteLine($"Total Revenue: ${totalRevenue:F2}");
        }
    }
}