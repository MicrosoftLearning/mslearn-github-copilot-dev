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
            - Output-ECommercePricingEngine.txt
            - SecurityTest.cs
        - LoanApprovalWorkflow\
            - Dependencies\
            - LoanApprovalDemo.cs
            - Output-LoanApprovalWorkflow.txt
            - SecurityTest.cs

## Exercise scenario

You're a software developer working for a consulting firm. New clients need help refactoring complex conditional logic to improve code maintainability and readability. You're assigned to the following apps:

- E-commerce pricing engine: The first app is an E-commerce Pricing Engine that calculates dynamic pricing based on various business rules. Conditionals include membership levels, order values, coupon codes, product categories, and shipping rules.
- Loan approval workflow: The second app is a Loan Approval Workflow that evaluates loan applications based on various factors. Conditionals include income, employment status, debt ratios, collateral, and credit history.

This exercise includes two parts, a guided exercise that teaches you on how to refactor complex conditional logic using GitHub Copilot, and an optional "if time permits" exercise that provides an opportunity to demonstrate what you learned in a controlled environment. In the first part, you're guided through the process of refactoring complex conditional logic in the E-commerce pricing engine demo app.

The tasks in this exercise include:

1. Review the E-commerce pricing engine codebase.
1. Identify refactoring opportunities in the E-commerce pricing code.
1. Refactor the E-commerce pricing code with GitHub Copilot.
1. Test the refactored E-commerce pricing code.
1. (OPTIONAL) Refactor the complex conditional logic in the loan approval workflow demo app.

## Refactor complex conditionals using GitHub Copilot tools

GitHub Copilot provides tools that can help you refactor complex conditional logic in your code. These tools include:

- Code explanations: Copilot can explain the purpose and functionality of specific code blocks, helping you understand the existing logic.
- Code suggestions: Copilot can suggest code snippets to replace complex conditionals with simpler, more maintainable code.
- Code generation: Copilot can generate new code based on your descriptions, allowing you to implement refactoring changes quickly.

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

    These settings are available in the bottom-left corner of the Chat view. GitHub Copilot's **Ask** mode is used to ask general coding questions and to generate code related explanations. The **GPT-4.1** model, which is included with the GitHub Copilot Free plan, provides advanced capabilities for understanding and generating code.

    You'll be using Agent mode later in this exercise, but for now you'll use Ask mode for code analysis and explanations.

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

1. Take a minute to scroll through the conditional logic in the **CalculateFinalPrice** method.

    The method is complex and difficult to read, with multiple nested conditionals that handle different discount scenarios. The complexity arises from the various business rules that need to be applied based on user status, order details, and coupon codes.

1. Right-click the selected code, and then select **Copilot** > **Explain**.

    GitHub Copilot will analyze the complex conditional logic and provide an explanation of what the code does, helping you understand the business rules before you investigate refactoring the code.

1. Take a few minutes to review GitHub Copilot's explanation.

    The explanation should highlight the main discount categories such as the hierarchical membership, coupon applications, bulk purchase incentives, and category-specific rules.

    Understanding these business domains is crucial for creating effective helper methods that help simplify the logic.

1. In the Chat view, to get a deeper analysis of the calculation process, enter the following prompt:

    ```plaintext
    @workspace Explain the business logic flow in the CalculateFinalPrice method. What are the different discount paths and how do they interact with each other? What are the key business rules that govern pricing calculations?
    ```

    This analysis should help you understand how the different discount categories interact and what business rules are applied at each step of the pricing calculation.

1. Take a few minutes to review the discount paths and other information listed in GitHub Copilot's explanation.

    The explanation should cover the main discount paths, such as:

    - **Membership Discounts**: How different membership levels (Premium, Gold, Silver) apply discounts and how first-time buyers are treated.
    - **Coupon Discounts**: How coupons are validated, applied, and how they interact with membership discounts.
    - **Bulk Discounts**: How volume-based discounts are applied based on item counts.

