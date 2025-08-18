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

1. To download a zip file containing the sample app project, open the following URL in your browser: [GitHub Copilot lab - refactor large functions](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx8LabApps.zip)

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
            - src\
                - ECommerce.ApplicationCore\
                - ECommerce.Infrastructure\
                - ECommerce.Console\
            - ECommerceOrderProcessing.sln

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help refactoring large functions in legacy applications. Your goal is to improve code readability and maintainability while preserving the existing functionality. You're assigned to the following app:

- E-CommerceOrderProcessing: This is an e-commerce order processing application that handles customer order validation, inventory management, payment processing, shipping coordination, and customer notifications. The application uses Clean Architecture principles with a layered structure, but contains a large `ProcessOrder` method in the `OrderProcessor` class that handles multiple responsibilities and needs to be refactored into smaller, more focused functions.

This exercise includes the following tasks:

1. Review the e-commerce order processing codebase manually.
1. Identify large functions and refactoring opportunities using GitHub Copilot Chat (Ask mode).
1. Refactor large functions into smaller, more manageable functions using GitHub Copilot Chat (Agent mode).
1. Test the refactored e-commerce order processing code.

### Review the e-commerce order processing codebase manually

The first step in any refactoring effort is to ensure that you understand the existing codebase. It's important to understand the code structure, the business logic, and the results generated when the code runs.

In this task, you'll review the main components of the E-commerce order processing project, identify large functions that need refactoring, and test the code.

Use the following steps to complete this task:

1. Ensure that you have the GHCopilotEx8LabApps folder open in Visual Studio Code.

    Refer to the **Before you start** section if you haven't downloaded the sample code project.

1. Take a minute to review the ECommerceOrderProcessing project structure.

    The codebase follows a layered architecture pattern with three main projects:

    - **ECommerce.ApplicationCore**: Contains domain entities, business logic interfaces, and the main `OrderProcessor` service
    - **ECommerce.Infrastructure**: Contains service implementations for external integrations (payment, shipping, inventory, etc.)
    - **ECommerce.Console**: Contains the console application entry point and dependency injection setup

    This structure represents a real-world .NET application using Clean Architecture principles, where business logic is separated from infrastructure concerns.

1. Verify that the **ECommerceOrderProcessing** solution builds successfully.

    For example, in the SOLUTION EXPLORER view, right-click **ECommerceOrderProcessing.sln**, and then select **Build**.

    You should see a successful build with no errors. This confirms that all project dependencies are properly configured.

1. Open the GitHub Copilot Chat view.

    If the Chat view isn't already open, you can open it by selecting the **Chat** icon at the top of the Visual Studio Code window.

1. In the Chat view, ensure that the chat mode is set to **Ask** and the model is set to **GPT-4.1**.

    These settings are available in the bottom-left corner of the Chat view. GitHub Copilot's **Ask** mode is used to ask general coding questions and generate code-related explanations. The **GPT-4.1** model, which is included with the GitHub Copilot Free plan, is a good choice for code analysis and refactoring guidance.

    You'll be using GitHub Copilot's **Agent** mode later in this exercise, but for now you'll use **Ask** mode for code analysis and explanations.

1. In Visual Studio Code, navigate to **src/ECommerce.ApplicationCore/Services/OrderProcessor.cs**.

    This file contains the `OrderProcessor` class with the large `ProcessOrder` method that you'll be refactoring. The method is over 200 lines long and handles multiple responsibilities including validation, security checks, inventory management, payment processing, shipping, notifications, and order finalization.

1. Locate the **ProcessOrder** method in the **OrderProcessor** class.

    The `ProcessOrder` method represents the core business logic for processing customer orders. Notice that it handles multiple distinct operations:

    - **Input validation and security checks**: Email validation, address validation, payment info validation, risk assessment
    - **Inventory management**: Stock checking and reservation with rollback on failures
    - **Payment processing**: Secure payment validation and processing with fraud detection
    - **Shipping coordination**: Shipment scheduling and tracking number generation  
    - **Customer notifications**: Email confirmations and high-value order alerts
    - **Order finalization**: Status updates, completion tracking, and audit logging
    - **Error handling**: Comprehensive exception management with cleanup procedures

    The method is intentionally large and complex to demonstrate real-world scenarios where business logic has accumulated over time, making it difficult to read, test, and maintain.

