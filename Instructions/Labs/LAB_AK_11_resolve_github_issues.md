<!-- ---
lab:
    title: 'Exercise - Resolve GitHub issues using GitHub Copilot'
    description: 'Learn how to identify and address performance bottlenecks and code inefficiencies using GitHub Copilot tools.'
--- -->

# Resolve GitHub issues using GitHub Copilot

GitHub issues are a powerful way to track bugs, enhancements, and tasks for a project.

In this exercise, you use GitHub Copilot to help you analyze and resolve GitHub issues that relate to security vulnerabilities in an e-commerce application.

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

You're a software developer working for a consulting firm. Your clients need help with resolving issues in their GitHub repositories. You need to ensure that all issues are addressed and closed. You use Visual Studio Code and GitHub Copilot as your development environment. You're assigned to the following app:

- ContosoShopEasy: ContosoShopEasy is an e-commerce application that contains multiple security vulnerabilities. The vulnerabilities represent common security issues found in real-world applications.

This exercise includes the following tasks:

1. Import the ContosoShopEasy repository.
1. Review the issues in GitHub.
1. Clone the repository and review the codebase.
1. Analyze issues using GitHub Copilot's Ask mode.
1. Resolve issues using GitHub Copilot's Agent mode.
1. Test and verify the refactored code.
1. Commit changes and close issues.

> **NOTE**: To save time during this training exercise, you resolve a group of issues and push updates in a single commit. Processing issues in batches isn't a recommended best practice. Microsoft and GitHub recommend resolving each issue individually with separate commits rather than batch processing. Resolving issues individually provides better traceability, easier code reviews, and safer rollback options if problems arise.

### Import the ContosoShopEasy repository

GitHub Importer allows you to create a copy of an existing repository in your own GitHub account, giving you full control over the imported copy. Although GitHub Importer doesn't migrate Issues, PRs, or Discussions, it does import GitHub Actions workflows. The repository that you import includes a GitHub Actions workflow that creates issues associated with the codebase.

In this task, you import the ContosoShopEasy repository and run a workflow to create GitHub issues for the security vulnerabilities included in the codebase.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

1. Sign in to your GitHub account.

1. Open your repositories tab.

    You can open your repositories tab by clicking on your profile icon in the top-right corner, then selecting **Repositories**.

1. On the Repositories tab, select the **New** button.

1. Under the **Create a new repository** section, select **Import a repository**.

    The **Import your project to GitHub** page appears.

1. On the Import your project to GitHub page, under **Your source repository details**, enter the following URL for the source repository:

    ```plaintext
    https://github.com/MicrosoftLearning/resolve-github-issues-lab-project
    ```

1. Under the **Your new repository details** section, in the **Owner** dropdown, select your GitHub username.

1. In the **Repository name** field, enter **ResolveGitHubIssues** and then select **Begin import**.

    GitHub creates the new repository in your account with the ContosoShopEasy project files.

    > **NOTE**: It can take a minute or two to import the repository.

1. Wait for the import process to complete, then open your new repository.

1. Open the Actions tab of your repository.

1. On the left side under **All workflows**, select the **Create ContosoShopEasy Training Issues** workflow, and then select **Run workflow**.

1. In the workflow dialog that appears, type **CREATE** and then select **Run workflow**.

1. Monitor the onscreen progress of the workflow.

    After a moment, the page will refresh and display a progress bar. The workflow should complete successfully in less than a minute.

1. Ensure that the workflow completes successfully before proceeding.

    A checkmark in a green circle to the left of the workflow name indicates that the workflow ran successfully.

    If you see an X in a red circle to the left of the workflow name, it means that the workflow failed. If the workflow fails to run successfully, ensure that you selected your account when you imported the repository and that your account has read and write permissions. You can use GitHub's **Chat with Copilot** feature to help diagnose the issue.

### Review the issues in GitHub

