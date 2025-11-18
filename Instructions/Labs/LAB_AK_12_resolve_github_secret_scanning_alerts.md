<!-- ---
lab:
    title: 'Exercise - Resolve GitHub secret scanning alerts using GitHub Copilot'
    description: 'Learn  how to view and interpret Alerts on the repository Security tab, and how to use Copilot’s Chat modes (Ask and Agent) to remediate problems.'
--- -->

# Resolve GitHub secret scanning alerts using GitHub Copilot

GitHub secret scanning is a security feature that helps identify and prevent the exposure of sensitive information, such as API keys, passwords, and other secrets, in your code repositories. When a secret is detected, GitHub generates an alert to notify repository administrators and maintainers about the potential security risk.

In this exercise, you use GitHub Copilot to help you analyze and resolve GitHub secret scanning alerts that relate to sensitive information in the code repository of an e-commerce application.

This exercise should take approximately **40** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following resources: Git 2.48 or later, .NET SDK 9.0 or later, Visual Studio Code with the C# Dev Kit extension, and access to a GitHub account with GitHub Copilot enabled.

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

You're a software developer working for a consulting firm. Your clients need help with resolving secret scanning alerts in their GitHub repositories. You need to ensure that all alerts are addressed and closed. You use Visual Studio Code as your development environment and GitHub Copilot to assist with development tasks. You're assigned to the following legacy app:

- ContosoOrderProcessor: A legacy e-commerce order processing workflow, including customer validation, payment processing, email notifications, and database operations. The code contains (intentionally) exposed secrets that demonstrate common security vulnerabilities: hard-coded database connection strings with embedded credentials, payment provider API keys (Stripe, PayPal), email service credentials (SendGrid), and cloud infrastructure access keys (AWS, Azure Storage).

This exercise includes the following tasks:

1. Import the ContosoOrderProcessor repository.
1. Review security alerts on GitHub.
1. Review the code project in Visual Studio Code.
1. Configure environment variables and run the application.
1. Use GitHub Copilot's Ask mode to analyze secret scanning alerts.
1. Use GitHub Copilot's Agent mode to remediate secret scanning alerts.
1. Commit and push changes to GitHub.
1. Enable and test Push Protection.

### Import the ContosoOrderProcessor Repository

GitHub Importer allows you to create a copy of an existing repository in your own GitHub account, giving you full control over the imported copy.

In this task, you import the ContosoOrderProcessor repository.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

1. Sign in to your GitHub account, and then open your repositories tab.

    You can open your repositories tab by clicking on your profile icon in the top-right corner, then selecting **Repositories**.

1. On the Repositories tab, select the **New** button.

1. Under the **Create a new repository** section, select **Import a repository**.

    The **Import your project to GitHub** page appears.

1. On the Import your project to GitHub page, under **Your source repository details**, enter the following URL for the source repository:

    ```plaintext
    https://github.com/MicrosoftLearning/resolve-github-security-alerts-lab-project
    ```

1. Under the **Your new repository details** section, in the **Owner** dropdown, select your GitHub username.

1. In the **Repository name** field, enter **ResolveGitHubSecurityAlerts**.

    GitHub automatically checks the availability of the repository name. If this name is already taken, append a unique suffix (for example, your initials or a random number) to the repository name to make it unique.

1. Ensure that the repository is set to **Public**.

    Secret Scanning is enabled by default for public repositories.

1. Select the **Begin import** button.

    GitHub uses the import process to create the new repository in your account.

    > **NOTE**: It can take a minute or two for the import process to finish.

1. Wait for the import process to complete, and then open the **ResolveGitHubSecurityAlerts** repository.

    The ResolveGitHubSecurityAlerts repository contains the ContosoOrderProcessor application code, which includes intentionally exposed secrets for training purposes.

### Review security alerts on GitHub

GitHub's secret scanning feature detects tokens, passwords, and other secrets that are accidentally committed to a repository. Security alerts are generated when secrets are detected, providing information about the type of secret, its location in the code, and recommendations for remediation.

In this task, you examine the repository and review the security alerts generated by GitHub Secret Scanning.

Use the following steps to complete this task:

1. Take a minute to review the repository's README.md file.

    The README.md file provides an overview of the ContosoOrderProcessor application, including a description of the intentionally exposed secrets and instructions for running the application.

    > [!IMPORTANT]
    > Notice that you need to set up environment variables before running the application.

