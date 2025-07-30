---
# lab:
#     title: 'Exercise - Simplify complex conditionals using GitHub Copilot'
#     description: 'Learn how to refactor complex conditional logic in C# codebases using GitHub Copilot tools.'
---

# Simplify complex conditionals using GitHub Copilot

Conditional logic is often complex in business applications, especially in domains like e-commerce pricing and financial services. Deeply nested and complex conditionals can make code difficult to read, maintain, and test.

In this exercise, you use GitHub Copilot to analyze code that contains deeply nested conditional logic, refactor the code logic, and then test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of the code and explore options simplifying the logic. You use GitHub Copilot in Agent mode to refactor the code by extracting complex conditional logic into smaller, focused helper methods. By breaking down complex conditionals into smaller methods, you improve the maintainability and readability of the code.

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
        - ECommercePricingEngine\
            - Dependencies\
            - ECommercePricingDemo.cs
            - SecurityTest.cs
        - LoanApprovalWorkflow\
            - Dependencies\
            - LoanApprovalDemo.cs
            - SecurityTest.cs

## Exercise scenario

You're a software developer working for a consulting firm. New clients need help refactoring conditional logic to improve code maintainability and readability.

- E-commerce pricing demo: The first app is an E-commerce Pricing Engine that calculates dynamic pricing based on various business rules. Conditionals include membership levels, order values, coupon codes, product categories, and shipping rules.
- Loan approval demo: The second app is a Loan Approval Workflow that evaluates loan applications based on various factors. Conditionals include income, employment status, debt ratios, collateral, and credit history.

This exercise includes the following tasks:

Part 1: E-commerce pricing engine

1. Review the E-commerce pricing engine codebase.
1. Identify refactoring opportunities in the E-commerce pricing code.
1. Refactor the E-commerce pricing code with GitHub Copilot.
1. Test the refactored E-commerce pricing code.

Part 2: Loan approval workflow

1. Review the Loan Approval Workflow codebase.
1. Identify refactoring opportunities in the loan approval code.
1. Refactor the loan approval code with GitHub Copilot.
1. Test the refactored loan approval code.

## Part 1: E-commerce pricing engine

In this part of the exercise, you examine the E-commerce Pricing Engine sample app and use GitHub Copilot to refactor the conditional logic.

### Review the E-commerce pricing code

The first step in any refactoring effort is to ensure that you understand the existing codebase.

In this task, you'll open the E-commerce pricing engine project and use GitHub Copilot to help analyze the complex conditional logic.

Use the following steps to complete this task:

1. Ensure that you have the GHCopilotEx7LabApps folder open in Visual Studio Code.

    Refer to the **Before you start** section if you haven't downloaded the sample code projects.

1. Verify that the **ECommercePricingEngine** code project builds successfully.

    For example, in the SOLUTION EXPLORER view, right-click **ECommercePricingEngine**, and then select **Build**.

    You'll see warnings "Cannot convert null literal to non-nullable reference type." when you build the project, but there shouldn't be any errors. You can ignore the warnings for the purposes of this exercise.

1. Open the GitHub Copilot Chat view.

    If the Chat view isn't already open, you can open it by selecting the **Chat** icon at the top of the Visual Studio Code window, just to the right of the Search textbox.

1. In the Chat view, ensure that the chat mode is set to **Ask** and the model is set to **GPT-4.1**.

    Both settings are available in the bottom-left corner of the Chat view. The Ask mode allows you to interact with GitHub Copilot to ask questions and get explanations about the code. The GPT-4.1 model, which is included with the GitHub Copilot Free plan, provides advanced capabilities for understanding and generating code.

    You'll be using Agent mode later in this exercise to perform more complex tasks, but for now, Ask mode is sufficient for code analysis and explanations.

    > [!NOTE]
    > Some models are better suited for specific tasks than others. The model that you select can affect the responses generated by GitHub Copilot. After completing the lab exercise using the recommended settings, you may want to repeat the exercise using different models and compare the results.

