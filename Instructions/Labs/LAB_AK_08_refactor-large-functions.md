<!-- ---
lab:
    title: 'Exercise - Refactor large functions using GitHub Copilot'
    description: 'Learn how to analyze a complex codebase and consolidate duplicated code logic using GitHub Copilot tools.'
--- -->

# Refactor large functions using GitHub Copilot

Large functions can be difficult to read, maintain, and test. They often contain multiple responsibilities and can be challenging to understand at a glance. Refactoring large functions into smaller, more focused functions can improve code readability and maintainability.

In this exercise, you review an existing project that contains a large function, analyze your options for smaller single-responsibility functions, refactor the large function into smaller functions, and test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of an existing code project and explore options for refactoring the logic. You use GitHub Copilot in Agent mode to refactor the code by extracting code sections from the large function to create smaller functions. You test the original and refactored code to ensure the refactored code works as intended.

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

1. To download a zip file containing the sample app project, open the following URL in your browser: [GitHub Copilot lab - consolidate duplicate code](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx8LabApps.zip)

    The zip file is named **GHCopilotEx8LabApps.zip**.

1. Extract the files from the **GHCopilotEx8LabApps.zip** file.

    For example:

    1. Navigate to the downloads folder in your lab environment.

    1. Right-click *GHCopilotEx8LabApps.zip*, and then select **Extract all**.

    1. Select **Show extracted files when complete**, and then select **Extract**.

1. Copy the **GHCopilotEx8LabApps** folder to a location that's easy to access, such as your Windows Desktop folder.

1. Open the **GHCopilotEx8LabApps** folder in Visual Studio Code.

    For example:

    1. Open Visual Studio Code in your lab environment.

    1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

    1. Navigate to the Windows Desktop folder, select **GHCopilotEx8LabApps** and then select **Select Folder**.

1. In the Visual Studio Code SOLUTION EXPLORER view, verify the following solution structure:

    - GHCopilotEx8LabApps\
        - ?

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help refactoring large functions in legacy applications. Your goal is to code readability and maintainability while preserving the existing functionality. You're assigned to the following app:

- ?: This is an ? app that ?. It includes ?.

This exercise includes the following tasks:

1. Review the ? codebase manually.
1. Identify potential refactoring opportunities in large functions using GitHub Copilot Chat (Ask mode).
1. Refactor large functions into smaller, more manageable functions using GitHub Copilot Chat (Agent mode).
1. Test the refactored ? code.

### Review the ? codebase manually

