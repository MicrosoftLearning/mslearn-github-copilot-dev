<!-- ---
lab:
    title: 'Exercise - Refactor large functions using GitHub Copilot'
    description: 'Learn how to analyze complex code and refactor large functions into smaller, more focused methods using GitHub Copilot tools.'
--- -->

# Refactor large functions using GitHub Copilot

Large functions can be difficult to read, maintain, and test. They often contain multiple responsibilities and can be challenging to understand at a glance. Refactoring large functions into smaller, more focused functions can improve code readability and maintainability.

In this exercise, you review an existing project that contains a large function, analyze your options for smaller single-responsibility functions, refactor the large function into smaller functions, and test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of an existing code project and explore options for refactoring the logic. You use GitHub Copilot in Agent mode to refactor the code by extracting code sections from the large function to create smaller functions. You test the original and refactored code to ensure the refactored code works as intended.

This exercise should take approximately **30** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following: Git 2.48 or later, .NET SDK 9.0 or later, Visual Studio Code with the C# Dev Kit extension, and access to a GitHub account with GitHub Copilot enabled.

### Configure your lab environment

If you're using a local PC as a lab environment for this exercise:

- For help configuring your local PC as your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320147" target="_blank">Configure your lab environment resources</a>.

- For help enabling your GitHub Copilot subscription in Visual Studio Code, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

If you're using a hosted lab environment for this exercise:

- For help enabling your GitHub Copilot subscription in Visual Studio Code, paste the following URL into a browser's site navigation bar: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

- Open a command terminal and then run the following commands:

    To ensure that Visual Studio Code is configured to use the correct version of .NET, run the following command:

    ```bash

    dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

    ```

    To ensure that Git is configured to use your name and email address, update the following commands with your information, and then run the commands:

    ```bash

    git config --global user.name "John Doe"

    ```

    ```bash

    git config --global user.email johndoe@example.com

    ```

### Download sample code project

Use the following steps to download the sample project and open it in Visual Studio Code:

1. Open a browser window in your lab environment.

1. To download a zip file containing the sample app project, open the following URL in your browser: [GitHub Copilot lab - consolidate duplicate code](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx8LabApps.zip)

    The zip file is named **GHCopilotEx8LabApps.zip**.

1. Extract the files from the **GHCopilotEx8LabApps.zip** file.

    For example:

    1. Navigate to the downloads folder in your lab environment.

    1. Right-click *GHCopilotEx8LabApps.zip*, and then select **Extract all**.

    1. Select **Show extracted files when complete**, and then select **Extract**.

1. Copy the **GHCopilotEx8LabApps** folder to a location that's easy to access, such as your Windows Desktop folder.

1. Open the **GHCopilotEx8LabApps** folder in Visual Studio Code.

    For example:

    1. Open Visual Studio Code in your lab environment.

    1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

    1. Navigate to the Windows Desktop folder, select **GHCopilotEx8LabApps** and then select **Select Folder**.

1. In the Visual Studio Code SOLUTION EXPLORER view, verify the following solution structure:

    - GHCopilotEx8LabApps\
        - ECommerceOrderProcessing\

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help refactoring large functions in legacy applications. Your goal is to improve code readability and maintainability while preserving the existing functionality. You're assigned to the following app:

- E-CommerceOrderProcessing: This is an e-commerce order processing application that handles customer order validation, inventory management, payment processing, shipping coordination, and customer notifications. The application contains a large `ProcessOrder` method that handles multiple responsibilities and needs to be refactored into smaller, more focused functions.

This exercise includes the following tasks:

1. Review the e-commerce order processing codebase manually.
1. Identify large functions and refactoring opportunities using GitHub Copilot Chat (Ask mode).
1. Refactor large functions into smaller, more manageable functions using GitHub Copilot Chat (Agent mode).
1. Test the refactored e-commerce order processing code.

### Review the e-commerce order processing codebase manually

The first step in any refactoring effort is to ensure that you understand the existing codebase. It's important to understand the code structure, the business logic, and the results generated when the code runs.

In this task, you'll review the main components of the E-commerce order processing project, identify large functions that need refactoring, and test the code.

Use the following steps to complete this task:

1. Take a minute to review the ECommerceOrderProcessing project structure.

    The codebase contains an `OrderProcessor` class with a large `ProcessOrder` method that handles multiple responsibilities. Notice the comments marking steps 1 through 6 within the method. This method is over 80 lines long and handles validation, inventory management, payment processing, shipping, notifications, and order finalization.