1. Select the **Settings** tab.

1. In the left sidebar, select **Advanced Security**.

    The Advanced Security page displays various security features that you can enable for your repository.

1. Scroll to the bottom of the page, and then verify that **Secret Protection** and **Push protection** are enabled.

    > **NOTE**: For public repositories, secret scanning (Secret Protection and Push protection) are enabled by default. For private repositories, you must enable secret scanning manually.

1. At the top of the page, select the **Security** tab.

    The Security tab displays a security overview for your repository, including any security advisories, Dependabot alerts, code scanning alerts, and secret scanning alerts.

    You can find the Security tab at the top of the repository page (alongside Code, Issues, Pull Requests, etc.).

1. In the left sidebar, under the **Vulnerability alerts** section, select **Secret scanning**.

    The Secret scanning alerts page displays a list of all secret scanning alerts that have been detected in your repository. Each alert includes information about the type of secret, the file and line number where the secret was found, and the status of the alert (open, resolved, or dismissed).

1. Take a minute to review the Secret scanning alerts page.

    You should see eight alerts with the "Open" status. The following alerts should be included in the list:

    - ✓ Stripe API Key - PaymentService.cs.
    - ✓ Square Access Token - PaymentService.cs.
    - ✓ Mailgun API Key - EmailService.cs.
    - ✓ Slack API Token - AppConfig.cs.
    - ✓ Slack Incoming Webhook URL - AppConfig.cs.

    > **NOTE**: GitHub's secret scanning feature uses pattern matching to detect secrets in your codebase. The alerts you see are based on the secrets that were intentionally included in the ContosoOrderProcessor application for training purposes.

1. To view details about a specific alert, select the **Mailgun API Key** alert.

    The alert details page displays detailed information about the secret, including:

    - The file path and line number where the secret was found (Services/EmailService.cs, line 11).
    - A code snippet showing the secret in context.
    - The commit that introduced the secret.
    - Remediation steps and recommendations.

1. Take a minute to review the code snippet shown in the alert.

    Notice that the alert points to the EmailService.cs file (line 11) and shows a Mailgun API key. Exposing an email service API key is a security risk because it could allow unauthorized access to email sending capabilities, potentially leading to spam or phishing attacks.

1. Navigate back to the Secret scanning alerts page.

1. Select the **Stripe API Key** alert and review the details.

    Notice that this alert points to the PaymentService.cs file (line 8) and shows a Stripe live API key. Exposing a live payment API key is a critical security risk because it could allow unauthorized charges or access to sensitive payment data.

1. Navigate back to the Secret scanning alerts page and take a minute to review the other alerts.

    As you review the alerts, notice the following:

    - Each alert identifies a specific file and line number where the secret was found.
    - The alerts cover multiple categories of secrets.
    - All alerts are currently in the "Open" status, indicating they need to be addressed.
    - The alerts were generated based on pattern matching of known secret formats.

### Review the code project in Visual Studio Code

The ContosoOrderProcessor application is a C# console application that simulates an e-commerce order processing workflow. You can use Visual Studio Code to review the structure and behavior of the application.

In this task, you clone the repository to your local development environment and review the project in Visual Studio Code.

Use the following steps to complete this task:

1. Navigate back to the root page of your repository.

    The root page displays the list of files and folders in the repository.

1. Clone the ResolveGitHubSecurityAlerts repository to your local development environment.

    For example:

    1. Open the **Code** button dropdown.

    1. Select the **Copy URL to clipboard** icon to copy the repository URL.

    1. Open a terminal or command prompt on your local machine.

    1. Navigate to the directory where you want to clone the repository.

    1. Run the following command to clone the repository:

        ```bash
        git clone https://github.com/<your-account>/ResolveGitHubSecurityAlerts.git
        ```

        Replace **`<your-account>`** with your GitHub username.

1. Open the cloned repository in Visual Studio Code.

    For example:

    1. Launch Visual Studio Code.

    1. On the **File** menu, select **Open Folder**.

    1. Navigate to the directory where you cloned the repository.

    1. Select the **ResolveGitHubSecurityAlerts** folder and then select **Select Folder**.

1. Ensure that you're using the latest version of Visual Studio Code and that you have GitHub Copilot installed and enabled.

    You can verify that Visual Studio Code is up to date by selecting the Manage icon (gear icon) in the lower-left corner, then selecting **Check for Updates**.

    You can verify that GitHub Copilot is enabled by selecting the Copilot icon in lower-right corner, or by opening the Chat view and ensuring that the Chat features are active.

