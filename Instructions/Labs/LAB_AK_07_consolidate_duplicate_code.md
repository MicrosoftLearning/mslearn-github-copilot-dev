<!-- ---
lab:
    title: 'Exercise - Consolidate duplicate code using GitHub Copilot'
    description: 'Learn how to consolidate code that implements duplicate logic (either duplicated code or code that's similar and used to implement the same business logic) across multiple files using GitHub Copilot tools.'
--- -->

# Consolidate duplicate code using GitHub Copilot

Duplicate code logic can be introduced by individuals or teams when working rapidly to create (or extend) a codebase that includes similar or related features. In some cases, it can be as simple as copying code from one file or class into another to quickly prototype a new feature. To further complicate matters, duplicated logic can evolve differently based on the surrounding code, masking the duplicated logic. This can include changes to variable names, control flow structures, etc. A rushed schedule, poor documentation, and a lack of proper code reviews can exacerbate the issue. In the end, duplicated logic makes the code difficult to maintain, debug, and test.

In this exercise, you review an existing project that contains duplicated code logic, analyze your options for consolidation, consolidate the duplicated code logic, and test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of an existing code project and explore options for consolidating the logic. You use GitHub Copilot in Agent mode to refactor the code by combining duplicate logic into shared methods or functions. You test the original and refactored code to ensure the consolidated logic works as intended.

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

1. To download a zip file containing the sample app project, open the following URL in your browser: [GitHub Copilot lab - consolidate duplicate code](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx7LabApps.zip)

    The zip file is named **GHCopilotEx7LabApps.zip**.

1. Extract the files from the **GHCopilotEx7LabApps.zip** file.

    For example:

    1. Navigate to the downloads folder in your lab environment.

    1. Right-click *GHCopilotEx7LabApps.zip*, and then select **Extract all**.

    1. Select **Show extracted files when complete**, and then select **Extract**.

1. Copy the **GHCopilotEx7LabApps** folder to a location that's easy to access, such as your Windows Desktop folder.

1. Open the **GHCopilotEx7LabApps** folder in Visual Studio Code.

    For example:

    1. Open Visual Studio Code in your lab environment.

    1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

    1. Navigate to the Windows Desktop folder, select **GHCopilotEx7LabApps** and then select **Select Folder**.

1. In the Visual Studio Code SOLUTION EXPLORER view, verify the following solution structure:

    - GHCopilotEx7LabApps\
        - ECommerceOrderAndReturn\
            - bin\
            - Configuration\
                - AppConfig.cs
            - Models\
                - Order.cs
                - Return.cs
            - obj\
            - Security\
                - SecurityValidator.cs
            - Services\
                - AuditService.cs
                - EmailService.cs
                - InventoryService.cs
            - DUPLICATE_CODE_SUMMARY.md
            - ECommerceOrderAndReturn.csproj
            - EXPECTED_OUTPUT.md
            - OrderProcessor.cs
            - Program.cs
            - README.md
            - ReturnProcessor.cs

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help consolidating duplicate code logic. Your goal is to improve code maintainability while preserving the existing functionality. You're assigned to the following app:

- E-commerce orders and returns: This app is an E-commerce orders and returns system that processes customer orders and handles product returns.

This exercise includes the following tasks:

1. Review the E-commerce orders and returns codebase.
1. Identify duplicate code using GitHub Copilot Chat (Ask mode).
1. Consolidate duplicate logic using GitHub Copilot Chat (Agent mode).
1. Test the refactored E-commerce orders and returns code.

### Review the E-commerce orders and returns codebase

The first step in any refactoring effort is to ensure that you understand the existing codebase. It's important to understand the code structure, the business logic, and the results generated when the code runs.

In this task, you'll open the E-commerce order and return processing project, scan for duplicate code patterns, and test the code.

Use the following steps to complete this task:

1. Open the ECommerceOrderAndReturn project folder in Visual Studio Code.

    Take a moment to explore the project structure. Notice that the codebase includes multiple architectural layers including Models, Services, Security, and Configuration.

