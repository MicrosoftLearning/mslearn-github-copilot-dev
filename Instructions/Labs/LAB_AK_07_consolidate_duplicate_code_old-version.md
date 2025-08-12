<!-- ---
lab:
    title: 'Exercise - Consolidate duplicate code using GitHub Copilot'
    description: 'Learn how to consolidate code that implements duplicate logic (either duplicated code or code that's similar and used to implement the same business logic) across multiple files using GitHub Copilot tools.'
--- -->

# Consolidate duplicate code using GitHub Copilot

Duplicate code logic In this exercise, you used GitHub Copilot to identify and consolidate duplicate code patterns in an e-commerce application. You learned how to use Ask mode to systematically analyze complex codebases for subtle duplications, and Agent mode to implement comprehensive refactoring strategies across multiple files and architectural layers.s often introduced when different individuals or teams extend a code project with new features over several years. A rushed schedule, poor documentation, and a lack of proper code reviews can exacerbate the issue. In some cases, code from one section may be copied and pasted into another section to quickly implement a feature. Unfortunately, duplicated logic can evolve separately, implementing different variable names and control flow structures that mask the duplication. In the end, duplicated logic makes the code difficult to maintain, debug, and test.

In this exercise, you use GitHub Copilot to analyze code that contains duplicate logic, consolidate the duplicated code logic by extracting it into shared methods or functions, and then test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of the code and explore options for consolidating the logic. You use GitHub Copilot in Agent mode to refactor the code by combining duplicate logic into shared methods or functions. Consolidating duplicate code makes it easier to read, maintain, and test your code.

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

### Download sample code projects

Use the following steps to download the sample projects and open them in Visual Studio Code:

1. Open a browser window in your lab environment.

1. To download a zip file containing the sample app projects, open the following URL in your browser: [GitHub Copilot lab - develop code features](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx7LabApps.zip)

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
            - Configuration\
                - AppConfig.cs
            - Models\
                - Order.cs
                - Return.cs
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
            - REFACTORING_HINT.md
            - ReturnProcessor.cs

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help consolidating duplicate code logic. Your goal is to improve code maintainability while preserving the existing functionality. You're assigned to the following app:

- E-commerce orders and returns: This app is an E-commerce Orders and Returns system that processes customer orders and handles product returns. Duplicate code is present in order validation, shipping calculation, and error handling.

This exercise includes the following tasks:

1. Review the E-commerce orders and returns codebase.
1. Identify duplicate code using GitHub Copilot Chat (Ask Mode).
1. Consolidate duplicate logic using GitHub Copilot Chat (Agent Mode).
1. Test the refactored E-commerce orders and returns code.

### Review the E-commerce orders and returns codebase

The first step in any refactoring effort is to ensure that you understand the existing codebase and the types of duplicate code that need to be consolidated.

In this task, you'll open the E-commerce order and return processing project and analyze the various duplicate code patterns that exist across multiple files and architectural layers.

Use the following steps to complete this task:

1. Open the ECommerceOrderAndReturn project folder in Visual Studio Code.

    Take a moment to explore the project structure. Notice that the codebase has been enhanced to represent a more realistic e-commerce application with multiple architectural layers including Models, Services, Security, and Configuration.

1. Examine the main processing classes.

    Open **OrderProcessor.cs** and **ReturnProcessor.cs** side by side. These classes represent the core business logic for processing customer orders and product returns respectively.

    As you review the code, you'll notice that both classes have similar method signatures and processing patterns. This is the most obvious type of duplication, but there are additional, more subtle duplications throughout the codebase.

1. Review the Services layer.

    Navigate to the **Services** folder and examine **EmailService.cs**, **AuditService.cs**, and **InventoryService.cs**. These service classes contain duplicate logic patterns that are common in real-world e-commerce applications.

    Notice how each service has methods that follow similar patterns but are duplicated for different business processes (orders vs returns).

1. Run the application to understand its behavior.

    Open a terminal in Visual Studio Code and execute `dotnet run` from the **ECommerceOrderAndReturn** project directory.

    Observe the comprehensive console output that shows:
    - Initial inventory levels
    - Order processing with validation, shipping calculation, payment processing, inventory reservation, email notifications, and audit logging
    - Return processing with similar steps but adapted for returns
    - Updated inventory levels
    - Security validation tests with various invalid inputs

    The application runs 5 test scenarios to demonstrate both successful processing and security validation failures.

1. Identify the duplicate code patterns.

    From your code review and the application output, note the following types of duplication:

    **Core Business Logic Duplication**: Both OrderProcessor and ReturnProcessor have identical `Validate()` and similar `CalculateShipping()` methods.

    **Service Layer Duplication**:
    - EmailService has duplicate template building, formatting, and sending logic for orders and returns
    - AuditService has duplicate entry creation, validation, and storage logic
    - InventoryService has duplicate validation and transaction logging patterns

    **Cross-cutting Concerns**: Payment processing, status updates, and error handling patterns are repeated across both processors.

At this stage, it's important to understand the existing functionality before making changes. By running the code and reviewing the output, you establish a baseline that you can use to verify that your refactoring doesn't break existing functionality. The enhanced codebase demonstrates real-world duplicate code patterns that go beyond simple method duplication to include service-layer and architectural duplications commonly found in enterprise applications.

### Identify duplicate code using GitHub Copilot Chat (Ask Mode)

GitHub Copilot Chat's Ask Mode is excellent for analyzing complex codebases and identifying subtle duplication patterns that might not be immediately obvious. In Ask mode, Copilot acts as an intelligent code reviewer that can analyze multiple files simultaneously and identify both obvious and hidden duplications.