1. Use Visual Studio Code's EXPLORER view to expand the ContosoOrderProcessor folder, and then take a minute to review the project structure.

    The ContosoOrderProcessor application follows a simple layered architecture. It includes the following files and folders:

    - **Configuration**: Contains the AppConfig.cs file with application-wide configuration constants (including exposed secrets).
    - **Models**: Contains the Customer.cs and Order.cs model classes.
    - **Security**: Contains the SecurityValidator.cs file with security validation logic.
    - **Services**: Contains service classes for database access, email sending, and payment processing (DatabaseService.cs, EmailService.cs, PaymentService.cs).
    - **Program.cs**: The main entry point that demonstrates the order processing workflow.

1. Open the **Program.cs** file in the code editor.

1. Take a minute to review the code in the **Program.cs** file.

    Notice that the Main method performs the following tasks:

    1. First, the Main method configures the application settings using the AppConfig class.

        Notice the ValidateRequiredConfiguration method at the bottom of the Program.cs file. This method validates that all required configuration values are present before the application starts. It checks for configuration data in appsettings.json and secrets in environment variables (AWS credentials, SendGrid API key, PayPal credentials, Azure storage connection, database connection, GitHub token, and NPM token). If any are missing, it displays a formatted error message listing the missing items, instructs the user to run the setup-secrets.ps1 script, and exits the application with an error code.

        > [!NOTE]
        > The application terminates early if the configuration isn't validated successfully. For example, if the environment variables haven't been set. This prevents the application from running with incomplete or incorrect settings.

    1. Once the configuration is validated, the Main method simulates the following order processing workflow:

        - Creates a new order.
        - Validates customer information.
        - Processes payment using the PaymentService.
        - Sends email notifications using the EmailService.
        - Stores order details in the database using the DatabaseService.

1. Expand the **Configuration** folder and then open the **AppConfig.cs** file in the code editor.

1. Take a minute to review the **AppConfig.cs** file.

    AppConfig.cs is a static configuration class that manages all application settings, credentials, and feature flags for ContosoOrderProcessor. It implements a dual-source configuration pattern where some secrets are loaded securely from IConfiguration (environment variables) with fallback chains, while others remain intentionally hard-coded as public constants for training purposes. The class provides utility methods for initialization, configuration validation, and credential retrieval.

### Configure environment variables and run the application

Environment variables can be used to manage sensitive information, such as API keys and connection strings, without hard-coding them into your application's source code. Although environment variables provide a convenient way to manage secrets during development and testing, they have limitations in terms of security and scalability. Azure Key Vault and other secret management solutions are recommended for production deployments.

In this task, you create a PowerShell script to set up environment variables containing secrets required to run the ContosoOrderProcessor application. You then run the application to observe its behavior.

Use the following steps to complete this task:

1. At the root of your project, create a script file named **setup-secrets.ps1**.

