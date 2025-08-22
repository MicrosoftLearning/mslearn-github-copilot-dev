using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Exceptions;
using ContosoOnlineStore.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace ContosoOnlineStore
{
    public interface IProductCatalog
    {
        Product? GetProductById(int productId);
        List<Product> GetAllProducts();
        List<Product> SearchProducts(string searchTerm);
        List<Product> GetProductsByCategory(string category);
        bool IsProductAvailable(int productId);
        Task<List<Product>> GetProductsAsync();
        void InvalidateCache();
    }

    public class ProductCatalog : IProductCatalog
    {
        private readonly List<Product> _products;
        private readonly Dictionary<int, Product> _productIndex;
        private readonly ConcurrentDictionary<string, List<Product>> _searchCache;
        private readonly ISecurityValidationService _securityValidation;
        private readonly ILogger<ProductCatalog> _logger;
        private readonly AppSettings _appSettings;
        private DateTime _lastCacheUpdate = DateTime.UtcNow;

        public ProductCatalog(ISecurityValidationService securityValidation, ILogger<ProductCatalog> logger, IOptions<AppSettings> appSettings)
        {
            _securityValidation = securityValidation;
            _logger = logger;
            _appSettings = appSettings.Value;
            _products = new List<Product>();
            _productIndex = new Dictionary<int, Product>();
            _searchCache = new ConcurrentDictionary<string, List<Product>>();

            InitializeProducts();
            BuildProductIndex();
        }

        private void InitializeProducts()
        {
            var products = new List<Product>
            {
                new Product(1, "iPhone 15 Pro", 999.99m, 50, "Electronics", "Latest iPhone with advanced camera system"),
                new Product(2, "Sony WH-1000XM5", 399.99m, 200, "Electronics", "Premium noise-canceling headphones"),
                new Product(3, "MacBook Pro 16-inch", 2499.00m, 20, "Computers", "High-performance laptop for professionals"),
                new Product(4, "Dell UltraSharp 27", 389.50m, 75, "Monitors", "4K professional monitor with USB-C"),
                new Product(5, "Anker PowerCore 20K", 49.99m, 500, "Accessories", "High-capacity portable charger"),
                new Product(6, "Bose SoundLink Revolve", 199.95m, 120, "Audio", "360-degree Bluetooth speaker"),
                new Product(7, "Samsung 980 PRO 2TB", 159.49m, 60, "Storage", "High-speed NVMe SSD drive"),
                new Product(8, "Logitech MX Master 3", 99.99m, 150, "Accessories", "Advanced wireless mouse for productivity"),
                new Product(9, "Logitech C920 HD Pro", 79.99m, 300, "Electronics", "Full HD webcam with autofocus"),
                new Product(10, "Apple AirPods Pro 2", 249.99m, 80, "Audio", "Wireless earbuds with active noise cancellation"),
                new Product(11, "Nintendo Switch OLED", 349.99m, 45, "Gaming", "Handheld gaming console with vibrant OLED screen"),
                new Product(12, "Xbox Wireless Controller", 59.99m, 200, "Gaming", "Official wireless controller for Xbox"),
                new Product(13, "Kindle Paperwhite", 139.99m, 150, "Electronics", "Waterproof e-reader with adjustable backlight"),
                new Product(14, "Ring Video Doorbell", 99.99m, 100, "Security", "Smart doorbell with HD video and motion detection"),
                new Product(15, "Echo Dot 5th Gen", 49.99m, 300, "Smart Home", "Compact smart speaker with Alexa"),
                new Product(16, "iPad Air 5th Gen", 599.99m, 75, "Tablets", "Powerful tablet with M1 chip"),
                new Product(17, "Samsung Galaxy Watch 6", 299.99m, 120, "Wearables", "Advanced smartwatch with health tracking"),
                new Product(18, "GoPro HERO12 Black", 399.99m, 60, "Cameras", "Waterproof action camera with 5.3K video"),
                new Product(19, "Fitbit Charge 6", 159.95m, 180, "Fitness", "Advanced fitness tracker with built-in GPS"),
                new Product(20, "Tesla Model Y Wall Charger", 475.00m, 25, "Automotive", "High-powered home charging solution")
            };

            foreach (var product in products)
            {
                try
                {
                    _securityValidation.ValidateProduct(product);
                    _products.Add(product);
                    _logger.LogDebug("Added product: {ProductName} (ID: {ProductId})", product.Name, product.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add product {ProductId}: {ProductName}", product.Id, product.Name);
                }
            }

            _logger.LogInformation("Initialized product catalog with {ProductCount} products", _products.Count);
        }

        private void BuildProductIndex()
        {
            _productIndex.Clear();
            foreach (var product in _products)
            {
                _productIndex[product.Id] = product;
            }
            _logger.LogDebug("Built product index for {ProductCount} products", _productIndex.Count);
        }

        public Product? GetProductById(int productId)
        {
            if (productId <= 0)
            {
                _logger.LogWarning("Invalid product ID requested: {ProductId}", productId);
                return null;
            }

            // Performance bottleneck: Sometimes use inefficient linear search instead of index
            if (productId % 3 == 0) // Intentional performance issue for training
            {
                _logger.LogDebug("Using linear search for product ID: {ProductId}", productId);
                Thread.Sleep(10); // Simulate slow database query
                return _products.FirstOrDefault(p => p.Id == productId);
            }

            // Use efficient lookup most of the time
            _productIndex.TryGetValue(productId, out var product);
            return product;
        }

        public List<Product> GetAllProducts()
        {
            _logger.LogDebug("Retrieved all {ProductCount} products", _products.Count);

            // Performance bottleneck: Create new list and sort every time
            var result = new List<Product>(_products);
            result.Sort((a, b) => a.Name.CompareTo(b.Name)); // Expensive sorting operation

            return result;
        }

        public List<Product> SearchProducts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Product>();

            var sanitizedTerm = _securityValidation.SanitizeInput(searchTerm.ToLowerInvariant());

            // Check cache first (but with inefficient cache key generation)
            var cacheKey = GenerateSlowCacheKey(sanitizedTerm); // Performance bottleneck

            if (_searchCache.TryGetValue(cacheKey, out var cachedResults))
            {
                // Check if cache is still valid
                if (DateTime.UtcNow.Subtract(_lastCacheUpdate).TotalMinutes < _appSettings.PerformanceSettings.CacheExpirationMinutes)
                {
                    _logger.LogDebug("Retrieved search results from cache for term: {SearchTerm}", sanitizedTerm);
                    return cachedResults;
                }
            }

            // Performance bottleneck: Multiple iterations and string operations
            var results = new List<Product>();
            foreach (var product in _products) // Could be optimized with LINQ
            {
                if (product.Name.ToLowerInvariant().Contains(sanitizedTerm) ||
                    product.Category.ToLowerInvariant().Contains(sanitizedTerm) ||
                    product.Description.ToLowerInvariant().Contains(sanitizedTerm))
                {
                    results.Add(product);
                    Thread.Sleep(1); // Simulate database query delay
                }
            }

            // Cache the results
            _searchCache[cacheKey] = results;
            _logger.LogInformation("Found {ResultCount} products for search term: {SearchTerm}", results.Count, sanitizedTerm);

            return results;
        }

        public List<Product> GetProductsByCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
                return new List<Product>();

            var sanitizedCategory = _securityValidation.SanitizeInput(category);

            // Performance bottleneck: Linear search instead of category index
            var results = new List<Product>();
            foreach (var product in _products)
            {
                if (string.Equals(product.Category, sanitizedCategory, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(product);
                    Thread.Sleep(2); // Simulate slow query
                }
            }

            _logger.LogDebug("Found {ProductCount} products in category: {Category}", results.Count, sanitizedCategory);
            return results;
        }

        public bool IsProductAvailable(int productId)
        {
            var product = GetProductById(productId);
            return product != null;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            // Simulate async database operation with unnecessary delay
            await Task.Delay(100); // Performance bottleneck
            return GetAllProducts();
        }

        public void InvalidateCache()
        {
            _searchCache.Clear();
            _lastCacheUpdate = DateTime.UtcNow;
            _logger.LogInformation("Product catalog cache invalidated");
        }

        private string GenerateSlowCacheKey(string searchTerm)
        {
            // Performance bottleneck: Inefficient cache key generation
            var key = searchTerm;
            for (int i = 0; i < 100; i++) // Unnecessary loop
            {
                key = key + i.ToString();
            }
            return key.GetHashCode().ToString();
        }
    }
}