1. Right-click the **ProcessOrder** method, and then select **Copilot** > **Explain**.

    GitHub Copilot will analyze the ProcessOrder method and provide a detailed explanation of what the code does, helping you understand the business logic before refactoring.

1. Take a few minutes to review GitHub Copilot's explanation.

    The explanation should highlight the main processing steps and business rules, such as the comprehensive validation procedures, security risk assessments, multi-service coordination, and error handling with rollback capabilities.

1. Run the application to understand its current behavior.

    Navigate to the **src/ECommerce.Console** folder in the terminal and run:

    ```bash
    dotnet run
    ```

    Or, if you have the **Program.cs** file open in Visual Studio Code, you can select the run button above the editor.

1. Review the console output from running the application.

    The application runs four test cases that demonstrate different scenarios:

    - **Test 1**: Valid order processing with multiple items
    - **Test 2**: Invalid email address validation  
    - **Test 3**: Declined payment handling
    - **Test 4**: Suspicious order security checks

    Each test shows the step-by-step processing including validation messages, inventory checks, payment processing, shipping scheduling, and notifications. The output also shows how different failure scenarios are handled with appropriate error messages and cleanup procedures.

1. Take a minute to identify the refactoring opportunities in the `ProcessOrder` method.

    The `ProcessOrder` method contains several distinct responsibilities that could be extracted into separate methods:

    - **Validation and security logic**: Input validation and risk assessment could be extracted to methods like `ValidateOrderInput` and `AssessSecurityRisk`
    - **Inventory management**: Stock checking and reservation could be moved to a `HandleInventoryReservation` method  
    - **Payment processing**: The payment logic with fraud checks could be cleaner in a `ProcessOrderPayment` method
    - **Shipping coordination**: Shipping scheduling could be extracted to a `ScheduleOrderShipment` method
    - **Notification management**: Customer communication could be moved to a `SendOrderNotifications` method
    - **Order finalization**: Final order completion could be extracted to a `FinalizeOrderProcessing` method

Understanding the existing functionality and identifying these opportunities will help you create a refactoring strategy that maintains business logic while improving code structure. The layered architecture already provides good separation of concerns at the project level, but the large method needs similar attention at the function level.

### Identify large functions using GitHub Copilot Chat (Ask mode)

GitHub Copilot Chat's Ask mode is a powerful tool for analyzing complex code and identifying opportunities for refactoring large functions. In Ask mode, Copilot can analyze your code structure and suggest ways to break down monolithic methods into smaller, more focused functions.

In this task, you'll use GitHub Copilot to systematically identify refactoring opportunities in the `ProcessOrder` method and understand how to maintain business logic while improving code structure.

Use the following steps to complete this task:

1. Ensure that you have the GitHub Copilot Chat view open with **Ask** mode and the **GPT-4.1** model selected.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Ensure that the chat mode is set to **Ask** and you're using the **GPT-4.1** model. The GPT-4.1 model is available with the GitHub Copilot Free plan and provides intelligent code analysis and suggestions for refactoring tasks.

1. Close any files that are open in the editor except for OrderProcessor.cs.

    GitHub Copilot uses files that are open in the editor to establish context. Having only the target file open helps focus the analysis on the code you want to refactor and ensures GitHub Copilot provides the most relevant suggestions.

1. Add the OrderProcessor.cs file to the Chat context.

    Use a drag-and-drop operation to add the **src/ECommerce.ApplicationCore/Services/OrderProcessor.cs** file from the SOLUTION EXPLORER to the Chat context. Adding a file to the chat context tells GitHub Copilot to include that file when analyzing your prompt, which improves the accuracy of its analysis.