1. Add the following PowerShell code to the **setup-secrets.ps1** file:

    ```powershell
    # This script sets environment variables for the current PowerShell session only.
    #
    # These variables are required to run the application.
    #
    # IMPORTANT NOTES:
    #   • These variables are set for THIS PowerShell session only
    #   • Variables will be lost when you close this window
    #   • All secret values are FICTIONAL for training purposes only
    #    
    Write-Host "════════════════════════════════════════════════════════" -ForegroundColor Cyan
    Write-Host "  Contoso Order Processor - Environment Setup Script   " -ForegroundColor Cyan
    Write-Host "════════════════════════════════════════════════════════" -ForegroundColor Cyan
    Write-Host ""
    
    Write-Host "Setting environment variables for current session..." -ForegroundColor Yellow
    Write-Host ""
    
    # AWS Credentials
    $env:Aws__AccessKeyId = "AKIA1234567890EXAMPLE"
    $env:Aws__SecretAccessKey = "1234567890abcdefghijklmnopqrstuvwxyzABCD"
    Write-Host "✓ AWS credentials configured" -ForegroundColor Green
    
    # SendGrid API Key
    $env:SendGrid__ApiKey = "SG.1234567890abcdefghij.1234567890abcdefghijklmnopqrstuvwxyzABCDEF"
    Write-Host "✓ SendGrid API key configured" -ForegroundColor Green
    
    # PayPal Credentials
    $env:PayPal__ClientId = "AY1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmno"
    $env:PayPal__ClientSecret = "EJ1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklm"
    Write-Host "✓ PayPal credentials configured" -ForegroundColor Green
    
    # Azure Storage Connection String
    $env:Azure__StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=contosostorageacct;AccountKey=TRAINING-PURPOSE-ONLY-NOT-A-REAL-KEY-1234567890abcdefghijklmnopqrstuvwxyzABCDEFGH==;EndpointSuffix=core.windows.net"
    Write-Host "✓ Azure Storage connection string configured" -ForegroundColor Green
    
    # SQL Server Connection String
    $env:Database__ConnectionString = "Server=tcp:contoso-orders.database.windows.net,1433;Initial Catalog=ContosoOrdersDB;User ID=orderadmin;Password=MyPassword123!;Encrypt=True;TrustServerCertificate=False;MultipleActiveResultSets=False;Connection Timeout=30;"
    Write-Host "✓ SQL Server connection string configured" -ForegroundColor Green
    
    # GitHub Personal Access Token
    $env:GitHub__PersonalAccessToken = "ghp_1234567890abcdefghijklmnopqrstuvwxyzAB"
    Write-Host "✓ GitHub Personal Access Token configured" -ForegroundColor Green
    
    # npm Token
    $env:Npm__Token = "npm_1234567890abcdefghijklmnopqrstuvwxy"
    Write-Host "✓ npm Token configured" -ForegroundColor Green
    
    Write-Host ""
    Write-Host "════════════════════════════════════════════════════════" -ForegroundColor Cyan
    Write-Host "✓ All environment variables configured successfully!" -ForegroundColor Green
    Write-Host "════════════════════════════════════════════════════════" -ForegroundColor Cyan
    Write-Host ""
    ```

    > [!NOTE]
    > Azure Key Vault is recommended over environment variables for production deployments. Learn more: <a href="https://learn.microsoft.com/azure/key-vault/" target="_blank">Azure Key Vault documentation</a>.

1. Open Visual Studio Code's integrated terminal.

    On the top menu bar, select **Terminal**, and then select **New Terminal**.

1. To configure environment variables and then run the application, enter the following commands in the terminal:

    ```powershell
    . .\setup-secrets.ps1
    cd ContosoOrderProcessor
    dotnet run
    ```

    The **setup-secrets.ps1** script sets up environment variables containing secrets (AWS credentials, SendGrid API key, PayPal credentials, SQL Server connection string, GitHub PAT, and npm token).

    The application runs an order processing simulation that demonstrates the workflow from order creation through payment processing, email notifications, and database storage. The console output displays detailed logging information.

    > [!NOTE]
    > All environment variables are temporary and only exist in the current PowerShell window. They're lost when the terminal closes. Environment variables are used to simplify the training environment. A more secure approach would involve using Azure Key Vault or other secure secret management solutions.

1. Take a minute to review the console output generated by the ContosoOrderProcessor application.

    The application displays the entire order processing workflow, including configuration details and processing steps. Pay attention to the log messages that show how secrets are being used throughout the application. For example, you'll see connection strings, API keys, and other sensitive information being logged to the console.

1. Add a file named **OriginalConsoleOutput.txt** to the root of your project.

    You can create the file by right-clicking in the EXPLORER view, selecting **New File**, naming it **OriginalConsoleOutput.txt**.

1. Copy the console output to the **OriginalConsoleOutput.txt** file.

    You'll compare the original console output with the console output after remediating the secret scanning alerts.

### Use GitHub Copilot's Ask mode to analyze secret scanning alerts

GitHub Copilot's Ask mode provides intelligent code analysis that can help you understand security vulnerabilities, their potential impacts, and suggested remediation strategies. By analyzing the code that contains exposed secrets, you can develop a comprehensive understanding of the problems before implementing fixes.

In this task, you use GitHub Copilot's Ask mode to analyze the Stripe API key vulnerability in the PaymentService.cs file. You'll review the security risks associated with exposing the Stripe API key and gather remediation recommendations.

Use the following steps to complete this task:

1. Open GitHub Copilot's Chat view in Visual Studio Code.

    To open the Chat view, select the Chat icon from the Activity Bar on the left side of Visual Studio Code, or press `Ctrl+Alt+I` (Windows/Linux) or `Cmd+Option+I` (Mac).

