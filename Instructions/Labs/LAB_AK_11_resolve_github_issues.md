<!-- ---
lab:
    title: 'Exercise - Resolve GitHub issues using GitHub Copilot'
    description: 'Learn how to identify and address performance bottlenecks and code inefficiencies using GitHub Copilot tools.'
--- -->

# Resolve GitHub issues using GitHub Copilot

GitHub issues are a powerful way to track bugs, enhancements, and tasks for a project. In this exercise, you learn how to use GitHub Copilot to help you analyze and resolve issues in a sample codebase.

In this exercise, you work with a sample e-commerce application called ContosoShopEasy. The application contains several security vulnerabilities that have been logged as GitHub issues. Your goal is to use GitHub Copilot to help you analyze and resolve these issues.

This exercise should take approximately **40** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following: Git 2.48 or later, .NET SDK 9.0 or later, Visual Studio Code with the C# Dev Kit extension, and access to a GitHub account with GitHub Copilot enabled.

If you're using a local PC as a lab environment for this exercise:

- For help with configuring your local PC as your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320147" target="_blank">Configure your lab environment resources</a>.

- For help with enabling your GitHub Copilot subscription in Visual Studio Code, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

If you're using a hosted lab environment for this exercise:

- For help with enabling your GitHub Copilot subscription in Visual Studio Code, paste the following URL into a browser's site navigation bar: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

- To ensure that the .NET SDK is configured to use the official NuGet.org repository as a source for downloading and restoring packages:

    Open a command terminal and then run the following command:

    ```bash

    dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

    ```

- To ensure that Git is configured to use your name and email address:

    Update the following commands with your information, and then run the commands:

    ```bash

    git config --global user.name "John Doe"

    ```

    ```bash

    git config --global user.email johndoe@example.com

    ```

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help with resolving issues logged against a GitHub project. Your goal is to use the issues as guidance when updating the code project using GitHub Copilot in Visual Studio Code. You need to ensure that all of the issues are addressed and closed. You're assigned to the following app:

- ContosoShopEasy: A realistic e-commerce application with multiple security vulnerabilities that have been logged as GitHub issues. The application demonstrates common security issues found in real-world applications while maintaining a functional e-commerce workflow.

This exercise includes the following tasks:

1. Import the ContosoShopEasy repository.
1. Review the issues in GitHub.
1. Clone the repository and review the codebase.
1. Analyze issues using GitHub Copilot's Ask mode.
1. Resolve issues using GitHub Copilot's Agent mode.
1. Test and verify the refactored code.
1. Commit changes and close issues.

### Import the ContosoShopEasy repository

GitHub repository imports allow you to create a copy of an existing repository in your own GitHub account. This process preserves the original repository's history while giving you full control over the imported copy. In this task, you import the ContosoShopEasy project and create GitHub issues that mirror the security vulnerabilities present in the codebase.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

1. Sign in to your GitHub account.

1. Create a new repository named **ContosoShopEasy**.

    You can create a new repository by selecting the **+** icon in the top-right corner of GitHub and then selecting **New repository**.

1. Copy the ContosoShopEasy project files from your local lab environment to the new repository.

    You can upload files directly through the GitHub web interface or clone the empty repository and push the ContosoShopEasy files from your local machine.

1. Create the following GitHub issues to track the security vulnerabilities:

    - **SQL Injection Vulnerability**: Product search functionality accepts unsanitized input and is vulnerable to injection attacks. The `SearchProducts` method in `ProductService.cs` directly incorporates user input into simulated SQL queries without proper parameterization.

    - **Weak Password Hashing**: The application uses MD5 hashing for password storage, which is cryptographically weak. The `GetMd5Hash` method in `UserService.cs` implements this vulnerable hashing approach.

    - **Sensitive Data Exposure**: Full credit card numbers, CVV codes, and passwords are logged in plaintext. The `ProcessPayment` method in `PaymentService.cs` and registration/login methods in `UserService.cs` expose sensitive information.

    - **Hardcoded Admin Credentials**: Admin username and password are hardcoded in the `SecurityValidator.cs` class, making them accessible to anyone with source code access.

    - **Input Validation Issues**: The application accepts potentially dangerous characters without proper sanitization. The `ValidateInput` method in `SecurityValidator.cs` logs warnings but still accepts malicious input.

    - **Information Disclosure**: Debug logging exposes sensitive system information and configuration details. Multiple classes throughout the application log sensitive data for debugging purposes.

    - **Predictable Session Tokens**: Session tokens follow predictable patterns based on username and timestamp. The `GenerateSessionToken` method in `SecurityValidator.cs` creates easily guessable tokens.

    - **File Upload Security Issues**: The application accepts dangerous file types without proper validation. The `ValidateFileUpload` method in `SecurityValidator.cs` provides insufficient protection against malicious uploads.

