using ContosoOnlineStore.Configuration;
using ContosoOnlineStore.Exceptions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ContosoOnlineStore.Services
{
    public interface ISecurityValidationService
    {
        void ValidateProduct(Product product);
        void ValidateOrder(Order order);
        void ValidateOrderItem(OrderItem item, Product product);
        string SanitizeInput(string input);
    }

    public class SecurityValidationService : ISecurityValidationService
    {
        private readonly SecuritySettings _securitySettings;
        private readonly ILogger<SecurityValidationService> _logger;
        private static readonly Regex AllowedCharactersRegex = new(@"^[a-zA-Z0-9\s\-_\.@]+$", RegexOptions.Compiled);

        public SecurityValidationService(IOptions<AppSettings> appSettings, ILogger<SecurityValidationService> logger)
        {
            _securitySettings = appSettings.Value.SecuritySettings;
            _logger = logger;
        }

        public void ValidateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new SecurityValidationException("Product name cannot be empty.");

            if (product.Name.Length > 100)
                throw new SecurityValidationException("Product name exceeds maximum length of 100 characters.");

            if (product.Price < _securitySettings.MinProductPrice || product.Price > _securitySettings.MaxProductPrice)
                throw new SecurityValidationException($"Product price must be between {_securitySettings.MinProductPrice:C} and {_securitySettings.MaxProductPrice:C}.");

            if (product.InitialStock < 0)
                throw new SecurityValidationException("Initial stock cannot be negative.");

            // Validate product name contains only allowed characters
            if (!AllowedCharactersRegex.IsMatch(product.Name))
                throw new SecurityValidationException("Product name contains invalid characters.");

            _logger.LogDebug("Product {ProductId} validated successfully", product.Id);
        }

        public void ValidateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (order.Items == null || !order.Items.Any())
                throw new InvalidOrderException("Order must contain at least one item.");

            if (order.Items.Count > 50) // Hard limit for security
                throw new InvalidOrderException("Order cannot contain more than 50 items.");

            _logger.LogDebug("Order with {ItemCount} items validated successfully", order.Items.Count);
        }

        public void ValidateOrderItem(OrderItem item, Product product)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (item.Quantity <= 0)
                throw new InvalidOrderException("Order item quantity must be positive.");

            if (item.Quantity > 1000) // Reasonable limit to prevent abuse
                throw new InvalidOrderException("Order item quantity cannot exceed 1000 units.");

            // Calculate potential overflow
            try
            {
                var totalPrice = product.Price * item.Quantity;
                if (totalPrice > decimal.MaxValue / 2) // Conservative check
                    throw new InvalidOrderException("Order item total price would cause overflow.");
            }
            catch (OverflowException)
            {
                throw new InvalidOrderException("Order item calculation would cause numeric overflow.");
            }

            _logger.LogDebug("Order item for product {ProductId} with quantity {Quantity} validated successfully",
                item.ProductId, item.Quantity);
        }

        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remove potentially dangerous characters and limit length
            var sanitized = Regex.Replace(input.Trim(), @"[<>""'%;()&+]", "");
            return sanitized.Length > 255 ? sanitized[..255] : sanitized;
        }
    }
}