GitHub issues serve as a centralized tracking system for bugs, security vulnerabilities, and enhancement requests. Each issue provides context about the problem, its severity, and potential impact on the application. Understanding these issues before diving into the code helps establish priorities and ensures comprehensive remediation.

In this task, you review the GitHub issues and examine the security vulnerabilities that need to be addressed.

Use the following steps to complete this task:

1. Select the **Issues** tab of your repository, and then take a minute to review the Issues page.

    You should see 10 issues listed. Notice that the issues are defined as bugs and that they've been assigned a priority level.

1. To display only the critical issues, select the **Labels** dropdown, and then select the **critical** label.

    The issues list filters to show only the critical issues.

    - **🔐 Fix Credit Card Data Storage Violations**  

    - **🔐 Remove Hardcoded Admin Credentials**  

1. To display only the high-priority issues, select the **Labels** dropdown, deselect **critical**, and then select the **high-priority** label.

    The issues list filters to show only the high-priority issues.

    - **🔐 Fix Input Validation Security Bypass**  

    - **🔐 Remove Sensitive Data from Debug Logging**  

    - **🔐 Fix SQL Injection Vulnerability in Product Search**  

    - **🔐 Replace MD5 Password Hashing with Secure Alternative**  

1. Select the **Fix SQL Injection Vulnerability in Product Search** issue.

1. Take a minute to review the issue details.

    Issue details should describe the problem and the expected fix.

    > **NOTE**: The process used to generate issues, including manual versus automated processes, affects the overall quality and accuracy of the issue descriptions. The issues included in this training were written using GitHub Copilot's Agent mode after the agent reviewed the codebase. GitHub Copilot generated highly detailed descriptions of the vulnerabilities, code locations, examples of the vulnerable code, security risks, and acceptance criteria for fixes.

1. Notice that no one is assigned to the issue.

1. Navigate back to the Issues tab and clear the filters.

1. Select all of the issues, and then use the **Assign** dropdown to assign them to yourself.

    Assigning issues to yourself helps track your progress as you work through the remediation process.

### Clone the repository and review the codebase

Understanding the structure and functionality of an existing codebase is essential before implementing security fixes. The ContosoShopEasy application follows a layered architecture typical of enterprise applications, with clear separation between models, services, data access, and security components. Reviewing the code structure and running the application helps establish a baseline for testing after implementing security improvements.

In this task, you clone the ContosoShopEasy repository, examine the project structure, observe the application's current behavior, and review security vulnerabilities.

Use the following steps to complete this task:

1. Open the Code tab of your repository.

1. Clone the ResolveGitHubIssues repository to your local development environment.

    For example, you can use the following steps to clone the repository using Git CLI:

    1. Copy the repository URL by selecting the **Code** button and then copying the HTTPS URL.

    1. Open a terminal window, navigate to the directory where you want to clone the repository, and run the following command (replacing **your-username** with your GitHub username):

    ```bash
    git clone https://github.com/your-username/ResolveGitHubIssues.git
    ```

1. Open the cloned repository in Visual Studio Code.

    Navigate to the repository folder and open it in Visual Studio Code. Ensure that you have the GitHub Copilot and GitHub Copilot Chat extensions installed and enabled.

