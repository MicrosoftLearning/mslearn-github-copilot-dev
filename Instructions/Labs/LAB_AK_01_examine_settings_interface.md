---
lab:
  title: Exercise - Examine GitHub Copilot settings and user interface features
  description: Learn how to configure GitHub Copilot settings and how to access GitHub Copilot features in Visual Studio Code.
  duration: 25 minutes
  level: 200
  islab: true
  primarytopics:
    - GitHub
    - Visual Studio Code
---

# Examine GitHub Copilot settings and user interface features

Visual Studio Code provides a seamless and customizable GitHub Copilot experience for developers. In this exercise you examine GitHub Copilot settings and explore the GitHub Copilot user interface in Visual Studio Code.

This exercise should take approximately **25** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following:

- Git 2.48 or later
- Either .NET or Python:

    - **.NET SDK 9.0** or later with Visual Studio Code with the **C# Dev Kit** extension.
    - **Python 3.10** or later with Visual Studio Code with the **Python** extension

- Access to a GitHub account with GitHub Copilot enabled.

If you're using a local PC as a lab environment for this exercise:

- For help configuring your local PC as your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320147" target="_blank">Configure your lab environment resources</a>.

- For help enabling your GitHub Copilot subscription in Visual Studio Code, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

If you're using a hosted lab environment for this exercise:

- For help enabling your GitHub Copilot subscription in Visual Studio Code, paste the following URL into a browser's site navigation bar: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

- To ensure that the .NET SDK is configured to use the official NuGet.org repository as a source for downloading and restoring packages:

    Open a command terminal and then run the following command:

    ```bash

    dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

    ```

- To configure the hosted lab environment for Python, follow these steps:

    1. To determine the version of Python installed in the hosted environment, run the following command:

        ```bash
        python --version
        ```

        If necessary, use the following steps at the following URL to Configure Python in Visual Studio Code: <a href="https://code.visualstudio.com/docs/python/python-tutorial" target="_blank">Getting Started with Python in VS Code</a>.

    1. Install the Python extension using the Extensions view in Visual Studio Code.

## Exercise scenario

You're a developer working in the IT department of your local community. The backend systems that support the public library were lost in a fire. Your team needs to develop a temporary solution to help the library staff manage their operations until the system can be replaced. Your team chose GitHub Copilot to accelerate the development process.

This exercise includes the following tasks:

1. Examine GitHub Copilot settings in Visual Studio Code.
1. Explore the GitHub Copilot user interface in Visual Studio Code.

## Examine GitHub Copilot settings in Visual Studio Code

GitHub Copilot settings are split between your GitHub account and the Visual Studio Code environment. In Visual Studio Code, settings enable you to configure the behavior of GitHub Copilot. Starting with VS Code 1.116, GitHub Copilot is a built-in extension, so you no longer need to install a separate extension to use its features. In your GitHub account, settings enable you to manage your GitHub Copilot subscription, configure the retention of prompts and suggestions, and allow or block suggestions matching public code.

> **NOTE**: The GitHub Copilot settings described in this section are specific to the Visual Studio Code environment. If you use GitHub Copilot in other environments, such as GitHub Codespaces or JetBrains IDEs, there may be other settings that are specific to those environments. Visual Studio Code's settings editor organizes Copilot settings across several categories, including Code Editing, Chat, Agent, and Inline Chat settings.

### Enable or disable GitHub Copilot features in Visual Studio Code

GitHub Copilot in Visual Studio Code is enabled by default when you activate a plan. However, you can temporarily disable GitHub Copilot features if you need to, and then re-enable them when you're ready.

Use the following steps to complete this section of the exercise:

1. Open a new instance of Visual Studio Code.

1. On the bottom status bar, select the GitHub Copilot icon.

    The Copilot status menu opens with options to manage GitHub Copilot features, including the ability to enable or disable inline suggestions and next edit suggestions (NES).

1. Notice that the Copilot status menu includes options to enable/disable **Inline Suggestions** and **Next edit suggestions**.

    You can disable inline suggestions globally or for the current file's language. You can also use the **Snooze** button to temporarily pause inline suggestions for five-minute increments, and then select **Cancel Snooze** to resume them.