1. Examine the main processing classes.

    Open **OrderProcessor.cs** and **ReturnProcessor.cs** side by side. These classes represent the core business logic for processing customer orders and product returns respectively.

    As you review the code, you'll notice that both classes have similar method signatures and processing patterns. This is the most obvious type of duplication, but there are additional, more subtle duplications throughout the codebase.

1. Review the Services layer.

    Navigate to the **Services** folder and examine **EmailService.cs**, **AuditService.cs**, and **InventoryService.cs**.

1. Run the application to understand its behavior.

    For example, open a terminal in Visual Studio Code and execute `dotnet run` from the **ECommerceOrderAndReturn** project directory.

    Observe the comprehensive console output that shows:
    - Initial inventory levels
    - Order processing with validation, shipping calculation, payment processing, inventory reservation, email notifications, and audit logging
    - Return processing with similar steps but adapted for returns
    - Updated inventory levels
    - Security validation tests with various invalid inputs

    The application runs 5 test scenarios to demonstrate both successful processing and security validation failures.

1. Scan the code for duplicate code patterns.

    Look for the following types of duplication:

    **Core Business Logic Duplication**: Notice that OrderProcessor and ReturnProcessor have identical `Validate()` and similar `CalculateShipping()` methods.

    **Service Layer Duplication**: Notice how each service has methods that follow similar patterns but are duplicated for different business processes (orders vs returns).

    **Cross-cutting Concerns**: Notice that payment processing, status updates, and error handling patterns are repeated across both processors.

It's important to understand the existing functionality before making changes. By running the code and reviewing the output, you establish a baseline that you can use to verify that your refactoring doesn't break existing functionality.

### Identify duplicate code using GitHub Copilot Chat (Ask mode)

GitHub Copilot Chat's Ask mode is a great tool for analyzing complex codebases and identifying subtle duplication patterns that might not be immediately obvious. In Ask mode, Copilot acts as an intelligent code reviewer that can analyze multiple files simultaneously and identify both obvious and hidden (code logic) duplications.

In this task, you'll use GitHub Copilot to systematically identify the various types of duplicate code patterns in the enhanced e-commerce application.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view, and then configure Ask mode and the GPT-4.1 model.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Ensure that the chat mode is set to **Ask** and you're using the **GPT-4.1** model.

    The GPT-4.1 model is available with the GitHub Copilot Free plan, is designed to handle complex tasks, and provides intelligent code analysis/suggestions.

1. Add the OrderProcessor and ReturnProcessor files to the Chat context.

    Open **OrderProcessor.cs** and **ReturnProcessor.cs** in the editor. You can also drag these files directly into the Chat view to ensure GitHub Copilot reviews the full context of both files.

1. Ask GitHub Copilot to identify the primary duplicate code patterns.

    For example, submit the following prompt to analyze the core duplication:

    ```text
    What duplicate code exists between OrderProcessor.cs and ReturnProcessor.cs? Please identify specific methods and logic that are duplicated between these classes. Describe opportunities to consolidate this code.
    ```

1. Take a minute to review GitHub Copilot's response.

    GitHub Copilot should identify the `Validate()` method duplication and the similar patterns in `CalculateShipping()` methods. It may also note the similar audit logging and error handling patterns.

1. Add the **EmailService.cs** file to the Chat context.

    Add the **EmailService.cs** file to the Chat context by dragging it into the chat. You can also open it in the editor.

1. Ask GitHub Copilot to identify duplications in the EmailService class.

    For example:

    ```text
    Analyze the EmailService class. What duplicate logic exists within this service for handling order confirmations versus return confirmations? Describe opportunities to consolidate this code.
    ```

1. Take a minute to review GitHub Copilot's response.

    GitHub Copilot should identify the duplicate template building, subject formatting, email sending, and logging methods that are used by both `SendOrderConfirmation` and `SendReturnConfirmation`.

1. Add the **AuditService.cs** and **InventoryService.cs** files to the Chat context.

1. Ask GitHub Copilot to identify duplications in the audit and inventory service files.

    For example:

    ```text
    Analyze the AuditService and InventoryService classes. Identify the methods that contain duplicate logic patterns that could be consolidated? Describe opportunities to consolidate this code.
    ```

