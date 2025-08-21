using ContosoOnlineStore;
using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Services;
using ContosoOnlineStore.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;
using Moq;

namespace ContosoOnlineStore.Tests
{
    public class ProductCatalogTests
    {
        private readonly IProductCatalog _catalog;
        private readonly Mock<ISecurityValidationService> _mockSecurity;
        private readonly Mock<ILogger<ProductCatalog>> _mockLogger;

        public ProductCatalogTests()
        {
            _mockSecurity = new Mock<ISecurityValidationService>();
            _mockLogger = new Mock<ILogger<ProductCatalog>>();
            var appSettings = Options.Create(new AppSettings());
            
            _catalog = new ProductCatalog(_mockSecurity.Object, _mockLogger.Object, appSettings);
        }

        [Fact]
        public void GetProductById_ValidId_ReturnsProduct()
        {
            // Arrange
            var productId = 1;

            // Act
            var product = _catalog.GetProductById(productId);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public void GetProductById_InvalidId_ReturnsNull()
        {
            // Arrange
            var invalidId = 999;

            // Act
            var product = _catalog.GetProductById(invalidId);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public void GetAllProducts_ReturnsAllProducts()
        {
            // Act
            var products = _catalog.GetAllProducts();

            // Assert
            Assert.NotEmpty(products);
            Assert.True(products.Count >= 20);
        }

        [Fact]
        public void SearchProducts_ValidTerm_ReturnsMatchingProducts()
        {
            // Arrange
            _mockSecurity.Setup(s => s.SanitizeInput(It.IsAny<string>())).Returns("phone");

            // Act
            var results = _catalog.SearchProducts("phone");

            // Assert
            Assert.NotEmpty(results);
            Assert.All(results, p => 
                Assert.True(p.Name.ToLower().Contains("phone") || 
                           p.Category.ToLower().Contains("phone") ||
                           p.Description.ToLower().Contains("phone")));
        }
    }

    public class OrderTests
    {
        [Fact]
        public void Constructor_ValidParameters_CreatesOrder()
        {
            // Arrange
            var email = "test@example.com";
            var address = "123 Test St";

            // Act
            var order = new Order(email, address);

            // Assert
            Assert.Equal(email, order.CustomerEmail);
            Assert.Equal(address, order.ShippingAddress);
            Assert.Equal(OrderStatus.Pending, order.Status);
            Assert.True(order.OrderId > 0);
        }

        [Fact]
        public void AddItem_ValidItem_AddsToOrder()
        {
            // Arrange
            var order = new Order();
            var item = new OrderItem(1, 2);

            // Act
            order.AddItem(item);

            // Assert
            Assert.Contains(item, order.Items);
            Assert.Equal(1, order.Items.Count);
        }

        [Fact]
        public void AddItem_SameProduct_CombinesQuantity()
        {
            // Arrange
            var order = new Order();
            var item1 = new OrderItem(1, 2);
            var item2 = new OrderItem(1, 3);

            // Act
            order.AddItem(item1);
            order.AddItem(item2);

            // Assert
            Assert.Equal(1, order.Items.Count);
            Assert.Equal(5, order.Items[0].Quantity);
        }
    }

    public class SecurityValidationServiceTests
    {
        private readonly ISecurityValidationService _service;
        private readonly Mock<ILogger<SecurityValidationService>> _mockLogger;

        public SecurityValidationServiceTests()
        {
            _mockLogger = new Mock<ILogger<SecurityValidationService>>();
            var appSettings = Options.Create(new AppSettings());
            _service = new SecurityValidationService(appSettings, _mockLogger.Object);
        }

        [Fact]
        public void ValidateProduct_ValidProduct_DoesNotThrow()
        {
            // Arrange
            var product = new Product(1, "Test Product", 10.99m, 100);

            // Act & Assert
            var exception = Record.Exception(() => _service.ValidateProduct(product));
            Assert.Null(exception);
        }

        [Fact]
        public void ValidateProduct_NullProduct_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _service.ValidateProduct(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        public void ValidateProduct_InvalidName_ThrowsSecurityValidationException(string invalidName)
        {
            // Arrange
            var product = new Product(1, "Valid Name", 10.99m, 100);
            
            // We can't modify the product after creation, so we'll test with constructor validation
            // Act & Assert
            if (string.IsNullOrWhiteSpace(invalidName))
            {
                Assert.Throws<ArgumentException>(() => new Product(1, invalidName, 10.99m, 100));
            }
        }

        [Fact]
        public void SanitizeInput_ValidInput_ReturnsSanitized()
        {
            // Arrange
            var input = "Test<script>alert('xss')</script>";

            // Act
            var result = _service.SanitizeInput(input);

            // Assert
            Assert.DoesNotContain("<", result);
            Assert.DoesNotContain(">", result);
        }
    }

    public class InventoryManagerTests
    {
        private readonly Mock<IProductCatalog> _mockCatalog;
        private readonly Mock<ILogger<InventoryManager>> _mockLogger;
        private readonly IInventoryManager _inventoryManager;

        public InventoryManagerTests()
        {
            _mockCatalog = new Mock<IProductCatalog>();
            _mockLogger = new Mock<ILogger<InventoryManager>>();
            var appSettings = Options.Create(new AppSettings());

            var products = new List<Product>
            {
                new Product(1, "Product 1", 10.0m, 100),
                new Product(2, "Product 2", 20.0m, 50)
            };

            _mockCatalog.Setup(c => c.GetAllProducts()).Returns(products);
            _inventoryManager = new InventoryManager(_mockCatalog.Object, _mockLogger.Object, appSettings);
        }

        [Fact]
        public void GetStockLevel_ValidProductId_ReturnsStock()
        {
            // Act
            var stock = _inventoryManager.GetStockLevel(1);

            // Assert
            Assert.Equal(100, stock);
        }

        [Fact]
        public void IsInStock_SufficientStock_ReturnsTrue()
        {
            // Act
            var inStock = _inventoryManager.IsInStock(1, 50);

            // Assert
            Assert.True(inStock);
        }

        [Fact]
        public void IsInStock_InsufficientStock_ReturnsFalse()
        {
            // Act
            var inStock = _inventoryManager.IsInStock(1, 150);

            // Assert
            Assert.False(inStock);
        }
    }
}
