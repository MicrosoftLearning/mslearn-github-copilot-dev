<!-- ---
lab:
    title: 'Exercise - Implement performance profiling using GitHub Copilot'
    description: 'Learn how to identify and address performance bottlenecks and code inefficiencies using GitHub Copilot tools.'
--- -->

# Implement performance profiling using GitHub Copilot

Performance profiling is a critical aspect of software development that helps identify and address performance bottlenecks and code inefficiencies.

In this exercise, you review an existing project that contains poor performing and inefficient code, analyze your options for improving code performance, refactor the code to address the identified issues, and test the refactored code to ensure code performance has improved while retaining functionality and readability. You use GitHub Copilot in Ask mode to gain an understanding of an existing code project and to explore options for refactoring the identified issues. You use GitHub Copilot in Agent mode to refactor the code and improve performance. You test the original and refactored code to measure the impact of your changes.

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

1. To download a zip file containing the sample app projects, open the following URL in your browser: [GitHub Copilot lab - implement performance profiling](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx10LabApps.zip)

    The zip file is named **GHCopilotEx10LabApps.zip**.

1. Extract the files from the **GHCopilotEx10LabApps.zip** file.

    For example:

    1. Navigate to the downloads folder in your lab environment.

    1. Right-click *GHCopilotEx10LabApps.zip*, and then select **Extract all**.

    1. Select **Show extracted files when complete**, and then select **Extract**.

1. Copy the **GHCopilotEx10LabApps** folder to a location that's easy to access, such as your Windows Desktop folder.

1. Open the **GHCopilotEx10LabApps** folder in Visual Studio Code.

    For example:

    1. Open Visual Studio Code in your lab environment.

    1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

    1. Navigate to the Windows Desktop folder, select **GHCopilotEx10LabApps** and then select **Select Folder**.

1. In the Visual Studio Code SOLUTION EXPLORER view, verify the following project structure:

    - GHCopilotEx10LabApps\
        - ContosoOnlineStore\
            - Benchmarks\
            - Configuration\
            - Exceptions\
            - Services\
            - appsettings.json
            - InventoryManager.cs
            - Orders.cs
            - OrderItem.cs
            - OrderProcessor.cs
            - PERFORMANCE_GUIDE.md
            - Product.cs
            - ProductCatalog.cs
            - Program.cs
            - README.md
        - ContosoOnlineStore.Tests\
            - ContosoOnlineStoreTests.cs
            - Usings.cs
        - DataAnalyzerReporter\

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help implementing performance profiling in legacy applications. Your goal is to improve code performance while preserving readability and the existing functionality. You're assigned to the following app:

- ContosoOnlineStore: This is an e-commerce application that processes customer orders with realistic business complexity. The application includes product catalog management with search capabilities, inventory tracking with stock reservations, order processing with validation and receipts, email notification services, and security validation. The application uses modern .NET architecture patterns including dependency injection, structured logging, and configuration management, but contains performance bottlenecks that mirror real-world scenarios.

This exercise includes the following tasks:

1. Review the ContosoOnlineStore codebase manually.
1. Identify performance bottlenecks using GitHub Copilot Chat (Ask mode).
1. Refactor performance-critical code using GitHub Copilot Chat (Agent mode).
1. Test and verify the refactored ContosoOnlineStore code.

### Review the ContosoOnlineStore codebase manually

The first step in any code refactoring effort is to understand the existing codebase, including the project structure and business logic. When you're working on performance improvements, it's also important to establish baseline performance metrics.

In this task, you'll examine the main components of the ContosoOnlineStore project, run the application to establish baseline performance metrics, and identify potential areas for optimization.

Use the following steps to complete this task:

1. Take a few minutes to review the ContosoOnlineStore project structure.

    The codebase follows modern .NET architecture patterns with clear separation of concerns. The main architectural components include:

    - **Configuration**: Strongly-typed configuration with validation
    - **Services**: Business services with interfaces for testability  
    - **Exceptions**: Custom domain-specific exceptions
    - **Benchmarks**: Professional performance testing with BenchmarkDotNet
    - **Tests**: Unit tests with mocking framework