1. Ask GitHub Copilot to analyze the `ProcessOrder` method for refactoring opportunities.

    Submit a prompt that asks GitHub Copilot to analyze the ProcessOrder method and identify specific areas for improvement. Consider including details about what you want to achieve with the refactoring.

    For example:

    ```text
    Analyze the ProcessOrder method in the OrderProcessor class. This method is over 200 lines long and handles multiple responsibilities including validation, security checks, inventory management, payment processing, shipping, and notifications. Identify opportunities to break this large method into smaller, more focused methods. What specific functions could be extracted, and what would be the benefits of doing so?
    ```

1. Take a few minutes to review GitHub Copilot's response.

    GitHub Copilot should identify the various responsibilities within the `ProcessOrder` method and suggest how to extract them into separate methods. The analysis should identify distinct logical sections that can become individual methods, such as validation logic, security assessments, inventory operations, payment processing, shipping coordination, notification handling, and order finalization.

    For example, GitHub Copilot might identify:

    - Input validation and security checks as candidates for extraction
    - Inventory management operations that could be grouped together
    - Payment processing logic with its own error handling
    - Shipping and notification logic as separate concerns
    - Order finalization steps that could be isolated

1. Ask GitHub Copilot to provide a detailed refactoring plan.

    Follow-up with a more specific prompt that asks for a concrete refactoring strategy:

    ```text
    Create a detailed refactoring plan for the ProcessOrder method. Show me what the refactored ProcessOrder method would look like as a high-level outline, and list the specific methods that should be extracted. Include suggestions for method signatures and return types that would maintain the current error handling behavior.
    ```

1. Take a minute to review GitHub Copilot's refactoring plan.

    GitHub Copilot should provide a clear outline showing how the `ProcessOrder` method could be transformed from a large monolithic method into a series of smaller, focused method calls. This plan should maintain the existing business logic while improving code structure and readability.

    The response should include:
    - A high-level flow showing the main steps as separate method calls
    - Suggested method names and signatures for the extracted methods
    - Guidance on how to handle errors consistently across methods
    - Explanations of how the refactored structure improves maintainability

1. Ask for specific guidance on error handling patterns.

    Understanding error handling is crucial for maintaining existing behavior while refactoring. Submit a follow-up prompt:

    ```text
    In the current ProcessOrder method, there are multiple error scenarios with specific cleanup procedures (like releasing inventory on payment failure). In the refactored version, how should I handle errors consistently across the extracted methods? Should each method return a result object, throw exceptions, or use another pattern to maintain the existing error handling behavior?
    ```

1. Take a minute to review the error handling recommendations.

    GitHub Copilot should provide guidance on maintaining consistent error handling patterns across the refactored methods. This is critical because the current method has complex error handling with rollback procedures that must be preserved.

    The recommendations should address:
    - How to maintain the current rollback behavior (like releasing inventory on payment failures)
    - Whether to use return values, exceptions, or result objects for error signaling
    - How to preserve audit logging throughout the refactored methods
    - Ways to ensure cleanup procedures are still executed in error scenarios

GitHub Copilot's Ask mode excels at analyzing complex code structures and providing strategic guidance for refactoring. The insights from this analysis will inform the specific refactoring approach you implement in the next section, ensuring that you maintain business logic integrity while achieving better code organization.

### Refactor large functions using GitHub Copilot Chat (Agent mode)

GitHub Copilot's Agent mode enables you to assign complex refactoring tasks that involve multiple code changes. The agent can autonomously extract methods, update calls, and maintain proper error handling while keeping you informed of its progress.

In this task, you'll use GitHub Copilot Agent to systematically refactor the `ProcessOrder` method by extracting smaller, focused methods while preserving the existing business logic and error handling behavior.

Use the following steps to complete this task:

1. Ensure that the GitHub Copilot Chat view is open in Visual Studio Code.

1. In the Chat view, select the **Agent** mode.

    The **Set Mode** dropdown is located in the bottom-left corner of the Chat view. When you select **Agent**, GitHub Copilot will switch to Agent mode, which allows it to autonomously work on complex tasks that you assign.

