---
lab:
    title: 'Prepare - Configure your GitHub Spec Kit lab environment'
    description: 'Review the lab requirements and configure resources for the GitHub Spec Kit exercises.'
---

# Configure your GitHub Spec Kit lab environment

Before you begin the Spec-Driven Development with GitHub Dev Kit lab exercise, you need to ensure that your development environment includes the required tools and resources.

Your lab environment must include the following resources:

- Git version 2.48 or later.
- The .NET SDK version 8.0 or later.
- Access to a GitHub account with GitHub Copilot enabled.
- Visual Studio Code with the C# Dev Kit and GitHub Copilot Chat extensions.
- SQL Server LocalDB or SQLite.
- Python version 3.11 or later.
- The uv package manager.

## Install the GitHub, .NET, and Visual Studio Code resources

The "Spec-Driven Development with GitHub Dev Kit" lab exercise uses GitHub Copilot in Visual Studio Code as the primary AI assistant. To use GitHub Copilot, you need access to a GitHub account with a GitHub Copilot subscription. GitHub requires Git for version control operations. The lab application that you'll be working on was built using .NET (ASP.NET Core 8.0 and Blazor).

Complete the following steps to ensure that the required GitHub, .NET, and Visual Studio Code tools and resources are available.

1. Ensure that Git version 2.48 or later is installed in your lab environment.

    Run the following command in a terminal window to check the installed version of Git:

    ```bash
    git --version
    ```

    If you're running Windows and you want to update Git, you can use the following command:

    ```bash
    git update-git-for-windows
    ```

    If necessary, you can download Git using the following URL: <a href="https://git-scm.com/downloads" target="_blank">Download Git</a>.

1. Ensure that Git is configured to use your name and email address.

    If required, you can use the following commands to set your Git user name and email address.

    > **NOTE**: Update the following commands with your information before you run the commands.

    ```bash
    git config --global user.name "Julie Miller"
    ```

    ```bash
    git config --global user.email julie.miller@example.com
    ```

1. Ensure that the .NET 8.0 SDK, or a later version, is installed in your lab environment.

    Installing the latest LTS or STS version of the .NET SDK is recommended, however, you can use .NET 8.0 to complete this exercise.

    Run the following command in a terminal window to check the installed version of the .NET SDK:

    ```dotnetcli
    dotnet --version
    ```

    If necessary, you can download the .NET SDK using the following URL: <a href="https://dotnet.microsoft.com/download/dotnet" target="_blank">Download .NET SDK</a>.

1. Ensure that the .NET SDK is configured to use the official NuGet.org repository as a source for downloading and restoring packages.

    For example, open a terminal window and then run the following command:

    ```bash
    dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org
    ```

1. Ensure that Visual Studio Code and the C# Dev Kit extension are installed in your lab environment.

    If necessary, you can download Visual Studio Code using the following URL: <a href="https://code.visualstudio.com/download" target="_blank">Download Visual Studio Code</a>

    You can install the C# Dev Kit extension using the Extensions view in Visual Studio Code.

1. Ensure that you have access to a GitHub account and GitHub Copilot subscription.

    You can log in to your GitHub account using the following URL: <a href="https://github.com/login" target="_blank">GitHub login</a>.

    If you don't have a GitHub account, you can create an individual account from the GitHub login page. On the login page, select **Create an account**.

    Open the settings/profile page of your GitHub account and verify that you have access to a GitHub Copilot subscription. If you have an active subscription for GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise that you can use for training, you can use your existing GitHub Copilot subscription to complete the GitHub Copilot exercises.

    If you have an individual GitHub account, but you don't have a GitHub Copilot subscription, you can set up a GitHub Copilot Free plan from Visual Studio Code during a training exercise.

    > **IMPORTANT**: The GitHub Copilot Free plan is a limited version of GitHub Copilot, allowing up to 2,000 code completions and 50 chats or premium requests per month. If you use a GitHub Copilot Free plan outside training exercises, you may exceed the plan's resource limits before completing the training. The GitHub Copilot Free plan is not available for GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscriptions.

1. Ensure that GitHub Copilot Chat is accessible in your Visual Studio Code environment.

    You can install the GitHub Copilot Chat extension using the Extensions view in Visual Studio Code.

## Install the lab application dependencies

The application that you're working on during the lab uses either a SQL Server LocalDB database or a SQLite database to store application data. SQL Server LocalDB is a lightweight version of SQL Server that's ideal for development and testing. SQLite is a self-contained, serverless database engine that's easy to set up and use.

Complete the following steps to ensure that SQL Server LocalDB is installed in your lab environment.

