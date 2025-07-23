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

### Step 1: Review the E-commerce pricing code

Open the ECommercePricingEngine.cs file. Use GitHub Copilot's 'Explain this code' feature to understand the logic. Identify the nested conditions related to membership level, order value, coupon codes, product categories, and shipping rules.

### Step 2: Identify refactoring opportunities in the E-commerce pricing code

Look for repeated patterns and deeply nested if-else blocks. Use Copilot to suggest helper methods such as `ApplyMembershipDiscount`, `ApplyCouponDiscount`, `ApplyBulkDiscount`, and `CalculateShippingCost`.

### Step 3: Refactor the E-commerce pricing code with GitHub Copilot

Use GitHub Copilot to generate and insert these helper methods. Replace the nested logic in the main function with calls to these methods. Ensure each method handles a single responsibility.

### Step 4: Test the refactored E-commerce pricing code

Run the refactored code and verify that the final price and shipping cost calculations match the original logic. Use unit tests or sample inputs to validate correctness.

## Part 2: Loan approval workflow

In this section, you will examine the Loan Approval Workflow code and use GitHub Copilot to refactor it.

### Step 1: Review the loan approval code

Open the LoanApprovalWorkflow.cs file. Use GitHub Copilot to explain the logic. Identify the conditional checks for credit score, income, employment, debt ratio, collateral, criminal record, and residency.

### Step 2: Identify refactoring opportunities in the loan approval code

Look for long chains of if-else statements. Use Copilot to suggest helper methods such as `CheckCreditScore`, `EvaluateIncome`, `AssessCollateral`, and `DetermineLoanOffer`.

### Step 3: Refactor the loan approval code with GitHub Copilot

Use GitHub Copilot to generate these helper methods. Replace the nested logic with a sequence of method calls. Ensure each method returns a clear result or decision object.

### Step 4: Test the Refactored Code

Run the refactored code and verify that loan decisions are consistent with the original logic. Use test cases to cover different applicant profiles.
Review and Explanation of Simplified Solutions

After refactoring, compare the original and simplified versions of both codebases. Note how the use of helper methods, early returns (guard clauses), and modular logic improves readability and maintainability. GitHub Copilot can assist by suggesting method names, generating boilerplate, and explaining logic.

## Summary and Key Takeaways

- Deeply nested conditionals are common in real-world applications.
- Refactoring improves clarity, testability, and reduces bugs.
- GitHub Copilot can accelerate the refactoring process by suggesting helper methods and explanations.
- Use guard clauses and modular design to flatten complex logic.
- Always validate refactored code with tests to ensure correctness.