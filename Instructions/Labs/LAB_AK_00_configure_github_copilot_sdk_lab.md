---
lab:
    title: 'Prepare - Configure your GitHub Copilot SDK lab environment'
    description: 'Review the lab requirements and configure resources for the GitHub Copilot SDK exercises.'
---

# Configure your GitHub Copilot SDK lab environment

Before you begin the GitHub Copilot SDK lab exercise, you need to ensure that your development environment includes the required tools and resources.

Your lab environment must include the following resources:

- Git version 2.48 or later.
- The .NET SDK version 8.0 or later.
- Access to a GitHub account with GitHub Copilot enabled.
- Visual Studio Code with the C# Dev Kit and GitHub Copilot Chat extensions.

The lab application that you'll be working on was built using .NET (ASP.NET Core 8.0 and Blazor). The data access layer of the lab application uses Entity Framework Core and SQLite. The lab application is available in a GitHub repository that you clone to your lab environment during the lab exercise.

## Install the Git, .NET, Visual Studio Code, and GitHub resources

The "GitHub Copilot SDK" lab exercise uses GitHub Copilot in Visual Studio Code as the primary AI coding assistant. To use GitHub Copilot, you need access to a GitHub account with a GitHub Copilot subscription. GitHub requires Git for version control operations. The lab application that you'll be working on was built using .NET (ASP.NET Core 8.0 and Blazor).

Complete the following steps to ensure that the required Git, .NET, Visual Studio Code, and GitHub resources are available.

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

    The starter application for this lab was developed using .NET 8.0. However, you can update the projects to use the latest LTS or STS version of the .NET SDK if you don't have .NET 8 installed.

    Run the following command in a terminal window to check the installed versions of the .NET SDK:

    ```dotnetcli
    dotnet --list-sdks
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

The GitHub Copilot SDK uses underlying engine behind GitHub Copilot CLI for AI code generation and chat interactions. To ensure that the lab environment is properly configured for the GitHub Copilot SDK exercises, you need to install and authenticate the GitHub Copilot CLI.

For more information about the GitHub Copilot CLI, see the official documentation: <a href="https://docs.github.com/en/copilot/concepts/agents/about-copilot-cli" target="_blank">About GitHub Copilot CLI</a>.

Complete the following steps to ensure that the GitHub Copilot CLI is installed and configured in your lab environment.

1. To open the official installation instructions for the GitHub Copilot CLI, use the following link:

    <a href="https://docs.github.com/en/copilot/how-tos/copilot-cli/install-copilot-cli" target="_blank">Install GitHub Copilot CLI</a>

    The GitHub Copilot CLI is available for Windows, macOS, and Linux. Follow the instructions to install the GitHub Copilot CLI for your operating system.

1. After installing the GitHub Copilot CLI, run the following command to authenticate the CLI with your GitHub account:

    ```bash
    copilot login
    ```

    This command will open a browser window where you can log in to your GitHub account and authorize the GitHub Copilot CLI to access your account.

    > **NOTE**: The authorization code is displayed in the terminal window after you run the `copilot login` command. If the browser window doesn't open automatically, you can copy and paste the authorization code into the browser to complete the authentication process.

    After authenticating, you can start using the GitHub Copilot SDK in your lab exercises. The GitHub Copilot CLI will be used by the GitHub Copilot SDK to generate AI code completions and chat responses in Visual Studio Code.
