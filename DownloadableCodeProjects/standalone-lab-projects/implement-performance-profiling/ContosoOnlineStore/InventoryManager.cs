using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace ContosoOnlineStore
{
    public interface IInventoryManager
    {
        int GetStockLevel(int productId);
        bool IsInStock(int productId, int requestedQuantity);
        void UpdateStockLevels(Order order);
        void ReserveStock(Order order);
        void ReleaseReservedStock(Order order);
        Dictionary<int, int> GetLowStockProducts(int threshold = 10);
        Task<bool> RestockProductAsync(int productId, int quantity);
    }

    public class InventoryManager : IInventoryManager
    {
        private readonly ConcurrentDictionary<int, int> _stockByProductId;
        private readonly ConcurrentDictionary<int, int> _reservedStock;
        private readonly Dictionary<int, DateTime> _lastStockUpdate;
        private readonly IProductCatalog _catalog;
        private readonly ILogger<InventoryManager> _logger;
        private readonly AppSettings _appSettings;
        private readonly object _stockLock = new object();

        public InventoryManager(IProductCatalog catalog, ILogger<InventoryManager> logger, IOptions<AppSettings> appSettings)
        {
            _catalog = catalog;
            _logger = logger;
            _appSettings = appSettings.Value;
            _stockByProductId = new ConcurrentDictionary<int, int>();
            _reservedStock = new ConcurrentDictionary<int, int>();
            _lastStockUpdate = new Dictionary<int, DateTime>();

            InitializeInventory();
        }

        private void InitializeInventory()
        {
            var products = _catalog.GetAllProducts();
            foreach (var product in products)
            {
                _stockByProductId[product.Id] = product.InitialStock;
                _reservedStock[product.Id] = 0;
                _lastStockUpdate[product.Id] = DateTime.UtcNow;
                _logger.LogDebug("Initialized stock for product {ProductId}: {Stock} units", product.Id, product.InitialStock);
            }

            _logger.LogInformation("Initialized inventory for {ProductCount} products", products.Count);
        }

        public int GetStockLevel(int productId)
        {
            if (productId <= 0)
            {
                _logger.LogWarning("Invalid product ID requested for stock level: {ProductId}", productId);
                return 0;
            }

            // Performance bottleneck: Simulate database query delay
            if (productId % 5 == 0) // Intentional performance issue
            {
                Thread.Sleep(50); // Simulate slow database query
                _logger.LogDebug("Used slow stock lookup for product {ProductId}", productId);
            }

            var stock = _stockByProductId.GetValueOrDefault(productId, 0);
            var reserved = _reservedStock.GetValueOrDefault(productId, 0);
            var availableStock = stock - reserved;

            _logger.LogDebug("Stock level for product {ProductId}: {Stock} total, {Reserved} reserved, {Available} available",
                productId, stock, reserved, availableStock);

            return Math.Max(0, availableStock);
        }

        public bool IsInStock(int productId, int requestedQuantity)
        {
            if (requestedQuantity <= 0)
                return false;

            var availableStock = GetStockLevel(productId);
            return availableStock >= requestedQuantity;
        }

        public void UpdateStockLevels(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            lock (_stockLock)
            {
                var stockChanges = new Dictionary<int, int>();

                foreach (OrderItem item in order.Items)
                {
                    var currentStock = _stockByProductId.GetValueOrDefault(item.ProductId, 0);
                    var newStock = currentStock - item.Quantity;

                    // Security check: Prevent negative inventory if not allowed
                    if (!_appSettings.SecuritySettings.AllowNegativeInventory && newStock < 0)
                    {
                        var availableStock = GetStockLevel(item.ProductId);
                        throw new InsufficientInventoryException(item.ProductId, item.Quantity, availableStock);
                    }

                    _stockByProductId[item.ProductId] = newStock;
                    _lastStockUpdate[item.ProductId] = DateTime.UtcNow;
                    stockChanges[item.ProductId] = newStock;

                    _logger.LogInformation("Updated stock for product {ProductId}: {OldStock} -> {NewStock} (Change: -{Quantity})",
                        item.ProductId, currentStock, newStock, item.Quantity);
                }

                // Performance bottleneck: Inefficient logging of all stock changes
                LogAllStockChanges(stockChanges); // Could be optimized
            }
        }

        public void ReserveStock(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            lock (_stockLock)
            {
                // Check availability first
                foreach (OrderItem item in order.Items)
                {
                    if (!IsInStock(item.ProductId, item.Quantity))
                    {
                        var availableStock = GetStockLevel(item.ProductId);
                        throw new InsufficientInventoryException(item.ProductId, item.Quantity, availableStock);
                    }
                }

                // Reserve stock
                foreach (OrderItem item in order.Items)
                {
                    _reservedStock.AddOrUpdate(item.ProductId, item.Quantity, (key, existing) => existing + item.Quantity);
                    _logger.LogDebug("Reserved {Quantity} units of product {ProductId} for order {OrderId}",
                        item.Quantity, item.ProductId, order.OrderId);
                }
            }
        }

        public void ReleaseReservedStock(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            lock (_stockLock)
            {
                foreach (OrderItem item in order.Items)
                {
                    _reservedStock.AddOrUpdate(item.ProductId, 0, (key, existing) => Math.Max(0, existing - item.Quantity));
                    _logger.LogDebug("Released {Quantity} reserved units of product {ProductId} for order {OrderId}",
                        item.Quantity, item.ProductId, order.OrderId);
                }
            }
        }

        public Dictionary<int, int> GetLowStockProducts(int threshold = 10)
        {
            var lowStockProducts = new Dictionary<int, int>();

            // Performance bottleneck: Check all products individually instead of batch operation
            foreach (var productId in _stockByProductId.Keys)
            {
                Thread.Sleep(1); // Simulate individual database queries
                var stock = GetStockLevel(productId);
                if (stock <= threshold)
                {
                    lowStockProducts[productId] = stock;
                }
            }

            _logger.LogInformation("Found {LowStockCount} products with stock below {Threshold}",
                lowStockProducts.Count, threshold);

            return lowStockProducts;
        }

        public async Task<bool> RestockProductAsync(int productId, int quantity)
        {
            if (productId <= 0)
                throw new ArgumentException("Product ID must be positive", nameof(productId));

            if (quantity <= 0)
                throw new ArgumentException("Restock quantity must be positive", nameof(quantity));

            // Simulate async operation with delay
            await Task.Delay(200); // Performance bottleneck: Unnecessary delay

            lock (_stockLock)
            {
                var currentStock = _stockByProductId.GetValueOrDefault(productId, 0);
                _stockByProductId[productId] = currentStock + quantity;
                _lastStockUpdate[productId] = DateTime.UtcNow;

                _logger.LogInformation("Restocked product {ProductId}: {OldStock} -> {NewStock} (+{Quantity})",
                    productId, currentStock, currentStock + quantity, quantity);
            }

            return true;
        }

        private void LogAllStockChanges(Dictionary<int, int> stockChanges)
        {
            // Performance bottleneck: Inefficient logging implementation
            var logMessage = "Stock changes: ";
            foreach (var change in stockChanges)
            {
                logMessage += $"Product {change.Key}: {change.Value} units; ";
                Thread.Sleep(1); // Simulate slow logging
            }
            _logger.LogDebug(logMessage.TrimEnd(';', ' '));
        }
    }
}
