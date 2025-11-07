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

    git config --global user.name "Julie Miller"

    ```

    ```bash

    git config --global user.email julie.miller@example.com

    ```

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help with resolving issues in their GitHub repositories. You need to ensure that all issues are addressed and closed. You use Visual Studio Code as your development environment and GitHub Copilot to assist with development tasks. You're assigned to the following app:

- ContosoShopEasy: ContosoShopEasy is an e-commerce application that contains multiple security vulnerabilities. The vulnerabilities represent common security issues found in real-world applications.

This exercise includes the following tasks:

1. Import the ContosoShopEasy repository.
1. Review the issues in GitHub.
1. Clone the repository locally and review the codebase.
1. Analyze issues using GitHub Copilot's Ask mode.
1. Resolve issues using GitHub Copilot's Agent mode.
1. Test and verify the refactored code.
1. Commit changes and close issues.

> **NOTE**: To save time during this training exercise, you resolve a group of issues and push updates in a single commit. Processing issues in batches isn't a recommended best practice. Microsoft and GitHub recommend resolving each issue individually with separate commits. Resolving issues individually provides better traceability, easier code reviews, and safer rollback options if problems arise.

### Import the ContosoShopEasy repository

GitHub Importer allows you to create a copy of an existing repository in your own GitHub account, giving you full control over the imported copy. Although GitHub Importer doesn't migrate Issues, PRs, or Discussions, it does import GitHub Actions workflows. The repository that you import includes a GitHub Actions workflow that creates issues associated with the codebase.

In this task, you import the ContosoShopEasy repository and run a workflow to create GitHub issues for the security vulnerabilities included in the codebase.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

1. Sign in to your GitHub account, and then open your repositories tab.

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

1. Wait for the import process to complete, and then open your new repository.

    > **NOTE**: It can take a minute or two to import the repository.

1. Open the Actions tab of your repository.

1. On the left side of the page under **All workflows**, select the **Create ContosoShopEasy Training Issues** workflow, and then select **Run workflow**.

1. In the workflow dialog that appears, type **CREATE** and then select **Run workflow**.

1. Monitor the onscreen progress of the workflow.

    After a moment, the page will refresh and display a progress bar. The workflow should complete successfully in less than a minute.

1. Ensure that the workflow completes successfully before proceeding.

    A checkmark inside a green circle indicates that the workflow ran successfully (should appear on the left of the workflow name).

    If an X inside a red circle appears to the left of the workflow name, it means that the workflow failed. If the workflow fails to run successfully, ensure that you selected your account when you imported the repository and that your account has read and write permissions. You can use GitHub's **Chat with Copilot** feature to help diagnose the issue.

### Review the issues in GitHub

GitHub issues serve as a centralized tracking system for bugs, security vulnerabilities, and enhancement requests. Each issue provides context about the problem, its severity, and potential impact on the application. Understanding these issues before diving into the code helps establish priorities and ensures comprehensive remediation.

In this task, you review the GitHub issues and examine the security vulnerabilities that need to be addressed.

Use the following steps to complete this task:

1. Select the **Issues** tab of your repository, and then take a minute to review the Issues page.

    You should see 10 issues open listed. Notice the following:

    - All of the issues are labeled as bugs.
    - All of the issues have a priority level.
    - None of the issues are assigned to anyone.

1. To display only the critical issues, select the **Labels** dropdown, and then select the **critical** label.

    The issues list should update to show only the critical issues.

    - **🔐 Remove Hardcoded Admin Credentials**  

    - **🔐 Fix Credit Card Data Storage Violations**  

1. To display only the high-priority issues, select the **Labels** dropdown, deselect **critical**, and then select the **high-priority** label.

    The issues list should update to show only the high-priority issues.

    - **🔐 Fix Input Validation Security Bypass**  

    - **🔐 Remove Sensitive Data from Debug Logging**  

    - **🔐 Replace MD5 Password Hashing with Secure Alternative**  

    - **🔐 Fix SQL Injection Vulnerability in Product Search**  

1. Select the **Fix SQL Injection Vulnerability in Product Search** issue.

1. Take a minute to review the issue details.

    Issue details should describe the problem and the expected fix.

    > **NOTE**: The process used to document issues, including manual versus AI-automated processes, can affect the overall quality and accuracy of the issue descriptions. The issues included in this training were written using GitHub Copilot's Agent mode after the agent reviewed the codebase. GitHub Copilot can be used to generate highly detailed descriptions of the vulnerabilities, code locations, examples of the vulnerable code, security risks, and acceptance criteria for fixes.

1. Notice that no one is assigned to the issue.

1. Navigate back to the Issues tab and clear the filters.

1. Select the following critical and high-priority issues, and then use the **Assign** dropdown to assign them to yourself.

    - **🔐 Fix Credit Card Data Storage Violations**  

    - **🔐 Fix SQL Injection Vulnerability in Product Search**  

    It's generally best to work on the higher priority issues first. Assigning issues to yourself helps you track your progress as you work through the remediation process.

### Clone the repository locally and review the codebase

The ContosoShopEasy application follows a layered architecture typical of enterprise applications, with clear separation between models, services, data access, and security components.

Taking the time to understand the basic structure, behavior, and features of an existing codebase is an important first step when resolving security issues.

In this task, you create a local clone of your repository, examine the project structure in Visual Studio Code, review the application's console output, and identify security vulnerabilities within the codebase.

Use the following steps to complete this task:

1. Navigate back to the root page of your repository (Code tab).

1. Clone the ResolveGitHubIssues repository to your local development environment.

    For example, you can use the following steps to clone the repository using Git CLI:

    1. Copy the repository URL by selecting the **Code** button and then copying the HTTPS URL.

    1. Open a terminal window, navigate to the directory where you want to clone the repository, and then run a "git clone" command that uses the repository URL.

        For example, open Windows PowerShell, navigate to C:\TrainingProjects, and then run the following command (replacing **your-username** with your GitHub username):

        ```bash
        git clone https://github.com/your-username/ResolveGitHubIssues.git
        ```

1. Open the cloned repository in Visual Studio Code.

    Ensure that you're using the latest version of Visual Studio Code and that you have the GitHub Copilot and GitHub Copilot Chat extensions installed and enabled.

1. Examine the project structure in the EXPLORER view.

    The ContosoShopEasy application follows a layered architecture with the following components:

    - **Data/**: Contains data repositories in **OrderRepository.cs**, **ProductRepository.cs**, and **UserRepository.cs**.

    - **Models/**: Contains data models for **Category.cs**, **Order.cs**, **Product.cs**, and **User.cs**.

    - **Security/**: Contains security validation logic in **SecurityValidator.cs**

    - **Services/**: Contains business logic in **OrderService.cs**, **PaymentService.cs**, **ProductService.cs**, and **UserService.cs**.

    - **Program.cs**: Main application entry point with dependency injection setup

    - **README.md**: Documentation explaining the application's purpose and vulnerabilities

1. To observe the application's current behavior, build and run the application.

    For example, you can open Visual Studio Code's integrated terminal window and run the following commands:

    ```bash
    cd ContosoShopEasy
    dotnet build
    dotnet run
    ```

    The application runs an e-commerce workflow simulation that exposes security vulnerabilities through detailed console logging.

1. Take a minute to review the console output.

    The ContosoShopEasy application uses intentionally excessive logging as an educational tool to expose vulnerabilities. Excessive logging serves a dual purpose: both creating AND exposing security issues, which mimics actual over-logging problems found in some production systems. This implementation helps developers distinguish between two types of vulnerabilities:

    - Issues created BY logging: Approximately 40% of the vulnerabilities in the ContosoShopEasy application are caused by over-logging. For example, password exposure, credit card number disclosure, session token exposure, and configuration information disclosure.

    - Issues that exist independently of logging: Approximately 60% of the vulnerabilities in the ContosoShopEasy application exist independently of logging. For example, SQL injection, weak password hashing, hardcoded credentials, predictable tokens, input validation bypass, credit card storage, and weak email validation. Although logging doesn't create these vulnerabilities, it does help to expose them within the training environment.

1. To begin your review of security vulnerabilities in the codebase, expand the **Models** folder, and then open the **Order.cs** file.

    Notice that the ContosoShopEasy application uses code comments, logic, and logging to expose security issues. Although the implementation is contrived, it highlights vulnerabilities that are common in real-world applications.

1. Scroll down to find the **PaymentInfo** class.

    Notice the comments regarding the CardNumber and CVV properties. This code is related to the **Fix Credit Card Data Storage Violations** issue that you assigned to yourself.

1. Expand the **Security** folder and then open the **SecurityValidator.cs** file.

    > **NOTE**: The SecurityValidator.cs class is designed to centralize security-related logic for the ContosoShopEasy application, making it easier to locate, manage, and resolve security issues. In a real-world application, a class like SecurityValidator could be used to enforce security best practices and input validation. However, the specific implementation in ContosoShopEasy is intentionally insecure and contrived to expose vulnerabilities.

1. Take a minute to find the following security issues:

    - Near the top of the file, notice the comment related to the admin credential constants (lines 7-9). This code is related to the "Remove Hardcoded Admin Credentials" issue.

    - Locate the ValidateInput method and review the comments describing security vulnerabilities. This code is related to the "Fix Input Validation Security Bypass" issue.

    - Locate the ValidateEmail method and review the comments describing security vulnerabilities. This code is related to the "Improve Email Validation Security" issue.

    - Locate the ValidatePasswordStrength method and review the comments describing security vulnerabilities. This code is related to the "Strengthen Password Security Requirements" issue.

    - Locate the ValidateCreditCard method and review the comments describing security vulnerabilities. This code is related to the **Fix Credit Card Data Storage Violations** issue that you assigned to yourself.

    - Locate the GenerateSessionToken method and review the comments describing security vulnerabilities. This code is related to the "Fix Predictable Session Token Generation" issue.

    - Locate the RunSecurityAudit method and review the comments describing security vulnerabilities. This code is related to the "Reduce Information Disclosure in Error Messages (Console Output)" issue.

    Several of the methods in the SecurityValidator.cs file are also related to the "Remove Sensitive Data from Debug Logging" issue.

    The issues exposed by the SecurityValidator class are commonly found distributed among the classes of real-world applications, especially legacy or poorly maintained codebases.

1. Expand the **Services** folder and then open the **UserService.cs** file.

1. Take a minute to find the following security issues:

    - Locate the RegisterUser, LoginUser, and ValidateUserInput methods and review the comments describing security vulnerabilities. This code is related to the "Remove Sensitive Data from Debug Logging" issues.

    - Locate the GetMd5Hash method and review the comments describing security vulnerabilities. This code is related to the "Replace MD5 Password Hashing with Secure Alternative" issue.

1. Open the **PaymentService.cs** file.

1. Take a minute to review the comments describing security vulnerabilities.

    The security vulnerabilities in this code are related to the "Remove Sensitive Data from Debug Logging" issue.

    Notice that the PaymentService class uses OrderRepository to persist payment-related order data. If the OrderRepository class does not properly handle sensitive data, it could lead to data exposure vulnerabilities.

1. Open the **ProductService.cs** file.

1. Take a minute to review the SearchProducts method.

    The security vulnerabilities in this code are related to the **Fix SQL Injection Vulnerability in Product Search** issue that you assigned to yourself.

    Notice that the SearchProducts method calls the ProductRepository's Search method. If the Search method is not properly validating and sanitizing user input, it could be vulnerable to SQL injection attacks.

1. Make a list of the code files related to the issues assigned to you.

    The issues that you assigned to yourself are:

    - **🔐 Fix Credit Card Data Storage Violations**
    - **🔐 Fix SQL Injection Vulnerability in Product Search**

    The code files related to the "Fix Credit Card Data Storage Violations" issue are:

    - Models/Orders.cs/PaymentInfo class
    - Security/SecurityValidator.cs/ValidateCreditCard method
    - Data/OrderRepository.cs

    The code files related to the "Fix SQL Injection Vulnerability in Product Search" issue are:

    - Services/ProductService.cs/SearchProducts method
    - Data/ProductRepository.cs/SearchProducts method

### Analyze issues using GitHub Copilot's Ask mode

GitHub issues often contain complex problems that require careful analysis before implementing fixes. Understanding the root causes, potential impacts, and best remediation strategies is crucial for effective resolution.

The following GitHub extensions for Visual Studio Code can assist with issue analysis and resolution:

- **GitHub Copilot Chat**: GitHub Copilot's Ask mode provides intelligent code analysis capabilities that can help identify security vulnerabilities, understand their potential impact, and suggest remediation strategies.

- **GitHub Pull Requests**: The GitHub Pull Requests extension integrates GitHub issues directly into Visual Studio Code, allowing you to manage and interact with issues without leaving your development environment.

By systematically analyzing security issues, you can develop a comprehensive understanding of the problems before implementing fixes. This approach ensures that solutions address root causes rather than just symptoms.

In this task, you use GitHub Copilot's Ask mode to analyze the security vulnerabilities.

Use the following steps to complete this task:

1. Ensure that the GitHub Copilot Chat and GitHub Pull Requests extensions are installed in Visual Studio Code.

    Open the Extensions view in Visual Studio Code and review your installed extensions. If either extension is missing, install it before proceeding.

    For example, you can use the following steps to install the GitHub Pull Requests extension:

    1. Open the Extensions view in Visual Studio Code.

    1. In the Extensions view, search for **GitHub Pull Requests**.

    1. Select **GitHub Pull Requests** from the search results, and then install the extension.

        After the installation is complete, you may need to reload Visual Studio Code for the changes to take effect. A **GitHub** icon should be added to Visual Studio Code's Activity Bar.

1. To open the GitHub Pull Requests view, select the **GitHub** icon from the Activity Bar.

    If prompted, sign in to your GitHub account to connect Visual Studio Code to your GitHub repositories.

1. Notice the **Pull Requests** and **Issues** sections in the GitHub view.

    The **Issues** section allows you to view and manage issues from your GitHub repositories directly within Visual Studio Code.

    > **IMPORTANT**: To save time during this training exercise, issues are resolved in batches. However, Microsoft and GitHub recommend resolving each issue individually with separate commits. The GitHub Pull Requests extension is designed to facilitate processing issues individually and in separate branches. Resolving issues individually provides better traceability, easier code reviews, and safer rollback options if problems arise.

1. Take a minute to review the issues listed under the **Issues** section.

    You should see the same issues that you reviewed earlier in the GitHub web interface.

1. 








1. Open GitHub Copilot's Chat view and ensure that the **Ask** mode is selected.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window. Verify that the chat mode is set to **Ask** and that you're using the **GPT-4.1** model.

    > **NOTE**: The GPT-4.1 model provides excellent code analysis capabilities and is included with the GitHub Copilot Free plan. Choosing a different model may yield different results.

1. Ensure that you're starting with clean chat session.

    Chat sessions help to organize your interactions with GitHub Copilot. Each session maintains its own context, allowing you to focus on specific tasks or issues. The conversation history within a session provides continuity, enabling GitHub Copilot to build on previous interactions for more accurate and relevant responses. This chat conversation will focus on analyzing and resolving security vulnerabilities in the ContosoShopEasy application. After you complete your analysis of the GitHub issues using GitHub Copilot's Ask mode, you can use the same conversation to help implement code changes using GitHub Copilot's Agent mode. GitHub Copilot can use the detailed analysis from the Ask mode to inform its code generation in the Agent mode, ensuring that the fixes align with the identified vulnerabilities and recommended remediation strategies.

    If needed, you can start a new chat session by selecting the **New Chat** button (the **+** icon at the top of the Chat panel).

1. Open the **ProductService.cs** file, and then locate the **SearchProducts** method.

1. In the code editor, select the entire **SearchProducts** method.

    Selecting code in the editor helps to focus the Chat context. GitHub Copilot uses the selected code to provide relevant analysis and recommendations.

    The **SearchProducts** method is associated with the "Fix SQL Injection Vulnerability in Product Search" issue.

1. Ask GitHub Copilot to analyze the code for SQL injection vulnerability.

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

    You should see recommendations for using parameterized queries or ORM methods that help to manage SQL injection risks. You might also see suggestions for input validation and sanitization techniques. GitHub Copilot often provides code snippets that demonstrate how to implement suggestions.

1. Open the **UserService.cs** file.

1. Select the **RegisterUser**, **LoginUser**, and **ValidateUserInput** methods, and then ask GitHub Copilot to analyze sensitive data exposure vulnerabilities.

    For example, you can submit the following prompt:

    ```text
    What sensitive information is being logged in the user registration, login, and input validation methods? Why is logging passwords and user data a security risk?
    ```

1. Review GitHub Copilot's analysis and then ask for specific remediation guidance.

    For example, after reviewing the initial analysis, you can submit the following prompt:

    ```text
    How can I modify these methods to prevent sensitive data logging? What secure logging practices should I implement to protect user information?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

   You should see recommendations for removing sensitive information from logs, such as passwords and personal data. GitHub Copilot may also suggest implementing structured logging practices, using log redaction techniques, and ensuring that logs are stored securely.