1. Take a minute to plan your refactoring strategy.

    Based on the analysis from the previous task, plan the logical order for extracting methods. A systematic approach ensures that each change is manageable and testable. Consider this phased refactoring strategy:

    - **Phase 1**: Extract input validation and security assessment logic
    - **Phase 2**: Extract inventory management operations (checking and reservation)
    - **Phase 3**: Extract payment processing with fraud detection and error handling
    - **Phase 4**: Extract shipping coordination and tracking management
    - **Phase 5**: Extract notification and communication logic
    - **Phase 6**: Extract order finalization and completion procedures

    This phased approach ensures that changes are manageable and the refactored code maintains the same business logic and error handling as the original method.

1. Ask GitHub Copilot Agent to extract the validation and security logic.

    Start with the first phase of refactoring by extracting the input validation and security assessment logic:

    ```text
    Extract the order validation and security assessment logic from the ProcessOrder method into a new private method called ValidateOrderAndAssessSecurity. The method should take an Order parameter and return a string (null for success, or an error message for failure). Include all validation checks: null checks, email validation, address validation, payment info validation, and security risk assessment. Update ProcessOrder to use this new method while maintaining the same error handling behavior.
    ```

1. Monitor the agent's progress and review the changes.

    GitHub Copilot Agent will analyze the method, identify the validation logic, extract it into a new method, and update the ProcessOrder method to use the new method. Review the changes to ensure that all validation logic has been properly extracted and that the error handling behavior remains consistent.

    The agent should maintain the same audit logging and error messages as the original code while organizing the validation logic into a more focused, single-responsibility method.

1. Test the first refactoring phase.

    Run the application to ensure that the validation logic still works correctly after extraction:

    ```bash
    cd src/ECommerce.Console
    dotnet run
    ```

    All four test cases should produce the same results as before the refactoring. Pay particular attention to Test 2 (invalid email) and Test 4 (suspicious order) to ensure that validation logic is working correctly.

1. Ask GitHub Copilot Agent to extract the inventory management logic.

    Continue with the second phase by extracting inventory operations:

    ```text
    Extract the inventory checking and reservation logic from ProcessOrder into a new private method called HandleInventoryReservation. The method should take a list of OrderItems and return a boolean (true for success, false for failure). Include stock checking for each item, item validation, and inventory reservation. Maintain all console output and audit logging. Update ProcessOrder to use this new method.
    ```

1. Review and test the inventory changes.

    After the agent completes the extraction, run the application again to verify that inventory management still works correctly. The inventory reservation logic should behave exactly as before, including proper error handling for out-of-stock items.

1. Ask GitHub Copilot Agent to extract the payment processing logic.

    Move to the third phase with payment processing extraction:

    ```text
    Extract the payment processing logic from ProcessOrder into a new private method called ProcessOrderPayment. The method should take Order and PaymentInfo parameters and return a string containing the payment reference (null indicates failure). Include fraud detection, payment gateway calls, and comprehensive error handling. Ensure that inventory is released if payment fails, just like in the original code. Update ProcessOrder to use this new method.
    ```

1. Review and test the payment processing changes.

    Test the payment logic thoroughly, including the Test 3 scenario (declined payment) to ensure that error handling and inventory rollback procedures work correctly.

1. Ask GitHub Copilot Agent to extract the shipping coordination logic.

    Continue with the fourth phase:

    ```text
    Extract the shipping scheduling logic from ProcessOrder into a new private method called ScheduleOrderShipment. The method should take an Order parameter and return a boolean indicating success or failure. Include shipping requirement validation, shipment scheduling, and tracking number assignment. Handle exceptions internally and return false on failure while maintaining audit logging. Update ProcessOrder to use this new method.
    ```

1. Review and test the shipping changes.

    Verify that shipping coordination works correctly and that tracking numbers are still properly assigned to orders.

1. Ask GitHub Copilot Agent to extract the notification logic.

    Move to the fifth phase with notification handling:

    ```text
    Extract the notification and communication logic from ProcessOrder into a new private method called SendOrderNotifications. The method should take Order parameters and return a boolean indicating overall success. Include email confirmation sending and high-value order alerts. Handle exceptions internally and log warnings for notification failures, but don't fail the entire order process. Update ProcessOrder to use this new method.
    ```

1. Review and test the notification changes.

    Ensure that email notifications are still sent correctly and that notification failures don't prevent successful order completion.

