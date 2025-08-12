<!-- ---
lab:
    title: 'Exercise - Consolidate duplicate code using GitHub Copilot'
    description: 'Learn how to consolidate code that implements duplicate logic (either duplicated code or code that's similar and used to implement the same business logic) across multiple files using GitHub Copilot tools.'
--- -->

# Consolidate duplicate code using GitHub Copilot

Duplicate code logic is often introduced when different individuals or teams extend a code project with new features over several years. A rushed schedule, poor documentation, and a lack of proper code reviews can exacerbate the issue. In some cases, code from one section may be copied and pasted into another section to quickly implement a feature. Unfortunately, duplicated logic can evolve separately, implementing different variable names and control flow structures that mask the duplication. In the end, duplicated logic makes the code difficult to maintain, debug, and test.

In this exercise, you use GitHub Copilot to analyze code that contains duplicate logic, consolidate the duplicated code logic by extracting it into shared methods or functions, and then test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of the code and explore options for consolidating the logic. You use GitHub Copilot in Agent mode to refactor the code by combining duplicate logic into shared methods or functions. Consolidating duplicate code makes it easier to read, maintain, and test your code.

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

### Download sample code projects

Use the following steps to download the sample projects and open them in Visual Studio Code:

1. Open a browser window in your lab environment.

1. To download a zip file containing the sample app projects, open the following URL in your browser: [GitHub Copilot lab - develop code features](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx7LabApps.zip)

    The zip file is named **GHCopilotEx7LabApps.zip**.

1. Extract the files from the **GHCopilotEx7LabApps.zip** file.

    For example:

    1. Navigate to the downloads folder in your lab environment.

    1. Right-click *GHCopilotEx7LabApps.zip*, and then select **Extract all**.

    1. Select **Show extracted files when complete**, and then select **Extract**.

1. Copy the **GHCopilotEx7LabApps** folder to a location that's easy to access, such as your Windows Desktop folder.

1. Open the **GHCopilotEx7LabApps** folder in Visual Studio Code.

    For example:

    1. Open Visual Studio Code in your lab environment.

    1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

    1. Navigate to the Windows Desktop folder, select **GHCopilotEx7LabApps** and then select **Select Folder**.

1. In the Visual Studio Code SOLUTION EXPLORER view, verify the following solution structure:

    - GHCopilotEx7LabApps\
        - ECommerceOrderAndReturn\
            - Configuration\
            - Dependencies\
            - Models\
            - Security\
            - Services\
        - ??\
            - Dependencies\
            - ??
            - ??
            - ??

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help refactoring complex conditional logic to improve code readability and maintainability. You're assigned to the following apps:

- E-commerce pricing engine: The first app is an E-commerce Pricing Engine that calculates dynamic pricing based on various business rules. Conditionals include membership levels, order values, coupon codes, product categories, and shipping rules.
- Loan approval workflow: The second app is a Loan Approval Workflow that evaluates loan applications based on various factors. Conditionals include income, employment status, debt ratios, collateral, and credit history.

This exercise includes the following tasks:

1. Review the E-commerce pricing engine codebase.
1. Identify refactoring opportunities in the E-commerce pricing code using GitHub Copilot.
1. Refactor the E-commerce pricing code using GitHub Copilot Agent.
1. Test the refactored E-commerce pricing code.
1. (OPTIONAL) Simplify complex conditionals in the LoanApprovalWorkflow demo app.

### Review the E-commerce pricing engine codebase

1. Open the Projects – Launch Visual Studio Code and open the EcommerceApp project folder. Take a moment to explore OrderProcessor.cs, ReturnProcessor.cs, and Program.cs. Then, do the same for DataProcessingApp (CsvProcessor.cs, JsonProcessor.cs, etc.).

1. Read the Code – For each file, identify what it’s doing. You’ll quickly notice similar function names (Validate, CalculateShipping in the e-commerce app; ReadFile, Parse in the data app). As a 20-year veteran, I always scan for patterns: repeated log messages or similar control flow often signal duplication.