1. Examine the main business logic classes.

    Open **ProductCatalog.cs**, **OrderProcessor.cs**, and **InventoryManager.cs**. These classes contain the core business logic and are likely candidates for performance optimization.

    Notice the product data list (20 products with categories and descriptions), the complex order processing workflow, and the inventory management with stock reservations.

    > **NOTE**: The codebase includes comments that help identify performance issues. Look for comments marked "Performance bottleneck" or "Performance issue" that highlight intentional inefficiencies.

1. Review the Services layer and configuration.

    Navigate to the **Services** folder and examine **EmailService.cs** and **SecurityValidationService.cs**. Also review the **Configuration/AppSettings.cs** file.

    You'll notice that these services implement realistic business logic with configurable timeouts, security validation rules, and email notification workflows. The services use dependency injection and logging, following enterprise development patterns.

1. Run the application and observe the baseline performance.

    You can run the application from the Visual Studio Code integrated terminal by navigating to the project folder and running the following .NET CLI command:

    ```bash
    dotnet run
    ```

    The application will execute a comprehensive performance test that includes:

    - Order processing with timing measurements
    - Product catalog operations (search, lookup, category filtering)
    - Inventory management operations
    - Concurrent operation testing
    - Email notification simulation

1. Store the baseline performance metrics in a file named **baseline_metrics.txt**.

    Create a text file named baseline_metrics.txt. Copy the console output into the baseline_metrics.txt file.

    Pay attention to the timing information displayed in the console output, such as:

    - Order processing time (typically 2000-3000ms for a single order)
    - Product lookup performance (individual operation timing)
    - Search operation duration
    - Inventory check timings
    - Concurrent operation performance

    The application also runs a comprehensive performance analysis suite that tests various operations and reports timing details.

1. Take a minute to examine the performance benchmark capabilities provided by the OrderProcessingBenchmarks.cs file.

    The application includes professional benchmarking using BenchmarkDotNet. You can run detailed performance benchmarks by executing:

    ```bash
    dotnet run -c Release -- benchmark
    ```

    This will generate detailed performance reports including memory allocation patterns and statistical analysis.

Understanding the existing architecture and baseline performance metrics provides the foundation for identifying optimization opportunities. The application demonstrates realistic e-commerce performance challenges that you'll address in subsequent tasks.

### Identify performance bottlenecks using GitHub Copilot Chat (Ask mode)

GitHub Copilot Chat's Ask mode is an excellent tool for analyzing complex codebases and identifying performance bottlenecks. In Ask mode, Copilot can analyze your code patterns, identify inefficient algorithms, and suggest optimization strategies based on best practices.

In this task, you'll use GitHub Copilot to systematically analyze the ContosoOnlineStore application and identify specific performance improvement opportunities.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view, and then configure Ask mode and the GPT-4o model.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Ensure that the chat mode is set to **Ask** and you're using the **GPT-4o** model.

    > **NOTE**: The GPT-4o model provides excellent code analysis capabilities and is recommended for this performance analysis task.

1. Close any files that you have open in the editor.

    GitHub Copilot uses files that are open in the editor to establish context. Having only the target files open helps focus the analysis on the code you want to optimize.

1. Add the **InventoryManager.cs**, **OrderProcessor.cs**, and **ProductCatalog.cs** files to the Chat context.

    Use a drag-and-drop operation to add **InventoryManager.cs**, **OrderProcessor.cs**, and **ProductCatalog.cs** from the SOLUTION EXPLORER to the Chat context.

    Adding files to the chat context tells GitHub Copilot to include those files when analyzing your prompts, which improves the accuracy and relevance of its analysis.

1. Ask GitHub Copilot to identify performance bottlenecks in the ProductCatalog class and suggest optimizations.

    For example, enter the following prompt in the Chat view:

    ```text
    Analyze the ProductCatalog class for performance bottlenecks. Focus on the GetProductById, SearchProducts, and GetProductsByCategory methods. What are the main inefficiencies and how could they be optimized?
    ```

    Review GitHub Copilot's analysis, which should identify issues such as:

    - Linear search performance in GetProductById for certain conditions.
    - Inefficient cache key generation in SearchProducts.
    - Missing optimized data structures for category filtering in GetProductsByCategory.
    - Sequential processing with artificial delays in several of the methods.