1. Ask GitHub Copilot Agent to extract the order finalization logic.

    Complete the refactoring with the final phase:

    ```text
    Extract the order finalization logic from ProcessOrder into a new private method called FinalizeOrderProcessing. The method should take an Order parameter and handle setting the order status, completion date, processing duration, and final audit logging. Update ProcessOrder to use this new method.
    ```

1. Review the final refactored `ProcessOrder` method.

    After all extractions are complete, the `ProcessOrder` method should now read like a high-level workflow that clearly shows the main business process steps:

    ```csharp
    public OrderResult ProcessOrder(Order order)
    {
        _auditLogger.LogOrderProcessingStarted(order.Id, order.CustomerEmail);

        try
        {
            // Step 1: Validate order and assess security risks
            string validationError = ValidateOrderAndAssessSecurity(order);
            if (validationError != null)
                return OrderResult.Failure(validationError);

            // Step 2: Handle inventory reservation
            if (!HandleInventoryReservation(order.Items))
                return OrderResult.Failure("Inventory reservation failed");

            // Step 3: Process payment with fraud detection
            string paymentReference = ProcessOrderPayment(order, order.PaymentInfo);
            if (paymentReference == null)
                return OrderResult.Failure("Payment processing failed");
            
            order.PaymentReference = paymentReference;

            // Step 4: Schedule shipping and generate tracking
            if (!ScheduleOrderShipment(order))
                return OrderResult.Failure("Shipping scheduling failed");

            // Step 5: Send notifications (warnings only, don't fail order)
            SendOrderNotifications(order);

            // Step 6: Finalize order processing
            FinalizeOrderProcessing(order);

            return OrderResult.Success(order.Id, order.TrackingNumber ?? "");
        }
        catch (Exception ex)
        {
            // Existing cleanup logic remains the same
            _auditLogger.LogUnexpectedError(order?.Id ?? "UNKNOWN", ex.Message);
            // ... cleanup procedures
            return OrderResult.Failure("An unexpected error occurred during order processing");
        }
    }
    ```

1. Perform a final comprehensive test of the refactored code.

    Run the full application one more time to ensure that all test cases pass and produce identical results to the original implementation:

    ```bash
    cd src/ECommerce.Console
    dotnet run
    ```

    Verify that all four test cases still work correctly:
    - **Test 1**: Valid order processing should complete successfully
    - **Test 2**: Invalid email should be rejected during validation
    - **Test 3**: Declined payment should trigger inventory rollback
    - **Test 4**: Suspicious orders should be flagged by security assessment

GitHub Copilot Agent excels at systematic refactoring tasks that require understanding of code flow, business logic, and error handling patterns. By breaking the refactoring into logical phases, you ensure that each change is manageable, testable, and maintains the original system behavior while significantly improving code organization and maintainability.

### Test the refactored e-commerce order processing code

Manual testing and verification is crucial to ensure that your refactored code maintains the intended business logic and functionality. A successful refactoring process should improve code structure while producing identical behavior to the original implementation.

In this task, you'll thoroughly test the refactored code to verify that all business logic has been preserved and that the refactoring has achieved its goals of improved maintainability and readability.

Use the following steps to complete this task:

1. Build the refactored project to check for compilation errors.

    In the SOLUTION EXPLORER view, right-click **ECommerceOrderProcessing.sln** and select **Build**. If there are any compilation errors, review the refactored code and resolve issues. You can use GitHub Copilot to help diagnose and fix any problems that arise from the refactoring process.

    A successful build confirms that all method signatures, parameter types, and dependencies are correctly maintained after the refactoring.

1. Run the refactored application and verify identical behavior.

    Navigate to the **src/ECommerce.Console** folder and run the application:

    ```bash
    cd src/ECommerce.Console
    dotnet run
    ```

    Compare the output with the behavior you observed before refactoring. The console output should be identical, including:

    - **Test 1 (Valid Order)**: Should complete successfully with payment processing, shipping scheduling, and notifications
    - **Test 2 (Invalid Email)**: Should fail validation with the same error message
    - **Test 3 (Declined Payment)**: Should fail payment processing and trigger inventory rollback
    - **Test 4 (Suspicious Order)**: Should be flagged by security assessment and rejected

    The refactored code should produce exactly the same results, demonstrating that the business logic has been preserved throughout the refactoring process.