1. Take a minute to review GitHub Copilot's response.

    GitHub Copilot should identify patterns like audit entry creation/validation/storage in AuditService, and inventory validation/updating/logging in InventoryService.

1. Ask GitHub Copilot to perform a comprehensive duplication analysis.

    For a broader view, ask GitHub Copilot to analyze the entire codebase:

    ```text
    @workspace Analyze the entire ECommerceOrderAndReturn codebase and identify all forms of code duplication, including method-level, service-level, and architectural pattern duplications. Prioritize the duplications by impact and refactoring difficulty. Describe an approach for consolidating this code.
    ```

1. Take a minute to review GitHub Copilot's response.

    The response should provide a comprehensive analysis of all duplication patterns and suggest which ones should be addressed first.

1. Verify GitHub Copilot's analysis with manual code review.

    Cross-reference GitHub Copilot's findings with your own observations. Ensure that you understand not just what is duplicated, but why these patterns exist and how they should be consolidated while maintaining the business logic integrity.

GitHub Copilot's Ask mode is particularly powerful for identifying subtle duplications that go beyond simple copy-paste scenarios. It can recognize similar logical patterns, equivalent business rules implemented differently, and architectural duplications that span multiple files. The analysis from this task will inform the refactoring strategy you'll implement in the next section.

### Consolidate duplicate logic using GitHub Copilot Chat (Agent mode)

GitHub Copilot's Agent mode enables you to assign complex, multi-step refactoring tasks that span multiple files and architectural layers. The agent can autonomously create new files, modify existing code, and implement comprehensive refactoring strategies while keeping you informed of its progress.

In this task, you'll use GitHub Copilot Agent to systematically eliminate the duplicate code patterns identified in the previous task, starting with the most straightforward duplications and progressing to more complex service-layer consolidations.

Use the following steps to complete this task:

1. Switch the GitHub Copilot Chat view to Agent mode.

    In the Chat view, locate the mode selector (typically in the bottom-left corner) and select **Agent**. This enables GitHub Copilot to make autonomous changes to your codebase.

1. Plan the refactoring strategy.

    Before assigning tasks to the agent, consider the logical order for refactoring:

    - **Phase 1**: Core business logic duplication (validation and shipping calculation)
    - **Phase 2**: Service-layer duplications (email, audit, inventory services)
    - **Phase 3**: Cross-cutting concerns and architectural improvements

    This phased approach ensures that changes are manageable and can be tested incrementally.

1. Ask GitHub Copilot Agent to consolidate the validation logic in the OrderProcessor and ReturnProcessor classes.

    For example, submit the following task to GitHub Copilot Agent:

    ```text
    Create a shared ValidationService class that consolidates the duplicate Validate() method logic from OrderProcessor and ReturnProcessor. The service should handle ID validation for both orders and returns while maintaining all existing security checks, business rules, and logging. Update both processor classes to use the new shared service and remove the duplicate private methods.
    ```

1. Monitor the agent's progress in the Chat view.

    The agent's progress should be visible in the chat as it completes the assigned task. The agent will:

    - Create a new **ValidationService.cs** file in the Services folder
    - Extract the validation logic into a reusable method
    - Update both processor classes to use the new service
    - Remove the duplicate private validation methods

1. Review and accept the validation service changes.

    Examine the changes proposed by the agent. The new validation service should maintain all the existing validation logic while providing a single, reusable implementation. If the changes look correct, accept them.

    > [!NOTE]
    > When you're working on production code, it's good practice to perform incremental testing after significant refactoring operations. Build and test the application to verify that the output remains consistent with the original behavior. If any issues arise, work with GitHub Copilot to resolve them before proceeding to the next refactoring phase.

1. Ask GitHub Copilot Agent to consolidate shipping calculation logic.

    For example, use the following task to consolidate shipping calculations:

    ```text
    Create a shared ShippingCalculationService that consolidates the similar CalculateShipping() logic from OrderProcessor and ReturnProcessor. The service should handle both order shipping (with free shipping thresholds) and return shipping (with processing fees) while maintaining the different business rules for each type. Update both processor classes to use the new shared service.
    ```

    The agent should create a shipping service that handles both scenarios while preserving the different business rules for orders versus returns.