1. Ask GitHub Copilot to identify performance issues in the OrderProcessor class and suggest optimizations.

    For example, submit the following prompt:

    ```text
    Examine the OrderProcessor class, particularly the CalculateOrderTotal and FinalizeOrderAsync methods. What performance problems do you see and what optimization strategies would you recommend?
    ```

    GitHub Copilot should identify problems including:

    - Individual product lookups in loops (N+1 query pattern).
    - Redundant tax and shipping calculations.
    - Sequential processing of order items.
    - Blocking operations that could be made asynchronous.

1. Ask GitHub Copilot to identify performance issues in the InventoryManager class and suggest optimizations.

    For example, use this prompt to examine inventory operations:

    ```text
    Review the InventoryManager class, especially the GetLowStockProducts and UpdateStockLevels methods. What are the performance concerns and how could the inventory operations be improved?
    ```

    The analysis should reveal:

    - Individual database query simulation in loops.
    - Inefficient logging implementation with blocking operations.
    - Missing batch operation support.
    - Unnecessary thread delays in stock level checks.

1. Ask GitHub Copilot to identify performance issues in the EmailService class and suggest optimizations.

    For example, submit this prompt to analyze the email service:

    ```text
    Analyze the EmailService class for performance issues. How does the email sending process impact overall application performance and what improvements could be made?
    ```

    GitHub Copilot should identify:

    - Sequential email content generation with blocking operations.
    - Individual product lookups within email templates.
    - Synchronous validation operations.
    - Missing parallelization opportunities for multiple recipients.

By using GitHub Copilot's analytical capabilities, you've identified the main performance bottlenecks in the ContosoOnlineStore application. The analysis provides a roadmap for optimization efforts, focusing on algorithmic improvements, caching strategies, and asynchronous processing patterns.

### Refactor performance-critical code using GitHub Copilot Chat (Agent mode)

GitHub Copilot's Agent mode provides an autonomous agent that assists with programming tasks. Developers assign high-level tasks and then start an agentic code editing session to accomplish the task. In agent mode, Copilot autonomously plans the work needed and determines the relevant files and context. The agent can make changes to your code, run tests, and even deploy your application.

In Agent mode, GitHub Copilot can generate optimized code implementations, suggest architectural improvements, and help implement performance enhancements.

In this task, you'll use GitHub Copilot Agent mode to systematically address the performance bottlenecks identified in the previous task.

Use the following steps to complete this task:

1. Configure GitHub Copilot Chat for Agent mode.

    In the Chat view, change the mode from **Ask** to **Agent**. Agent mode provides more targeted code generation and modification capabilities.

1. Assign a task to the agent that optimizes the GetProductById method in the ProductCatalog class.

    Open **ProductCatalog.cs** and select the **GetProductById** method. Use the following prompt in Chat:

    ```text
    Optimize this GetProductById method to improve performance. Consider using a dictionary lookup instead of linear search and implement proper caching mechanisms.
    ```

    Review GitHub Copilot's suggested improvements and implement the changes. The optimized version should include:

    - Dictionary-based product lookups for O(1) performance.
    - Proper cache initialization and management.
    - Reduced redundant operations.

1. Assign a task to the agent that enhances the efficiency of the SearchProducts method.

    Select the **SearchProducts** method in **ProductCatalog.cs** and use this prompt:

    ```text
    Refactor the SearchProducts method to eliminate performance bottlenecks. Optimize the search algorithm and remove unnecessary delays while maintaining search functionality.
    ```

    Apply GitHub Copilot's suggestions to implement:

    - Efficient string matching algorithms.
    - Parallel processing for multiple search criteria.
    - Optimized cache key generation.

1. Assign a task to the agent that improves the performance of the CalculateOrderTotal method in the OrderProcessor class.

    Open **OrderProcessor.cs** and select the **CalculateOrderTotal** method. Submit this prompt:

    ```text
    Optimize the CalculateOrderTotal method to reduce redundant product lookups and improve calculation performance. Consider batch operations and caching strategies.
    ```

    Implement the suggested improvements, which should include:

    - Batch product retrieval to eliminate N+1 query patterns.
    - Cached product information during order processing.
    - Optimized tax and shipping calculations.

1. Optimize the FinalizeOrderAsync method.

    Select the **FinalizeOrderAsync** method in **OrderProcessor.cs** and use this prompt:

    ```text
    Refactor the FinalizeOrderAsync method to improve async performance. Focus on parallel processing where possible and optimizing await patterns.
    ```

    Apply the suggested changes to achieve:

    - Parallel processing of independent operations
    - Optimized async/await usage
    - Better exception handling in async contexts

