---
lab:
    title: 'Prepare - Configure your lab environment for GitHub Copilot exercises'
    description: 'Review lab requirements and configure resources before starting GitHub Copilot exercises.'
---

# Configure your lab environment for GitHub Copilot exercises

Your lab environment must be configured for C# development using Visual Studio Code and GitHub Copilot. Access to a GitHub account with GitHub Copilot enabled is required.

Complete the following steps to verify that your lab environment is configured correctly:

1. Verify that Git version 2.48 or later is installed in your lab environment.

    Run the following command in a terminal window to check the installed version of Git:

    ```bash
    git --version
    ```

    If you're running Windows and you want to update Git, you can use the following command:

    ```bash
    git update-git-for-windows
    ```

    If necessary, you can download Git using the following URL: <a href="https://git-scm.com/downloads" target="_blank">Download Git</a>.

1. Verify that the latest LTS or STS version of the .NET SDK is installed in your lab environment.

    Run the following command in a terminal window to check the installed version of the .NET SDK:

    ```dotnetcli
    dotnet --version
    ```

    If necessary, you can download the .NET SDK using the following URL: <a href="https://dotnet.microsoft.com/download/dotnet" target="_blank">Download .NET SDK</a>.

1. Verify that Visual Studio Code and the C# Dev Kit extension are installed in your lab environment.

    If necessary, you can download Visual Studio Code using the following URL: <a href="https://code.visualstudio.com/download" target="_blank">Download Visual Studio Code</a>

    You can install the C# Dev Kit extension using the Extensions view in Visual Studio Code.

1. Verify that you have access to a GitHub account and GitHub Copilot subscription.

    You can log in to your GitHub account using the following URL: <a href="https://github.com/login" target="_blank">GitHub login</a>.

    If you don't have a GitHub account, you can create an individual account from the GitHub login page. On the login page, select **Create an account**.

    Open the settings/profile page of your GitHub account and verify that you have access to a GitHub Copilot subscription. If you have an active subscription for GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise that you can use for training, you can use your existing GitHub Copilot subscription to complete the GitHub Copilot exercises.

    If you have an individual GitHub account, but you don't have a GitHub Copilot subscription, you can set up a GitHub Copilot Free plan from Visual Studio Code during a training exercise.

    > **IMPORTANT**: The GitHub Copilot Free plan is a limited version of GitHub Copilot, allowing up to 2,000 code completions and 50 chats or premium requests per month. If you use a GitHub Copilot Free plan outside training exercises, you may exceed the plan's resource limits before completing the training. The GitHub Copilot Free plan is not available for GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscriptions.