1. Check to see if SQL Server LocalDB is installed in your lab environment.

    Run the following command in a terminal window to check for LocalDB installation:

    ```powershell
    sqllocaldb info
    ```

    Expected output: List of LocalDB instances or an empty list if none exist. For example:

    ```output
    MSSQLLocalDB
    ```

    If the command fails or LocalDB is not installed, use the following steps to install SQL Server 2019 LocalDB. Otherwise, skip to the "Install the GitHub Spec Kit tools and resources" section.

1. To download the SQL Server 2019 Express edition installer file, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?LinkID=866658" target="_blank">SQL Server 2019 Express download</a>

1. After the download is complete, open the SQL Server 2019 installer file (for example, **SQL2019-SSEI-Expr.exe**).

1. On the SQL Server 2019 installation wizard, select **Download Media**.

1. Under **Specify SQL Server installer download**, select the **LocalDB** package, and then select the **Download** button.

1. When you see the **Download successful** message, select the **Open folder** button.

1. Run the SQL Server LocalDB installer file (for example, **SqlLocalDB.msi**), and then follow the prompts to complete the installation.

1. To verify the installation, open PowerShell or Command Prompt, and then run the following command:

    ```powershell
    sqllocaldb info
    ```

    You should see a list of LocalDB instances (or an empty list if none exist yet). For example:

    ```output
    MSSQLLocalDB
    ```

    If you need to create the default instance of MSSQLLocalDB, run the following commands:

    ```powershell
    sqllocaldb create MSSQLLocalDB
    sqllocaldb start MSSQLLocalDB
    ```

1. To download SQLite, follow the instructions at the following URL: <a href="https://www.sqlite.org/download.html" target="_blank">Download SQLite</a>.

## Install the GitHub Spec Kit tools and resources

The GitHub Spec Kit's command-line interface (CLI) tool is Python-based and requires Python 3.11 or later. The uv package manager is used to install and manage the GitHub Spec Kit CLI tool.

Complete the following steps to install and configure the GitHub Spec Kit tools and resources in your lab environment.

1. Ensure that Python 3.11 or later is installed in your lab environment.

    GitHub Spec Kit's CLI tool is Python-based and requires Python 3.11+.

    To check the installed Python version, run the following command:

    ```powershell
    python --version
    ```

    Required output: **Python 3.11.0** or later.

    If you need to install Python, you can download the installer from the following URL: <a href="https://www.python.org/downloads/" target="_blank">python.org</a>.

    If you're in a corporate environment, you can also use your organization's software distribution system.

1. Ensure that the uv package manager is installed in your lab environment.

    ```powershell
    uv --version
    ```

    You should see output similar to the following sample:

    ```output
    uv 0.9.17 (2b5d65e61 2025-12-09)
    ```

    To install uv using Windows PowerShell, run the following command:

    ```powershell
    powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"
    ```

    GitHub Spec Kit uses uv for CLI installation and management.

    You can find more installation instructions at the following URL: <a href="https://docs.astral.sh/uv/" target="_blank">docs.astral.sh/uv</a>.

1. To ensure that uv is in your environment PATH, restart your terminal window, and then run the following commands:

    ```powershell
    cd C:\
    uv --version
    ```

    You should see output similar to the following sample:

    ```output
    uv 0.9.17 (2b5d65e61 2025-12-09)
    ```

1. Open a terminal window.

    You can use a Command Prompt, PowerShell, or Terminal window.

1. To install GitHub Spec Kit's Specify CLI tool, run the following PowerShell command:

    ```powershell
    uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
    ```

    This command installs the latest version directly from the GitHub repository and makes the *specify* command available system-wide.

    The specify command-line tool is used to initialize projects for spec-driven development.

1. To ensure that the *specify* command is in your environment PATH, restart your terminal window, and then run the following command:

    ```powershell
    specify version
    ```

    After a short delay, you should see output that's similar to the following sample:

    ```output
         CLI Version    0.0.22
    Template Version    0.0.90
            Released    2025-12-04
              Python    3.14.0
            Platform    Windows
        Architecture    AMD64
          OS Version    10.0.26200
    ```

    Troubleshooting installation issues:

    - Command not found: If the *specify* command isn't recognized after installation, the *uv* tools directory might not be in your PATH. To verify the installation, run *uv tool list* command. You might need to restart your terminal or manually add the tools directory to your PATH.

    - In corporate environments with SSL interception, you might need to configure certificates. Contact your IT department for assistance.

Your GitHub Spec Kit development environment is now configured and ready.
