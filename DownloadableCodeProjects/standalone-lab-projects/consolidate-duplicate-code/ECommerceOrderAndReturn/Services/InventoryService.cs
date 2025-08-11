// Services/InventoryService.cs - Inventory management service (contains duplicate logic)
using System;
using System.Collections.Generic;
using EcommerceApp.Models;

namespace EcommerceApp.Services;

public class InventoryService
{
    // Simulated inventory database
    private static Dictionary<string, int> _inventory = new Dictionary<string, int>
        {
            { "PROD001", 50 },  // Laptop
            { "PROD002", 100 }, // Mouse
            { "PROD003", 25 },  // Keyboard
            { "PROD004", 75 }   // Monitor
        };

    public static void ReserveOrderInventory(Order order)
    {
        Console.WriteLine($"[INVENTORY] Processing inventory reservation for order {order.OrderId}");

        foreach (var item in order.Items)
        {
            // Duplicate inventory validation logic - also used in returns
            if (!ValidateInventoryAvailability(item.ProductId, item.Quantity))
            {
                Console.WriteLine($"[INVENTORY] Warning: Insufficient stock for {item.ProductId}");
                continue;
            }

            // Duplicate inventory update logic
            UpdateInventoryLevel(item.ProductId, -item.Quantity, "RESERVED");

            // Duplicate logging logic
            LogInventoryTransaction("RESERVE", order.OrderId, item.ProductId, item.Quantity, "Order processing");
        }
    }

    public static void RestoreReturnInventory(Return returnRequest)
    {
        Console.WriteLine($"[INVENTORY] Processing inventory restoration for return {returnRequest.ReturnId}");

        // Duplicate inventory validation logic - same pattern as order processing
        if (!ValidateInventoryAvailability(returnRequest.ProductId, 0)) // Check if product exists
        {
            Console.WriteLine($"[INVENTORY] Warning: Product {returnRequest.ProductId} not found in inventory");
            return;
        }

        // Duplicate inventory update logic - but with positive quantity
        UpdateInventoryLevel(returnRequest.ProductId, returnRequest.Quantity, "RESTORED");

        // Duplicate logging logic - same pattern as order processing
        LogInventoryTransaction("RESTORE", returnRequest.ReturnId, returnRequest.ProductId, returnRequest.Quantity, "Return processing");
    }

    // Duplicate Helper Methods Start - These methods are used in both order and return flows
    private static bool ValidateInventoryAvailability(string productId, int requiredQuantity)
    {
        if (!_inventory.ContainsKey(productId))
        {
            Console.WriteLine($"[INVENTORY] Product {productId} not found in inventory system");
            return false;
        }

        var currentStock = _inventory[productId];
        if (currentStock < requiredQuantity)
        {
            Console.WriteLine($"[INVENTORY] Insufficient stock for {productId}: Required {requiredQuantity}, Available {currentStock}");
            return false;
        }

        Console.WriteLine($"[INVENTORY] Stock validation passed for {productId}: {currentStock} available");
        return true;
    }

    private static void UpdateInventoryLevel(string productId, int quantityChange, string reason)
    {
        if (!_inventory.ContainsKey(productId))
        {
            Console.WriteLine($"[INVENTORY] Cannot update inventory: Product {productId} not found");
            return;
        }

        var oldLevel = _inventory[productId];
        _inventory[productId] += quantityChange;
        var newLevel = _inventory[productId];

        Console.WriteLine($"[INVENTORY] Updated {productId}: {oldLevel} → {newLevel} (Change: {quantityChange:+#;-#;0}, Reason: {reason})");

        // Alert if inventory is low
        if (newLevel < 10)
        {
            Console.WriteLine($"[INVENTORY] LOW STOCK ALERT: {productId} has only {newLevel} units remaining!");
        }
    }

    private static void LogInventoryTransaction(string transactionType, string referenceId, string productId, int quantity, string description)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var logEntry = $"[{timestamp}] INVENTORY_{transactionType} | Ref: {referenceId} | Product: {productId} | Qty: {quantity:+#;-#;0} | {description}";
        Console.WriteLine($"[AUDIT] {logEntry}");

        // In a real application, this would write to audit logs or database
    }
    // Duplicate Helper Methods End

    public static void DisplayCurrentInventory()
    {
        Console.WriteLine("[INVENTORY] Current Stock Levels:");
        foreach (var item in _inventory)
        {
            Console.WriteLine($"  {item.Key}: {item.Value} units");
        }
    }
}