1. In Visual Studio Code, open the **ECommercePricingDemo.cs** file.

    This file includes the following classes:

    - User: Represents a customer, with properties for membership level, purchase history, and special statuses (student, employee, corporate, etc.). Used to determine eligibility for various discounts and benefits.
    - Coupon: Represents a discount or shipping coupon, with properties for code, validity, type (percent or shipping), and value. Used in pricing calculations to apply additional discounts or free shipping.
    - Item: Represents a product in an order, with name, category, and price. Used to build up orders and calculate subtotals and category-specific discounts.
    - Order: Represents a customer’s order, containing a list of items, shipping region, coupon, event, payment method, and other order-specific flags. Provides methods to calculate subtotals, check for category presence, and determine order characteristics (e.g., high value, mixed categories).
    - PricingEngine: Contains the main logic for calculating the final price of an order. Applies discounts based on user status, order details, coupons, and category-specific rules. Handles security checks and ensures discounts and prices stay within safe bounds.
    - Program: The entry point. Creates test users, coupons, and orders, then runs a series of complex pricing scenarios using the above classes. Demonstrates how the pricing engine applies its logic in different situations.

    Each class models a real-world entity or process in an e-commerce pricing system, and they interact in the Program class to simulate and test pricing calculations. For example, User, Order, and Coupon instances are passed to PricingEngine.CalculateFinalPrice to compute and display the final price with all applicable discounts.

1. Locate the **PricingEngine** class, and then select the entire **CalculateFinalPrice** method.

    This method contains over 8 levels of nested conditional logic that evaluates membership levels, seasonal events, corporate accounts, subscription services, and various discount scenarios.

1. Right-click the selected code, and then select **Copilot** > **Explain**.

    GitHub Copilot will analyze the complex conditional logic and provide an explanation of what the code does, helping you understand the business rules before you investigate refactoring the code.

1. Take a few minutes to review GitHub Copilot's explanation.

    The explanation should highlight the main discount categories such as the hierarchical membership, coupon applications, bulk purchase incentives, and category-specific rules.

    Understanding these business domains is crucial for creating effective helper methods to simplify the logic.

1. In the Chat view, to get a deeper analysis of the calculation workflow, enter the following prompt:

    ```plaintext
    @workspace Explain the business logic flow in the CalculateFinalPrice method. What are the different discount paths and how do they interact with each other? What are the key business rules that govern pricing calculations?
    ```

    This analysis should help you understand how the different discount categories interact and what business rules are applied at each step of the pricing calculation.

1. Take a few minutes to review the discount paths and other information listed in GitHub Copilot's explanation.

    The explanation should cover the main discount paths, such as:

    - **Membership Discounts**: How different membership levels (Premium, Gold, Silver) apply discounts and how first-time buyers are treated.
    - **Coupon Processing**: How coupons are validated, applied, and how they interact with membership discounts.
    - **Bulk Discounts**: How volume-based discounts are applied based on item counts.

    Understanding the discount paths is essential for identifying refactoring opportunities.

### Identify refactoring opportunities in the E-commerce pricing code

GitHub Copilot is a great tool for analyzing complex code and identifying refactoring opportunities.

In this task, you'll use GitHub Copilot to identify specific refactoring opportunities and suggest helper methods that can simplify the complex nested conditions. You'll use the discount paths identified in the previous task to help construct a prompt for GitHub Copilot.

Use the following steps to complete this task:

1. Ensure you have the GitHub Copilot Chat view open with **Ask** mode and the **GPT-4.1** model selected.

1. Construct a prompt that asks GitHub Copilot to evaluate refactoring opportunities using the discount paths identified in the previous task.

    Consider the following items when constructing your prompt:

    - Focus on the **CalculateFinalPrice** method and its nested conditionals.
    - Ask GitHub Copilot to suggest helper methods that can extract the complex logic into more manageable, single-responsibility methods.
    - List the discount paths that should be considered for extraction.
    - Emphasize the importance of maintaining the business logic while simplifying the code structure.
    - It's often beneficial to have GitHub Copilot consider code interactions when analyzing your code. You can use the **@workspace** or **#codebase** tag to tell GitHub Copilot to include your codebase in its analysis.

1. Add the **ECommercePricingDemo.cs** file to the Chat context using drag-and-drop.

    Although ECommercePricingDemo.cs is already open in the Visual Studio Code editor, adding it to the Chat context encourages GitHub Copilot to analyze the code file, which can result in more accurate suggestions. Adding relevant files to the Chat context is a best practice when using GitHub Copilot, even when you include the **@workspace** or **#codebase** tags in your prompt.

1. Enter your constructed prompt in the Chat view:

    For example:

    ```plaintext
    @workspace Analyze the CalculateFinalPrice method and suggest refactoring opportunities. Suggest helper methods that could extract the nested conditional logic into more manageable, single-responsibility methods. Focus on membership discounts, coupon processing, bulk discounts, and shipping calculations. Maintain the business logic while simplifying the code structure.
    ```

    GitHub Copilot will analyze the method and suggest specific refactoring opportunities, identifying patterns in the nested conditionals that can be extracted into helper methods.