1. Run the ECommercePricingEngine project and review the output.

    If you have the ECommercePricingDemo.cs file open in the Visual Studio Code editor, you can run the project by selecting the run button (Run project associated with this file) that's located above the top-right corner of the editor pane. To run the project from the SOLUTION EXPLORER view, right-click **ECommercePricingEngine**, select **Debug**, and then select **Start New Instance**.

    The output should show the final prices calculated for various orders based on the complex conditional logic in the **CalculateFinalPrice** method.

    > [!NOTE]
    > A copy of the output can be found in the **Output-ECommercePricingEngine.txt** file that's included in the ECommercePricingEngine folder. You can create your own output file if you want to compare results or if you modify sample data. When you reach the end of this exercise, you'll use the output file to ensure that your refactored code produces the same results as the original code.

### Identify refactoring opportunities in the E-commerce pricing code

GitHub Copilot is a great tool for analyzing complex code and identifying refactoring opportunities.

In this task, you'll use GitHub Copilot to identify specific refactoring opportunities and suggest helper methods that can simplify the complex nested conditions. You'll use the discount paths identified in the previous task to help construct a prompt for GitHub Copilot.

Use the following steps to complete this task:

1. Ensure you have the GitHub Copilot Chat view open with **Ask** mode and the **GPT-4.1** model selected.

1. Add the **ECommercePricingDemo.cs** file to the Chat context using drag-and-drop operation.

    Although ECommercePricingDemo.cs is already open in the Visual Studio Code editor, adding it to the Chat context encourages GitHub Copilot to analyze the entire code file, which can result in more accurate suggestions. Adding relevant files to the Chat context is a best practice when using GitHub Copilot, even when you include the **@workspace** or **#codebase** tags in your prompt.

1. Submit a prompt that asks GitHub Copilot to identify refactoring opportunities that improve code modularity related to the main discount paths.

    Consider the following items when constructing your prompt:

    - Tell GitHub Copilot to focus on the **CalculateFinalPrice** method and its nested conditionals.
    - Ask GitHub Copilot to suggest options that move complex logic into more manageable, single-responsibility helper methods.
    - List the main discount paths that should be considered for extraction into helper methods.
    - Emphasize the importance of maintaining the business logic while simplifying the code structure.

    It's often beneficial to have GitHub Copilot consider code interactions when analyzing your code. You can use the **@workspace** or **#codebase** tag to tell GitHub Copilot to include your entire codebase in its analysis.

    For example:

    ```plaintext
    @workspace Analyze the CalculateFinalPrice method and suggest refactoring opportunities. Include options that move the nested conditional logic into more manageable, single-responsibility helper methods. Focus on the main discount paths: membership discounts, coupon discounts, and bulk discounts. Maintain the business logic while simplifying the code structure.
    ```

    GitHub Copilot will analyze the method and suggest specific refactoring opportunities, identifying patterns in the nested conditionals that can be extracted into helper methods.

1. Take a couple minutes to review GitHub Copilot's suggestions.

    The response should identify opportunities that improve code manageability and modularity. For example:

    - Extract membership-level discount logic to create the **CalculateMembershipDiscount** helper method.
    - Extract coupon validation and application logic to create the **CalculateCouponDiscount** helper method.
    - Extract volume-based discount logic to create the **CalculateBulkDiscount** helper method.

1. Submit a prompt that asks GitHub Copilot how to simplify complex conditional logic and reduce nesting levels in the CalculateFinalPrice method.

    For example:

    ```plaintext
    How can I simplify the complex conditional logic and reduce nesting levels in the CalculateFinalPrice method? For example, the method has multiple nested conditionals that apply different membership discounts based on user status and order details. What are some best practices for reducing nesting levels and improving readability? Explain the benefit of each approach when applied to the CalculateFinalPrice method.
    ```

1. Take a minute to review GitHub Copilot's suggestions for simplifying the conditional logic and reducing nesting levels.

    GitHub Copilot should provide a list of suggestions for simplifying the conditional logic and reducing nesting levels. For example, it might suggest:

    - Use early returns or guard clauses to flatten logic, reduce indentation, and clarify flow.
    - Use local helper functions to remove duplication and clarify intent.
    - Use switch expressions or pattern matching to make membership logic explicit and maintainable.
    - Use smaller methods per membership to modularize logic and make it easier to test and update.
    - Use Boolean variables for conditions to improve readability of complex checks.

> [!NOTE]
> You'll use the results of this analysis to help construct the task assigned to GitHub Copilot Agent in the next section of the exercise.

