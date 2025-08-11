// Services/AuditService.cs - Audit and logging service (contains duplicate logic)
using System;
using EcommerceApp.Models;

namespace EcommerceApp.Services;

public class AuditService
{
    // Very common duplication pattern - audit logging appears everywhere in e-commerce systems

    public static void LogOrderActivity(Order order, string action, string details = "")
    {
        Console.WriteLine($"[AUDIT] Logging order activity: {action}");

        // Duplicate audit entry creation logic - will also appear in return logging
        var auditEntry = CreateAuditEntry("ORDER", order.OrderId, order.CustomerId, action, details);

        // Duplicate validation logic
        if (ValidateAuditEntry(auditEntry))
        {
            // Duplicate storage logic
            StoreAuditEntry(auditEntry);

            // Duplicate compliance checking
            CheckComplianceRequirements(auditEntry);
        }
    }

    public static void LogReturnActivity(Return returnRequest, string action, string details = "")
    {
        Console.WriteLine($"[AUDIT] Logging return activity: {action}");

        // Duplicate audit entry creation logic - same pattern as order logging
        var auditEntry = CreateAuditEntry("RETURN", returnRequest.ReturnId, returnRequest.CustomerId, action, details);

        // Duplicate validation logic
        if (ValidateAuditEntry(auditEntry))
        {
            // Duplicate storage logic
            StoreAuditEntry(auditEntry);

            // Duplicate compliance checking
            CheckComplianceRequirements(auditEntry);
        }
    }

    // Duplicate Helper Methods Start - These are duplicated across order and return processing
    private static AuditEntry CreateAuditEntry(string transactionType, string transactionId, string customerId, string action, string details)
    {
        return new AuditEntry
        {
            Id = Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow,
            TransactionType = transactionType,
            TransactionId = transactionId,
            CustomerId = customerId,
            Action = action,
            Details = details,
            UserAgent = "ECommerceApp/1.0",
            IpAddress = "127.0.0.1" // Would be actual IP in real app
        };
    }

    private static bool ValidateAuditEntry(AuditEntry entry)
    {
        if (string.IsNullOrWhiteSpace(entry.TransactionId))
        {
            Console.WriteLine("[AUDIT] Validation failed: Transaction ID is required");
            return false;
        }

        if (string.IsNullOrWhiteSpace(entry.CustomerId))
        {
            Console.WriteLine("[AUDIT] Validation failed: Customer ID is required");
            return false;
        }

        if (string.IsNullOrWhiteSpace(entry.Action))
        {
            Console.WriteLine("[AUDIT] Validation failed: Action is required");
            return false;
        }

        Console.WriteLine("[AUDIT] Audit entry validation passed");
        return true;
    }

    private static void StoreAuditEntry(AuditEntry entry)
    {
        // Simulate storing to audit database/file
        Console.WriteLine($"[AUDIT] Storing: {entry.Timestamp:yyyy-MM-dd HH:mm:ss} | {entry.TransactionType} | {entry.TransactionId} | {entry.Action}");

        if (!string.IsNullOrWhiteSpace(entry.Details))
        {
            Console.WriteLine($"[AUDIT] Details: {entry.Details}");
        }

        // In real application, this would write to secure audit storage
    }

    private static void CheckComplianceRequirements(AuditEntry entry)
    {
        // Check if this action requires special compliance logging (e.g., GDPR, SOX)
        if (entry.Action.Contains("REFUND") || entry.Action.Contains("PAYMENT"))
        {
            Console.WriteLine($"[COMPLIANCE] Financial action detected - enhanced logging required for {entry.TransactionId}");
        }

        if (entry.TransactionType == "RETURN" && entry.Action.Contains("APPROVED"))
        {
            Console.WriteLine($"[COMPLIANCE] Return approval logged for compliance tracking: {entry.TransactionId}");
        }
    }
    // Duplicate Helper Methods End
}

// Supporting class for audit entries
public class AuditEntry
{
    public string Id { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string TransactionType { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public string CustomerId { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
}
