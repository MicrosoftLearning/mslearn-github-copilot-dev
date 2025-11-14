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
1. Open the project in Visual Studio Code.
1. Use Copilot Ask Mode to analyze the issue.
1. Use Copilot Agent Mode to fix the code.
1. Commit and push the changes, verify alerts.
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

1. In the **Repository name** field, enter **ResolveGitHubSecurityAlerts** and then select **Begin import**.

    GitHub creates the new repository in your account with the ContosoOrderProcessor project files.

1. Wait for the import process to complete, and then open your new repository.

    > **NOTE**: It can take a minute or two to import the repository.

### Enable secret scanning and review security alerts on GitHub

GitHub's secret scanning feature detects and alerts you when tokens, passwords, and other secrets are accidentally committed to a repository. Before fixing any code, you need to enable secret scanning and review the security alerts to understand the vulnerabilities that exist in your codebase.

In this task, you enable secret scanning for your repository and review the security alerts that are generated.

Use the following steps to complete this task:

1. Open a browser window, navigate to GitHub.com, and then ensure that you're signed in to your GitHub account.

1. Open your **ResolveGitHubSecurityAlerts** repository.

    You can navigate to the repository by selecting your profile icon in the top-right corner, selecting **Your repositories**, and then selecting **ResolveGitHubSecurityAlerts**.

1. To access repository settings, select the **Settings** tab.

    On the repo's GitHub page, select the "Security" tab (alongside Code, Issues, Pull Requests, etc.).

1. In the left sidebar, select **Code security and analysis**.

    The Code security and analysis page displays various security features that you can enable for your repository.

1. Under the **Secret scanning** section, notice that secret scanning is currently disabled.

    > **NOTE**: For public repositories, secret scanning is enabled by default. For private repositories, you must enable secret scanning manually.

1. To enable secret scanning, select **Enable** for the **Secret scanning** feature.

    After a moment, the page should refresh and display the enabled status.

1. Under the **Push protection** section, notice that push protection is currently disabled.

    Push protection prevents contributors from pushing commits that contain supported secrets. You'll enable push protection later in this exercise to test how it works.

1. To view the security alerts for your repository, select the **Security** tab at the top of the page.

    The Security tab displays a security overview for your repository, including any security advisories, Dependabot alerts, code scanning alerts, and secret scanning alerts.

1. Under the **Vulnerability alerts** section, select **Secret scanning**.

    The Secret scanning alerts page displays a list of all secret scanning alerts that have been detected in your repository. Each alert includes information about the type of secret, the file and line number where the secret was found, and the status of the alert (open, resolved, or dismissed).

1. Take a minute to review the Secret scanning alerts page.

    You should see multiple alerts with the "Open" status. The alerts are grouped by secret type and include:

    - AWS credentials (AWS Access Key ID and AWS Secret Access Key)
    - Azure Storage Account Key
    - Stripe API Key
    - PayPal OAuth Client ID and Secret
    - SendGrid API Key
    - Azure SQL Connection String

    > **NOTE**: GitHub's secret scanning feature uses pattern matching to detect secrets in your codebase. The alerts you see are based on the secrets that were intentionally included in the ContosoOrderProcessor application for training purposes.

1. To view details about a specific alert, select the **Azure SQL Connection String** alert.

    The alert details page displays detailed information about the secret, including:

    - The file path and line number where the secret was found (Services/DatabaseService.cs, line 9)
    - A code snippet showing the secret in context
    - The commit that introduced the secret
    - Options to resolve or dismiss the alert

1. Take a minute to review the code snippet shown in the alert.

    Notice that the connection string includes the database server address, database name, user ID, and password. This is a critical security vulnerability because anyone with access to the repository can see these credentials and potentially access the database.

1. Navigate back to the Secret scanning alerts page.

1. Select the **Stripe API Key** alert and review the details.

    Notice that this alert points to the PaymentService.cs file (line 8) and shows a Stripe live API key. Exposing a live payment API key is a critical security risk because it could allow unauthorized charges or access to sensitive payment data.

1. Navigate back to the Secret scanning alerts page and take a minute to review the other alerts.

    As you review the alerts, notice the following:

    - Each alert identifies a specific file and line number where the secret was found
    - The alerts cover multiple categories of secrets: database credentials, payment API keys, email service credentials, cloud storage credentials, and more
    - All alerts are currently in the "Open" status, indicating they need to be addressed
    - The alerts were generated based on pattern matching of known secret formats