1. Select the **GetMd5Hash** method, and then ask GitHub Copilot to analyze the weak password hashing vulnerability.

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

    GitHub Copilot should provide a comparison of "PBKDF2" and "bcrypt". It should also provide code snippets demonstrating how to implement secure password hashing using these algorithms, and a list of additional security measures for password handling.

1. Open the **PaymentService.cs** file, and then locate the **ProcessPayment** method.

1. In the code editor, select the entire **ProcessPayment** method.

    The **ProcessPayment** method is associated with the "Remove Sensitive Data from Debug Logging" issue.

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

    Agent mode allows GitHub Copilot to make direct code modifications based on your instructions. Agent mode works to establish an appropriate context by reviewing relevant files in the codebase. You can add files and folders to the context manually to ensure that the agent has the necessary information to perform complex tasks.

1. Take a minute to consider your remediation strategy.

    Create a plan that's based on the analysis you completed using GitHub Copilot's Ask mode. Consider the order in which you'll address the issues, dependencies between fixes, and how to verify that each vulnerability has been successfully remediated.

    The GitHub issues, starting with the critical issues and descending by priority, are as follows:

    1. 🔐 Fix SQL Injection Vulnerability in Product Search
    1. 🔐 Replace MD5 Password Hashing with Secure Alternative
    1. 🔐 Remove Sensitive Data from Debug Logging
    1. 🔐 Remove Hardcoded Admin Credentials
    1. 🔐 Fix Credit Card Data Storage Violations
    1. 🔐 Fix Input Validation Security Bypass
    1. 🔐 Fix Predictable Session Token Generation
    1. 🔐 Improve Email Validation Security
    1. 🔐 Strengthen Password Security Requirements
    1. 🔐 Reduce Information Disclosure in Error Messages

    These issues are associated with specific files and methods in the codebase. When organized by file association, the issues are as follows:

    - **ProductService.cs**: Issue #1
    - **UserService.cs**: Issues #2 and #3
    - **PaymentService.cs**: Issue #3
    - **SecurityValidator.cs**: Issues #4, #6, #7, #8, #9, and #10
    - **Models/Order.cs**: Issue #5

    Your remediation strategy should involve addressing each issue systematically, ensuring that fixes are implemented correctly and consistently.

    To save time during this training exercise, you resolve all of the issues and then commit all code updates together. A file-based remediation strategy makes sense in this case. However, processing issues in large batches isn't a recommended best practice.

    In a production environment, it's often better to address each issue individually with separate commits. This approach provides better traceability, easier code reviews, and safer rollback options if problems arise.

1. Close all open files in the code editor.

    Closing files helps the agent focus on the files you add to the context. Files that are left open in the editor unintentionally can distract from the task at hand.

1. Add the **ProductService.cs** file to the chat context.

    The SQL injection issue is associated with the SearchProducts method in the ProductService.cs file.

1. Ask GitHub Copilot to address the SQL injection vulnerability.

    The analysis that you completed using GitHub Copilot's Ask mode revealed that the method constructs SQL queries using user input without proper sanitization. Use your analysis to construct clear task instructions that the agent can use to remediate the vulnerability.

    For example, you can assign the following task to the agent:

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
