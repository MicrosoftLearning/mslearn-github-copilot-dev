// Services/EmailService.cs - Email notification service (contains duplicate logic)
using System;
using EcommerceApp.Models;
using EcommerceApp.Configuration;

namespace EcommerceApp.Services;

public class EmailService
{
    // This class would normally handle email sending, but for the lab we'll simulate it
    // The duplicate patterns here are very common in real e-commerce applications

    public static void SendOrderConfirmation(Order order)
    {
        Console.WriteLine($"[EMAIL] Preparing order confirmation email for {order.CustomerId}");

        // Duplicate email template logic - will also appear in return notifications
        var emailContent = BuildEmailTemplate("order", order.OrderId, order.CustomerId);
        var subject = FormatEmailSubject("Order Confirmation", order.OrderId);

        // Duplicate sending logic
        SendEmail(order.CustomerId, subject, emailContent);
        LogEmailActivity("OrderConfirmation", order.CustomerId, order.OrderId);
    }

    public static void SendReturnConfirmation(Return returnRequest)
    {
        Console.WriteLine($"[EMAIL] Preparing return confirmation email for {returnRequest.CustomerId}");

        // Duplicate email template logic - same pattern as order emails
        var emailContent = BuildEmailTemplate("return", returnRequest.ReturnId, returnRequest.CustomerId);
        var subject = FormatEmailSubject("Return Confirmation", returnRequest.ReturnId);

        // Duplicate sending logic
        SendEmail(returnRequest.CustomerId, subject, emailContent);
        LogEmailActivity("ReturnConfirmation", returnRequest.CustomerId, returnRequest.ReturnId);
    }

    // Duplicate Helper Methods Start - These appear in both order and return processing
    private static string BuildEmailTemplate(string type, string transactionId, string customerId)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        // Evolutionary change: Add a personalized greeting
        var greeting = type == "order" ? "Thank you for your purchase!" : "We appreciate your return!";

        return $"""
                Dear Customer {customerId},
                
                {greeting}
                
                Your {type} with ID {transactionId} has been received and is being processed.
                
                Transaction Details:
                - ID: {transactionId}
                - Date: {timestamp}
                - Status: Confirmed
                
                Thank you for your business!
                
                Best regards,
                E-Commerce Team
                """;
    }

    private static string FormatEmailSubject(string actionType, string transactionId)
    {
        // Evolutionary change: Add a prefix to the subject line
        return $"[E-Commerce] {actionType} - Transaction ID: {transactionId}";
    }

    private static void SendEmail(string customerId, string subject, string content)
    {
        // Simulate email sending
        Console.WriteLine($"[EMAIL] Sending to customer {customerId}: {subject}");
        Console.WriteLine($"[EMAIL] Content preview: {content.Substring(0, Math.Min(50, content.Length))}...");
        System.Threading.Thread.Sleep(50); // Simulate email sending delay
        Console.WriteLine($"[EMAIL] Successfully sent!");
    }

    private static void LogEmailActivity(string emailType, string customerId, string transactionId)
    {
        var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] EMAIL_SENT | Type: {emailType} | Customer: {customerId} | Transaction: {transactionId}";
        Console.WriteLine($"[AUDIT] {logEntry}");
        // In real application, this would write to a log file or database
    }
    // Duplicate Helper Methods End
}