1. Review GitHub Copilot's suggestions.

    The response should identify several refactoring opportunities such as:

    - **ApplyMembershipDiscount**: Extract membership-level discount logic
    - **ApplyCouponDiscount**: Handle coupon validation and application
    - **ApplyBulkDiscount**: Manage volume-based discounts
    - **CalculateShippingCost**: Separate shipping cost calculations

1. To generate specific guidance on how to implement these helper methods, enter the following prompt:

    ```plaintext
    For each suggested helper method, explain what specific conditional logic should be extracted and how it would simplify the main method. Include the method signatures and return types that would be most appropriate.
    ```

1. Take a minute to review GitHub Copilot's detailed refactoring plan.

    The response should outline the specific logic to be extracted for each helper method, along with suggested method signatures and return types. Review the plan to ensure it aligns with the business logic and maintains the intended functionality.

### Refactor the E-commerce pricing code with GitHub Copilot Agent

GitHub Copilot has three modes, **Ask**, **Edit**, and **Agent**. When running in Agent mode, GitHub Copilot works as an autonomous AI agent.

In Agent mode:

- Your prompt specifies the task to be performed, and GitHub Copilot determines the best way to accomplish that task.
- GitHub Copilot reviews the codebase to determine the relevant files and context.
- GitHub Copilot uses the Chat view to inform you of its plans and to keep you informed as changes are being made. It may also provide explanations or justifications for the changes being made.
- GitHub Copilot can invoke tools to accomplish the task you requested.
- GitHub Copilot may pause during the task and ask you for assistance or clarification. Monitor the chat and respond to these prompts to assist the autonomous agent.
- GitHub Copilot updates your code file in the editor. Once the task is complete, you can review the changes made by GitHub Copilot before applying them to your codebase (individually or collectively).

In this section of the exercise, you'll use GitHub Copilot Agent to refactor the code by creating helper methods and replacing the nested conditional logic with method calls. You'll create helper methods for each identified area of complexity, starting with the membership discount logic, then moving on to coupon processing, bulk discounts, and finally shipping calculations. This approach follows the Single Responsibility Principle and makes the code more maintainable. Refactoring the complex conditional logic makes it more modular, maintainable, and easier to read.

Use the following steps to complete this task:

1. Ensure that the GitHub Copilot Chat view is open in Visual Studio Code.

1. In the chat view, select the **Agent** mode.

1. Take a minute to plan your code refactoring tasks.

    You need to write one or more tasks that can be assigned to GitHub Copilot Agent. These tasks will be used to refactor the **CalculateFinalPrice** method by extracting complex conditional logic into smaller, focused helper methods. Each helper method will handle a specific aspect of the pricing calculation, making the main method much more readable and maintainable.

    To keep the refactoring process manageable, you can construct a series of tasks that focus on one area of complexity at a time.

    The four main areas of complexity identified by GitHub Copilot in the previous task (the example response) are:

    - **Membership Discounts**: Extract the membership-level discount logic into a helper method named **ApplyMembershipDiscount**.
    - **Coupon Processing**: Extract the coupon validation and application logic into a helper method named **ApplyCouponDiscount**.
    - **Bulk Discounts**: Extract the bulk discount calculation logic into a helper method named **ApplyBulkDiscount**.
    - **Shipping Calculations**: Extract the shipping cost calculation logic into a helper method named **CalculateShippingCost**.

    Each task that you create should include the following elements:

    - Natural language text that provides high-level context for the task. For example, a description of the code that's being refactored and the area of complexity that's being extracted into a helper method.
    - Specific details about the helper method that's being created. For example, the method name, parameters, and return type.

    For example, the first refactoring task could be similar to the following:

    ```plaintext
    Refactor the selected CalculateFinalPrice method by extracting the membership discount logic into a new helper method called ApplyMembershipDiscount. This method should take User, Order, and current discount percentage as parameters and return the updated discount percentage with a list of applied discounts.
    ```

    This task prompt clearly states the action to be taken (refactoring the method) and specifies the new helper method to be created, along with its parameters and return type.

1. Select the entire **CalculateFinalPrice** method.

1. In the Chat view, submit a refactoring task to extract the membership discount logic:

    For example:

    ```plaintext
    Refactor the selected CalculateFinalPrice method by extracting the membership discount logic into a new helper method called ApplyMembershipDiscount. This method should take User, Order, and current discount percentage as parameters and return the updated discount percentage with a list of applied discounts.
    ```