1. Examine the `ProcessOrder` method in the `OrderProcessor` class.

    Open **Program.cs** and locate the `ProcessOrder` method. This method represents the core business logic for processing customer orders. Notice that it handles multiple distinct operations:

    - Order validation
    - Inventory checking and reservation  
    - Payment processing
    - Shipping scheduling
    - Email confirmation
    - Order finalization

    The method is long and handles multiple concerns, making it difficult to read, test, and maintain.

1. Add a test method to run the application.

    Since there's no Main method currently, add the following code at the bottom of the **Program.cs** file to create a simple test:

    ```csharp
    // Entry point for testing the order processor
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create a sample order for testing
            var order = new Order
            {
                Id = "ORDER-123",
                CustomerEmail = "customer@example.com", 
                ShippingAddress = "123 Main St, City, State 12345",
                TotalAmount = 99.99m,
                PaymentInfo = new PaymentInfo 
                { 
                    CardNumber = "1234567890123456",
                    CardHolderName = "John Doe",
                    CardCVV = "123",
                    BillingAddress = "123 Main St, City, State 12345"
                },
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductId = "PROD-001", Quantity = 2 },
                    new OrderItem { ProductId = "PROD-002", Quantity = 1 }
                },
                Status = OrderStatus.Pending
            };

            var processor = new OrderProcessor();
            var result = processor.ProcessOrder(order);

            if (result.IsSuccess)
            {
                Console.WriteLine($"Order processed successfully: {result.OrderId}");
            }
            else
            {
                Console.WriteLine($"Order processing failed: {result.ErrorMessage}");
            }
        }
    }
    ```

1. Run the application and review the console output.

    You can run the application from the terminal or by pressing F5 in Visual Studio Code. The console output should show the step-by-step processing of the order, including:

    - Order validation messages
    - Inventory reservation confirmation
    - Payment processing status
    - Shipping scheduling confirmation
    - Email confirmation status
    - Order completion message

1. Take a minute to identify the refactoring opportunities in the `ProcessOrder` method.

    The `ProcessOrder` method contains several distinct responsibilities that could be extracted into separate methods:

    - **Validation logic**: Order validation could be extracted to a `ValidateOrder` method
    - **Inventory management**: Stock checking and reservation could be moved to a `ReserveInventory` method  
    - **Payment processing**: The payment logic could be cleaner with better error handling
    - **Shipping logic**: Shipping scheduling could be extracted to a `ScheduleDelivery` method
    - **Notification logic**: Email confirmation could be moved to a `SendConfirmation` method
    - **Order finalization**: Final order completion could be extracted to a `FinalizeOrder` method

It's important to understand the existing functionality before making changes. By running the code and reviewing the output, you establish a baseline that you can use to verify that your refactoring doesn't break existing functionality.

### Identify large functions using GitHub Copilot Chat (Ask mode)

GitHub Copilot Chat's Ask mode is a powerful tool for analyzing complex code and identifying opportunities for refactoring large functions. In Ask mode, Copilot can analyze your code structure and suggest ways to break down monolithic methods into smaller, more focused functions.

In this task, you'll use GitHub Copilot to systematically identify refactoring opportunities in the `ProcessOrder` method.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view, and then configure Ask mode and the GPT-4.1 model.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Ensure that the chat mode is set to **Ask** and you're using the **GPT-4.1** model.

    The GPT-4.1 model is available with the GitHub Copilot Free plan, is designed to handle complex tasks, and provides intelligent code analysis and suggestions.

1. Close any files that are open in the editor except for Program.cs.

    GitHub Copilot uses files that are open in the editor to establish context. Having only the target file open helps focus the analysis on the code you want to refactor.

1. Add the Program.cs file to the Chat context.

    Use a drag-and-drop operation to add the **Program.cs** file from the SOLUTION EXPLORER to the Chat context.

    Adding a file to the chat context tells GitHub Copilot to include that file when analyzing your prompt.

1. Ask GitHub Copilot to analyze the `ProcessOrder` method for refactoring opportunities.

    For example, submit the following prompt:

    ```text
    Analyze the ProcessOrder method in the OrderProcessor class. Identify opportunities to break this large method into smaller, more focused methods. What specific functions could be extracted, and what would be the benefits of doing so?
    ```

