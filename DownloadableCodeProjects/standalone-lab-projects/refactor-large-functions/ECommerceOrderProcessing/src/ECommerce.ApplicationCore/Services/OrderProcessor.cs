using ECommerce.ApplicationCore.Entities;
using ECommerce.ApplicationCore.Exceptions;
using ECommerce.ApplicationCore.Interfaces;

namespace ECommerce.ApplicationCore.Services;

/// <summary>
/// Main order processing service
/// </summary>
public class OrderProcessor
{
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentGateway _paymentGateway;
    private readonly IShippingService _shippingService;
    private readonly INotificationService _notificationService;
    private readonly ISecurityValidator _securityValidator;
    private readonly IAuditLogger _auditLogger;

    public OrderProcessor(
        IInventoryService inventoryService,
        IPaymentGateway paymentGateway,
        IShippingService shippingService,
        INotificationService notificationService,
        ISecurityValidator securityValidator,
        IAuditLogger auditLogger)
    {
        _inventoryService = inventoryService;
        _paymentGateway = paymentGateway;
        _shippingService = shippingService;
        _notificationService = notificationService;
        _securityValidator = securityValidator;
        _auditLogger = auditLogger;
    }

    /// <summary>
    /// Responsibilities include:
    /// 1. Input Validation & Security Checks
    /// 2. Inventory Management
    /// 3. Payment Processing
    /// 4. Shipping Management
    /// 5. Customer Notifications
    /// 6. Order Finalization
    /// 7. Audit Logging
    /// 8. Error Handling & Cleanup
    /// </summary>
    public OrderResult ProcessOrder(Order order)
    {
        // Log the start of order processing for audit trail
        _auditLogger.LogOrderProcessingStarted(order.Id, order.CustomerEmail);

        try
        {
            // Comprehensive Input Validation and Security Checks
            if (order == null)
            {
                _auditLogger.LogSecurityEvent("NULL_ORDER_ATTEMPT", "Attempted to process null order");
                return OrderResult.Failure("No order provided");
            }

            if (order.Items == null || order.Items.Count == 0)
            {
                _auditLogger.LogValidationFailure(order.Id, "Empty order items");
                return OrderResult.Failure("Order has no items");
            }

            // Validate email format and domain
            if (!_securityValidator.IsValidEmail(order.CustomerEmail))
            {
                _auditLogger.LogValidationFailure(order.Id, "Invalid email format");
                return OrderResult.Failure("Invalid email address format");
            }

            // Validate shipping address format
            if (!_securityValidator.IsValidShippingAddress(order.ShippingAddress))
            {
                _auditLogger.LogValidationFailure(order.Id, "Invalid shipping address");
                return OrderResult.Failure("Invalid shipping address format");
            }

            // Validate payment information
            if (order.PaymentInfo == null || !_securityValidator.IsValidPaymentInfo(order.PaymentInfo))
            {
                _auditLogger.LogValidationFailure(order.Id, "Invalid payment information");
                return OrderResult.Failure("Payment information is invalid or incomplete");
            }

            // Security risk assessment - check for suspicious patterns
            var riskScore = _securityValidator.CalculateRiskScore(order);
            if (riskScore > 75) // High risk threshold
            {
                _auditLogger.LogSecurityEvent("HIGH_RISK_ORDER", $"Order {order.Id} flagged with risk score: {riskScore}");
                return OrderResult.Failure("Order flagged for manual review due to security concerns");
            }

            // Validate order amounts and item prices
            if (!_securityValidator.ValidateOrderAmounts(order))
            {
                _auditLogger.LogValidationFailure(order.Id, "Invalid order amounts or pricing");
                return OrderResult.Failure("Order amounts validation failed");
            }

            Console.WriteLine($"Processing Order {order.Id} for {_securityValidator.MaskEmail(order.CustomerEmail)}...");
            Console.WriteLine($"Order contains {order.Items.Count} items, Total: ${order.TotalAmount:F2}");

            // Inventory Management - Check Stock Availability and Reserve Items
            Console.WriteLine("Checking inventory availability...");
            foreach (OrderItem item in order.Items)
            {
                bool inStock = _inventoryService.CheckStock(item.ProductId, item.Quantity);
                if (!inStock)
                {
                    _auditLogger.LogInventoryIssue(order.Id, item.ProductId, "Out of stock");
                    Console.WriteLine($"Item {item.ProductId} is out of stock. Aborting order.");
                    return OrderResult.Failure($"Item {item.ProductId} is out of stock");
                }

                // Validate item details for security
                if (!_securityValidator.ValidateOrderItem(item))
                {
                    _auditLogger.LogSecurityEvent("INVALID_ITEM_DATA", $"Invalid item data for {item.ProductId}");
                    return OrderResult.Failure($"Invalid item data for {item.ProductId}");
                }
            }

            // Reserve inventory for all items
            bool inventoryReserved = _inventoryService.ReserveStock(order.Items);
            if (!inventoryReserved)
            {
                _auditLogger.LogInventoryIssue(order.Id, "ALL_ITEMS", "Failed to reserve inventory");
                Console.WriteLine("Failed to reserve inventory for the order.");
                return OrderResult.Failure("Inventory reservation failed");
            }
            Console.WriteLine("Inventory reserved successfully.");
            _auditLogger.LogInventoryReserved(order.Id, order.Items.Count);

            // Payment Processing with Enhanced Security
            Console.WriteLine("Processing payment...");
            try
            {
                // Additional fraud checks before processing payment
                if (!_securityValidator.ValidatePaymentSecurity(order.PaymentInfo, order.TotalAmount))
                {
                    _inventoryService.ReleaseStock(order.Items);
                    _auditLogger.LogSecurityEvent("PAYMENT_FRAUD_DETECTED", $"Suspicious payment attempt for order {order.Id}");
                    return OrderResult.Failure("Payment failed security validation");
                }

                // Attempt to charge the customer's card
                var paymentReference = _paymentGateway.Charge(order.PaymentInfo, order.TotalAmount);
                order.PaymentReference = paymentReference;
                Console.WriteLine($"Payment processed successfully. Reference: {paymentReference}");
                _auditLogger.LogPaymentProcessed(order.Id, order.TotalAmount, paymentReference);
            }
            catch (PaymentException ex)
            {
                // If payment fails, release reserved stock and return failure
                _inventoryService.ReleaseStock(order.Items);
                _auditLogger.LogPaymentFailure(order.Id, ex.Message);
                Console.WriteLine($"Payment failed for Order {order.Id}: {ex.Message}");
                return OrderResult.Failure("Payment processing failed: " + ex.Message);
            }

            // Shipping and Logistics Management
            Console.WriteLine("Scheduling shipping...");
            try
            {
                // Validate shipping requirements and restrictions
                if (!_securityValidator.ValidateShippingRequirements(order))
                {
                    _auditLogger.LogSecurityEvent("SHIPPING_VALIDATION_FAILED", $"Shipping validation failed for order {order.Id}");
                    return OrderResult.Failure("Shipping requirements validation failed");
                }

                var shippingDetails = _shippingService.ScheduleShipment(order);
                order.TrackingNumber = shippingDetails.TrackingNumber;
                order.EstimatedDeliveryDate = shippingDetails.EstimatedDelivery;
                Console.WriteLine($"Shipping scheduled successfully. Tracking: {shippingDetails.TrackingNumber}");
                _auditLogger.LogShippingScheduled(order.Id, shippingDetails.TrackingNumber);
            }
            catch (Exception ex)
            {
                _auditLogger.LogShippingFailure(order.Id, ex.Message);
                Console.WriteLine($"Error scheduling shipment: {ex.Message}");
                return OrderResult.Failure("Shipping scheduling failed: " + ex.Message);
            }

            // Customer Communication and Notifications
            Console.WriteLine("Sending notifications...");
            try
            {
                // Send order confirmation with all details
                _notificationService.SendOrderConfirmation(order.CustomerEmail, order.Id, order.TrackingNumber);
                Console.WriteLine($"Confirmation sent to {_securityValidator.MaskEmail(order.CustomerEmail)}.");
                _auditLogger.LogNotificationSent(order.Id, "ORDER_CONFIRMATION");

                // Send internal notifications for high-value orders
                if (order.TotalAmount > 1000)
                {
                    _notificationService.SendHighValueOrderAlert(order);
                    _auditLogger.LogNotificationSent(order.Id, "HIGH_VALUE_ALERT");
                }
            }
            catch (Exception ex)
            {
                // If confirmation fails, log a warning but do not abort the order
                _auditLogger.LogNotificationFailure(order.Id, ex.Message);
                Console.WriteLine($"Warning: failed to send notification: {ex.Message}");
            }

            // Order Finalization and Data Recording
            Console.WriteLine("Finalizing order...");
            order.Status = OrderStatus.Completed;
            order.CompletionDate = DateTime.UtcNow;
            order.ProcessingDuration = DateTime.UtcNow - order.OrderDate;
            
            // In a real app, this would update the order record in a database
            // _orderRepository.UpdateOrder(order);
            
            Console.WriteLine($"Order {order.Id} completed successfully in {order.ProcessingDuration.TotalSeconds:F1} seconds.");
            _auditLogger.LogOrderCompleted(order.Id, order.TotalAmount);

            return OrderResult.Success(order.Id, order.TrackingNumber ?? "");
        }
        catch (Exception ex)
        {
            // Catch-all exception handler for unexpected errors
            _auditLogger.LogUnexpectedError(order?.Id ?? "UNKNOWN", ex.Message);
            Console.WriteLine($"Unexpected error processing order: {ex.Message}");
            
            // Attempt cleanup if order ID exists
            if (order?.Id != null)
            {
                try
                {
                    _inventoryService.ReleaseStock(order.Items);
                }
                catch (Exception cleanupEx)
                {
                    _auditLogger.LogUnexpectedError(order.Id, $"Cleanup failed: {cleanupEx.Message}");
                }
            }
            
            return OrderResult.Failure("An unexpected error occurred during order processing");
        }
    }
}