### Review the issues in GitHub

GitHub issues serve as a centralized tracking system for bugs, security vulnerabilities, and enhancement requests. Each issue provides context about the problem, its severity, and potential impact on the application. Understanding these issues before diving into the code helps establish priorities and ensures comprehensive remediation.

In this task, you review the open issues for the ContosoShopEasy project and understand the security vulnerabilities that need to be addressed.

Use the following steps to complete this task:

1. Navigate to your ContosoShopEasy repository in GitHub.

1. Select the **Issues** tab to view all open issues.

1. Review each issue description and take note of the following security vulnerability categories:

    **SQL Injection Vulnerabilities**: Product search functionality that directly incorporates user input into database queries, creating opportunities for malicious SQL injection attacks.

    **Weak Cryptographic Practices**: Use of MD5 hashing for password storage, which is considered cryptographically broken and unsuitable for password security.

    **Sensitive Data Exposure**: Logging of full credit card numbers, CVV codes, passwords, and other sensitive information that should never be stored or logged in plaintext.

    **Hardcoded Credentials**: Admin usernames and passwords embedded directly in source code, making them accessible to anyone with repository access.

    **Input Validation Failures**: Insufficient validation and sanitization of user input, allowing potentially malicious content to be processed by the application.

    **Information Disclosure**: Debug logging that exposes sensitive system configuration, internal processes, and user data.

    **Weak Session Management**: Predictable session token generation that could allow attackers to hijack user sessions.

    **File Upload Vulnerabilities**: Inadequate validation of uploaded files, potentially allowing execution of malicious code.

1. Note that these issues represent common security vulnerabilities found in real-world applications and align with OWASP security guidelines.

### Clone the repository and review the codebase

Understanding the structure and functionality of an existing codebase is essential before implementing security fixes. The ContosoShopEasy application follows a layered architecture typical of enterprise applications, with clear separation between models, services, data access, and security components. Reviewing the code structure and running the application helps establish a baseline for testing after implementing security improvements.

In this task, you clone the ContosoShopEasy repository, examine the project structure, and observe the application's current behavior.

Use the following steps to complete this task:

1. Clone your ContosoShopEasy repository to your local development environment.

    Open a terminal window and run the following command, replacing `your-username` with your GitHub username:

    ```bash
    git clone https://github.com/your-username/ContosoShopEasy.git
    ```

1. Open the cloned repository in Visual Studio Code.

    Navigate to the repository folder and open it in Visual Studio Code. Ensure that you have the GitHub Copilot and GitHub Copilot Chat extensions installed and enabled.