1. Review the suggested updates in the Visual Studio Code editor.

    The suggested refactoring should create a new **ApplyMembershipDiscount** method that handles all the membership-level logic (Premium, Gold, Silver, and first-time buyer discounts) and simplifies the main method.

1. To accept the changes, select **Keep**.

    You should always review the changes suggested by GitHub Copilot before accepting them. This ensures that the updates align with your intended business logic, app functionality, and coding standards.

    You can reject the suggested changes if they don't meet your expectations. You can also accept a subset of the changes and reject others.

    If you accept GitHub Copilot's suggested updates, and then realize that the suggestions introduced issues that are difficult to resolve moving forward, you can revert the changes by selecting **Undo Last Request** in the Chat view or by using Visual Studio Code's undo functionality.

1. In the Chat view, submit a refactoring task to extract the coupon logic:

    For example:

    ```plaintext
    Now extract the coupon validation and application logic into a helper method called ApplyCouponDiscount. This method should handle coupon validation, membership-enhanced coupons, and both percentage and shipping coupons.
    ```

1. Review the suggested updates in the Visual Studio Code editor.

    This refactoring should extract all coupon-related conditional logic into a separate method, making the coupon processing logic reusable and easier to test.

1. To accept the changes, select **Keep**.

1. In the Chat view, submit a refactoring task to extract the bulk discount logic:

    ```plaintext
    Extract the bulk purchase logic into a helper method called ApplyBulkDiscount that handles volume-based discounts based on item count.
    ```

1. Review the suggested updates and then select **Keep** to accept the changes.

    This refactoring should create a new method that encapsulates the bulk discount logic.

1. Finally, submit a refactoring task to extract the shipping calculation logic:

    ```plaintext
    Create a CalculateShippingCost helper method that handles shipping cost calculations based on order properties and user membership level.
    ```

1. Review the suggested updates and then select **Keep** to accept the changes.

1. Take a minute to verify that the main `CalculateFinalPrice` method is now much shorter and more readable.

    The refactored main method should primarily consist of method calls to the helper functions, making the business logic flow much clearer and easier to understand.

### Test the refactored E-commerce pricing code

Testing is crucial to ensure that your refactoring doesn't change the business logic behavior. You'll run the code with various scenarios to verify that the calculations remain consistent.

Use the following steps to complete this task:

1. Open Visual Studio Code's **TERMINAL** view.

    You can open the built-in terminal by selecting the **Terminal** menu and then selecting **New Terminal**.

1. In the TERMINAL view, navigate to the ECommercePricingEngine project directory.

1. Build the project to ensure there are no compilation errors:

    ```powershell
    dotnet build
    ```

    If there are any compilation errors, review the refactored code and fix any issues. GitHub Copilot can help resolve compilation problems if needed.

1. Run the application to test the refactored pricing logic:

    ```powershell
    dotnet run
    ```

    The application should execute without errors and display pricing calculations for various test scenarios.

1. In the Chat view, enter the following prompt to create additional test scenarios:

    ```plaintext
    Create test scenarios in the Main method that verify the refactored pricing logic works correctly. Include tests for different membership levels, coupon types, bulk orders, and edge cases. Each test should display the scenario description and final pricing results.
    ```

    This will help you create comprehensive test cases that validate your refactoring work.

1. Apply the suggested test scenarios and run the application again to verify the results.

    Compare the outputs before and after refactoring to ensure the business logic remains unchanged. The refactored code should produce identical results while being much more maintainable and readable.

## Part 2: Loan approval workflow

In this section, you examine the Loan Approval Workflow sample app and use GitHub Copilot to refactor the conditional logic.

### Task 1: Review the loan approval code

The loan approval workflow contains complex financial logic that evaluates multiple risk factors. You'll analyze this code to understand the decision-making process before refactoring it.

Use the following steps to complete this task:

1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

1. Navigate to the project folder and select the **LoanApprovalWorkflow** folder.

    The LoanApprovalWorkflow folder is located at: `DownloadableCodeProjects\standalone-lab-projects\simplify-compex-conditionals\LoanApprovalWorkflow`

1. In the EXPLORER view, open the **LoanApprovalDemo.cs** file.

    This file contains the complex loan approval logic that evaluates credit scores, income verification, employment stability, debt ratios, and various risk factors.

1. Locate and select the `Evaluate` method in the `LoanEvaluator` class.

    This method contains 8+ levels of nested conditionals that assess loan eligibility based on credit scores, employment status, debt-to-income ratios, loan-to-value ratios, and government program eligibility.