1. Ensure that you're starting with a clean chat session.

    If you've been using the Chat view previously, you may want to start fresh. Select the **New Chat** button (the **+** icon at the top of the Chat panel) to begin a new conversation.

1. Use the Set Agent button to select **Ask** mode, and then use the Pick Model button to select the **GPT-4.1** model.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window.

    > **NOTE**: The **GPT-4.1** model is included with the GitHub Copilot Free plan. This model also provides excellent code analysis capabilities. Choosing a different model may yield different results.

1. Open the **Services/PaymentService.cs** file in the code editor.

1. Ask GitHub Copilot to identify the secrets exposed in the PaymentService class, analyze the secrets, and explain the security risks.

    For example, enter the following prompt in the Chat:

    ```plaintext
    Analyze the PaymentService class. Identify secrets exposed in the code. Analyze the secret types and providers. Explain the security risks associated with the identified secrets.
    ```

1. Take a minute to review GitHub Copilot's analysis.

    GitHub Copilot should provide a detailed analysis that identifies two hard-coded secrets in the PaymentService class: a Stripe API key and a Square access token. It should explain the types of secrets, their providers, and the associated security risks.

    ```md
    The `PaymentService` class exposes two hard-coded secrets:
    
    1. **Stripe API Key**
       - **Type:** API Key
       - **Provider:** Stripe
       - **Location:** `private const string StripeApiKey = "sk_live_51MqxYzABC123def456GHI789jklMNO012pqrSTU345vwxYZ678abcDEF901ghiJKL234mnoPQR567stuVWX890yzABC";`
       - **Risk:** Anyone with access to this key can make authenticated requests to Stripe, potentially initiating fraudulent transactions, accessing payment data, or causing financial loss.
    
    2. **Square Access Token**
       - **Type:** Access Token
       - **Provider:** Square
       - **Location:** `private const string SquareAccessToken = "EAAAEOuLQavTvyym5PByGZrGdRLWiL_RB0n8YF0gELTxFqLhNp6bKHRhA6P7Uv5F";`
       - **Risk:** Exposure allows attackers to access Square APIs, potentially viewing sensitive transaction data, issuing refunds, or manipulating payment flows.
    
    **Security Risks:**
    - **Credential Leakage:** Hard-coded secrets are easily discoverable in source code, especially if the repository is public or shared.
    - **Unauthorized Access:** Attackers can use these credentials to impersonate your service, access customer data, or perform malicious transactions.
    - **Compliance Violations:** Storing secrets in code may violate PCI DSS, GDPR, or other regulatory requirements.
    - **Lack of Rotation:** Hard-coded secrets are difficult to rotate, increasing the risk if they are ever compromised.
    
    **Best Practices:**
    - Remove hard-coded secrets from code.
    - Store secrets in secure configuration (environment variables, secret managers).
    - Audit and rotate exposed credentials immediately.
    
    You can find these exposed secrets in `PaymentService.StripeApiKey` and `PaymentService.SquareAccessToken`.
    ```
    






1. Locate the **StripeApiKey** constant (line 8).

1. In the code editor, select the entire line containing the StripeApiKey constant.

1. Ask GitHub Copilot to analyze the security risk.

    In the Chat input box, enter the following prompt:

    ```plaintext
    Analyze this Stripe API key. What are the security risks of having it in the source code?
    ```








1. Review GitHub Copilot's analysis.

    GitHub Copilot should explain that exposing a Stripe live API key (sk_live_*) is extremely dangerous because:

    - Attackers could make unauthorized charges
    - They could access customer payment information
    - They could create refunds or modify transactions
    - The key provides full API access with no restrictions
    - Once exposed in Git history, the key must be revoked

1. Ask for specific remediation guidance for the Stripe API key.

    In the Chat input box, enter the following prompt:

    ```plaintext
    What's the best practice for storing API keys like Stripe in a C# application? How should I refactor this code to use environment variables?
    ```

1. Review GitHub Copilot's remediation suggestions.

    You should see recommendations such as:

    - Using environment variables to store the API key
    - Reading the key with `Environment.GetEnvironmentVariable()`
    - Adding null checking to handle missing environment variables
    - Using .NET User Secrets for local development
    - Using Azure Key Vault or AWS Secrets Manager for production
    - Never committing actual API keys to source control