1. Run Each App – Open a terminal in VS Code for each project and execute dotnet run.

    Observe the console output for EcommerceApp: It should show the order being processed, validated, shipping calculated, then the return processed similarly.

    Observe output for DataProcessingApp: It will indicate reading and parsing of the files (though the files might not exist in this demo, the code handles that by returning an empty string).

1. Note the Redundancies – From the output and code, note how both Order and Return processing print “Validating …” and “Calculating shipping …”. In the data app, both processors announce file reading and parsing. 

    This redundancy is what we aim to eliminate.

    At this stage, resist the urge to immediately refactor. An experienced engineer always establishes a safety net (ensuring the code runs and understanding its function) before modifying anything. By running the code, you confirm it works and have a reference output to test against after refactoring. You’re also mentally mapping the code – noticing, for example, that if you change how Validate works, both order and return processing outcomes should change consistently. This understanding will guide you in the consolidation steps.

### Identify Duplicate Code Using Copilot Chat (Ask Mode)

Goal: Utilize GitHub Copilot Chat’s Ask Mode to analyze the code for duplicate logic. In Ask mode, Copilot acts like an intelligent advisor: it won’t edit your code, but it will answer questions and highlight patterns using the context of your open files.

1. Open Relevant Files Side-by-Side – In VS Code, open OrderProcessor.cs and ReturnProcessor.cs next to each other. Having both files visible provides context to Copilot Chat (the AI can “see” the content of open editors by default).

1. Ask Copilot – Open the GitHub Copilot Chat panel. If it’s not visible, use the Copilot extension icon or the keyboard shortcut to open it. Ensure it’s set to Ask (you’ll find a mode selector in the chat UI, likely a dropdown at the bottom; choose “Ask” mode). Now ask a question in natural language, for example: What code is duplicated in OrderProcessor and ReturnProcessor? Voice of the Expert: I often phrase questions to Copilot as if I’m asking a colleague in a code review. Be direct and specific about what you want to find.

1. Analyze Copilot’s Response – Copilot might respond with something like: “Both classes have a Validate method that checks if an ID string is non-empty, and a CalculateShipping method that returns a flat rate (5.99). These methods are essentially duplicate logic.”

    - It may even highlight the code snippets in each file. Review this carefully.
    - If Copilot’s answer is too high-level, try a more pointed question: “Show me the duplicate methods in both classes.”

1. Repeat for DataProcessingApp – Now, do the same for CsvProcessor.cs and JsonProcessor.cs. For example: Do CsvProcessor and JsonProcessor have duplicate code? You should get a similar identification of ReadFile and Parse being duplicated.

1. Cross-Verify – Double-check Copilot’s findings with your own observations. A seasoned developer trusts but verifies. Ensure there aren’t other duplicate sections (in our simple projects, the ones identified are the main culprits).

    GitHub Copilot in Ask mode is like having a smart pair-programming partner who has read your code and can point out things quickly. It’s especially useful for legacy code or large codebases where duplications might be far apart. In my experience, even if I suspect duplication, asking Copilot can save time by confirming it and sometimes catching less-obvious cases (like logically similar code that isn’t textually identical). Copilot essentially performs a static analysis: it won’t modify anything, so it’s a risk-free way to gather information. By the end of this task, you should clearly understand what needs to be consolidated and where it lives in the code.

### Consolidate Duplicate Logic Using Copilot Chat (Agent Mode)

Goal: Refactor the projects to eliminate duplicate code by introducing shared helper methods (or classes), with GitHub Copilot’s Agent Mode assisting in multi-step changes. In Agent mode, Copilot can act on your behalf in the editor: it can create files, modify code across the workspace, and execute tasks in sequence to achieve a goal. This is powerful for refactoring, but it’s essential to guide it carefully and review its changes.