1. Right-click the selected code and select **Copilot** > **Explain This**.

    GitHub Copilot will analyze the loan approval logic and explain how the method evaluates different risk factors to make approval decisions.

1. Review the explanation to understand the loan approval criteria.

    The explanation should cover the main evaluation areas: credit score tiers, income verification requirements, employment stability checks, debt service coverage analysis, loan-to-value assessments, and government program eligibility (FHA/VA loans).

1. In the Chat view, enter the following prompt to get a deeper analysis:

    ```plaintext
    @workspace Explain the business logic flow in the LoanEvaluator.Evaluate method. What are the different approval paths and what risk factors determine loan terms like interest rates and approved amounts?
    ```

    This will help you understand the financial decision-making process that the nested conditionals implement.

### Task 2: Identify refactoring opportunities in the loan approval code

You'll analyze the complex conditional structure to identify specific areas where helper methods can improve code organization and readability.

Use the following steps to complete this task:

1. Add the **LoanApprovalDemo.cs** file to the Chat context using drag-and-drop.

1. Enter the following prompt in the Chat view:

    ```plaintext
    Analyze the Evaluate method and identify refactoring opportunities. The method has deeply nested conditionals that check credit scores, employment, debt ratios, and loan terms. Suggest helper methods that could extract this logic into more manageable, single-purpose methods.
    ```

    GitHub Copilot will identify patterns in the nested conditionals and suggest specific helper methods for different aspects of the loan evaluation process.

1. Review the suggested refactoring opportunities.

    The response should identify several helper methods such as:
    - **CheckCreditScore**: Evaluate credit score tiers and set base approval criteria
    - **VerifyIncomeAndEmployment**: Assess income stability and employment history
    - **CalculateFinancialRatios**: Compute DTI, LTV, and payment-to-income ratios
    - **AssessGovernmentProgramEligibility**: Evaluate FHA/VA loan qualification
    - **DetermineLoanTerms**: Set interest rates and loan amounts based on risk factors
    - **EvaluateCreditHistory**: Analyze bankruptcy, foreclosure, and credit events

1. Enter the following prompt for more detailed guidance:

    ```plaintext
    For each suggested helper method, explain what specific conditions and calculations it should handle. Include the method signatures showing parameters and return types that would work best for this financial domain.
    ```

    This will provide you with a clear refactoring roadmap that separates the different aspects of loan evaluation into logical, testable components.

### Task 3: Refactor the loan approval code with GitHub Copilot

You'll implement the refactoring by creating helper methods that handle specific aspects of loan evaluation, making the code more modular and easier to maintain.

Use the following steps to complete this task:

1. Select the entire `Evaluate` method in the **LoanApprovalDemo.cs** file.

1. In the Chat view, enter the following prompt:

    ```plaintext
    Refactor the selected Evaluate method by extracting the credit score evaluation logic into a helper method called CheckCreditScore. This method should take an Applicant parameter and return a structure indicating the credit tier and base approval criteria.
    ```

    Starting with credit score evaluation makes sense because it's the primary factor that determines the approval path in the original logic.

1. Review and apply the suggested refactoring for the credit score method.

    The refactored method should clearly separate the different credit score tiers (740+, 620+, below 620) and their corresponding approval criteria.

1. Next, extract the income and employment verification logic:

    ```plaintext
    Extract the income verification and employment stability checks into a helper method called VerifyIncomeAndEmployment. This method should validate income against verification thresholds and assess employment stability based on type and duration.
    ```

1. Apply the suggested changes for the income/employment verification method.

    This method should handle the complex logic around income verification percentages, employment types (salaried, self-employed, contract), and employment duration requirements.

1. Continue with the financial ratio calculations:

    ```plaintext
    Create a CalculateFinancialRatios helper method that computes debt-to-income ratio, loan-to-value ratio, payment-to-income ratio, and liquidity ratio. Return these ratios in a structured format for evaluation.
    ```

1. Extract the government program eligibility logic:

    ```plaintext
    Extract the FHA and VA loan eligibility logic into a method called AssessGovernmentProgramEligibility. This method should handle first-time homebuyer and veteran status, along with program-specific debt-to-income allowances.
    ```

1. Finally, create a method for determining loan terms:

    ```plaintext
    Create a DetermineLoanTerms helper method that sets interest rates and approved amounts based on the risk assessment results from the other helper methods. This should centralize the logic for setting loan terms based on various risk factors.
    ```