1. Review and accept the changes.

1. Ask GitHub Copilot Agent to refactor the EmailService duplications.

    For example, use the following task to consolidate the email service duplications:

    ```text
    Refactor the EmailService class to eliminate duplicate logic in the helper methods. Create a unified approach for template building, subject formatting, email sending, and activity logging that can handle both order and return confirmations. The public methods SendOrderConfirmation and SendReturnConfirmation should remain, but they should use shared private helper methods.
    ```

1. Review and accept the changes.

1. Ask GitHub Copilot Agent to consolidate AuditService duplications.

    For example, use the following task to consolidate the audit service duplications:

    ```text
    Refactor the AuditService class to consolidate the duplicate logic in LogOrderActivity and LogReturnActivity. Create shared helper methods for audit entry creation, validation, storage, and compliance checking. The public methods should remain but use common underlying logic.
    ```

1. Review and accept the changes.

1. Ask GitHub Copilot Agent to address InventoryService duplications.

    For example, use the following task to handle the inventory service duplications:

    ```text
    Refactor the InventoryService class to eliminate duplicate logic between ReserveOrderInventory and RestoreReturnInventory. Create shared helper methods for inventory validation, level updates, and transaction logging while maintaining the different business logic for reservations versus restorations.
    ```

1. Review the final architecture.

    Once all refactoring tasks are complete, review the updated codebase structure. You should now have:

    - Consolidated validation and shipping services
    - Refactored service classes with eliminated duplications
    - Maintained business logic and functionality
    - Improved code maintainability and reusability

The GitHub Copilot Agent mode excels at complex, multi-file refactoring tasks that require understanding of business logic and architectural patterns. By breaking the refactoring into logical phases and testing incrementally, you ensure that the consolidation maintains system integrity while significantly improving code quality.

### Test the refactored E-commerce orders and returns code

Testing is crucial to ensure that your refactoring maintains the original business logic and functionality. A successful refactoring should eliminate duplicate code while producing identical behavior to the original implementation.

In this task, you'll validate that the refactored code maintains all original functionality and that the consolidation has been successful.

Use the following steps to complete this task:

1. Build the refactored project to check for compilation errors.

    Open a terminal in the **ECommerceOrderAndReturn** project directory and run:

    ```bash
    dotnet build
    ```

    If there are any compilation errors, review the refactored code and resolve issues. You can use GitHub Copilot to help diagnose and fix any problems that arise from the refactoring process.

1. Run the refactored application and capture the output.

    Execute the application and carefully observe the console output:

    ```bash
    dotnet run
    ```

    The application should run all 5 test scenarios exactly as before:

    - Initial inventory display
    - Valid order processing with complete workflow
    - Valid return processing with complete workflow
    - Updated inventory levels
    - Security validation tests with invalid inputs

1. Compare the refactored output with the original behavior.

    The output should be identical to the pre-refactoring version, confirming that:

    - All validation logic still works correctly
    - Shipping calculations produce the same results
    - Email notifications are still sent
    - Audit logging captures all activities
    - Inventory tracking functions properly
    - Security validations catch invalid inputs appropriately

    If you notice any differences in behavior, investigate and resolve them before proceeding.

1. Verify code consolidation success.

    Use GitHub Copilot to confirm that duplication has been eliminated:

    ```text
    @workspace Analyze the refactored codebase and confirm that the previously identified duplicate code patterns have been successfully consolidated. List any remaining duplications that should be addressed.
    ```

    GitHub Copilot should confirm that the major duplications have been resolved and the code is now more maintainable.

1. Test edge cases and error scenarios.

    Although the current test scenarios cover basic functionality, consider testing additional scenarios to ensure robustness:

    - Very long IDs (to test length validation)
    - Special characters in IDs (to test security validation)
    - Different order amounts (to test shipping calculations)

    You can modify the test data in **Program.cs** or ask GitHub Copilot to help create additional test scenarios.

1. Perform a final code review.

    Review the refactored codebase to ensure:

    - **Code Quality**: Methods are well-named and follow consistent patterns
    - **Maintainability**: Changes to business rules now require updates in only one location
    - **Readability**: The code structure is clear and logical
    - **Reusability**: Shared services can be easily extended for future requirements

