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
        Console.WriteLine("[INVENTORY] Validating inventory availability...");

        // Evolutionary change: Add a check for discontinued products
        if (_inventory.ContainsKey(productId) && _inventory[productId] == 0)
        {
            Console.WriteLine($"[INVENTORY] Product {productId} is discontinued.");
            return false;
        }

        // Existing validation logic
        if (!_inventory.ContainsKey(productId) || _inventory[productId] < requiredQuantity)
        {
            Console.WriteLine($"[INVENTORY] Insufficient stock for {productId}.");
            return false;
        }

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

    private static void LogInventoryTransaction(string action, string transactionId, string productId, int quantity, string details)
    {
        Console.WriteLine("[INVENTORY] Logging inventory transaction...");

        // Evolutionary change: Add a new log field for transaction type
        Console.WriteLine($"[INVENTORY] Action: {action}, Transaction ID: {transactionId}, Product ID: {productId}, Quantity: {quantity}, Details: {details}, Transaction Type: {(quantity > 0 ? "Restoration" : "Reservation")}");
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