1. After implementing all helper methods, verify that the main `Evaluate` method is now much simpler and follows a clear logical flow.

    The refactored method should primarily coordinate calls to the helper methods, making the loan evaluation process much easier to understand and modify.

### Task 4: Test the refactored loan approval code

Testing the refactored loan approval logic is critical to ensure that financial decisions remain consistent and accurate after the refactoring.

Use the following steps to complete this task:

1. In the TERMINAL view, navigate to the LoanApprovalWorkflow project directory.

1. Build the project to check for compilation errors:

    ```powershell
    dotnet build
    ```

    Address any compilation errors that may have been introduced during refactoring. The helper methods should integrate seamlessly with the existing code structure.

1. Run the application to test the refactored loan approval logic:

    ```powershell
    dotnet run
    ```

    The application should execute and display loan decisions for various applicant profiles without errors.

1. In the Chat view, enter the following prompt to create comprehensive test scenarios:

    ```plaintext
    Create test scenarios in the Main method that validate the refactored loan approval logic. Include applicants with different credit scores, employment types, debt ratios, and special programs (FHA/VA). Each scenario should display the applicant profile and the resulting loan decision with interest rate and approved amount.
    ```

    This ensures that your refactoring preserves the financial decision-making logic while improving code maintainability.

1. Apply the suggested test scenarios and run the application to verify the results.

    The refactored code should produce identical loan decisions to the original implementation. Pay special attention to edge cases and boundary conditions in the financial calculations.

1. In the Chat view, create a final validation test:

    ```plaintext
    Create edge case tests for loan approval including: minimum credit scores, maximum debt ratios, bankruptcy recovery scenarios, and high loan-to-value ratios. Verify that the helper methods handle boundary conditions correctly.
    ```

    Edge case testing is particularly important in financial applications where boundary conditions often represent critical business rules.

## Review and Explanation of Simplified Solutions

After completing both refactoring exercises, you should take time to understand the improvements you've achieved and the principles that guided the refactoring process.

Use the following steps to complete this review:

1. In the Chat view, enter the following prompt:

    ```plaintext
    Compare the original complex conditional structures with the refactored helper methods. Explain how the refactoring improves code maintainability, testability, and readability. What software engineering principles were applied during this refactoring process?
    ```

    This analysis will help you understand the broader impact of the refactoring beyond just simplifying nested conditions.

1. Review GitHub Copilot's analysis of the refactoring benefits.

    The response should highlight improvements in:
    - **Single Responsibility Principle**: Each helper method handles one specific aspect of the business logic
    - **Readability**: The main methods now read like a high-level business process
    - **Testability**: Individual helper methods can be unit tested in isolation
    - **Maintainability**: Changes to specific business rules are localized to relevant helper methods
    - **Reusability**: Helper methods can potentially be reused in other parts of the application

1. Enter the following prompt to understand the guard clause benefits:

    ```plaintext
    Explain how using early returns (guard clauses) in the helper methods improves the code structure compared to deeply nested if-else statements. What are the cognitive benefits for developers reading and maintaining this code?
    ```

    Understanding guard clauses helps you apply this pattern in future refactoring work.

1. Create a summary of lessons learned:

    ```plaintext
    Summarize the key refactoring techniques used in both exercises. What patterns can be applied to other complex conditional logic in real-world applications?
    ```

    This summary will help you apply these refactoring techniques to your own projects.

## Summary and Key Takeaways

In this exercise, you've successfully used GitHub Copilot to refactor two complex C# codebases with deeply nested conditional logic. The key takeaways from this exercise include:

- **Complex conditional logic is common** in real-world business applications, particularly in domains like e-commerce pricing and financial services where multiple business rules interact.

- **Refactoring improves maintainability** by breaking down complex logic into smaller, focused methods that are easier to understand, test, and modify.

- **GitHub Copilot accelerates refactoring** by analyzing existing code, suggesting appropriate helper methods, and generating refactored implementations that preserve business logic while improving code structure.

- **The Single Responsibility Principle** guides effective refactoring by ensuring each method handles one specific aspect of the business logic.

- **Guard clauses and early returns** help flatten deeply nested conditional structures, making code more readable and reducing cognitive load for developers.

- **Comprehensive testing** is essential when refactoring complex business logic to ensure that the behavior remains consistent while the internal structure improves.

- **Modular design** makes code more maintainable by isolating different business concerns into separate methods that can be tested and modified independently.

These refactoring techniques and principles can be applied to any codebase with complex conditional logic, helping you create more maintainable and robust applications.