Successful testing validates that your refactoring has achieved its goals: eliminating duplicate code while maintaining system functionality. The enhanced architecture now provides a more maintainable foundation for future development, where business rule changes can be implemented in a single location rather than requiring updates across multiple duplicate implementations.

## Summary

In this exercise, you used GitHub Copilot to identify and consolidate duplicate code patterns in an e-commerce application. You learned how to use Ask mode to systematically analyze complex codebases for subtle duplications, and Agent mode to implement comprehensive refactoring strategies across multiple files and architectural layers. 

Key accomplishments include:

- **Systematic Analysis**: Used GitHub Copilot's Ask mode to identify various types of duplicate code patterns, from obvious method duplications to subtle service-layer patterns
- **Strategic Refactoring**: Applied Agent mode to implement multi-phase consolidation strategies that maintain business logic while improving code quality
- **Enterprise Patterns**: Worked with realistic duplicate code scenarios common in enterprise applications, including service-layer duplications and cross-cutting concerns
- **Quality Assurance**: Validated that refactoring maintained system functionality through comprehensive testing and behavioral verification

The consolidation process transformed the codebase from having duplicate implementations scattered across multiple files to a maintainable architecture where business rules are implemented in single locations. This improvement significantly reduces the risk of inconsistent behavior when business requirements change and makes the code easier to test, debug, and extend.

GitHub Copilot's dual approach of Ask mode for analysis and Agent mode for implementation provides a powerful framework for tackling complex refactoring challenges that would be time-consuming and error-prone to handle manually.

## Duplicate Code Summary

The E-Commerce Order and Return Processing System has been significantly enhanced to include **5 different types of duplicate code patterns** that are commonly found in real-world e-commerce applications.

## Duplicate Code Patterns Implemented

### 1. Core Business Logic Duplication (Original)

**Location**: `OrderProcessor.cs` and `ReturnProcessor.cs`

- `Validate(string id)` - Identical validation logic
- `CalculateShipping()` - Similar shipping calculation patterns

### 2. Email Communication Duplication (Very Common)

**Location**: `Services/EmailService.cs`

- `BuildEmailTemplate()` - Template building for orders vs returns
- `FormatEmailSubject()` - Subject line formatting
- `SendEmail()` - Email sending mechanism
- `LogEmailActivity()` - Email audit logging

**Why this is realistic**: Email notifications are frequently copy-pasted between different business processes in e-commerce systems.

### 3. Audit and Logging Duplication (Extremely Common)

**Location**: `Services/AuditService.cs`

- `CreateAuditEntry()` - Audit entry creation pattern
- `ValidateAuditEntry()` - Entry validation logic
- `StoreAuditEntry()` - Storage mechanism
- `CheckComplianceRequirements()` - Compliance checking

**Why this is realistic**: Audit logging is one of the most frequently duplicated patterns in enterprise applications, often copy-pasted for different transaction types.

### 4. Inventory Management Duplication (Common)

**Location**: `Services/InventoryService.cs`

- `ValidateInventoryAvailability()` - Stock validation
- `UpdateInventoryLevel()` - Inventory updates (+ for returns, - for orders)
- `LogInventoryTransaction()` - Transaction logging

### 5. Cross-Cutting Concerns Duplication (Hidden)

**Throughout the application**:

- Payment processing with audit logging (in both processors)
- Status updates with logging
- Error handling patterns

## Expected Refactoring Outcomes

Learners should be able to:

1. **Identify** 5+ distinct duplicate code patterns
2. **Prioritize** which duplications to address first
3. **Choose** appropriate refactoring strategies for each pattern
4. **Implement** consolidated solutions using GitHub Copilot
5. **Verify** that functionality remains unchanged

## Real-World Application

These patterns directly translate to:

- **E-commerce platforms** (order/return/exchange processing)
- **Financial systems** (transaction processing with audit trails)
- **Healthcare systems** (patient record management with compliance)
- **Supply chain systems** (inventory and logistics management)

The enhanced codebase now provides a much more comprehensive and realistic foundation for learning code consolidation techniques with GitHub Copilot.