1. Take a minute to review GitHub Copilot's response.

    GitHub Copilot should identify the various responsibilities within the `ProcessOrder` method and suggest how to extract them into separate methods. It may recommend extracting validation, inventory management, payment processing, shipping, notification, and finalization logic.

1. Ask GitHub Copilot to provide a refactoring plan.

    For example:

    ```text
    Create a detailed refactoring plan for the ProcessOrder method. Show me what the refactored ProcessOrder method would look like as a high-level outline, and list the specific methods that should be extracted.
    ```

1. Take a minute to review GitHub Copilot's refactoring plan.

    GitHub Copilot should provide a clear outline showing how the `ProcessOrder` method could be transformed from a large monolithic method into a series of smaller, focused method calls. This makes the code more readable and maintainable.

1. Ask for specific guidance on error handling patterns.

    ```text
    In the refactored version, how should I handle errors consistently across the extracted methods? Should each method return a boolean, throw exceptions, or use another pattern?
    ```

1. Take a minute to review the error handling recommendations.

    Understanding the error handling patterns is crucial for maintaining the existing behavior while improving the code structure.

GitHub Copilot's Ask mode is particularly effective at analyzing code structure and suggesting architectural improvements. It can identify not just what code can be extracted, but why the extractions improve maintainability, testability, and readability.

> **NOTE**: The analysis generated during this task is used to inform the refactoring strategy that you implement in the next section.

### Refactor large functions using GitHub Copilot Chat (Agent mode)

GitHub Copilot's Agent mode enables you to assign complex refactoring tasks that involve multiple code changes. The agent can autonomously extract methods, update calls, and maintain proper error handling while keeping you informed of its progress.

In this task, you'll use GitHub Copilot Agent to systematically refactor the `ProcessOrder` method by extracting smaller, focused methods.

Use the following steps to complete this task:

1. Switch the GitHub Copilot Chat view to Agent mode.

    To change modes, locate the mode selector (typically in the bottom-left corner of the Chat view) and select **Agent**.

1. Take a minute to plan your refactoring strategy.

    Based on the analysis from the previous task, plan the logical order for extracting methods. A suggested approach:

    - **Phase 1**: Extract validation logic into a `ValidateOrder` method
    - **Phase 2**: Extract inventory management into a `ReserveInventory` method  
    - **Phase 3**: Extract shipping logic into a `ScheduleDelivery` method
    - **Phase 4**: Extract notification logic into a `SendConfirmation` method
    - **Phase 5**: Extract finalization logic into a `FinalizeOrder` method

    This phased approach ensures that changes are manageable and can be tested incrementally.

1. Ask GitHub Copilot Agent to extract the validation logic.

    ```text
    Extract the order validation logic from the ProcessOrder method into a new private method called ValidateOrder. The method should take an Order parameter and return a string (null for success, or an error message for failure). Update ProcessOrder to use this new method.
    ```

1. Monitor the agent's progress and review the changes.

    The agent should create a new `ValidateOrder` method and update the `ProcessOrder` method to use it. Review the changes to ensure the validation logic has been properly extracted and the error handling remains consistent.

1. Ask GitHub Copilot Agent to extract the inventory management logic.

    ```text
    Extract the inventory checking and reservation logic from ProcessOrder into a new private method called ReserveInventory. The method should take a list of OrderItems and return a boolean (true for success, false for failure). Include appropriate console output and error handling. Update ProcessOrder to use this new method.
    ```

1. Review and test the inventory changes.

    Run the application to ensure that the inventory logic still works correctly after extraction.

1. Ask GitHub Copilot Agent to extract the shipping logic.

    ```text
    Extract the shipping scheduling logic from ProcessOrder into a new private method called ScheduleDelivery. The method should take an Order parameter and return a boolean. Handle exceptions internally and return false on failure. Update ProcessOrder to use this new method.
    ```

1. Review and test the shipping changes.

1. Ask GitHub Copilot Agent to extract the notification logic.

    ```text
    Extract the email confirmation logic from ProcessOrder into a new private method called SendConfirmation. The method should take email and orderId parameters and return a boolean. Handle exceptions internally and log warnings, but don't fail the order. Update ProcessOrder to use this new method.
    ```

1. Review and test the notification changes.