In this task, you'll use GitHub Copilot to systematically identify the various types of duplicate code patterns in the enhanced e-commerce application.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view and set it to Ask mode.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Ensure that the chat mode is set to **Ask** and you're using an appropriate model like **GPT-4o** or **GPT-4**.

1. Add the main processor files to the Chat context.

    Open **OrderProcessor.cs** and **ReturnProcessor.cs** in the editor. You can also drag these files directly into the Chat view to ensure GitHub Copilot has full context of both files.

1. Ask GitHub Copilot to identify the primary duplicate code patterns.

    Submit the following prompt to analyze the core duplication:

    ```text
    What duplicate code exists between OrderProcessor.cs and ReturnProcessor.cs? Please identify specific methods and logic that are duplicated between these classes.
    ```

    GitHub Copilot should identify the `Validate()` method duplication and the similar patterns in `CalculateShipping()` methods. It may also note the similar audit logging and error handling patterns.

1. Analyze service-layer duplications.

    Add the **EmailService.cs** file to the Chat context by dragging it into the chat or opening it in the editor. Then ask:

    ```text
    Analyze the EmailService class. What duplicate logic exists within this service for handling order confirmations versus return confirmations?
    ```

    GitHub Copilot should identify the duplicate template building, subject formatting, email sending, and logging methods that are used by both `SendOrderConfirmation` and `SendReturnConfirmation`.

1. Examine audit and inventory service duplications.

    Add **AuditService.cs** and **InventoryService.cs** to the context and ask:

    ```text
    In the AuditService and InventoryService classes, what methods contain duplicate logic patterns that could be consolidated?
    ```

    GitHub Copilot should identify patterns like audit entry creation/validation/storage in AuditService, and inventory validation/updating/logging in InventoryService.

1. Get a comprehensive duplication analysis.

    For a broader view, ask GitHub Copilot to analyze the entire codebase:

    ```text
    @workspace Analyze the entire ECommerceOrderAndReturn codebase and identify all forms of code duplication, including method-level, service-level, and architectural pattern duplications. Prioritize the duplications by impact and refactoring difficulty.
    ```

    This should provide a comprehensive analysis of all duplication patterns and suggest which ones should be addressed first.

1. Verify GitHub Copilot's analysis with manual code review.

    Cross-reference GitHub Copilot's findings with your own observations. Ensure that you understand not just what is duplicated, but why these patterns exist and how they should be consolidated while maintaining the business logic integrity.

GitHub Copilot's Ask mode is particularly powerful for identifying subtle duplications that go beyond simple copy-paste scenarios. It can recognize similar logical patterns, equivalent business rules implemented differently, and architectural duplications that span multiple files. The analysis from this task will inform the refactoring strategy you'll implement in the next section.

### Consolidate duplicate logic using GitHub Copilot Chat (Agent Mode)

GitHub Copilot's Agent Mode enables you to assign complex, multi-step refactoring tasks that span multiple files and architectural layers. The agent can autonomously create new files, modify existing code, and implement comprehensive refactoring strategies while keeping you informed of its progress.

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

1. Assign the first refactoring task for core business logic.

    Submit the following task to GitHub Copilot Agent:

    ```text
    Create a shared ValidationService class that consolidates the duplicate Validate() method logic from OrderProcessor and ReturnProcessor. The service should handle ID validation for both orders and returns while maintaining all existing security checks, business rules, and logging. Update both processor classes to use the new shared service and remove the duplicate private methods.
    ```

    Monitor the agent's progress in the Chat view. It should:
    - Create a new **ValidationService.cs** file in the Services folder
    - Extract the validation logic into a reusable method
    - Update both processor classes to use the new service
    - Remove the duplicate private validation methods

1. Review and accept the validation service changes.

    Examine the changes proposed by the agent. The new validation service should maintain all the existing validation logic while providing a single, reusable implementation. If the changes look correct, accept them.

1. Consolidate shipping calculation logic.

    Assign the next task for shipping calculations:

    ```text
    Create a shared ShippingCalculationService that consolidates the similar CalculateShipping() logic from OrderProcessor and ReturnProcessor. The service should handle both order shipping (with free shipping thresholds) and return shipping (with processing fees) while maintaining the different business rules for each type. Update both processor classes to use the new shared service.
    ```

    The agent should create a shipping service that handles both scenarios while preserving the different business rules for orders versus returns.

1. Refactor the EmailService duplications.

    Assign a task to consolidate the email service duplications:

    ```text
    Refactor the EmailService class to eliminate duplicate logic in the helper methods. Create a unified approach for template building, subject formatting, email sending, and activity logging that can handle both order and return confirmations. The public methods SendOrderConfirmation and SendReturnConfirmation should remain, but they should use shared private helper methods.
    ```

1. Consolidate AuditService duplications.

    Address the audit service duplications:

    ```text
    Refactor the AuditService class to consolidate the duplicate logic in LogOrderActivity and LogReturnActivity. Create shared helper methods for audit entry creation, validation, storage, and compliance checking. The public methods should remain but use common underlying logic.
    ```

1. Address InventoryService duplications.

    Handle the inventory service duplications:

    ```text
    Refactor the InventoryService class to eliminate duplicate logic between ReserveOrderInventory and RestoreReturnInventory. Create shared helper methods for inventory validation, level updates, and transaction logging while maintaining the different business logic for reservations versus restorations.
    ```

1. Perform incremental testing after each major change.

    After each refactoring phase, build and test the application:
    
    ```bash
    dotnet build
    dotnet run
    ```

    Verify that the output remains consistent with the original behavior. If any issues arise, work with GitHub Copilot to resolve them before proceeding to the next refactoring phase.

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
