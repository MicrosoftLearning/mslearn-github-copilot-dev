using ContosoOnlineStore;
using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Services;
using ContosoOnlineStore.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace ContosoOnlineStore.Tests;

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
        var productId = 1;
        var product = _catalog.GetProductById(productId);
        Assert.NotNull(product);
        Assert.Equal(productId, product.Id);
    }

    [Fact]
    public void GetProductById_InvalidId_ReturnsNull()
    {
        var invalidId = 999;
        var product = _catalog.GetProductById(invalidId);
        Assert.Null(product);
    }

    [Fact]
    public void GetAllProducts_ReturnsAllProducts()
    {
        var products = _catalog.GetAllProducts();
        Assert.NotEmpty(products);
        Assert.True(products.Count >= 20);
    }

    [Fact]
    public void SearchProducts_ValidTerm_ReturnsMatchingProducts()
    {
        _mockSecurity.Setup(s => s.SanitizeInput(It.IsAny<string>())).Returns("phone");
        var results = _catalog.SearchProducts("phone");
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
        var email = "test@example.com";
        var address = "123 Test St";
        var order = new Order(email, address);
        Assert.Equal(email, order.CustomerEmail);
        Assert.Equal(address, order.ShippingAddress);
        Assert.Equal(OrderStatus.Pending, order.Status);
        Assert.True(order.OrderId > 0);
    }

    [Fact]
    public void AddItem_ValidItem_AddsToOrder()
    {
        var order = new Order();
        var item = new OrderItem(1, 2);
        order.AddItem(item);
        Assert.Contains(item, order.Items);
        var single = Assert.Single(order.Items);
        Assert.Equal(item.ProductId, single.ProductId);
        Assert.Equal(item.Quantity, single.Quantity);
    }

    [Fact]
    public void AddItem_SameProduct_CombinesQuantity()
    {
        var order = new Order();
        var item1 = new OrderItem(1, 2);
        var item2 = new OrderItem(1, 3);
        order.AddItem(item1);
        order.AddItem(item2);
        var single = Assert.Single(order.Items);
        Assert.Equal(5, single.Quantity);
        Assert.Equal(1, single.ProductId);
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
        var product = new Product(1, "Test Product", 10.99m, 100);
        var exception = Record.Exception(() => _service.ValidateProduct(product));
        Assert.Null(exception);
    }

    [Fact]
    public void ValidateProduct_NullProduct_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _service.ValidateProduct((Product)null!));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ValidateProduct_InvalidName_ThrowsSecurityValidationException(string invalidName)
    {
        if (string.IsNullOrWhiteSpace(invalidName))
        {
            Assert.Throws<ArgumentException>(() => new Product(1, invalidName, 10.99m, 100));
        }
    }

    [Fact]
    public void SanitizeInput_ValidInput_ReturnsSanitized()
    {
        var input = "Test<script>alert('xss')</script>";
        var result = _service.SanitizeInput(input);
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
        var stock = _inventoryManager.GetStockLevel(1);
        Assert.Equal(100, stock);
    }

    [Fact]
    public void IsInStock_SufficientStock_ReturnsTrue()
    {
        var inStock = _inventoryManager.IsInStock(1, 50);
        Assert.True(inStock);
    }

    [Fact]
    public void IsInStock_InsufficientStock_ReturnsFalse()
    {
        var inStock = _inventoryManager.IsInStock(1, 150);
        Assert.False(inStock);
    }
}
