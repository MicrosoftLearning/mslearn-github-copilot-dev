---
# lab:
#     title: 'Exercise - Simplify complex conditionals using GitHub Copilot'
#     description: 'Learn how to refactor complex conditional logic in C# codebases using GitHub Copilot tools.'
---

# Simplify complex conditionals using GitHub Copilot

This lab exercise guides you through examining and refactoring two complex C# codebases using GitHub Copilot tools: an E-commerce Pricing Engine and a Loan Approval Workflow. Both scenarios feature deeply nested conditional logic that mirrors real-world business rules. The E-commerce scenario focuses on dynamic pricing, discounts, and shipping rules, while the Loan Approval scenario models financial eligibility and fallback loan offers. You will use GitHub Copilot to analyze, explain, and simplify these conditionals into more maintainable structures.

This exercise should take approximately **30** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following: Git 2.48 or later, .NET SDK 9.0 or later, Visual Studio Code with the C# Dev Kit extension, and access to a GitHub account with GitHub Copilot enabled.

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

## Exercise scenario

You're a software development engineer working for a consulting agency. Your clients have complex C# codebases that require refactoring to improve maintainability and readability. The first codebase is an E-commerce Pricing Engine that calculates dynamic pricing based on various business rules, including membership levels, order values, coupon codes, product categories, and shipping rules. The second codebase is a Loan Approval Workflow that evaluates loan applications based on credit scores, income, employment status, debt ratios, collateral, criminal records, and residency.

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

In this section, you will examine the E-commerce Pricing Engine code and use GitHub Copilot to refactor it.

### Task 1: Review the E-commerce pricing code

The first step in any refactoring effort is to understand the existing codebase. You'll open the E-commerce pricing engine project and use GitHub Copilot to help analyze the complex conditional logic.

Use the following steps to complete this task:

1. Open Visual Studio Code in your lab environment.

1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

1. Navigate to the project folder and select the **ECommercePricingEngine** folder.

    The ECommercePricingEngine folder is located at: `DownloadableCodeProjects\standalone-lab-projects\simplify-compex-conditionals\ECommercePricingEngine`

1. In the EXPLORER view, expand the project structure and locate the **ECommercePricingDemo.cs** file.

1. Open the **ECommercePricingDemo.cs** file.

    This file contains the complex pricing logic that you'll be refactoring. Notice the deeply nested conditional statements in the `CalculateFinalPrice` method.

1. Select the entire `CalculateFinalPrice` method.

    This method contains over 8 levels of nested conditional logic that evaluates membership levels, seasonal events, corporate accounts, subscription services, and various discount scenarios.

1. Right-click the selected code and select **Copilot** > **Explain This**.

    GitHub Copilot will analyze the complex conditional logic and provide an explanation of what the code does, helping you understand the business rules before refactoring.

1. Take a minute to review GitHub Copilot's explanation.

    The explanation should highlight the main discount categories: membership-based discounts, seasonal promotions, coupon applications, bulk purchase incentives, and category-specific rules. Understanding these business domains is crucial for creating effective helper methods.

### Task 2: Identify refactoring opportunities in the E-commerce pricing code

Now that you understand the code structure, you'll use GitHub Copilot to identify specific refactoring opportunities and suggest helper methods that can simplify the complex nested conditions.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view.

1. Add the **ECommercePricingDemo.cs** file to the Chat context using drag-and-drop.

1. Enter the following prompt in the Chat view:

    ```plaintext
    @workspace Analyze the CalculateFinalPrice method and identify refactoring opportunities. Suggest helper methods that could extract the nested conditional logic into more manageable, single-responsibility methods. Focus on membership discounts, coupon processing, bulk discounts, and shipping calculations.
    ```

    GitHub Copilot will analyze the method and suggest specific refactoring opportunities, identifying patterns in the nested conditionals that can be extracted into helper methods.

1. Review GitHub Copilot's suggestions.

    The response should identify several refactoring opportunities such as:
    - **ApplyMembershipDiscount**: Extract membership-level discount logic
    - **ApplyCouponDiscount**: Handle coupon validation and application
    - **ApplyBulkDiscount**: Manage volume-based discounts
    - **CalculateShippingCost**: Separate shipping cost calculations
    - **ApplyCategorySpecificRules**: Handle category-based discount limitations

1. Enter the following prompt to get more specific guidance:

    ```plaintext
    For each suggested helper method, explain what specific conditional logic should be extracted and how it would simplify the main method. Include the method signatures and return types that would be most appropriate.
    ```

    This will provide you with a detailed refactoring plan that you can follow in the next task.

### Task 3: Refactor the E-commerce pricing code with GitHub Copilot

You'll now implement the refactoring by creating helper methods and replacing the nested conditional logic with method calls. This approach follows the Single Responsibility Principle and makes the code more maintainable.

Use the following steps to complete this task:

1. Select the entire `CalculateFinalPrice` method in the **ECommercePricingDemo.cs** file.

1. In the Chat view, enter the following prompt:

    ```plaintext
    Refactor the selected CalculateFinalPrice method by extracting the membership discount logic into a new helper method called ApplyMembershipDiscount. This method should take User, Order, and current discount percentage as parameters and return the updated discount percentage with a list of applied discounts.
    ```

    This prompt focuses on extracting one specific area of complexity first, making the refactoring more manageable.

1. Review the suggested refactoring and apply the changes to your code.

    The suggested refactoring should create a new `ApplyMembershipDiscount` method that handles all the membership-level logic (Premium, Gold, Silver, and first-time buyer discounts) and simplifies the main method.

1. Next, enter the following prompt to extract coupon logic:

    ```plaintext
    Now extract the coupon validation and application logic into a helper method called ApplyCouponDiscount. This method should handle coupon validation, membership-enhanced coupons, and both percentage and shipping coupons.
    ```

1. Apply the suggested changes for the coupon discount method.

    This refactoring should extract all coupon-related conditional logic into a separate method, making the coupon processing logic reusable and easier to test.

1. Continue with the bulk discount extraction:

    ```plaintext
    Extract the bulk purchase logic into a helper method called ApplyBulkDiscount that handles volume-based discounts based on item count.
    ```

1. Finally, extract the shipping calculation logic:

    ```plaintext
    Create a CalculateShippingCost helper method that handles shipping cost calculations based on order properties and user membership level.
    ```

1. After implementing all the helper methods, verify that the main `CalculateFinalPrice` method is now much shorter and more readable.

    The refactored main method should primarily consist of method calls to the helper functions, making the business logic flow much clearer and easier to understand.

### Task 4: Test the refactored E-commerce pricing code

Testing is crucial to ensure that your refactoring doesn't change the business logic behavior. You'll run the code with various scenarios to verify that the calculations remain consistent.

Use the following steps to complete this task:

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

In this section, you will examine the Loan Approval Workflow code and use GitHub Copilot to refactor it.

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