1. Plan the Refactoring – Before writing any prompt, outline what needs to happen. In our case:

    For EcommerceApp: Extract the Validate and CalculateShipping logic into a common place that both OrderProcessor and ReturnProcessor can use. Perhaps a new static class OrderHelper or SharedValidator (naming can vary, but clarity is key).

    For DataProcessingApp: Extract ReadFile and Parse into a utility (maybe FileHelper).\ As an expert, I’d choose simple solutions: static helper classes with clearly named methods. That avoids altering the class hierarchy or other complex design changes in a short exercise.

1. Enable Agent Mode

    In the Copilot Chat panel, switch to Agent mode. This tells Copilot it can take actions. When using Agent mode for the first time, you may need to grant permission for it to run commands or confirm edits.

1. Prompt for Refactoring

    You can now tell Copilot what you want to do. Be specific and incremental to maintain control. For example, start with the e-commerce app: Create a static class named OrderHelper with two methods: Validate(string id) and CalculateShipping(string id). Use the logic currently duplicated in OrderProcessor and ReturnProcessor.

    Copilot should create a new file OrderHelper.cs (or it might suggest placing it in the same file; prefer a new file for clarity).

    Review Generated Code: Copilot might produce something like:

    ```csharp
    
    namespace EcommerceApp {
        public static class OrderHelper {
            public static bool Validate(string id) {
                return !string.IsNullOrEmpty(id);
            }
            public static double CalculateShipping(string id) {
                return 5.99;
            }
        }
    }
    
    ```  

    Ensure it captured the logic correctly. It likely will, since this is straightforward.

1. Integrate the Helper.

    Now instruct Copilot to update the processors to use these methods: In OrderProcessor and ReturnProcessor, use OrderHelper.Validate and OrderHelper.CalculateShipping instead of their private methods. Remove the now-unneeded duplicate methods.

    Copilot’s Agent will edit both files. Watch the diffs it proposes. It should delete the private bool Validate and private double CalculateShipping from each, and modify calls to use OrderHelper.Validate(...) etc.

    This is a critical moment: verify that only the intended changes were made and that the program’s logic remains the same. For instance, the conditional if (Validate(orderId)) should now be if (OrderHelper.Validate(orderId)). The overall flow in ProcessOrder/ProcessReturn should remain unchanged aside from the call redirection.

    If Copilot missed something or you spot an issue (maybe it forgot to remove the old methods, or a using/import is needed), you can either fix it manually or prompt Copilot (in Edit or Ask mode) to make the tweak. Don’t hesitate to intervene; even an AI pair programmer benefits from human oversight!

1. Refactor DataProcessingApp

    Repeat a similar process:

    Prompt Copilot to create a FileHelper static class with ReadFile(string path) and Parse(string content) methods (the combined logic from the two processors).

    Then prompt it to update CsvProcessor and JsonProcessor to use FileHelper.ReadFile and FileHelper.Parse. Remove their private duplicates.

    Here you might encounter a subtlety: both classes used identical code, so it should be straightforward. But ensure the namespace is correct and both can access FileHelper (if they’re in the same namespace DataProcessingApp, it’s fine).

1. Build incrementally

    After refactoring each app, build the project (dotnet build or simply running it) to catch any compile errors. Common mistakes could be: forgot to using the helper class’s namespace, or a typo in method names. Copilot is usually precise, but it’s your responsibility to ensure the code compiles and works.

