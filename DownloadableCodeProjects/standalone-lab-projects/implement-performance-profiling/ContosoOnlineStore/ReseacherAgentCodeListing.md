
ContosoOnlineStore/
├── Program.cs
├── Product.cs
├── ProductCatalog.cs
├── Order.cs
├── OrderItem.cs
├── OrderProcessor.cs
├── InventoryManager.cs
├── EmailService.cs
├── ContosoOnlineStore.csproj
└── README.md


# 🛒 ContosoOnlineStore

## Program.cs
```csharp
using System;
using System.Diagnostics;

namespace ContosoOnlineStore
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Contoso Online Store ===");

            ProductCatalog catalog = new ProductCatalog();
            InventoryManager inventory = new InventoryManager(catalog);
            EmailService emailService = new EmailService();
            OrderProcessor processor = new OrderProcessor(catalog, inventory, emailService);

            Order order = new Order();
            order.Items.Add(new OrderItem(2, 5));
            order.Items.Add(new OrderItem(7, 3));
            order.Items.Add(new OrderItem(4, 10));
            order.Items.Add(new OrderItem(9, 2));

            Stopwatch sw = Stopwatch.StartNew();
            decimal total = await processor.FinalizeOrder(order);
            sw.Stop();

            Console.WriteLine($"\nOrder processed. Total Cost = ${total:F2}");
            Console.WriteLine($"Processing Time = {sw.ElapsedMilliseconds} ms");

            Console.WriteLine("Remaining stock:");
            foreach (var item in order.Items)
            {
                Product prod = catalog.GetProductById(item.ProductId);
                int stock = inventory.GetStockLevel(item.ProductId);
                Console.WriteLine($"- {prod.Name}: {stock} units");
            }
        }
    }
}
```

## Product.cs
```csharp
namespace ContosoOnlineStore
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int InitialStock { get; }

        public Product(int id, string name, decimal price, int initialStock)
        {
            Id = id;
            Name = name;
            Price = price;
            InitialStock = initialStock;
        }
    }
}
```

## ProductCatalog.cs
```csharp
using System.Collections.Generic;
using System.Linq;

namespace ContosoOnlineStore
{
    public class ProductCatalog
    {
        private List<Product> _products;

        public ProductCatalog()
        {
            _products = new List<Product>
            {
                new Product(1, "Phone", 299.99m, 50),
                new Product(2, "Headphones", 99.99m, 200),
                new Product(3, "Laptop", 1499.00m, 20),
                new Product(4, "Monitor", 389.50m, 75),
                new Product(5, "Charger", 19.99m, 500),
                new Product(6, "Speaker", 79.95m, 120),
                new Product(7, "SSD", 159.49m, 60),
                new Product(8, "Keyboard", 49.99m, 150),
                new Product(9, "Webcam", 39.99m, 300),
                new Product(10, "Earbuds", 129.99m, 80)
            };
        }

        public Product GetProductById(int productId)
        {
            // Performance Issue: Linear search
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        public List<Product> GetAllProducts() => _products;
    }
}
```

## Order.cs
```csharp
using System.Collections.Generic;

namespace ContosoOnlineStore
{
    public class Order
    {
        public List<OrderItem> Items { get; }

        public Order()
        {
            Items = new List<OrderItem>();
        }
    }
}
```

## OrderItem.cs
```csharp
namespace ContosoOnlineStore
{
    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public OrderItem(int productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
```

## InventoryManager.cs
```csharp
using System.Collections.Generic;

namespace ContosoOnlineStore
{
    public class InventoryManager
    {
        private Dictionary<int, int> _stockByProductId;

        public InventoryManager(ProductCatalog catalog)
        {
            _stockByProductId = new Dictionary<int, int>();
            foreach (var product in catalog.GetAllProducts())
            {
                _stockByProductId[product.Id] = product.InitialStock;
            }
        }

        public int GetStockLevel(int productId)
        {
            return _stockByProductId.ContainsKey(productId) ? _stockByProductId[productId] : 0;
        }

        public void UpdateStockLevels(Order order)
        {
            foreach (OrderItem item in order.Items)
            {
                if (_stockByProductId.ContainsKey(item.ProductId))
                {
                    _stockByProductId[item.ProductId] -= item.Quantity;
                }
            }
        }
    }
}
```

## EmailService.cs
```csharp
using System;
using System.Threading.Tasks;

namespace ContosoOnlineStore
{
    public class EmailService
    {
        public async Task SendConfirmationAsync(Order order)
        {
            Console.WriteLine("Sending confirmation email...");
            await Task.Delay(2000); // Performance Issue: Simulated blocking call
            Console.WriteLine("Email sent.");
        }
    }
}
```

## OrderProcessor.cs
```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContosoOnlineStore
{
    public class OrderProcessor
    {
        private ProductCatalog _catalog;
        private InventoryManager _inventory;
        private EmailService _emailService;

        public OrderProcessor(ProductCatalog catalog, InventoryManager inventory, EmailService emailService)
        {
            _catalog = catalog;
            _inventory = inventory;
            _emailService = emailService;
        }

        public decimal CalculateOrderTotal(Order order)
        {
            decimal total = 0;
            var productCache = new Dictionary<int, Product>();

            foreach (OrderItem item in order.Items)
            {
                if (!productCache.ContainsKey(item.ProductId))
                {
                    productCache[item.ProductId] = _catalog.GetProductById(item.ProductId);
                }

                var prod = productCache[item.ProductId];
                if (prod != null)
                {
                    total += prod.Price * item.Quantity;
                }
            }

            return total;
        }

        public async Task<decimal> FinalizeOrder(Order order)
        {
            decimal total = CalculateOrderTotal(order);
            _inventory.UpdateStockLevels(order);
            await _emailService.SendConfirmationAsync(order);
            return total;
        }
    }
}
```
