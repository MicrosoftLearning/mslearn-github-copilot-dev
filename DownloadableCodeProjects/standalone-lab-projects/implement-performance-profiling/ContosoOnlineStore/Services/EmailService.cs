using ContosoOnlineStore.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace ContosoOnlineStore.Services
{
    public interface IEmailService
    {
        Task<bool> SendConfirmationAsync(Order order);
        Task<bool> SendShippingNotificationAsync(Order order, string trackingNumber);
        Task<bool> SendLowStockAlertAsync(Dictionary<int, int> lowStockProducts);
        Task<bool> ValidateEmailAsync(string email);
    }

    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly AppSettings _appSettings;
        private readonly IProductCatalog _catalog;
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public EmailService(ILogger<EmailService> logger, IOptions<AppSettings> appSettings, IProductCatalog catalog)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _catalog = catalog;
        }

        public async Task<bool> SendConfirmationAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            try
            {
                _logger.LogInformation("Sending order confirmation email for order {OrderId}", order.OrderId);

                // Validate email address
                if (!await ValidateEmailAsync(order.CustomerEmail))
                {
                    _logger.LogWarning("Invalid email address for order {OrderId}: {Email}", order.OrderId, order.CustomerEmail);
                    return false;
                }

                // Performance bottleneck: Generate email content inefficiently
                var emailContent = await GenerateOrderConfirmationEmailAsync(order);

                // Simulate sending email with configurable delay
                await Task.Delay(_appSettings.EmailTimeoutMs);

                // Performance bottleneck: Log detailed email information
                await LogEmailDetailsAsync("Order Confirmation", order.CustomerEmail, emailContent);

                _logger.LogInformation("Order confirmation email sent successfully for order {OrderId}", order.OrderId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send order confirmation email for order {OrderId}", order.OrderId);
                return false;
            }
        }

        public async Task<bool> SendShippingNotificationAsync(Order order, string trackingNumber)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (string.IsNullOrWhiteSpace(trackingNumber))
                throw new ArgumentException("Tracking number cannot be empty", nameof(trackingNumber));

            try
            {
                _logger.LogInformation("Sending shipping notification email for order {OrderId}", order.OrderId);

                if (!await ValidateEmailAsync(order.CustomerEmail))
                {
                    _logger.LogWarning("Invalid email address for shipping notification {OrderId}: {Email}", order.OrderId, order.CustomerEmail);
                    return false;
                }

                var emailContent = await GenerateShippingNotificationEmailAsync(order, trackingNumber);

                // Simulate email sending delay
                await Task.Delay(_appSettings.EmailTimeoutMs / 2);

                await LogEmailDetailsAsync("Shipping Notification", order.CustomerEmail, emailContent);

                _logger.LogInformation("Shipping notification email sent successfully for order {OrderId}", order.OrderId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send shipping notification email for order {OrderId}", order.OrderId);
                return false;
            }
        }

        public async Task<bool> SendLowStockAlertAsync(Dictionary<int, int> lowStockProducts)
        {
            if (lowStockProducts == null || !lowStockProducts.Any())
                return true;

            try
            {
                _logger.LogInformation("Sending low stock alert for {ProductCount} products", lowStockProducts.Count);

                var emailContent = await GenerateLowStockAlertEmailAsync(lowStockProducts);

                // Simulate sending to multiple administrators
                var adminEmails = new[] { "admin@contoso.com", "inventory@contoso.com", "manager@contoso.com" };

                foreach (var adminEmail in adminEmails)
                {
                    await Task.Delay(100); // Performance bottleneck: Sequential email sending
                    await LogEmailDetailsAsync("Low Stock Alert", adminEmail, emailContent);
                }

                _logger.LogInformation("Low stock alert emails sent successfully");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send low stock alert emails");
                return false;
            }
        }

        public async Task<bool> ValidateEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Performance bottleneck: Unnecessary async operation for simple validation
            await Task.Delay(10); // Simulate validation delay

            // Basic email validation
            if (email.Length > 254) // RFC 5321 limit
                return false;

            if (!EmailRegex.IsMatch(email))
                return false;

            // Additional security checks
            var suspiciousPatterns = new[] { "script", "javascript", "<", ">", "eval", "exec" };
            var lowerEmail = email.ToLowerInvariant();

            foreach (var pattern in suspiciousPatterns)
            {
                if (lowerEmail.Contains(pattern))
                {
                    _logger.LogWarning("Suspicious email pattern detected: {Email}", email);
                    return false;
                }
            }

            return true;
        }

        private async Task<string> GenerateOrderConfirmationEmailAsync(Order order)
        {
            // Performance bottleneck: Inefficient string building
            var emailBuilder = new StringBuilder();
            emailBuilder.AppendLine("Dear Customer,");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine($"Thank you for your order #{order.OrderId}!");
            emailBuilder.AppendLine($"Order Date: {order.OrderDate:yyyy-MM-dd HH:mm:ss}");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine("Order Details:");

            decimal totalAmount = 0;
            foreach (var item in order.Items)
            {
                // Performance bottleneck: Individual product lookups in loop
                await Task.Delay(5); // Simulate database query
                var product = _catalog.GetProductById(item.ProductId);
                if (product != null)
                {
                    var itemTotal = product.Price * item.Quantity;
                    totalAmount += itemTotal;
                    emailBuilder.AppendLine($"- {product.Name} x {item.Quantity} = {itemTotal:C}");
                }
            }

            emailBuilder.AppendLine();
            emailBuilder.AppendLine($"Total Amount: {totalAmount:C}");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine($"Shipping Address: {order.ShippingAddress}");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine("Your order will be processed within 24 hours.");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine("Thank you for shopping with Contoso Online Store!");

            return emailBuilder.ToString();
        }

        private async Task<string> GenerateShippingNotificationEmailAsync(Order order, string trackingNumber)
        {
            await Task.Delay(50); // Performance bottleneck: Unnecessary delay

            return $@"Dear Customer,

Your order #{order.OrderId} has been shipped!

Tracking Number: {trackingNumber}
Estimated Delivery: {DateTime.Now.AddDays(3):yyyy-MM-dd}

You can track your package at: https://tracking.contoso.com/{trackingNumber}

Thank you for shopping with Contoso Online Store!";

        }

        private async Task<string> GenerateLowStockAlertEmailAsync(Dictionary<int, int> lowStockProducts)
        {
            var emailBuilder = new StringBuilder();
            emailBuilder.AppendLine("Low Stock Alert - Contoso Online Store");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine("The following products are running low on stock:");
            emailBuilder.AppendLine();

            foreach (var kvp in lowStockProducts)
            {
                // Performance bottleneck: Individual product lookups
                await Task.Delay(10);
                var product = _catalog.GetProductById(kvp.Key);
                if (product != null)
                {
                    emailBuilder.AppendLine($"- {product.Name} (ID: {kvp.Key}): {kvp.Value} units remaining");
                }
            }

            emailBuilder.AppendLine();
            emailBuilder.AppendLine("Please consider restocking these items.");
            emailBuilder.AppendLine();
            emailBuilder.AppendLine($"Generated at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            return emailBuilder.ToString();
        }

        private async Task LogEmailDetailsAsync(string emailType, string recipient, string content)
        {
            // Performance bottleneck: Detailed logging that could be optimized
            await Task.Delay(20);

            _logger.LogDebug("Email sent - Type: {EmailType}, Recipient: {Recipient}, Content Length: {ContentLength}",
                emailType, recipient, content.Length);

            if (_appSettings.EnableDetailedLogging)
            {
                _logger.LogTrace("Email content preview: {ContentPreview}...",
                    content.Length > 100 ? content[..100] : content);
            }
        }
    }
}