1. Clean up any leftover artifacts.

    For example, if the original private methods are now unused but still present (in case Copilot didn’t remove them), delete them. Confirm the code style remains consistent (our helper methods should be public static for reuse; that’s okay here). Optionally, add comments or documentation to the new helper methods to explain their purpose. (This is something a lead dev would definitely do when introducing new shared code—document it for the team).

    Expert Insight: Refactoring with Copilot’s Agent mode feels like having an assistant who can execute your game plan across the codebase. The key is clear communication: a vague prompt like “fix the duplication” might lead to undesired changes or confusion. By specifying what helper to create and where to use it, you keep control. I recall mentoring junior devs through similar refactoring: we’d first do it manually for one method, then I’d let them handle the rest. Agent mode is like that junior dev who works fast but still needs your guidance and review. Here are some pro tips for such AI-assisted refactoring:

    - One change at a time: If you ask Copilot to do too much in one go, it might falter or produce a lot of changes that are hard to track. Bite-sized tasks (create a class, then integrate it) work best.
    - Keep an eye on the console: Agent mode may run build/test commands if it thinks it should validate the changes. This is generally good. Check the terminal output Copilot provides; if tests fail or build errors show up, the AI will often try to fix them. This is like TDD (Test-Driven Development) in fast-forward!
    - Learn from the AI’s approach: Notice how Copilot names things or structures the helper class. It might even suggest a different name or structure. For instance, it might call OrderHelper something like SharedValidator or OrderUtils. These are reasonable alternatives. As a lead dev, I’d consider if those names better convey intent. The point is to remain actively involved – accept the good suggestions, and override anything that doesn’t seem right.

### Test the Refactored Code

Goal: Validate that your changes have not broken anything and that the behavior remains correct. In professional practice, after any refactor, we ensure all functionality is intact (often via automated tests or running the app). Here, we will manually run the applications again and optionally leverage Copilot to assist in writing tests for extra confidence.

1. Run EcommerceApp

    Execute dotnet run for the EcommerceApp project. The output should match or be very similar to the pre-refactoring output from Task 1:

    You should see the order being processed, validated, shipping cost calculated, etc., and the return similarly processed.

    If anything is amiss (e.g., validation now always fails, or maybe no output for shipping cost), revisit the changes. It’s possible a mistake slipped in, like using the wrong condition or not calling the helper correctly. Fix it (with Copilot’s help if needed) and run again.

1. Run DataProcessingApp

    Do the same for the data processing project. The behavior should remain: it will attempt to read the files and report how many records were parsed. With no actual input files provided, it should simply indicate 0 records (both before and after refactoring). The key is that it runs without errors.

1. Write Unit Tests (Optional, Extra Learning): If you want to go the extra mile (which I always encourage!), you can create a simple test project or add a few test methods to verify our new helpers. For example:

    Test that OrderHelper.Validate returns false for null or "" and true for a sample ID.

    Test that OrderHelper.CalculateShipping returns 5.99 (or whatever logic you have).

    Test FileHelper.ReadFile with a known file path.

    You can ask Copilot in Ask mode, “Can you provide a unit test for OrderHelper in xUnit?” and see it generate some test code (just ensure to set up a project and include xUnit references if you go down this path).

1. Final Code Review: Finally, do a quick read-through of the modified classes and new helper classes.

    As an experienced engineer, I always do one last review to ensure consistency: Are all duplicate pieces truly consolidated? Did we leave any stray unused code? Is everything named clearly and appropriately? Clean code matters – even though this is an exercise, treating it like production code builds good habits.

    Expert Insight: Testing and verification complete the feedback loop. In real projects, this is where we run the full test suite. Since these are simple apps, the “tests” are just running the program and checking output, but the principle stands: never assume your refactor is perfect on the first try. I’ve seen even minor changes inadvertently alter behavior. For instance, if our Validate logic had subtle differences (imagine one allowed a certain prefix and the other didn’t), merging them could introduce a bug. That’s why understanding the code in Task 1 and verifying in Task 4 are so crucial.

    Moreover, writing tests for the new consolidated methods is a teachable moment: it reinforces why having one source of truth is beneficial. Now, if a business rule changes (say, shipping cost isn’t flat anymore), we update one method (OrderHelper.CalculateShipping) and all parts of the app reflect it. A unit test for that method would quickly tell us if the calculation holds for all scenarios. In contrast, before refactoring, you’d worry about updating multiple places consistently and testing them individually.

    You should now have cleaner code with no duplicate logic, and full confidence that the apps behave as intended. Congratulations – you’ve successfully used AI assistance and your own skills to improve code quality!