1. Document the Stripe API key analysis results.

    Take notes on GitHub Copilot's recommendations. You'll use these specific insights to guide the Agent mode remediation in the next task.

### Use GitHub Copilot's Agent mode to remediate secret scanning alerts

GitHub Copilot's Agent mode can help you implement security fixes by proposing code changes that replace hard-coded secrets with secure alternatives. The Agent mode goes beyond analysis to actively suggest and apply code modifications that follow security best practices.

In this task, you use GitHub Copilot's Agent mode to remediate the secret scanning alerts that you analyzed in the previous task. You'll apply the remediation strategies identified during your Ask mode analysis. You'll intentionally leave some secrets unfixed to test GitHub's Push Protection feature later.

Use the following steps to complete this task:

1. Ensure you're in GitHub Copilot's Chat view in Visual Studio Code.

1. Switch to **Agent** mode.

    At the top of the Chat panel, select the mode dropdown and choose **Agent**. This mode allows Copilot to propose and apply code edits directly.

1. Ensure the **Services/PaymentService.cs** file is open in the code editor.

    Based on your Ask mode analysis, you identified that the Stripe API key should be moved to an environment variable. Now you'll use Agent mode to implement this fix.

1. Prompt GitHub Copilot to fix the Stripe API key issue using the remediation strategy from your analysis.

    In the Chat input box, enter the following prompt that incorporates your Ask mode findings:

    ```plaintext
    In the PaymentService class, replace the hard-coded Stripe API key with code that reads it from an environment variable called STRIPE_API_KEY. Add proper null checking and error handling.
    ```

    This prompt reflects the remediation approach that GitHub Copilot recommended during your Ask mode analysis.

1. Review the changes proposed by GitHub Copilot.

    GitHub Copilot should propose changes similar to:

    ```csharp
    - private const string StripeApiKey = "<hard-coded-stripe-api-key>";
    + private static readonly string? StripeApiKey = Environment.GetEnvironmentVariable("STRIPE_API_KEY");
    ```

    GitHub Copilot might also suggest adding null checking logic to handle cases where the environment variable isn't set. This aligns with the best practices identified in your Ask mode analysis.

1. Apply the changes.

    If the proposed changes look correct and match the remediation strategy from your analysis, select **Keep** or **Accept** to apply the edit to your PaymentService.cs file. The hard-coded secret is now removed from the source code.

1. Save the file.

    Press `Ctrl+S` (Windows/Linux) or `Cmd+S` (Mac) to save your changes.

    For the purposes of this lab, you'll leave the remaining secrets exposed. You'll use these to test GitHub's Push Protection feature in a later task.

1. Take a moment to review your changes.

    You've fixed two of the secret scanning alerts (Stripe and SendGrid API keys) but intentionally left others unfixed. This allows you to:

    - Commit and push the fixes to see how GitHub handles resolved alerts
    - Test Push Protection with the remaining secrets in a later task

1. Proceed to the next task where you'll commit and push your changes.

### Commit and push changes to GitHub

After fixing some of the secret scanning alerts, you need to commit and push your changes to GitHub. This allows you to observe how GitHub updates the security alerts based on the remediated code.

In this task, you commit and push your code changes, then verify that the fixed alerts are updated in GitHub's Security tab.

Use the following steps to complete this task:

1. Open the Source Control view in Visual Studio Code.

    Select the Source Control icon from the Activity Bar on the left side, or press `Ctrl+Shift+G` (Windows/Linux) or `Cmd+Shift+G` (Mac).

1. Review the changes.

    You should see the modified files (Services/PaymentService.cs and Services/EmailService.cs) listed under **Changes**. These are the files where you removed hard-coded secrets.

1. Stage the changes.

    Click the **+** icon next to each modified file, or click the **+** icon at the top of the **Changes** section to stage all changes.

1. Commit the changes with a descriptive message.

    In the message box at the top of the Source Control view, enter a commit message such as:

    ```plaintext
    Remove hard-coded Stripe and SendGrid API keys
    ```

    Then click the **Commit** button (checkmark icon) or press `Ctrl+Enter` (Windows/Linux) or `Cmd+Enter` (Mac).

1. Push the changes to GitHub.

    Click the **Sync Changes** button or the **Push** option in the Source Control view. If you're using the command line, you can run:

    ```bash
    git push origin main
    ```

    > **NOTE**: If Push Protection is already enabled for your repository, you won't be blocked because you've removed secrets from the code rather than adding them.