1. To open the Chat menu, select the More Actions dropdown to the right of the Chat button in the title bar.

     The Chat menu includes options to open the Chat view, Quick Chat, and Inline Chat interfaces, as well as options to manage settings for each of those interfaces.

1. On the Chat menu, select **Configure Inline Suggestions**.

1. Notice that the Configure Inline Suggestions menu provides an option to **Disable Inline Suggestions**.

1. To open Visual Studio Code's Extensions view, select the Extensions icon on the left menu bar.

1. In the Extensions view, enter **GitHub Copilot Chat** in the search bar.

1. To open the GitHub Copilot Chat settings menu, select the gear icon on the GitHub Copilot Chat extension.

1. Notice the options to Enable AI Features and Disable AI Features.

The GitHub Copilot Chat settings menu includes options to enable or disable GitHub Copilot features for specific languages. For example, you can disable GitHub Copilot for Markdown files if you don't want suggestions when working on documentation.

If you want to test the enable/disable options, you can select the disable option. However, be sure to re-enable GitHub Copilot before you continue with this exercise.

### Examine settings for the GitHub Copilot Chat extension

Default settings are configured for you when you activate GitHub Copilot in Visual Studio Code. The settings are organized under the Extensions label, which includes settings for GitHub Copilot Chat. You can customize settings for GitHub Copilot using Visual Studio Code's settings tab.

Use the following steps to complete this section of the exercise:

1. On Visual Studio Code's top menu bar, open the Chat menu (next to the Chat button in the title bar).

    The Chat menu includes a **Configure Code Completions** option that provides the option to **Edit Settings** associated with GitHub Copilot.

1. On the Chat menu, select **Configure Code Completions**, and then select **Edit Settings**.

1. Take a moment to review how the GitHub Copilot settings are organized.

    Notice that the left side of the settings editor shows the settings organized into sections, including a section for Extensions. The extensions section should include the GitHub Copilot Chat extension. The right side of the screen shows the settings for the selected section (all settings are displayed when no section is selected). You can use the search bar at the top of the settings editor to filter settings by keyword.

1. Under the Extensions label, select **GitHub Copilot Chat**.

    Notice that the settings list is now filtered for GitHub Copilot Chat settings only.

    The GitHub Copilot Chat extension has a long list of available settings and it's updated regularly. The GitHub Copilot Chat extension also includes preview and experimental settings that are subject to change and could be discontinued. The preview and experimental settings are included at the end of the list and they're tagged as either **Preview** or **Experimental**.

1. Take a couple of minutes to review the settings for GitHub Copilot Chat.

    We recommend keeping the default settings during this training. This helps to ensure that you have the expected experience when working through the exercises. When you have completed the training, you can experiment with these settings to customize your experience.

1. In the search bar at the top of the settings editor, type **GitHub Copilot Enable**.

    You should now see the setting that can be used to enable or disable GitHub Copilot completions for specified languages.

1. In the list of languages, select **markdown**.

    The default value for Markdown is set to **false**. This means that GitHub Copilot completions are disabled for Markdown files.

1. To enable GitHub Copilot for Markdown files, select **Edit Item** (pencil icon), select **false**, change the value to **true**, and then select **OK**.

    You can now use GitHub Copilot to help you author or update Markdown files. For example, GitHub Copilot can generate code completion suggestions when you're working on project documentation.

1. Close the Visual Studio Code settings editor.

## Explore the GitHub Copilot user interface in Visual Studio Code

Visual Studio Code seamlessly integrates GitHub Copilot's AI features into your development environment.

GitHub Copilot's features are organized into the following categories:

- Agents: An agent is an AI assistant that works autonomously to complete a coding task. Give it a high-level goal, and it breaks the goal into steps, edits files across your project, runs commands, and self-corrects when something goes wrong. Agents can run locally, in the background, or in the cloud.

- Natural language chat: Visual Studio Code works with GitHub Copilot to provide multiple chat interfaces: Chat view, Quick Chat, and Inline Chat. The Chat view supports three modes (Ask, Plan, and Agent) that you can switch between during a conversation.

- Inline suggestions: GitHub Copilot provides two kinds of inline suggestions as you type. Ghost text suggestions provide dimmed code suggestions at your current cursor location. Next edit suggestions (NES) predict both the location and content of your next edit based on changes you're already making.