1. Enhance InventoryManager batch operations.

    Open **InventoryManager.cs** and select the **UpdateStockLevels** method. Use this prompt:

    ```text
    Optimize the UpdateStockLevels method to support batch operations and reduce individual update overhead. Implement efficient logging and remove unnecessary delays.
    ```

    Implement the improvements to include:

    - Batch stock level updates
    - Efficient logging strategies
    - Reduced blocking operations

1. Improve EmailService asynchronous processing.

    Open **Services/EmailService.cs** and select the email sending methods. Submit this prompt:

    ```text
    Optimize the email service to support parallel email processing and improve async performance. Consider implementing email queuing and batch operations.
    ```

    Apply the suggested optimizations for:

    - Parallel email content generation
    - Asynchronous email sending operations
    - Improved error handling and retry logic

Throughout this refactoring process, GitHub Copilot Agent mode serves as your collaborative partner, providing specific code improvements and optimization strategies. The key is to review each suggestion carefully and adapt it to fit your specific requirements and coding standards.

### Test and verify the refactored ContosoOnlineStore code

After implementing performance optimizations, it's crucial to validate that the changes improve performance while maintaining functional correctness. This task focuses on comprehensive testing and performance measurement.

Use the following steps to complete this task:

1. Build and test the refactored application.

    Run the following commands in the Visual Studio Code terminal to ensure the application builds successfully:

    ```bash
    dotnet build
    ```

    Address any compilation errors that may have been introduced during the refactoring process.

1. Run the performance test suite.

    Execute the application to measure the performance improvements:

    ```bash
    dotnet run
    ```

    Compare the new performance metrics with your baseline measurements from the first task. You should observe:

    - Significantly reduced order processing time (from 2000-3000ms to under 500ms)
    - Faster product lookup operations
    - Improved search performance
    - Better inventory management response times

1. Execute the comprehensive benchmark suite.

    Run the detailed performance benchmarks to get precise measurements:

    ```bash
    dotnet run -c Release -- benchmark
    ```

    Review the BenchmarkDotNet reports, which provide detailed statistics including:

    - Mean execution times with confidence intervals
    - Memory allocation patterns
    - Throughput measurements
    - Statistical significance of improvements

1. Validate functional correctness.

    Ensure that the optimizations haven't introduced functional regressions:

    - Verify that order totals are calculated correctly
    - Confirm that inventory levels are updated properly
    - Test that email notifications are sent successfully
    - Validate that security validation still functions

1. Run the unit test suite.

    Execute the existing unit tests to ensure code correctness:

    ```bash
    dotnet test
    ```

    All tests should pass, confirming that the refactored code maintains the expected behavior.

1. Document the performance improvements.

    Use GitHub Copilot to help document the changes. Submit this prompt in Chat:

    ```text
    Help me create a summary of the performance optimizations implemented in this ContosoOnlineStore project. Include before/after metrics and the main optimization strategies used.
    ```

    Create a performance improvement report that includes:

    - Baseline vs. optimized performance metrics
    - Key optimization techniques applied
    - Areas of most significant improvement
    - Recommendations for future enhancements

The testing and verification process confirms that your performance optimization efforts have been successful. The ContosoOnlineStore application now demonstrates significantly improved performance while maintaining its functional requirements and architectural integrity.

Through this exercise, you've learned how to use GitHub Copilot as a powerful tool for performance analysis and optimization, demonstrating the value of AI-assisted development in addressing complex performance challenges.

## Summary

In this exercise, you successfully used GitHub Copilot to identify and resolve performance bottlenecks in a complex e-commerce application. Key accomplishments include:

- **Performance Analysis**: Used GitHub Copilot Ask mode to systematically analyze code and identify bottlenecks
- **Strategic Optimization**: Applied targeted optimizations addressing N+1 query patterns, inefficient algorithms, and blocking operations
- **Collaborative Refactoring**: Leveraged GitHub Copilot Agent mode to implement performance improvements
- **Validation**: Confirmed both performance improvements and functional correctness through comprehensive testing

The optimized ContosoOnlineStore demonstrates dramatic performance improvements while maintaining code quality and architectural best practices. This approach showcases how AI-powered development tools can accelerate performance optimization efforts and help developers make data-driven improvements to complex applications.
