// ReturnProcessor.cs – Processes product returns.
using System;
using EcommerceApp.Models;
using EcommerceApp.Security;
using EcommerceApp.Configuration;
using EcommerceApp.Services;

namespace EcommerceApp;

public class ReturnProcessor
{
    public void ProcessReturn(string returnId)
    {
        try
        {
            Console.WriteLine($"=== Starting Return Processing ===");
            Console.WriteLine($"Processing return: {returnId}");

            // Ensure the return ID is valid before continuing.
            if (Validate(returnId))
            {
                // Get return details (simulated)
                var returnRequest = GetReturnDetails(returnId);

                // Audit the start of return processing
                AuditService.LogReturnActivity(returnRequest, "PROCESSING_STARTED", "Return validation passed");

                // Validate return eligibility
                if (ValidateReturnEligibility(returnRequest))
                {
                    // Calculate return shipping costs
                    decimal shippingCost = CalculateShipping(returnId, returnRequest);
                    Console.WriteLine($"Return {returnId} shipping cost: ${shippingCost:F2}");

                    // Update return status
                    returnRequest.Status = ReturnStatus.Approved;
                    Console.WriteLine($"Return {returnId} status updated to: {returnRequest.Status}");

                    // Additional return processing logic
                    ProcessRefund(returnRequest);

                    // Handle inventory restoration
                    InventoryService.RestoreReturnInventory(returnRequest);

                    // Send confirmation email
                    EmailService.SendReturnConfirmation(returnRequest);

                    // Final audit log
                    AuditService.LogReturnActivity(returnRequest, "PROCESSING_COMPLETED", $"Refund amount: ${returnRequest.RefundAmount:F2}, Shipping: ${shippingCost:F2}");

                    Console.WriteLine($"Return {returnId} processed successfully!");
                }
                else
                {
                    Console.WriteLine($"Return {returnId} is not eligible for processing.");
                    AuditService.LogReturnActivity(returnRequest, "REJECTED", "Failed eligibility validation");
                }
            }
            else
            {
                Console.WriteLine($"Return {returnId} is invalid and cannot be processed.");
                throw new ArgumentException($"Invalid return ID: {returnId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing return {returnId}: {ex.Message}");
            // In a real application, this would be logged to a proper logging system
        }
        finally
        {
            Console.WriteLine($"=== Return Processing Complete ===\n");
        }
    }

    // Validation logic
    private bool Validate(string returnId)
    {
        Console.WriteLine("[VALIDATION] Validating return ID...");

        // Check for null or whitespace
        if (string.IsNullOrWhiteSpace(returnId))
        {
            Console.WriteLine("[VALIDATION] Return ID cannot be empty.");
            return false;
        }

        // Security validation
        if (!SecurityValidator.IsValidId(returnId, "Return"))
        {
            Console.WriteLine("[VALIDATION] Return ID failed security validation.");
            return false;
        }

        // Length validation (evolved logic)
        if (returnId.Length < AppConfig.MinIdLength || returnId.Length > AppConfig.MaxIdLength)
        {
            Console.WriteLine("[VALIDATION] Return ID length is invalid.");
            return false;
        }

        // Additional prefix validation (evolutionary change)
        if (!returnId.StartsWith("RET"))
        {
            Console.WriteLine("[VALIDATION] Return ID must start with 'RET'.");
            return false;
        }

        return true;
    }

    private decimal CalculateShipping(string returnId, Return returnRequest)
    {
        Console.WriteLine("[SHIPPING] Calculating return shipping cost...");

        // Base shipping cost
        decimal shippingCost = 3.00m;

        // Weight-based adjustment (evolved logic)
        if (returnRequest.TotalWeight > 5)
        {
            shippingCost += 1.50m;
        }

        // Value-based adjustment (evolved logic)
        if (returnRequest.TotalAmount > 30)
        {
            shippingCost -= 0.50m; // Discount for high-value returns
        }

        // Additional handling fee for oversized items (evolutionary change)
        if (returnRequest.IsOversized)
        {
            shippingCost += 4.00m;
        }

        return shippingCost;
    }

    private Return GetReturnDetails(string returnId)
    {
        // Simulate retrieving return request from database
        return new Return
        {
            ReturnId = returnId,
            OriginalOrderId = "ORD12345",
            CustomerId = "CUST001",
            ReturnDate = DateTime.Now,
            ProductId = "PROD001",
            ProductName = "Laptop",
            Quantity = 1,
            RefundAmount = 699.99m,
            Reason = "Product defective",
            Status = ReturnStatus.Pending,
            Weight = 4.5m
        };
    }

    private bool ValidateReturnEligibility(Return returnRequest)
    {
        Console.WriteLine($"Validating return eligibility for {returnRequest.ReturnId}");

        // Check if return is within allowed timeframe
        var daysSinceOrder = (returnRequest.ReturnDate - returnRequest.ReturnDate.AddDays(-AppConfig.MaxReturnDays)).TotalDays;
        if (daysSinceOrder > AppConfig.MaxReturnDays)
        {
            Console.WriteLine($"Return rejected: Exceeds {AppConfig.MaxReturnDays} day return policy");
            return false;
        }

        // Check refund amount limits
        if (returnRequest.RefundAmount > AppConfig.MaxRefundAmount)
        {
            Console.WriteLine($"Return rejected: Refund amount exceeds maximum of ${AppConfig.MaxRefundAmount:F2}");
            return false;
        }

        // Additional business rules could be added here
        Console.WriteLine("Return eligibility validated successfully");
        return true;
    }

    private void ProcessRefund(Return returnRequest)
    {
        Console.WriteLine($"Processing refund for return {returnRequest.ReturnId}, amount: ${returnRequest.RefundAmount:F2}");

        // Audit refund processing start
        AuditService.LogReturnActivity(returnRequest, "REFUND_STARTED", $"Amount: ${returnRequest.RefundAmount:F2}");

        // Simulate refund processing
        System.Threading.Thread.Sleep(100); // Simulate processing time
        Console.WriteLine("Refund processed successfully");

        // Audit refund completion
        AuditService.LogReturnActivity(returnRequest, "REFUND_COMPLETED", "Refund successful");
    }
}