1. To prioritize your work, take note of the most critical alerts:

    - **Azure SQL Connection String** (Services/DatabaseService.cs) - Contains database credentials
    - **Stripe API Key** (Services/PaymentService.cs) - Payment processing credentials
    - **AWS credentials** (Configuration/AppConfig.cs) - Cloud infrastructure access
    - **SendGrid API Key** (Services/EmailService.cs) - Email service credentials

    These critical secrets represent the highest risk because they provide direct access to production services and sensitive data. You'll focus on remediating these secrets using GitHub Copilot in the following tasks.

### Clone the repository locally and open the project in Visual Studio Code

The ContosoOrderProcessor application is a C# console application that simulates an e-commerce order processing workflow. You can use Visual Studio Code to review the structure and behavior of the application.

In this task, you clone the repository to your local development environment and open the project in Visual Studio Code.

Use the following steps to complete this task:

1. Navigate back to the root page of your repository (Code tab).

1. Clone the ResolveGitHubSecurityAlerts repository to your local development environment.

    If you haven’t already, use a Git client or command line:

    ```bash
    git clone https://github.com/<your-account>/ResolveGitHubSecurityAlerts.git
    ```

    Replace `<your-account>` with your GitHub username.

1. Open the cloned repository in Visual Studio Code.

    Ensure that you're using the latest version of Visual Studio Code and that you have the GitHub Copilot and GitHub Copilot Chat extensions installed and enabled.

1. Examine the project structure in the EXPLORER view.

    The ContosoOrderProcessor application follows a simple layered architecture with the following key folders:

    - **Configuration**: Contains the AppConfig.cs file with application-wide configuration constants (including exposed secrets)
    - **Models**: Contains the Customer.cs and Order.cs model classes
    - **Security**: Contains the SecurityValidator.cs file with security validation logic
    - **Services**: Contains service classes for database access, email sending, and payment processing (DatabaseService.cs, EmailService.cs, PaymentService.cs)
    - **Program.cs**: The main entry point that demonstrates the order processing workflow

1. To observe the application's current behavior, build and run the application.

    To run the application, open the integrated terminal in Visual Studio Code (Terminal > New Terminal) and execute the following commands:

    ```bash
    dotnet build
    dotnet run --project ContosoOrderProcessor
    ```

    The application runs an order processing simulation that demonstrates the workflow from order creation through payment processing, email notifications, and database storage. The console output displays detailed logging information.

1. Take a minute to review the console output.

    The application displays the entire order processing workflow, including configuration details and processing steps. Pay attention to the log messages that show how secrets are being used throughout the application. For example, you'll see connection strings, API keys, and other sensitive information being logged to the console.

1. Add a file named **OriginalConsoleOutput.txt** to the root of your project.

    You can create the file by right-clicking in the EXPLORER view, selecting **New File**, naming it **OriginalConsoleOutput.txt**.

1. Copy the console output to the **OriginalConsoleOutput.txt** file.

    You'll compare the original console output with the console output after remediating the secret scanning alerts.

Now that you understand the application structure and behavior, you're ready to analyze the security alerts using GitHub Copilot.

### Use Copilot's Ask mode to analyze secret scanning alerts

GitHub Copilot's Ask mode provides intelligent code analysis that can help you understand security vulnerabilities, their potential impacts, and suggested remediation strategies. By analyzing the code that contains exposed secrets, you can develop a comprehensive understanding of the problems before implementing fixes.

In this task, you use GitHub Copilot's Ask mode to analyze the secret scanning alerts in your codebase.

Use the following steps to complete this task:

1. Open GitHub Copilot's Chat view in Visual Studio Code.

    To open the Chat view, select the Chat icon from the Activity Bar on the left side of Visual Studio Code, or press `Ctrl+Alt+I` (Windows/Linux) or `Cmd+Option+I` (Mac).

1. Open GitHub Copilot's Chat view, ensure that the **Ask** mode is selected and that you're using the **GPT-4.1** model.

    If the Chat view isn't already open, select the **Chat** icon at the top of the Visual Studio Code window.

    > **NOTE**: GitHub Copilot offers different chat models. The GPT-4.1 model provides excellent code analysis capabilities and it's included with the GitHub Copilot Free plan. Choosing a different model may yield different results.

1. Ensure that you're starting with a clean chat session.

    If you've been using the Chat view previously, you may want to start fresh. Select the **New Chat** button (the **+** icon at the top of the Chat panel) to begin a new conversation.

#### Analyze database connection string vulnerability

The database connection string vulnerability exists in the DatabaseService.cs file. You'll analyze this file to understand the security implications of storing credentials in code.

Use the following steps to analyze the database connection string vulnerability:

1. Open the **Services/DatabaseService.cs** file in the code editor.

1. Locate the **ConnectionString** constant (line 9).

1. In the code editor, select the entire line containing the ConnectionString constant.

    Selecting code in the editor helps to focus the Chat context. GitHub Copilot uses the selected code to provide relevant analysis and recommendations.

1. Ask GitHub Copilot to analyze the code for security vulnerabilities.

    In the Chat input box, enter the following prompt:

    ```plaintext
    Analyze this connection string for security vulnerabilities. What are the risks of storing it this way?
    ```

1. Review GitHub Copilot's analysis.

    GitHub Copilot should identify that the connection string contains hard-coded credentials including the database password. The analysis might explain risks such as:

    - Anyone with access to the repository can see the credentials
    - The credentials are visible in version control history
    - There's no way to rotate credentials without changing code
    - Compliance violations (PCI DSS, HIPAA, etc.)
    - Potential unauthorized database access

1. Ask for specific remediation guidance.

    In the Chat input box, enter the following prompt:

    ```plaintext
    What's the recommended way to store database connection strings securely in a .NET application?
    ```

1. Take a minute to review GitHub Copilot's remediation suggestions.

    You should see recommendations such as:

    - Using environment variables
    - Using .NET User Secrets for local development
    - Using Azure Key Vault or AWS Secrets Manager for production
    - Using configuration files that are excluded from version control
    - Implementing connection string builders that separate credentials from connection details

#### Analyze Stripe API key vulnerability

The Stripe API key vulnerability exists in the PaymentService.cs file. You'll analyze this file to understand the security implications of exposing payment credentials.

Use the following steps to analyze the Stripe API key vulnerability:

1. Open the **Services/PaymentService.cs** file in the code editor.

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

### Use Copilot's Agent mode to remediate secret scanning alerts

GitHub Copilot's Agent mode can help you implement security fixes by proposing code changes that replace hard-coded secrets with secure alternatives. The Agent mode goes beyond analysis to actively suggest and apply code modifications that follow security best practices.

In this task, you use GitHub Copilot's Agent mode to remediate the secret scanning alerts that you analyzed in the previous task. You'll apply the remediation strategies identified during your Ask mode analysis. You'll intentionally leave some secrets unfixed to test GitHub's Push Protection feature later.

Use the following steps to complete this task:

1. Ensure you're in GitHub Copilot's Chat view in Visual Studio Code.

1. Switch to **Agent** mode.

    At the top of the Chat panel, select the mode dropdown and choose **Agent**. This mode allows Copilot to propose and apply code edits directly.

#### Remediate the Stripe API key vulnerability

Based on your Ask mode analysis, you identified that the Stripe API key should be moved to an environment variable. Now you'll use Agent mode to implement this fix.

Use the following steps to remediate the Stripe API key vulnerability:

1. Ensure the **Services/PaymentService.cs** file is open in the code editor.

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

#### Remediate the database connection string vulnerability

Based on your Ask mode analysis, you identified that the database connection string should be moved to an environment variable or configuration file. Now you'll use Agent mode to implement this fix.

Use the following steps to remediate the database connection string vulnerability:

1. Open the **Services/DatabaseService.cs** file in the code editor.

1. Prompt GitHub Copilot to fix the database connection string issue using the remediation strategy from your analysis.

    In the Chat input box, enter the following prompt that incorporates your Ask mode findings:

    ```plaintext
    In the DatabaseService class, replace the hard-coded connection string with code that reads it from an environment variable called DB_CONNECTION_STRING. Add proper null checking and error handling.
    ```

    This prompt reflects the remediation approach that GitHub Copilot recommended during your Ask mode analysis.

1. Review the changes proposed by GitHub Copilot.

    GitHub Copilot should propose changes that remove the hard-coded connection string and replace it with an environment variable reference. The changes might look similar to:

    ```csharp
    - private const string ConnectionString = "<hard-coded-connection-string>";
    + private static readonly string? ConnectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    ```

    GitHub Copilot might also suggest null checking and error handling. This aligns with the best practices identified in your Ask mode analysis.

1. Apply the changes.

    If the proposed changes look correct and match the remediation strategy from your analysis, select **Keep** or **Accept** to apply the edit to your DatabaseService.cs file. The hard-coded credentials are now removed from the source code.

1. Save the file.

#### Leave remaining secrets unfixed

For the purposes of this lab, you'll leave the remaining secrets (SendGrid API key, PayPal credentials, AWS credentials, Azure Storage keys, etc.) unfixed. You'll use these to test GitHub's Push Protection feature in a later task.

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