### Refactor the E-commerce pricing code with GitHub Copilot Agent

GitHub Copilot has three modes, **Ask**, **Edit**, and **Agent**. When running in Agent mode, GitHub Copilot works as an autonomous AI agent.

In Agent mode:

- Your prompt specifies the task assigned to GitHub Copilot Agent.
- GitHub Copilot uses the task information and your codebase to identify the relevant code files and establish the context for the task.
- GitHub Copilot formulates a process that it can use to accomplish the task. The agent uses an iterative approach and code reviews to help ensure the task is completed successfully.
- GitHub Copilot uses the Chat view to keep you informed as it works on the assigned task. It may also provide explanations or justifications for the changes being made.
- GitHub Copilot can invoke tools to help it accomplish the task or to verify that code changes are working correctly.
- GitHub Copilot may pause during the task and ask you for assistance or clarification. It's important to monitor the chat and respond when prompted to assist the autonomous agent.
- GitHub Copilot updates your code file in the Visual Studio Code editor. Once the task is complete, you should review the changes made by GitHub Copilot before applying them to your codebase (individually or collectively).

In this section of the exercise, you'll use GitHub Copilot Agent to refactor the PricingEngine class and simplify the complex conditional logic in the CalculateFinalPrice method. The task that you assign to GitHub Copilot Agent will be based on the suggestions provided by GitHub Copilot during your code analysis (the previous task).

Use the following steps to complete this task:

1. Ensure that the GitHub Copilot Chat view is open in Visual Studio Code.

1. In the chat view, select the **Agent** mode.

    The **Set Mode** dropdown is located in the bottom-left corner of the Chat view. When you select **Agent**, GitHub Copilot will switch to Agent mode, which allows it to autonomously work on tasks that you assign.

1. Take a minute to identify the requirements for the task that you'll assign to GitHub Copilot Agent.

    You need to write a task that explains your goal and describes the steps that GitHub Copilot Agent can use to achieve that goal.

    In this case, your goal includes the following items:

    - Simplify the complex conditional logic and reduce nesting levels.
    - Improve code maintainability and readability.
    - Preserve existing functionality and ensure that the refactored code produces the same output as the original code.

    In the previous task, you asked GitHub Copilot to analyze the code and identify refactoring opportunities. Your generated two sets of suggestions, one for extracting complex logic into helper methods and another for simplifying the conditional logic and reducing nesting levels.

    For example, the refactoring opportunities identified in the previous task may include:

    - Extract membership-level discount logic to create the **CalculateMembershipDiscount** helper method.
    - Extract coupon validation and application logic to create the **CalculateCouponDiscount** helper method.
    - Extract volume-based discount logic to create the **CalculateBulkDiscount** helper method.
    - Use early returns or guard clauses to flatten logic, reduce indentation, and clarify flow.
    - Use local helper functions to remove duplication and clarify intent.
    - Use switch expressions or pattern matching to make membership logic explicit and maintainable.
    - Use smaller methods per membership to modularize logic and make it easier to test and update.
    - Use Boolean variables for conditions to improve readability of complex checks.

    You can use your goal and the suggestions that you generated to construct a detailed task for GitHub Copilot Agent. The task you create should implement the following organizational structure:

    1. Goal statement: Explain the overall goal of the task.
    1. Major code modularity updates: Identify any large code sections that can be extracted to improve modularity.
    1. Small or detailed code improvements: Describe the specific approaches to reduce nesting levels and improve readability.
    1. Outcome: Describe the expected outcome of the refactoring process.

1. Construct a task that meets your requirements.

    Your task should include your goal and the suggested refactoring opportunities arranged using the suggested organizational structure.

    For example:

    ```plaintext
    I need to simplify the complex conditional logic and reduce nesting levels in the CalculateFinalPrice method. The main areas of complexity are associated with membership discounts, coupon processing, and bulk discounts. Each of these areas could be moved into separate helper methods that are called from CalculateFinalPrice. There are several options to reduce nesting levels in the helper methods. Use early returns or guard clauses to flatten logic, reduce indentation, and clarify flow. Use local helper functions to remove duplication and clarify intent. Use switch expressions or pattern matching to make membership logic explicit and maintainable. Use smaller methods per membership to modularize logic and make it easier to test and update. Use Boolean variables for conditions to improve readability of complex checks. The goal is to improve code maintainability and readability while preserving the existing functionality and generated output.
    ```

    This task uses natural language text to describe the goal, major restructuring opportunities, and specific approaches to reduce nesting levels and improve readability. The task also describes the expected outcome of the refactoring process.