1. Test different edge case scenarios to ensure robustness.

    Create additional test scenarios to verify that error handling still works correctly in various edge cases. You can modify the test cases in **Program.cs** temporarily to test additional scenarios:

    ```csharp
    // Test with null order (should fail validation immediately)
    var nullOrderResult = processor.ProcessOrder(null);

    // Test with empty items list (should fail validation)
    var emptyOrder = CreateSampleOrder("ORD-EMPTY", "test@example.com", "123 Test St", 
        new List<OrderItem>(), 
        new PaymentInfo { CardNumber = "4111111111111111", CardCVV = "123", CardHolderName = "Test User", ExpiryMonth = "12", ExpiryYear = "2025", BillingAddress = "123 Test St" });

    // Test with invalid shipping address (should fail validation)
    var invalidAddressOrder = CreateSampleOrder("ORD-ADDR", "user@example.com", "", 
        new List<OrderItem> { new() { ProductId = "BOOK-001", Quantity = 1, Price = 15.99m } },
        new PaymentInfo { CardNumber = "4111111111111111", CardCVV = "123", CardHolderName = "Test User", ExpiryMonth = "12", ExpiryYear = "2025", BillingAddress = "123 Test St" });
    ```

    These additional tests help verify that the refactored validation logic handles edge cases correctly and that error messages remain consistent with the original implementation.

1. Verify that audit logging continues to work correctly.

    Check the **order_audit_log.txt** file in the **src/ECommerce.Console** directory to ensure that audit logging is still functioning properly throughout the refactored methods. The audit log should contain:

    - Order processing start events
    - Validation failure logs for invalid orders
    - Security event logs for suspicious orders
    - Payment processing logs
    - Shipping scheduling logs
    - Order completion logs

    The audit trail should be complete and demonstrate that logging has been preserved across all the extracted methods.

1. Perform a code review of the refactored structure.

    Review the refactored **OrderProcessor.cs** file and evaluate the improvements achieved:

    - **Readability**: The main `ProcessOrder` method should now read like a clear, high-level business workflow
    - **Single Responsibility**: Each extracted method should have a focused, single responsibility
    - **Maintainability**: Individual business logic sections can now be modified independently without affecting other parts of the process
    - **Testability**: Each extracted method could be unit tested separately if needed
    - **Error Handling**: Complex error handling and rollback procedures should be maintained while being better organized
    - **Reusability**: Some extracted methods might be reusable in other contexts (like validation methods)

1. Compare the before and after code structure.

    Take a moment to compare the original 200+ line `ProcessOrder` method with the refactored version. The refactored version should:

    - Have a clear, readable main method that shows the business process flow
    - Contain 5-6 focused helper methods, each handling a specific aspect of order processing
    - Maintain identical functionality and error handling behavior
    - Be significantly easier to understand, maintain, and test

1. Document the refactoring results.

    Consider creating a summary of what was accomplished:

    - **Original**: One 200+ line method handling all order processing responsibilities
    - **Refactored**: One main orchestration method + 5-6 focused helper methods
    - **Benefits Achieved**: Improved readability, better separation of concerns, enhanced maintainability, easier testing, and preserved functionality
    - **Business Logic Preserved**: All validation rules, security checks, error handling, and rollback procedures maintained

Manual testing verifies that your refactoring efforts have successfully achieved the goal of improving code structure while maintaining system functionality. The refactored code now provides a much more maintainable foundation where each method has a clear, focused responsibility, making future enhancements and bug fixes significantly easier to implement.

## Summary

In this exercise, you learned how to use GitHub Copilot to refactor large functions in an application. You explored the E-commerce Order Processing System, identified large functions that needed refactoring, and used GitHub Copilot to break down monolithic methods into smaller, more focused functions for improved maintainability and readability.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. If you made any changes, revert them as needed. If you're using a local PC as your lab environment, you can archive or delete the sample projects folder that you created for this exercise.
