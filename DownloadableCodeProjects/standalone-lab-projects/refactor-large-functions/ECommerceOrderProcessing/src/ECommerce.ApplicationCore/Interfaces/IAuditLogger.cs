namespace ECommerce.ApplicationCore.Interfaces;

/// <summary>
/// Interface for audit logging operations
/// </summary>
public interface IAuditLogger
{
    void LogOrderProcessingStarted(string orderId, string email);
    void LogOrderCompleted(string orderId, decimal amount);
    void LogSecurityEvent(string eventType, string details);
    void LogValidationFailure(string orderId, string reason);
    void LogInventoryIssue(string orderId, string productId, string issue);
    void LogInventoryReserved(string orderId, int itemCount);
    void LogPaymentProcessed(string orderId, decimal amount, string reference);
    void LogPaymentFailure(string orderId, string reason);
    void LogShippingScheduled(string orderId, string trackingNumber);
    void LogShippingFailure(string orderId, string reason);
    void LogNotificationSent(string orderId, string type);
    void LogNotificationFailure(string orderId, string reason);
    void LogUnexpectedError(string orderId, string error);
}