1. Use the Chat view to assign your task to GitHub Copilot Agent.

    GitHub Copilot Agent will evaluate the task and the codebase to develop an approach for refactoring CalculateFinalPrice and extracting the membership discount logic into a new helper method. The agent will test the code updates at various stages to ensure that the refactoring is successful and that the business logic remains intact.

    After submitting the task, GitHub Copilot Agent will start working on the task. You can monitor its progress in the Chat view.

    > [!NOTE]
    > If you need to make changes to the task, you can edit the text in the Chat view and resubmit it. GitHub Copilot Agent will re-evaluate the task and continue working on it.

1. Monitor the Chat view as GitHub Copilot Agent works on the task.

    The agent will provide updates on its progress, including any challenges it encounters and how it plans to address them. It may also ask for clarification or additional information if needed. If necessary, provide assistance by responding to the agent's prompts in the Chat view.

1. Once the refactoring task is complete, review the suggested updates in the Visual Studio Code editor.

    Always review the changes suggested by GitHub Copilot before accepting them. Verify that the updates align with your intended business logic, app functionality, and coding standards.

    The refactored code should include a greatly simplified **CalculateFinalPrice** method that primarily consists of method calls to helper methods that manage membership discounts, coupon discounts, and bulk discounts. The complexity of the original method should be significantly reduced, making it easier to read and maintain.

    The membership discounts helper method should also be refactored to handle the complex logic around membership levels, with an additional helper method for each level (Premium, Gold, Silver, and first-time buyers).

1. In the Chat view, to accept all edits, select **Keep**.

    You can reject the suggested changes if they don't meet your expectations, or you can accept a subset of the changes and reject others. Working with GitHub Copilot to refactor your code is often an iterative process that includes refining your prompts to achieve the intended results.

    If you accept GitHub Copilot's suggested updates, and then realize that the suggestions introduced issues that may be difficult to resolve moving forward, you can revert the changes by selecting **Undo Last Request** in the Chat view, or by using Visual Studio Code's undo functionality.

1. Ask GitHub Copilot to explain the code refactoring updates that it implemented.

    For example:

    ```plaintext
    Explain the refactoring updates that you implemented to simplify the complex conditional logic, reduce nesting levels, and improve code maintainability and readability.
    ```

    GitHub Copilot will provide an explanation of the changes it made, including how the complex conditional logic was simplified and how the code structure was improved.

    For example:

    ```md

    Here’s a summary of the refactoring updates made to simplify the complex conditional logic, reduce nesting, and improve maintainability and readability in the `CalculateFinalPrice` method and related logic:
    
    ### 1. Extraction of Complex Logic into Helper Methods
    - The main areas of complexity—membership discounts, coupon processing, and bulk discounts—were each moved into their own private static helper methods:
      - `ApplyMembershipDiscounts`
      - `ApplyCouponDiscounts`
      - `ApplyBulkDiscounts`
    - This keeps the main `CalculateFinalPrice` method concise and focused on orchestration, not business rules.
    
    ### 2. Flattened Logic with Guard Clauses
    - Deeply nested `if` statements were replaced with early returns (guard clauses) in the helper methods.
    - For example, in `ApplyPremiumDiscounts`, after each check, if a condition is not met, the method returns immediately, reducing indentation and making the flow clear.
    
    ### 3. Use of Switch Expressions and Boolean Variables
    - Membership logic is handled with a `switch` statement in `ApplyMembershipDiscounts`, making it explicit and easy to extend.
    - Boolean variables (e.g., `isBlackFridayOrCyberMonday`, `isAltPayment`, `isPremiumPayment`) are used to clarify intent and improve readability of complex checks.
    
    ### 4. Modularization by Membership Type
    - Each membership type (Premium, Gold, Silver, FirstTimeBuyer) has its own method, making the logic for each tier isolated and easier to test or update.
    
    ### 5. Removal of Duplication and Improved Intent
    - Local helper functions and clear method names remove duplication and clarify the purpose of each block.
    - The same business rules are preserved, but the code is now much easier to follow.
    
    ### 6. Improved Maintainability
    - The code is now easier to extend (e.g., adding a new membership tier or discount rule).
    - Each helper method is small and focused, making unit testing and debugging simpler.
    
    **Result:**  
    The refactored code is flatter, more modular, and much easier to read and maintain, while preserving all original business logic and security checks.
    
    ```