1. Wait for the push to complete.

    Visual Studio Code will display a notification when the push is successful.

1. Navigate to your repository on GitHub in a web browser.

1. Open the Security tab.

    Click the **Security** tab at the top of your repository page.

1. Select **Secret scanning** from the left sidebar.

1. Review the updated alert status.

    After a few moments (GitHub may take a minute to rescan), you should see that:

    - The Stripe API key alert has been automatically closed or marked as resolved
    - The SendGrid API key alert has been automatically closed or marked as resolved
    - Other alerts (Azure SQL connection string, PayPal credentials, AWS credentials, etc.) remain open

    GitHub's secret scanning automatically detects when secrets are removed from the codebase and updates the alert status accordingly.

1. If alerts are still showing as open, manually close them.

    Click on the alert, review the resolution, and select **Close as** > **Revoked** or **Fixed**. Add a comment explaining that the secret was removed from the code.

### Enable and test Push Protection

GitHub's Push Protection feature prevents secrets from being accidentally pushed to your repository. When enabled, it scans commits for known secret patterns and blocks the push if secrets are detected, giving you a chance to remove them before they enter the repository history.

In this task, you enable Push Protection for your repository and test it by attempting to push a commit containing a dummy secret.

Use the following steps to complete this task:

1. Navigate to your repository on GitHub in a web browser.

1. Open the repository Settings.

    Click the **Settings** tab at the top of your repository page.

1. Select **Code security and analysis** from the left sidebar.

1. Locate the **Push protection** section.

    Scroll down to find the Push protection settings within the Secret scanning section.

1. Enable Push Protection.

    Click the **Enable** button next to "Push protection". If it's already enabled, you can skip this step.

    > **NOTE**: Push Protection is available for public repositories and for private repositories with GitHub Advanced Security enabled.

1. Return to Visual Studio Code.

1. Open the **Services/EmailService.cs** file in the code editor.

1. Locate the **SendGridApiKey** constant (line 9).

1. Modify the SendGrid API key to test Push Protection.

    Change the existing SendGrid API key to a different value to simulate adding a new secret:

    This simulates a developer accidentally committing a new API key to the code. Since the SendGrid key wasn't remediated in the previous task, this represents one of the remaining unfixed secrets.

1. Save the file.

1. Stage and commit the change.

    In the Source Control view, stage the EmailService.cs file and commit with a message like:

    ```plaintext
    Update SendGrid configuration
    ```

1. Attempt to push the commit.

    Click **Sync Changes** or **Push**, or use the command line:

    ```bash
    git push origin main
    ```

1. Observe the Push Protection block.

    The push should be rejected with an error message indicating that secrets were detected. The error message will list the detected secret pattern (SendGrid API Key) and provide instructions on how to proceed.

    Example error message:

    ```plaintext
    remote: error: GH013: Repository rule violations found for refs/heads/main.
    remote: 
    remote: - Push cannot contain secrets
    remote: 
    remote: Secret scanning found the following secrets:
    remote: 
    remote:   (SendGrid API Key) on line 9 of Services/EmailService.cs
    remote: 
    remote: To push your changes anyway, you can bypass this rule...
    ```

1. Read and understand the error message.

    The error confirms that Push Protection is working correctly. It prevented a secret from entering your repository. Notice that this is one of the secrets you intentionally left unfixed from the earlier tasks.

1. Revert the SendGrid API key change.

    Open Services/EmailService.cs and restore the original SendGrid API key value, or simply undo your changes.

1. Save the file.

1. Amend the commit to remove the modified secret.

    In the terminal, run:

    ```bash
    git add Services/EmailService.cs
    git commit --amend --no-edit
    ```

    Or discard the commit entirely:

    ```bash
    git reset --soft HEAD~1
    ```

1. Verify your working directory is clean.

    Ensure no dummy secrets remain in your code. The Source Control view should show no pending changes.

Push Protection has successfully prevented a secret from being pushed to your repository. In a real-world scenario, this feature would catch accidental commits of API keys, tokens, passwords, and other sensitive information before they become part of your repository history.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. For example, you might want to delete the ResolveGitHubSecurityAlerts repository. If you're using a local PC as your lab environment, you can archive or delete the local clone of the repository created for this exercise.
