using System;
using EcommerceApp.Services;

namespace EcommerceApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== E-Commerce Order and Return Processing System ===");
        Console.WriteLine("Starting application tests...\n");

        // Show initial inventory
        Console.WriteLine("INITIAL INVENTORY STATUS:");
        InventoryService.DisplayCurrentInventory();
        Console.WriteLine();

        // Test successful order processing
        Console.WriteLine("TEST 1: Processing a valid order");
        var orderProcessor = new OrderProcessor();
        orderProcessor.ProcessOrder("ORD12345");   // simulate processing a valid order

        // Test successful return processing
        Console.WriteLine("TEST 2: Processing a valid return");
        var returnProcessor = new ReturnProcessor();
        returnProcessor.ProcessReturn("RET98765"); // simulate processing a valid return

        // Show inventory after processing
        Console.WriteLine("INVENTORY STATUS AFTER PROCESSING:");
        InventoryService.DisplayCurrentInventory();
        Console.WriteLine();

        // Test invalid order processing (for security validation)
        Console.WriteLine("TEST 3: Processing an invalid order (security test)");
        try
        {
            orderProcessor.ProcessOrder("INVALID123"); // This should fail validation
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error caught: {ex.Message}");
        }

        // Test invalid return processing (for security validation)
        Console.WriteLine("TEST 4: Processing an invalid return (security test)");
        try
        {
            returnProcessor.ProcessReturn("<script>alert('xss')</script>"); // This should fail security validation
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error caught: {ex.Message}");
        }

        // Test empty ID validation
        Console.WriteLine("TEST 5: Processing with empty ID");
        try
        {
            orderProcessor.ProcessOrder(""); // This should fail validation
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Expected error caught: {ex.Message}");
        }

        Console.WriteLine("\n=== All tests completed ===");
        Console.WriteLine("Application is ready for refactoring exercise!");
        Console.WriteLine("\nDuplicate code locations found:");
        Console.WriteLine("1. Validate() method in both OrderProcessor and ReturnProcessor");
        Console.WriteLine("2. CalculateShipping() method in both OrderProcessor and ReturnProcessor");
        Console.WriteLine("3. Email template and sending logic in EmailService (SendOrderConfirmation & SendReturnConfirmation)");
        Console.WriteLine("4. Audit entry creation and logging in AuditService (LogOrderActivity & LogReturnActivity)");
        Console.WriteLine("5. Inventory validation and update logic in InventoryService (ReserveOrderInventory & RestoreReturnInventory)");
        Console.WriteLine("\nThese methods demonstrate common real-world duplicate code patterns and should be consolidated during the lab exercise.");
        Console.WriteLine("Students can use GitHub Copilot to help identify and extract common functionality into shared services or base classes.");
    }
}
