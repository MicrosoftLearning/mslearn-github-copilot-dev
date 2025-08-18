using ECommerce.ApplicationCore.Interfaces;

namespace ECommerce.Infrastructure.Services;

/// <summary>
/// Audit logging service for security compliance and debugging
/// </summary>
public class AuditLogger : IAuditLogger
{
    private static readonly string LogFileName = "order_audit_log.txt";
    
    public void LogOrderProcessingStarted(string orderId, string email)
    {
        LogEvent("ORDER_PROCESSING_STARTED", orderId, $"Started processing order for {MaskEmail(email)}");
    }
    
    public void LogOrderCompleted(string orderId, decimal amount)
    {
        LogEvent("ORDER_COMPLETED", orderId, $"Order completed successfully, amount: ${amount:F2}");
    }
    
    public void LogSecurityEvent(string eventType, string details)
    {
        LogEvent($"SECURITY_{eventType}", "SYSTEM", details);
    }
    
    public void LogValidationFailure(string orderId, string reason)
    {
        LogEvent("VALIDATION_FAILURE", orderId, reason);
    }
    
    public void LogInventoryIssue(string orderId, string productId, string issue)
    {
        LogEvent("INVENTORY_ISSUE", orderId, $"Product: {productId}, Issue: {issue}");
    }
    
    public void LogInventoryReserved(string orderId, int itemCount)
    {
        LogEvent("INVENTORY_RESERVED", orderId, $"Reserved {itemCount} item(s)");
    }
    
    public void LogPaymentProcessed(string orderId, decimal amount, string reference)
    {
        LogEvent("PAYMENT_PROCESSED", orderId, $"Amount: ${amount:F2}, Reference: {reference}");
    }
    
    public void LogPaymentFailure(string orderId, string reason)
    {
        LogEvent("PAYMENT_FAILURE", orderId, reason);
    }
    
    public void LogShippingScheduled(string orderId, string trackingNumber)
    {
        LogEvent("SHIPPING_SCHEDULED", orderId, $"Tracking: {trackingNumber}");
    }
    
    public void LogShippingFailure(string orderId, string reason)
    {
        LogEvent("SHIPPING_FAILURE", orderId, reason);
    }
    
    public void LogNotificationSent(string orderId, string type)
    {
        LogEvent("NOTIFICATION_SENT", orderId, $"Type: {type}");
    }
    
    public void LogNotificationFailure(string orderId, string reason)
    {
        LogEvent("NOTIFICATION_FAILURE", orderId, reason);
    }
    
    public void LogUnexpectedError(string orderId, string error)
    {
        LogEvent("UNEXPECTED_ERROR", orderId, error);
    }
    
    private void LogEvent(string eventType, string orderId, string details)
    {
        var logEntry = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff} UTC | {eventType} | Order: {orderId} | {details}";
        
        try
        {
            // In a real application, this would use a proper logging framework like Serilog
            File.AppendAllText(LogFileName, logEntry + Environment.NewLine);
            Console.WriteLine($"[AUDIT] {logEntry}");
        }
        catch
        {
            // Fail silently for logging issues to not interrupt order processing
            Console.WriteLine($"[AUDIT-ERROR] Failed to log: {logEntry}");
        }
    }
    
    private string MaskEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            return "***@***.***";
            
        var parts = email.Split('@');
        return parts.Length == 2 
            ? $"{parts[0][0]}***@{parts[1]}"
            : "***@***.***";
    }
}