1. Examine the project structure in the SOLUTION EXPLORER.

    The ContosoShopEasy application follows a layered architecture with the following components:

    - **Models/**: Contains data models for `Product.cs`, `User.cs`, `Order.cs`, and `Category.cs`
    - **Services/**: Contains business logic in `ProductService.cs`, `UserService.cs`, `PaymentService.cs`, and `OrderService.cs`
    - **Data/**: Contains data repositories in `ProductRepository.cs`, `UserRepository.cs`, and `OrderRepository.cs`
    - **Security/**: Contains security validation logic in `SecurityValidator.cs`
    - **Program.cs**: Main application entry point with dependency injection setup
    - **README.md**: Documentation explaining the application's purpose and vulnerabilities

1. Build and run the application to observe its current behavior.

    Run the following commands in the terminal:

    ```bash
    cd ContosoShopEasy
    dotnet build
    dotnet run
    ```

    The application will display a comprehensive demonstration of the e-commerce functionality, including a security audit that reveals the intentional vulnerabilities.

1. Review the console output to identify security-related logging.

    Notice that the application logs sensitive information such as passwords, credit card numbers, admin credentials, and internal system details. This output provides clear evidence of the security issues that need to be addressed.

1. Identify the specific files and methods associated with each security vulnerability:

    - **SQL Injection**: `ProductService.cs` - `SearchProducts` method
    - **Weak Password Hashing**: `UserService.cs` - `GetMd5Hash` method
    - **Sensitive Data Exposure**: `PaymentService.cs` - `ProcessPayment` method and `UserService.cs` - registration/login methods
    - **Hardcoded Credentials**: `SecurityValidator.cs` - admin credential constants
    - **Input Validation**: `SecurityValidator.cs` - `ValidateInput` method
    - **Information Disclosure**: Multiple classes with debug logging
    - **Predictable Tokens**: `SecurityValidator.cs` - `GenerateSessionToken` method
    - **File Upload Issues**: `SecurityValidator.cs` - `ValidateFileUpload` method

### Analyze issues using GitHub Copilot's Ask mode

GitHub Copilot's Ask mode provides intelligent code analysis capabilities that can help identify security vulnerabilities, understand their potential impact, and suggest remediation strategies. By systematically analyzing each security issue, you can develop a comprehensive understanding of the problems before implementing fixes. This approach ensures that solutions address root causes rather than just symptoms.

In this task, you use GitHub Copilot's Ask mode to systematically analyze each security vulnerability in the ContosoShopEasy application.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view and ensure that Ask mode is selected.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Verify that the chat mode is set to **Ask** and you're using the **GPT-4.1** model for complex security analysis.

1. Begin with the SQL injection vulnerability analysis.

    Open the `ProductService.cs` file and locate the `SearchProducts` method. Select the entire method and add it to the Chat context using drag-and-drop or by right-clicking and selecting **Add to Chat**.

1. Ask GitHub Copilot to analyze the SQL injection vulnerability.

    Submit the following prompt to analyze the security issue:

    ```text
    Analyze the SearchProducts method for security vulnerabilities. What makes this code susceptible to SQL injection attacks, and what are the potential consequences if an attacker exploits this vulnerability?
    ```

1. Review GitHub Copilot's analysis and ask for specific remediation guidance.

    After reviewing the initial analysis, ask for specific fixes:

    ```text
    How can I modify this method to prevent SQL injection attacks? What secure coding practices should I implement to safely handle user input in database queries?
    ```

1. Analyze the weak password hashing vulnerability.

    Open the `UserService.cs` file and locate the `GetMd5Hash` method. Add this method to the Chat context and submit the following prompt:

    ```text
    Why is MD5 hashing unsuitable for password storage? What are the security risks of using MD5 for passwords, and what stronger alternatives should I use instead?
    ```

1. Ask for specific guidance on implementing secure password hashing.

    ```text
    Show me how to implement secure password hashing using bcrypt or PBKDF2. What additional security measures should I implement for password handling?
    ```

1. Analyze the sensitive data exposure issues.

    Open the `PaymentService.cs` file and locate the `ProcessPayment` method. Add it to the Chat context and ask:

    ```text
    What sensitive information is being logged in this payment processing method? Why is logging full credit card numbers and CVV codes a security risk, and how should payment data be handled securely?
    ```

1. Examine the hardcoded credentials vulnerability.

    Open the `SecurityValidator.cs` file and locate the admin credential constants. Add the relevant code to the Chat context and ask:

    ```text
    What security risks are created by hardcoding admin credentials in source code? How should application credentials be managed securely in production environments?
    ```

1. Analyze the input validation weaknesses.

    Focus on the `ValidateInput` method in `SecurityValidator.cs` and ask:

    ```text
    What makes this input validation method ineffective against malicious input? How can I implement proper input sanitization to prevent XSS and other injection attacks?
    ```

1. Review the information disclosure issues.

    Select multiple methods that contain debug logging across different files and ask:

    ```text
    How does excessive debug logging create security vulnerabilities? What information should never be logged, and how can I implement secure logging practices?
    ```

1. Examine the predictable token generation.

    Focus on the `GenerateSessionToken` method and ask:

    ```text
    Why are predictable session tokens a security risk? How should secure, unpredictable session tokens be generated to prevent session hijacking attacks?
    ```

1. Analyze the file upload validation issues.

    Review the `ValidateFileUpload` method and ask:

    ```text
    What makes this file upload validation insufficient for security? How can I implement comprehensive file upload security to prevent malicious file execution?
    ```

1. Document the analysis results for reference during the remediation phase.

    Take notes on GitHub Copilot's recommendations for each vulnerability category. This documentation will guide your implementation of security fixes in the next task.

### Resolve issues using GitHub Copilot's Agent mode

GitHub Copilot's Agent mode enables autonomous implementation of complex security fixes across multiple files and methods. Unlike Ask mode, which provides analysis and recommendations, Agent mode can directly modify code to implement security improvements. This approach is particularly effective for systematic security remediation, where multiple related vulnerabilities need to be addressed consistently.

In this task, you use GitHub Copilot's Agent mode to implement comprehensive security fixes for all identified vulnerabilities in the ContosoShopEasy application.

Use the following steps to complete this task:

1. Switch GitHub Copilot Chat to Agent mode.

    In the Chat view, locate the mode selector and change from **Ask** to **Agent**. Agent mode allows GitHub Copilot to make direct code modifications based on your instructions.

1. Address the SQL injection vulnerability first.

    Open the `ProductService.cs` file and locate the `SearchProducts` method. Use the following prompt to instruct the agent:

    ```text
    Fix the SQL injection vulnerability in the SearchProducts method. Remove the simulated SQL query logging that demonstrates the vulnerability, and implement proper input sanitization to safely handle search terms. Ensure the method still functions correctly for legitimate searches while preventing malicious input.
    ```

1. Monitor the agent's progress and review the proposed changes.

    The agent will modify the code to remove vulnerable logging and implement safer input handling. Review the changes to ensure they maintain functionality while addressing the security concern.

1. Implement secure password hashing.

    Focus on the `UserService.cs` file and use the following prompt:

    ```text
    Replace the MD5 password hashing with bcrypt or PBKDF2. Update the GetMd5Hash method to use a cryptographically secure hashing algorithm with proper salt generation. Ensure compatibility with existing user authentication while improving security.
    ```

1. Review and test the password hashing changes.

    The agent will implement stronger password hashing. Test the changes by running the application to ensure user registration and login still function correctly.

1. Address sensitive data exposure in payment processing.

    Open the `PaymentService.cs` file and instruct the agent:

    ```text
    Fix sensitive data logging in the ProcessPayment method. Remove logging of full credit card numbers, CVV codes, and other sensitive payment information. Implement secure logging that masks sensitive data while maintaining useful operational information.
    ```

1. Remove hardcoded admin credentials.

    Focus on the `SecurityValidator.cs` file and use this prompt:

    ```text
    Remove hardcoded admin credentials and replace them with a secure configuration approach. Implement environment variable or configuration file-based credential management while maintaining the functionality for educational demonstration purposes.
    ```

1. Implement proper input validation.

    Instruct the agent to fix the input validation vulnerabilities:

    ```text
    Strengthen the ValidateInput method to properly sanitize user input and prevent XSS attacks. Implement comprehensive input validation that rejects malicious content while allowing legitimate user input. Ensure the method returns false for truly dangerous input.
    ```

1. Reduce information disclosure through logging.

    Address the debug logging issues across multiple files:

    ```text
    Review all console logging throughout the application and remove or mask sensitive information. Implement secure logging practices that provide useful debugging information without exposing passwords, credit card data, or system internals.
    ```

1. Implement secure session token generation.

    Focus on the session token vulnerability:

    ```text
    Replace the predictable session token generation with a cryptographically secure random token generator. Ensure tokens are unpredictable and sufficiently long to prevent brute force attacks.
    ```

1. Strengthen file upload validation.

    Address the file upload security issues:

    ```text
    Implement comprehensive file upload validation that checks file types, sizes, and content. Reject dangerous file types and implement proper security controls to prevent malicious file uploads.
    ```

1. Test the application after each major change.

    After the agent implements fixes for each vulnerability category, run the application to ensure functionality is preserved:

    ```bash
    dotnet build
    dotnet run
    ```

1. Verify that security improvements don't break core functionality.

    Ensure that product search, user registration, payment processing, and other core features continue to work correctly after implementing security fixes.

### Test and verify the refactored code

Comprehensive testing after security remediation ensures that vulnerability fixes don't introduce functional regressions while confirming that security improvements are effective. This verification process should test both the security aspects and the business functionality of the application. Proper testing validates that the application maintains its intended behavior while being more secure.

In this task, you systematically test the updated ContosoShopEasy application to verify that security issues have been resolved and that core functionality remains intact.

Use the following steps to complete this task:

1. Build the application and resolve any compilation errors.

    Run the following command to ensure the code compiles successfully:

    ```bash
    dotnet build
    ```

    If there are compilation errors, use GitHub Copilot to help identify and resolve any issues introduced during the security fixes.

1. Run the complete application to observe the overall behavior.

    Execute the application and review the console output:

    ```bash
    dotnet run
    ```

    Compare the output with your notes from the original application run. You should see significantly less sensitive information being logged.

1. Test the SQL injection fix by examining product search functionality.

    Verify that the `SearchProducts` method no longer logs vulnerable SQL queries and that search functionality still works correctly for legitimate search terms.

1. Verify password security improvements.

    Check that user registration and login processes no longer log plaintext passwords and that stronger password hashing is implemented. The application should still authenticate users correctly.

1. Confirm payment processing security enhancements.

    Ensure that payment processing no longer logs full credit card numbers or CVV codes while maintaining the ability to process payments successfully.

1. Validate admin credential security.

    Verify that hardcoded admin credentials are no longer displayed in logs or security audits, while admin functionality remains accessible through secure means.

1. Test input validation improvements.

    Confirm that the improved input validation properly handles both legitimate and potentially malicious input without breaking normal user interactions.

1. Check information disclosure fixes.

    Review the console output to ensure that sensitive system information, configuration details, and user data are no longer exposed through debug logging.

1. Verify session token security.

    If session tokens are generated during application execution, confirm that they appear random and unpredictable rather than following the previous pattern-based approach.

1. Validate file upload security enhancements.

    Test the file upload validation improvements to ensure dangerous file types are properly rejected while legitimate files are accepted.

1. Compare the security audit output with the original version.

    Run the security audit feature and compare the results with your initial observations. The audit should show improved security posture while maintaining educational value.

1. Document any remaining issues or areas for improvement.

    Note any security concerns that may require additional attention or any functional issues that need to be addressed.

### Commit changes and close issues

Proper version control practices ensure that security improvements are properly documented and tracked. Commit messages should clearly describe the security fixes implemented, making it easy for team members to understand what changes were made and why. Closing GitHub issues with descriptive commit messages creates a clear audit trail of security remediation efforts.

In this task, you commit your security improvements to the repository and close the corresponding GitHub issues.

Use the following steps to complete this task:

1. Review all changes made to the codebase.

    Use Git to see what files have been modified:

    ```bash
    git status
    git diff
    ```

1. Stage all security-related changes for commit.

    Add the modified files to the staging area:

    ```bash
    git add .
    ```

1. Commit the changes with descriptive messages that reference the GitHub issues.

    Use commit messages that clearly describe the security fixes and reference the issue numbers:

    ```bash
    git commit -m "Fix SQL injection vulnerability in ProductService SearchProducts method

    - Remove vulnerable SQL query logging
    - Implement proper input sanitization
    - Maintain search functionality while preventing injection attacks

    Fixes #1"
    ```

1. Make additional commits for each major security fix category.

    Commit password hashing improvements:

    ```bash
    git commit -m "Replace MD5 with secure password hashing

    - Implement bcrypt/PBKDF2 for password storage
    - Add proper salt generation
    - Maintain authentication compatibility

    Fixes #2"
    ```

1. Continue with commits for remaining security fixes:

    ```bash
    git commit -m "Remove sensitive data from payment processing logs

    - Mask credit card numbers in logging
    - Remove CVV code exposure
    - Maintain operational logging capabilities

    Fixes #3"

    git commit -m "Remove hardcoded admin credentials

    - Implement secure credential management
    - Use environment variables for sensitive config
    - Maintain admin functionality for demo purposes

    Fixes #4"

    git commit -m "Strengthen input validation and prevent XSS

    - Implement comprehensive input sanitization
    - Reject malicious content properly
    - Maintain user experience for legitimate input

    Fixes #5"

    git commit -m "Reduce information disclosure in debug logging

    - Remove sensitive data from console output
    - Implement secure logging practices
    - Maintain useful debugging information

    Fixes #6"

    git commit -m "Implement secure session token generation

    - Replace predictable tokens with cryptographically secure random generation
    - Increase token entropy to prevent brute force attacks

    Fixes #7"

    git commit -m "Enhance file upload security validation

    - Implement comprehensive file type checking
    - Add file size and content validation
    - Reject dangerous file types effectively

    Fixes #8"
    ```

1. Push the changes to your GitHub repository.

    ```bash
    git push origin main
    ```

1. Verify that the GitHub issues are automatically closed.

    Navigate to your repository on GitHub and check that the issues are marked as closed due to the commit messages that referenced them.

1. Review the commit history to ensure all security fixes are properly documented.

    Verify that the commit messages clearly describe the security improvements and provide a good audit trail for future reference.