- Smart actions: GitHub Copilot automates common tasks with Smart actions to eliminate repetitive prompt writing.

GitHub Copilot's productivity features are easy to access and fit seamlessly into your workflow without interrupting your coding experience.

### Explore the Chat view features

Visual Studio Code's Chat view provides a comprehensive interface for interacting with GitHub Copilot. The Chat view is a unified experience that supports Ask, Plan, and Agent modes, so you can switch between asking questions, planning a task, and running an agent all within the same conversation. The Chat view also provides features for managing your chat sessions, such as saving and loading chat history, adding context to your chats, and selecting different models for generating responses.

Use the following steps to complete this section of the exercise:

1. To toggle the Chat view from open to closed, select the **Chat** button (or press **Ctrl+Alt+I**).

    The Chat button (labeled **Toggle Chat** in the user interface) is located at the top of the Visual Studio Code window, just to the right of the search textbox.

1. To toggle the Chat view from closed to open, select the **Chat** button again.

    The default location for the Chat view is the Secondary Side Bar on the right side of the Visual Studio Code window. A **Views and More Actions** button (three dots on the top menu of the Chat view) can be used to open a context menu with options for moving the Chat view to different locations, opening the Chat view in an editor tab, or opening it in a separate window.

1. Take a few minutes to examine the Chat view interface.

    Starting from the top and moving down, the Chat view includes the following interface elements:

    - Chat view toolbar: The Chat view toolbar is located in the top right corner of the Chat view. You can use the toolbar to manage the chat history, start a new chat, open the Chat view in another location, or hide the Chat view. Hover your mouse pointer over the toolbar button icons to see a description.

    - Chat response area: The Chat response area is the space below the Chat view toolbar where GitHub Copilot displays responses. Responses include code suggestions, explanations, interactive elements, and other information related to your prompt.

    - Ask Copilot: The Ask Copilot text box is where you enter your prompts. You can use the Ask Copilot text box to ask GitHub Copilot questions about your codebase, request code suggestions, or ask for help with specific tasks.

    - Add Context button: The Add Context button is located in bottom section of the Chat view. You can use this button search for resources that add context to Chat session. The resources can be anything from internal project files to public repositories on GitHub that are external to your organization.

    - Start Voice Chat button: The microphone button in the Chat input area starts a voice chat session with GitHub Copilot. Voice Chat requires the **VS Code Speech** extension to be installed (the microphone icon does not appear if the VS Code Speech extension isn't installed). When a voice session is active, your spoken input is transcribed and submitted as a chat prompt.

    - Chat Mode menu: The Chat Mode dropdown menu is located to the right of the Start Voice Chat button. Based on your specific needs, you can choose between different modes of chat:

        - **Ask**: Use this mode to ask GitHub Copilot questions about your codebase. You can use Ask mode to explain code, suggest changes, or provide information about the codebase.
        - **Plan**: Use this mode to plan code changes in your workspace before implementing them. When you select the Plan mode, GitHub Copilot provides a structured response that breaks down the task into smaller steps, helping you understand the overall approach before any code changes are made. The Plan mode is usually reserved for highly complex tasks.
        - **Agent**: Use this mode to run GitHub Copilot as an agent. You can use GitHub Copilot to run commands, execute code, or perform other tasks in your workspace.

    - Pick Model menu: The Pick Model menu is located to the right of the Chat Mode menu. You can use this button to select the model that GitHub Copilot uses to generate responses. The default model is currently GPT-4.1, but newer models are available. Model selections may be limited based on your GitHub Copilot subscription, your GitHub Copilot settings, and the models available in your region.

    - Configure Tools button: The Configure Tools button is located to the right of the Pick Model menu. You can use this button to manage tools that GitHub Copilot can use in Agent mode. For example, you can use the Configure Tools menu to connect GitHub Copilot to your codebase, terminal, or other resources that an agent might need to access when performing a task.

    - Send button: The Send button is located to the right of the Pick Model menu. You can use this button to submit your prompt to GitHub Copilot and receive a response. The menu includes several options for how your prompt is submitted.

    - Set Session Target menu: The Set Session Target menu is located in the bottom left corner of the Chat view. You can use this menu to select where the agent runs. For example, you can select to run the agent locally in the VS Code editor, run the agent in the background using Copilot CLI, run the agent remotely in the cloud, or use a third-party agent harness and SDK such as Anthropic's Claude.

    - Set Permissions button: The Set Permissions button is located to the right of the Delegate Session button. You can use this button to manage permissions for the current chat session. For example, you can use this menu to allow or restrict GitHub Copilot's access to your codebase, terminal, or other resources.

1. Use the Ask Copilot textbox to enter the following prompt, and then submit the prompt:

    **For C#:**

    ```text
    Create a C# console app that prints Hello World to the console.
    ```

    **Or for Python:**

    ```text
    Create a Python console app that prints Hello World to the console.
    ```

1. Take a minute to review the response.

    Notice that GitHub Copilot's response provides instructions for creating a console app project, but doesn't offer to complete the task on your behalf.

1. To create the console app project, follow the instructions provided in the Chat view response.

    For example:

    Open Visual Studio Code's integrated terminal, and then run the following commands:

    **For C#:**

    ```bash
    dotnet new console -n HelloWorldApp
    code HelloWorldApp
    ```

    **Or for Python:**

    ```bash
    mkdir HelloWorldApp
    cd HelloWorldApp
    echo "print('Hello World')" > Program.py
    code .
    ```

### Explore the Quick Chat features

The Quick Chat window is a simplified interface for interacting with GitHub Copilot. It provides a quick way to ask questions, request code suggestions, or get help with specific tasks without leaving the code editor.

Use the following steps to complete this section of the exercise:

1. Open Visual Studio Code's Chat menu.

    The Chat menu includes options such as:

    - Open Chat         **Ctrl+Alt+I**
    - Open Inline Chat  **Ctrl+I**
    - Open Quick Chat   **Ctrl+Shift+Alt+L**

1. On the Chat menu, select **Open Quick Chat**.

    By default, the Quick Chat window opens at the top center of the Visual Studio Code window.

1. Notice that the Quick Chat window provides a subset of the options provided by the Chat view.

1. Use the Quick Chat window to submit the following prompt:

    ```text
    Tell me about the Program.cs file
    ```

1. Take a moment to review the response.

    As long as you haven't opened the Program.cs file in the editor or added it to the Quick Chat context, GitHub Copilot is unable to provide specific information about the file. It should ask you to add the file to the context.

1. To add your Program.cs file to the Quick Chat context, drag-and-drop the Program.cs file from the Explorer view to the very top of the Quick Chat window.

1. Notice that the Quick Chat window now includes **Program.cs** just below the prompt textbox.

    > **TIP**: Adding context to the Chat helps GitHub Copilot provide more relevant responses. When adding project files to the Chat context, it's often easier to use a drag-and-drop operation rather than the Add Context button.

1. Scroll to the top of the Quick Chat window and resubmit the same prompt:

    ```text
    Tell me about the Program.cs file
    ```

1. Notice that the new response provides a detailed description of your Program.cs file, including what it does and how to run it.

1. In the top-right corner of the Quick Chat window, select **Open in Chat View**.

    Notice that the Quick Chat window closes and the Chat view opens with responses that appeared the Quick Chat window. If the Chat view doesn't display the Quick Chat session, use the Copilot Chat menu to open the Quick Chat window, and then select **Open in Chat View**.

    Switching to the Chat view is useful when you need to extend and manage a chat session that started in the Quick Chat window.

    > **TIP**: The Quick Chat window is great for quick questions and simple tasks. However, if you want a more dedicated Chat environment, you should use the Chat view.

### Review the Inline Chat features

Inline Chat lets you interact with GitHub Copilot directly in the code editor without switching to the Chat view. Press **Ctrl+I** to open a chat prompt at your cursor position, describe a change, and Copilot suggests edits in place.

The Inline Chat V2 experience (enabled by the `inlineChat.enableV2` setting) uses the same editing logic as the Chat view's Agent mode under the hood, which means it has access to the same context and produces higher-quality edits. Inline Chat V2 also supports a lightweight mode: when the `inlineChat.hideOnRequest` setting is enabled, the inline chat input hides as soon as you submit a prompt, and the changes appear through the standard chat-editing overlay with **Keep** and **Undo** controls.

When a file belongs to an active agent session, pressing **Ctrl+I** opens "Ask in Chat" in the Chat view instead of regular inline chat. This routes your prompt into the existing session so it can use the full conversation context. You can disable this behavior by setting `inlineChat.askInChat` to `false`.

You can also add a code selection or file to an existing Chat session without opening Inline Chat. Right-click selected code in the editor and choose **Add Selection to Chat**, or right-click a file in the Explorer and choose **Add File to Chat**. This attaches the code or file as context in the Chat view, so you can ask questions or request edits that reference it.

### Compare the Chat view's Ask and Agent modes

The Chat view has three modes: **Ask**, **Plan**, and **Agent**. The Ask mode is designed for asking questions and generating code suggestions. The Agent mode is designed for autonomous coding tasks where Copilot can search your workspace, edit files, run terminal commands, and use tools. The Plan mode is designed for planning complex tasks by breaking them down into smaller steps.

> **NOTE**: Edit mode is deprecated as of VS Code 1.110 and will be removed in a future release. Agent mode provides a superset of Edit mode's capabilities. The following steps use Ask and Agent modes instead.

Use the following steps to complete this section of the exercise:

1. Open the Program.cs file in the code editor, and then replace the existing code with the following code snippet:

    ```csharp

    using System;
    
    namespace HelloWorldApp;
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
        }
    }

    ```

1. Ensure that you have the Chat view open and that the **Ask** agent is selected.

1. Select the following code:

    ```csharp

    static void Main(string[] args)
    {
        Console.WriteLine("Hello World");
    }

    ```

    Notice that the Chat view context is updated to specify the selected code lines in the Program.cs file.

1. In the Chat view, enter the following prompt:

    ```text

    Refactor the selected code to display "Generate equations for addition and subtraction:"

    ```

1. Take a minute to review response displayed in the Chat view.

    Notice that the response includes a code snippet. When you hover the mouse pointer over the code, the Chat view displays buttons in the upper right corner of the code that provide the following options:

    - **Apply to Program.cs**: Use the Apply to Program.cs option to apply the suggested code update to the Program.cs file.
    - **Insert at Cursor**: Use the Insert at Cursor option to insert the suggested code update at the current cursor position in the editor.
    - **Copy**: Use the Copy option to copy the suggested code update to the clipboard.

    You could use one of these options to apply the suggested code update, but for this exercise, you use Agent mode to apply the update directly.

1. In the Chat view, open the Set Agent dropdown, and then select **Agent**.

1. Submit the following prompt:

    ```text

    Refactor the selected code to display "Generate equations for addition and subtraction:"

    ```

1. Take a minute to review the updates suggested in the code editor.

    Notice the following:

    - In Agent mode, Copilot applies edits directly to your files. The code editor displays a *Diff-style* view that shows the changes, similar to the Diff view used in GitHub pull requests.
    - The code editor displays **Keep** and **Undo** buttons that you can use to apply or reject the changes made to the code.
    - The code editor displays additional buttons that can be used to manage the suggested edits.

    In addition to the edit controls displayed in the editor tab, the Chat view displays a **Keep** button that you can use to apply all edits and an **Undo** button to cancel the edits, and an abbreviated description of the suggested update.

1. In the Chat view, select **Keep** to apply all suggested code updates.

> **NOTE**: In Agent mode, Copilot may also run terminal commands, search your workspace for context, and use other tools autonomously. By default, Copilot asks for your approval before running terminal commands. To conserve GitHub Copilot resources, this exercise uses a simple prompt that only requires file edits.

### Explore code completion and next edit suggestions

GitHub Copilot provides two kinds of inline suggestions as you type:

- **Ghost text suggestions (code completions)**: Dimmed text appears at your current cursor position suggesting the completion of the current line or a block of new code.
- **Next edit suggestions (NES)**: Copilot predicts both the location and content of your next edit based on the changes you're already making. A gutter arrow appears to indicate where the next suggested edit is located.

Both features are enabled by default and can be configured via the GitHub Copilot status menu in the Status Bar.

#### Explore ghost text code completions

Use the following steps to complete this section of the exercise:

1. With the Program.cs file open in the code editor, position the cursor at the end of the Console.WriteLine statement.

1. To generate a code completion suggestion, press **Enter**.

    After a moment, GitHub Copilot generates a code completion suggestion (ghost text) based on the context of the code in the editor.

1. To accept the code completion suggestion, press **Tab**.

    The code in the editor is updated to include the suggested code lines.

    When you accept a code completion suggestion, GitHub Copilot may suggest additional code lines. When this happens, you can press the **Tab** key to accept the suggestion, press the **Esc** key to reject the suggestion, or enter your own code to override the suggestion.

#### Explore next edit suggestions (NES)

Next edit suggestions predict your next code edit based on changes you've already made. For example, if you rename a variable on one line, NES suggests updating the same variable name on other lines.

Use the following steps to complete this section of the exercise:

1. Ensure that you have the Program.cs file open in the code editor.

1. Replace the existing Program class with the following code snippet:

    ```csharp

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generate equations for addition and subtraction:");
            for (int i = 0; i < 5; i++)
            {
                int num1 = new Random().Next(1, 100);
                int num2 = new Random().Next(1, 100);
                Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
                Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
            }
    
        }
    }

    ```

1. In the code editor, select the variable name **num1**.

1. To change the variable name, type **position1**

1. Look for the next edit suggestions.

    You should see next edit suggestions to update the variable name **num1** to **position1** on the other lines of code in the Main method. You may also see next edit suggestions to change the **num2** variable name to **position2**. The suggestions may not appear on all lines at once. Instead, they may appear one at a time as you make changes to the code.

1. Look for an arrow indicator in the left gutter of the editor.

1. To see a list of options for the next edit suggestions, hover the mouse pointer over the arrow.

    You should see options that include accepting or rejecting the next suggestion(s).

1. Press **Tab** to "Go To / Accept" the suggested edits.

    NES helps you stay in the flow of coding by predicting related changes you need to make. Suggestions might span a single symbol, an entire line, or multiple lines, depending on the scope of the change.

### Access Smart Actions

Smart Actions are a set of predefined actions that are available from the Copilot context menu. You can use Smart Actions to quickly perform common tasks in Visual Studio Code without having to write prompts.

Use the following steps to complete this section of the exercise:

1. Ensure that you have the Program.cs file open in the code editor.

1. In the code editor, select the Main method, right-click the selected code, and then select **Explain**.

1. Take a minute to consider the results of the smart action.

    Notice that the Explain smart action constructs a prompt that's based on the code selection and submits the prompt in the Chat view.

    The explanation includes a detailed description of the selected code, and may include suggested updates.

    For example, the explanation may include a suggestion to update the use of the `Random` class to use  rather than creating a new instance in each loop iteration.

    ```csharp

    // ...existing code...
    Random random = new Random();
    for (int i = 0; i < 5; i++)
    {
        int position1 = random.Next(1, 100);
        int position2 = random.Next(1, 100);
        // ...existing code...
    }

    ```

    The Explain smart action isn't designed as a code review tool. You could make updates based on its suggestions, but there is another option. The Review smart action.

1. Right-click the selected code again, and then select **Review**.

    You can use the Review smart action to get suggestions for improving your code, such as suggestions for improving code quality, security, performance, and adherence to best practices.

    Even if the Explain smart action didn't catch the issue above, the Review smart action should.

1. Review the suggested updates provided by the Review smart action.

    The Review smart action should recommend a single instance of the `Random` class that's reused across loop iterations, which is a best practice for generating random numbers in .NET. It may also suggest improved variable names that replace `position1` and `position2`, since the current names might not be descriptive inthe current context.

1. Select the apply options to implement the suggested changes.

    Use the **Apply and Go To Next** button for initial suggestion and the **Apply** button for the final suggestion that you want to keep. There are also options to reject suggestions if you don't want to apply them.

## Summary

In this exercise, you examined GitHub Copilot settings and explored the GitHub Copilot user interface in Visual Studio Code. You configured Copilot settings and explored the Chat view, including Ask mode for questions and Agent mode for autonomous code edits. You used Quick Chat for lightweight interactions and reviewed how Inline Chat integrates with the editor. You also used ghost text code completions and next edit suggestions (NES) to accelerate coding, and applied Smart Actions such as Explain and Review to analyze and improve your code.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. If you made any changes, revert them now.