1. Take a minute to review the refactored code in the PricingEngine class.

### Test the refactored E-commerce pricing code

Testing is crucial to ensure that your refactoring doesn't change the business logic behavior. You'll run the code with various scenarios to verify that the calculations remain consistent.

Use the following steps to complete this task:

1. Open Visual Studio Code's **TERMINAL** view.

    You can open the built-in terminal by selecting the **Terminal** menu and then selecting **New Terminal**.

1. In the TERMINAL view, navigate to the ECommercePricingEngine project directory.

1. Build the project to ensure there are no compilation errors.

    ```powershell
    dotnet build
    ```

    If there are any compilation errors, review the refactored code and fix any issues. GitHub Copilot can help resolve compilation problems if needed.

1. Run the application to test the refactored pricing logic.

    ```powershell
    dotnet run
    ```

    The application should execute without errors and display pricing calculations for various test scenarios.

1. In the Chat view, enter the following prompt to create additional test scenarios.

    ```plaintext
    Create test scenarios in the Main method that verify the refactored pricing logic works correctly. Include tests for different membership levels, coupon types, bulk orders, and edge cases. Each test should display the scenario description and final pricing results.
    ```

    This will help you create comprehensive test cases that validate your refactoring work.

1. Apply the suggested test scenarios and run the application again to verify the results.

    Compare the outputs before and after refactoring to ensure the business logic remains unchanged. The refactored code should produce identical results while being much more maintainable and readable.

### (OPTIONAL) Simplify complex conditionals in the LoanApprovalWorkflow demo app

If time permits, simplify complex conditionals in the LoanApprovalWorkflow demo app using the same process that you used to simplify pricing logic in the Loan Approval Workflow sample app.

In this optional task, you'll apply the same techniques you used in the E-commerce pricing engine to refactor the complex conditional logic in the Loan Approval Workflow demo app. You'll analyze the loan approval code, identify refactoring opportunities, and then use GitHub Copilot Agent to extract complex conditionals into smaller, focused helper methods.

> [!IMPORTANT]
> The instructions for this task include the high-level process steps, but they don't include suggested prompts or detailed explanations. You can refer to the previous tasks in this exercise for examples of how to construct prompts and use GitHub Copilot effectively.

Use the following steps to complete this task:

1. Expand the **LoanApprovalWorkflow** project folder, open the **LoanApprovalDemo.cs** file, and then review the code.

1. Use GitHub Copilot to explain the **LoanApprovalDemo.cs** file, including the complex conditional logic in the `Evaluate` method of the `LoanEvaluator` class.

1. Run the LoanApprovalWorkflow project to test the initial loan approval logic and create a record of the output.

1. Use GitHub Copilot to identify code refactoring opportunities that simplify complex conditionals in the Evaluate method.

1. Use GitHub Copilot to explain the implementation of any helper methods used in the suggested refactoring.

1. Use GitHub Copilot Agent to refactor the `Evaluate` method by extracting complex conditional logic into smaller, focused helper methods.

    For example, you could have helper methods for credit score evaluation, income and employment verification, financial ratio calculations, government program eligibility, and loan terms determination.

1. Use GitHub Copilot Agent to simplify complex conditional logic and reduce nesting levels in the helper methods.

1. Test your refactored code and ensure that the loan approval demo application produces the same results as the original implementation.

## Summary

In this exercise, you learned how to use GitHub Copilot to simplify complex conditional logic in a codebase. You explored the E-commerce pricing engine and Loan Approval Workflow demo apps, identified refactoring opportunities, and used GitHub Copilot Agent to extract complex conditionals into smaller, focused helper methods. You also learned how to reduce nesting levels in the code to improve readability while maintaining the same business logic.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. If you made any changes, revert them as needed. If you're using a local PC as your lab environment, you can archive or delete the sample projects folder that you created for this exercise.