1. Examine the project structure in the EXPLORER view.

    The ContosoShopEasy application follows a layered architecture with the following components:

    - **Models/**: Contains data models for **Category.cs**, **Order.cs**, **Product.cs**, and **User.cs**.

    - **Services/**: Contains business logic in **OrderService.cs**, **PaymentService.cs**, **ProductService.cs**, and **UserService.cs**.

    - **Data/**: Contains data repositories in **OrderRepository.cs**, **ProductRepository.cs**, and **UserRepository.cs**.

    - **Security/**: Contains security validation logic in **SecurityValidator.cs**

    - **Program.cs**: Main application entry point with dependency injection setup

    - **README.md**: Documentation explaining the application's purpose and vulnerabilities

1. Build and run the application to observe its current behavior.

    Run the following commands in the terminal:

    ```bash
    cd ContosoShopEasy
    dotnet build
    dotnet run
    ```

    The application runs an e-commerce workflow simulation that exposes security vulnerabilities through detailed console logging.

1. Review the console output.

    Notice that the application logs sensitive information such as passwords, credit card numbers, admin credentials, and internal system details. This output provides clear evidence of the security issues that need to be addressed.

    > **NOTE**: Code logic and logging in this app are designed to expose security vulnerabilities. Although the implementation is contrived, the logs highlight security issues that are common in real-world applications.

1. To begin a review process that identifies security vulnerabilities in the codebase, expand the **Models** folder, and then open the **Order.cs** file.

1. Scroll down to find the **PaymentInfo** class.

    Notice the comments regarding the CardNumber and CVV properties. This code is related to the "Fix Credit Card Data Storage Violations" issue.

1. Expand the **Security** folder and then open the **SecurityValidator.cs** file.

1. Take a minute to locate the following security issues:

    - Near the top of the file, notice the comment related to the admin credential constants (lines 7-9). This code is related to the "Remove Hardcoded Admin Credentials" issue.

    - Locate the ValidateInput method and review the comments describing security vulnerabilities. This code is related to the "Fix Input Validation Security Bypass" issue.

    - Locate the ValidateEmail method and review the comments describing security vulnerabilities. This code is related to the "Improve Email Validation Security" issue.

    - Locate the ValidatePasswordStrength method and review the comments describing security vulnerabilities. This code is related to the "Strengthen Password Security Requirements" issue.

    - Locate the GenerateSessionToken method and review the comments describing security vulnerabilities. This code is related to the "Fix Predictable Session Token Generation" issue.

    - Locate the RunSecurityAudit method and review the comments describing security vulnerabilities. This code is related to the "Reduce Information Disclosure in Error Messages" issue.

1. Expand the **Services** folder and then open the **UserService.cs** file.

1. Take a minute to locate the following security issues:

    - Locate the RegisterUser, LoginUser, and ValidateUserInput methods and review the comments describing security vulnerabilities. This code is related to the "Remove Sensitive Data from Debug Logging" issues.
    - Locate the GetMd5Hash method and review the comments describing security vulnerabilities. This code is related to the "Replace MD5 Password Hashing with Secure Alternative" issue.

1. Open the **PaymentService.cs** file.

1. Take a minute to review the comments describing security vulnerabilities.

    This code is related to the "Remove Sensitive Data from Debug Logging" issue.

1. Open the **ProductService.cs** file.

1. Take a minute to review the SearchProducts method.

    This code is related to the "Fix SQL Injection Vulnerability in Product Search" issue.

### Analyze issues using GitHub Copilot's Ask mode

GitHub Copilot's Ask mode provides intelligent code analysis capabilities that can help identify security vulnerabilities, understand their potential impact, and suggest remediation strategies. By systematically analyzing each security issue, you can develop a comprehensive understanding of the problems before implementing fixes. This approach ensures that solutions address root causes rather than just symptoms.

In this task, you use GitHub Copilot's Ask mode to systematically analyze the security vulnerabilities.

Use the following steps to complete this task:

1. Open the GitHub Copilot Chat view and ensure that Ask mode is selected.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Verify that the chat mode is set to **Ask** and you're using the **GPT-4.1** model.

1. Open the **ProductService.cs** file, and then locate the **SearchProducts** method.

1. In the code editor, select the entire **SearchProducts** method.

    Selecting code in the editor focuses the Chat context. GitHub Copilot uses the selected code to provide relevant analysis and recommendations.

1. Ask GitHub Copilot to analyze the the code for SQL injection vulnerability.

    For example, you can submit the following prompt:

    ```text
    Analyze the SearchProducts method for security vulnerabilities. What makes this code susceptible to SQL injection attacks, and what are the potential consequences if an attacker exploits this vulnerability?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify this method to prevent SQL injection attacks? What secure coding practices should I implement to safely handle user input in database queries?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. Open the **UserService.cs** file, and then locate the **GetMd5Hash** method.

1. In the code editor, select the entire **GetMd5Hash** method.

1. Ask GitHub Copilot to analyze the weak password hashing vulnerability.

    For example, you can submit the following prompt:

    ```text
    Why is MD5 hashing unsuitable for password storage? What are the security risks of using MD5 for passwords, and what stronger alternatives should I use instead?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    Show me how to implement secure password hashing using bcrypt or PBKDF2. What additional security measures should I implement for password handling?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. In the **UserService.cs** file, locate the **RegisterUser** and **LoginUser** methods.

    These methods log user information. Logging sensitive information is a security vulnerability.

1. In the code editor, select both methods.

1. Ask GitHub Copilot to analyze the sensitive data logging vulnerability.

    For example, you can submit the following prompt:

    ```text
    What sensitive information is being logged in the user registration and login methods? Why is logging passwords and user data a security risk?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify these methods to prevent sensitive data logging? What secure logging practices should I implement to protect user information?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. Open the **PaymentService.cs** file, and then locate the **ProcessPayment** method.

1. In the code editor, select the entire **ProcessPayment** method.

1. Ask GitHub Copilot to analyze the logging of sensitive payment data.

    For example, you can submit the following prompt:

    ```text
    What sensitive payment information is being logged in this method? Why is logging credit card numbers and CVV codes a security risk?
    ```

1. Open the **SecurityValidator.cs** file, and then locate the admin credential constants near the top of the file.

1. In the code editor, select the hardcoded admin credential constants.

1. Ask GitHub Copilot to analyze the hardcoded credentials vulnerability.

    For example, you can submit the following prompt:

    ```text
    What security risks are created by hardcoding admin credentials in source code? How should application credentials be managed securely in production environments?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    What are best practices for managing application credentials securely? How can I implement secure credential management in this application?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. In the **SecurityValidator.cs** file, locate the **ValidateInput** method.

1. In the code editor, select the entire **ValidateInput** method.

1. Ask GitHub Copilot to analyze the input validation bypass vulnerability.

    For example, you can submit the following prompt:

    ```text
    What makes this input validation method ineffective? Why does it detect dangerous input but still return true, and how should proper input validation work?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify this method to implement effective input validation? What secure coding practices should I follow to prevent input validation bypass vulnerabilities?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. In the **SecurityValidator.cs** file, locate the **GenerateSessionToken** method.

1. In the code editor, select the entire **GenerateSessionToken** method.

1. Ask GitHub Copilot to analyze the predictable session token generation vulnerability.

    For example, you can submit the following prompt:

    ```text
    Why are predictable session tokens based on username and timestamp a security risk? How should secure, unpredictable session tokens be generated?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify this method to generate secure, unpredictable session tokens? What cryptographic techniques should I use to enhance session token security?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. In the **SecurityValidator.cs** file, locate the **ValidateEmail** method.

1. In the code editor, select the entire **ValidateEmail** method.

1. Ask GitHub Copilot to analyze the weak email validation vulnerability.

    For example, you can submit the following prompt:

    ```text
    What makes this email validation insufficient? What are the security risks of weak email validation, and how should proper email validation be implemented?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify this method to implement robust email validation? What techniques should I use to ensure email addresses are properly validated?
    ```

1. In the **SecurityValidator.cs** file, locate the **ValidatePasswordStrength** method.

1. In the code editor, select the entire **ValidatePasswordStrength** method.

1. Ask GitHub Copilot to analyze the insufficient password requirements vulnerability.

    For example, you can submit the following prompt:

    ```text
    Why are these password requirements insufficient for security? What are proper password complexity requirements, and how should password strength be validated?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify this method to enforce strong password requirements? What best practices should I follow for password strength validation?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

1. Under the **Models** folder, open the **Order.cs** file, and then locate the **PaymentInfo** class.

1. In the code editor, select the **CardNumber** and **CVV** properties within the **PaymentInfo** class.

1. Ask GitHub Copilot to analyze the credit card data storage violations.

    For example, you can submit the following prompt:

    ```text
    Why is storing full credit card numbers and CVV codes a PCI DSS compliance violation? What are the proper ways to handle payment card data securely?
    ```

1. Return to the **SecurityValidator.cs** file, and then locate the **RunSecurityAudit** method.

1. In the code editor, select the entire **RunSecurityAudit** method.

1. Ask GitHub Copilot to analyze the information disclosure vulnerability.

    For example, you can submit the following prompt:

    ```text
    How does the security audit method create information disclosure vulnerabilities? What information should never be exposed in logs or error messages?
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

1. Monitor the agent's progress.

    The agent will modify the code to remove vulnerable logging and implement safer input handling.

1. Take a minute to review the proposed changes, and then select **Keep** in the Chat view.

    Always review GitHub Copilot's suggested edits in the code editor. Ensure that they maintain functionality while addressing the security concern.

    In a production environment, your team should complete the following checklist before moving on to the next issue:

    - Code no longer contains the vulnerability.
    - Application still functions correctly.
    - Security best practices are implemented and no new security issues are introduced.
    - Automated tests (if available) pass successfully.
    - Code updates are clearly documented.
    - Changes are committed with descriptive messages and peer-reviewed before merging and closing the issue.

1. Implement secure password hashing.

    Focus on the `UserService.cs` file and use the following prompt:

    ```text
    Replace the MD5 password hashing with bcrypt or PBKDF2. Update the GetMd5Hash method to use a cryptographically secure hashing algorithm with proper salt generation. Ensure compatibility with existing user authentication while improving security.
    ```

1. Review and test the password hashing changes.

    The agent will implement stronger password hashing. Test the changes by running the application to ensure user registration and login still function correctly.

1. Address sensitive data logging (Issue #3).

    Focus on the `PaymentService.cs` and `UserService.cs` files and instruct the agent:

    ```text
    Fix sensitive data logging throughout the application. Remove logging of passwords, full credit card numbers, CVV codes, and other sensitive information. Implement secure logging that masks sensitive data while maintaining useful operational information.
    ```

1. Remove hardcoded admin credentials (Issue #4).

    Focus on the `SecurityValidator.cs` file and use this prompt:

    ```text
    Remove hardcoded admin credentials from the SecurityValidator class. Replace the hardcoded ADMIN_USERNAME and ADMIN_PASSWORD constants with a secure configuration approach using environment variables while maintaining the functionality for educational demonstration purposes.
    ```

1. Fix credit card data storage violations (Issue #5).

    Focus on the `Models/Order.cs` file and instruct the agent:

    ```text
    Fix PCI DSS compliance violations in the Order model. Remove or modify the CardNumber and CVV properties to avoid storing full credit card numbers and CVV codes. Implement secure payment data handling that stores only last 4 digits for display purposes.
    ```

1. Fix input validation bypass (Issue #6).

    Instruct the agent to fix the input validation vulnerability:

    ```text
    Fix the ValidateInput method in SecurityValidator that currently always returns true despite detecting threats. Implement proper input validation that actually rejects dangerous content when SQL injection, XSS, or other malicious patterns are detected.
    ```

1. Implement secure session token generation (Issue #7).

    Focus on the session token vulnerability:

    ```text
    Replace the predictable session token generation in GenerateSessionToken method with a cryptographically secure random token generator. Remove the username and timestamp-based pattern and implement unpredictable tokens with sufficient entropy.
    ```

1. Strengthen email validation (Issue #8).

    Address the weak email validation:

    ```text
    Fix the ValidateEmail method that only checks for '@' and '.' characters. Implement proper email format validation using regex or built-in validation methods. Remove email logging and add appropriate length restrictions.
    ```

1. Improve password requirements (Issue #9).

    Focus on the password strength validation:

    ```text
    Strengthen the ValidatePasswordStrength method that currently only requires 4 characters. Implement proper password complexity requirements including minimum 8 characters, uppercase, lowercase, numbers, and special characters. Remove password logging.
    ```

1. Reduce information disclosure (Issue #10).

    Address the debug logging and security audit issues:

    ```text
    Fix information disclosure vulnerabilities by removing or restricting the RunSecurityAudit method and reducing verbose error messages throughout the application. Remove sensitive system information from logs while maintaining useful debugging capabilities.
    ```

1. Test the application.

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

1. Test the SQL injection fix (Issue #1).

    Verify that the `SearchProducts` method no longer logs vulnerable SQL queries and that search functionality still works correctly for legitimate search terms.

1. Verify password security improvements (Issue #2).

    Check that user registration and login processes no longer log plaintext passwords and that stronger password hashing is implemented. The application should still authenticate users correctly.

1. Confirm sensitive data logging fixes (Issue #3).

    Ensure that payment processing and user operations no longer log passwords, full credit card numbers, or CVV codes while maintaining the ability to process transactions successfully.

1. Validate hardcoded credential removal (Issue #4).

    Verify that hardcoded admin credentials are no longer displayed in logs or security audits, while admin functionality remains accessible through secure configuration.

1. Test credit card storage compliance (Issue #5).

    Confirm that the Order model no longer stores full credit card numbers or CVV codes, and that only masked payment information is retained for display purposes.

1. Verify input validation fixes (Issue #6).

    Confirm that the improved ValidateInput method now properly rejects dangerous input instead of just logging warnings and returning true.

1. Check session token security (Issue #7).

    If session tokens are generated during application execution, confirm that they appear random and unpredictable rather than following the previous username-timestamp pattern.

1. Test email validation improvements (Issue #8).

    Verify that the email validation now properly rejects invalid email formats instead of accepting any string with "@" and "." characters.

1. Validate password requirement enhancements (Issue #9).

    Test that password validation now enforces proper complexity requirements instead of accepting any 4-character string.

1. Review information disclosure fixes (Issue #10).

    Check that the security audit method is removed or restricted and that verbose error messages no longer expose sensitive system information.

1. Compare the overall security posture with the original version.

    Run the application and compare the console output with your initial observations. The application should show significantly improved security while maintaining all core functionality.

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

1. Commit all security fixes with a comprehensive message that references all GitHub issues.

    Create a single commit that addresses all security vulnerabilities identified in the training exercise:

    ```bash
    git commit -m "Fix all ContosoShopEasy security vulnerabilities

    Security improvements implemented:
    - Fix SQL injection in ProductService SearchProducts method
    - Replace MD5 with secure password hashing (bcrypt/PBKDF2)
    - Remove sensitive data from debug logging (passwords, card numbers, CVV)
    - Remove hardcoded admin credentials, use environment variables
    - Fix PCI DSS violations in Order model (remove full card storage)
    - Fix input validation bypass to properly reject dangerous input
    - Implement secure session token generation with crypto randomness
    - Strengthen email validation with proper format checking
    - Improve password requirements (8+ chars, complexity rules)
    - Reduce information disclosure from security audit and debug logs

    Fixes #1 #2 #3 #4 #5 #6 #7 #8 #9 #10"
    ```

    > **NOTE**: In a production environment, each issue would typically be addressed in separate commits with individual testing and code review. This single commit approach is used here only to save time during the training exercise.

1. Push the changes to your GitHub repository.

    ```bash
    git push origin main
    ```

1. Verify that the GitHub issues are automatically closed.

    Navigate to your repository on GitHub and check that the issues are marked as closed due to the commit messages that referenced them.

1. Review the commit history to ensure all security fixes are properly documented.

    Verify that the commit messages clearly describe the security improvements and provide a good audit trail for future reference.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. For example, you might want to delete the ResolveGitHubIssues repository. If you're using a local PC as your lab environment, you can archive or delete the local clone of the repository created for this exercise.