1. Ask GitHub Copilot Agent to extract the finalization logic.

    ```text
    Extract the order finalization logic from ProcessOrder into a new private method called FinalizeOrder. The method should take an Order parameter and handle setting the status and logging completion. Update ProcessOrder to use this new method.
    ```

1. Review the final refactored `ProcessOrder` method.

    After all extractions, the `ProcessOrder` method should now read like a high-level workflow:

    ```csharp
    public OrderResult ProcessOrder(Order order)
    {
        string validationError = ValidateOrder(order);
        if (validationError != null)
            return OrderResult.Failure(validationError);

        if (!ReserveInventory(order.Items))
            return OrderResult.Failure("Inventory reservation failed");

        try
        {
            PaymentGateway.Charge(order.PaymentInfo, order.TotalAmount);
            Console.WriteLine("Payment processed successfully.");
        }
        catch (PaymentException ex)
        {
            InventoryService.ReleaseStock(order.Items);
            Console.WriteLine($"Payment failed for Order {order.Id}: {ex.Message}");
            return OrderResult.Failure("Payment processing failed");
        }

        if (!ScheduleDelivery(order))
            return OrderResult.Failure("Shipping scheduling failed");

        if (!SendConfirmation(order.CustomerEmail, order.Id))
            Console.WriteLine("Warning: failed to send confirmation email.");

        FinalizeOrder(order);
        return OrderResult.Success(order.Id);
    }
    ```

GitHub Copilot Agent excels at systematic refactoring tasks that require understanding of code flow and maintaining behavior. By breaking the refactoring into logical phases, you ensure that each change is manageable and testable.

### Test the refactored e-commerce order processing code

Manual testing and verification is crucial to ensure that your refactored code maintains the intended business logic and functionality. A successful refactoring process should improve code structure while producing identical behavior to the original implementation.

In this task, you'll verify that the refactored code maintains all original functionality and that the refactoring has been successful.

Use the following steps to complete this task:

1. Build the refactored project to check for compilation errors.

    If there are any compilation errors, review the refactored code and resolve issues. You can use GitHub Copilot to help diagnose and fix any problems that arise from the refactoring process.

1. Run the refactored application and capture the output.

    Run the same test order that you used in the first task. The console output should match the original output, including:

    - Order validation messages
    - Inventory reservation confirmation  
    - Payment processing status
    - Shipping scheduling confirmation
    - Email confirmation status
    - Order completion message

1. Test different scenarios to ensure robustness.

    Create additional test scenarios to verify that error handling still works correctly:

    ```csharp
    // Test with invalid order (should fail validation)
    var invalidOrder = new Order { Id = "INVALID", Items = null };
    var invalidResult = processor.ProcessOrder(invalidOrder);

    // Test with payment failure (use card starting with "0000")  
    var paymentFailOrder = new Order
    {
        Id = "PAY-FAIL",
        CustomerEmail = "test@example.com",
        ShippingAddress = "123 Test St",
        PaymentInfo = new PaymentInfo { CardNumber = "0000111122223333" },
        Items = new List<OrderItem> { new OrderItem { ProductId = "TEST", Quantity = 1 } }
    };
    var paymentResult = processor.ProcessOrder(paymentFailOrder);
    ```

1. Compare the behavior with the original implementation.

    The refactored code should produce identical results and error messages. Any differences should be purely cosmetic (formatting, spacing) rather than functional.

1. Perform a final code review of the structure.

    Review the refactored code structure:

    - **Readability**: The `ProcessOrder` method now reads like a high-level workflow
    - **Maintainability**: Each extracted method has a single responsibility and can be modified independently  
    - **Testability**: Individual methods can be unit tested separately
    - **Reusability**: Extracted methods can potentially be reused in other contexts

Manual testing verifies that your refactoring efforts have achieved the intended goal: improving code structure while maintaining system functionality. The refactored code now provides a more maintainable foundation where each method has a clear, focused responsibility.

## Summary

In this exercise, you learned how to use GitHub Copilot to refactor large functions in an application. You explored the E-commerce Order Processing System, identified large functions that needed refactoring, and used GitHub Copilot to break down monolithic methods into smaller, more focused functions for improved maintainability and readability.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. If you made any changes, revert them as needed. If you're using a local PC as your lab environment, you can archive or delete the sample projects folder that you created for this exercise.
