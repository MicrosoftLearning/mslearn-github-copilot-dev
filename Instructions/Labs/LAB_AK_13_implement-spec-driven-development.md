---
lab:
    title: 'Exercise - Implement a product feature using GitHub Spec Kit'
    description: 'Learn how to add new features to existing applications using GitHub Spec Kit workflows, Visual Studio Code, and GitHub Copilot.'
---

# Implement a product feature using GitHub Spec Kit

GitHub Spec Kit is an open-source toolkit that enables Spec-Driven Development (SSD) by integrating specifications with AI coding assistants like GitHub Copilot.

In this exercise, you learn how to use the GitHub Spec Kit to implement a new feature for an existing application. You begin by initializing the GitHub Spec Kit for an existing .NET project. You then use GitHub Spec Kit workflows to create the constitution, specification, plan, and tasks documents for a new app feature. Finally, you use GitHub Spec Kit's implementation workflow to implement an initial MVP version of the app that includes the new feature.

This exercise should take approximately **75** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment MUST include the following resources: Git 2.48 or later, .NET SDK 8.0 or later, Visual Studio Code with the C# Dev Kit and GitHub Copilot Chat extensions, SQL Server LocalDB, Python 3.11 or later, the uv package manager, Specify CLI, and access to a GitHub account with GitHub Copilot enabled.

For help with configuring your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2345907" target="_blank">Configure your GitHub Spec Kit lab environment</a>.

## Exercise scenario

You're a software developer working for a consulting firm. The firm is adopting a spec-driven development (SDD) approach using GitHub Spec Kit and GitHub Copilot in Visual Studio Code. Your client, Contoso Corporation, needs you to add a new "document upload and management" feature to their internal employee dashboard application (ContosoDashboard).

Contoso's business stakeholders documented the requirements for the new feature. Essentially, employees need the ability to upload work-related documents, organize them by category and project, and share them with team members. The new feature must integrate seamlessly with the existing dashboard interface while maintaining security and compliance standards.

You need to follow a spec-driven development methodology with GitHub Spec Kit to implement the new feature. The GitHub Spec Kit enables you to create the constitution.md, spec.md, plan.md, and tasks.md files that guide the development process. The SDD approach with GitHub Spec Kit ensures that the implementation aligns with business requirements and organizational constraints.

This exercise includes the following tasks:

1. Import the ContosoDashboard repository and initialize GitHub Spec Kit.
1. Review the ContosoDashboard project and GitHub Spec Kit files.
1. Generate a constitution based on repository files.
1. Create the feature specification using stakeholder requirements and the constitution.
1. Update the specification with clarified requirements.
1. Generate the technical plan using the specification and constitution.
1. Generate the tasks file using the specification, plan, and constitution.
1. Implement the tasks required for an MVP application.

## Import the ContosoDashboard repository and initialize GitHub Spec Kit

GitHub Importer can be used to create a copy of an existing repository in your own GitHub account, giving you full control over the imported copy.

In this task, you import the existing ContosoDashboard application repository to your GitHub account and initialize GitHub Spec Kit in your project directory.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

    You can log in to your GitHub account using the following URL: <a href="https://github.com/login" target="_blank">GitHub login</a>.

1. Sign in to your GitHub account, and then open your repositories tab.

    You can open your repositories tab by clicking on your profile icon in the top-right corner, then selecting **Repositories**.

1. On the Repositories tab, select the **New** button.

1. Under the **Create a new repository** section, select **Import a repository**.

1. On the **Import your project to GitHub** page, under **Your source repository details**, enter the following URL for the source repository:

    ```plaintext
    https://github.com/MicrosoftLearning/ContosoDashboard-SSD
    ```

1. Under the **Your new repository details** section, in the **Owner** dropdown, select your GitHub username.

1. Enter **ContosoDashboard** in the **Repository name** field.

    GitHub automatically checks the availability of the repository name. If this name is already taken, append a unique suffix (for example, your initials or a random number) to the repository name to make it unique.

1. To create a private repository, select **Private**, and then select **Begin import**.

    GitHub uses the import process to create the new repository in your account.

    > **NOTE**: It can take a minute or two for the import process to finish. Wait for the import process to complete.

    GitHub displays a progress indicator and notify you when the import is complete.

1. Once the import is complete, open your new repository.

    A link to your repository should be displayed. Your repository should be located at: `https://github.com/YOUR-USERNAME/ContosoDashboard`.

    You can create a local clone of your ContosoDashboard repository and then initialize GitHub Spec Kit within the project directory.

1. On your ContosoDashboard repository page, select the **Code** button, and then copy the HTTPS URL.

    The URL should be similar to: `https://github.com/YOUR-USERNAME/ContosoDashboard.git`

1. Open a terminal window in your development environment, and then navigate to the location where you want to create the local clone of the repository.

    For example:

    Open a terminal window (Command Prompt, PowerShell, or Terminal), and then run:

    ```powershell
    cd C:\TrainingProjects
    ```

    Replace `C:\TrainingProjects` with your preferred location. You can use any directory where you have write permissions, and you can create a new folder location if needed.

1. To clone your ContosoDashboard repository, enter the following command:

    Be sure to replace `YOUR-USERNAME` with your actual GitHub username before running the command.

    ```powershell
    git clone https://github.com/YOUR-USERNAME/ContosoDashboard.git
    ```

    You might be prompted to authenticate using your GitHub credentials during the clone operation. You can authenticate using your browser.

1. To navigate into your ContosoDashboard directory, enter the following command:

    ```powershell
    cd ContosoDashboard
    ```

    > **IMPORTANT**: GitHub Spec Kit must be initialized in the root directory of your cloned repository. For example, if you cloned the repository to `C:\TrainingProjects\ContosoDashboard`, ensure that you run the `specify init` command from within the `C:\TrainingProjects\ContosoDashboard` directory.

1. To initialize GitHub Spec Kit within your existing project, enter the following command:

    ```powershell
    specify init --here --ai copilot --script ps
    ```

    The command uses the following components:

    - `--here` - Initializes GitHub Spec Kit in the current directory (your existing ContosoDashboard project).
    - `--ai copilot` - Configures the project for GitHub Copilot.
    - `--script ps` - Uses PowerShell scripts (use `--script sh` for bash/zsh on macOS/Linux).

    If you're using macOS or Linux, replace `--script ps` with `--script sh`.

    The CLI detects an existing Git repository ("Current directory isn't empty") and ask for confirmation to proceed.

1. Enter **y** to continue with the initialization process.

    The CLI will:

    - Create agent prompt files in the `.github/agents/` and `.github/prompts/` directories.
    - Create template files in the `.specify/memory/` and `.specify/templates/` directories.
    - Create script files in the `.specify/scripts/powershell/` directory.
    - Update or create a settings.json file in the `.vscode/` directory.
    - Preserve all existing application files.
    - Display a success message ("Project ready").
    - Suggest some optional next steps.

## Review the ContosoDashboard project and GitHub Spec Kit files

GitHub Spec Kit works with GitHub Copilot through Visual Studio Code's chat interface. When you run "specify init --ai copilot" in your project directory, the toolkit configures your workspace to recognize "/speckit.*" commands.

In this task, you explore the ContosoDashboard project files in Visual Studio Code, verify that GitHub Spec Kit is properly initialized, and then *push* the updated files to your GitHub repository.

Use the following steps to complete this task:

1. Open the ContosoDashboard project in Visual Studio Code.

    For example, if the terminal window is still open, you can use the following command to open the project:

    ```powershell
    code .
    ```

    The `code .` command opens the current directory (ContosoDashboard) in Visual Studio Code.

    Wait for Visual Studio Code to fully load the project.

1. Take a minute to familiarize yourself with the project structure.

    Use Visual Studio Code's EXPLORER view to expand the application folders. You should see a folder structure that's similar to the following example:

    ```plaintext
    CONTOSODASHBOARD (root)
    ├── .github/
    │   ├── agents/                 (GitHub Spec Kit executable workflows that can be triggered via commands)
    │   └── prompts/                (GitHub Spec Kit prompt files that provide detailed instructions for each of the agent workflows)
    ├── .specify/                   (GitHub Spec Kit configuration)
    │   ├── memory/                 (GitHub Spec Kit stores the project constitution defining core principles and governance rules that all features must follow)
    │   ├── scripts/powershell/     (GitHub Spec Kit uses automation utilities (scripts) for creating features, setting up plans, and managing the specification workflow)
    │   └── templates/              (GitHub Spec Kit provides standardized markdown formats for specs, plans, tasks, and checklists to ensure consistent documentation across all features)
    ├── ContosoDashboard/           (Main application folder)
    │   ├── Data/                   (ApplicationDbContext.cs)
    │   ├── Models/                 (Announcement, Notification, Project, ProjectMember, TaskComment, TaskItem, User)
    │   ├── Pages/                  (_Host, _Imports, Index, Login, Logout, Notifications, Profile, ProjectDetails, Projects, Tasks, Team)
    │   ├── Properties/             (launchSettings.json)
    │   ├── Services/               (CustomAuthenticationStateProvider, DashboardService, NotificationService, ProjectService, TaskService, UserService)
    │   ├── Shared/                 (_Imports, MainLayout, NavMenu, RedirectToLogin)
    │   ├── wwwroot/                (Static files, CSS)
    │   └── Program.cs              (App configuration)
    ├── StakeholderDocs/            (Business requirements)
    └── README.md                   (Application documentation)
    ```

1. Ensure that GitHub Copilot's Chat view is open.

    Using one of the newer language models might improve the quality of responses. This lab exercise was tested using the GPT-5 and Claude Sonnet 4.5 models. Results were comparable between the two models.

1. In the Chat view, to verify that GitHub Spec Kit commands are available, type **/speckit**

    You should see autocomplete suggestions that show the available commands:

    - `/speckit.analyze` - Audit implementation plans.
    - `/speckit.checklist` - Validate specification completeness.
    - `/speckit.clarify` - Refine specifications through question and answer process.
    - `/speckit.constitution` - Define project governing principles.
    - `/speckit.implement` - Execute the implementation.
    - `/speckit.plan` - Generate technical implementation plans.
    - `/speckit.specify` - Create feature specifications.
    - `/speckit.tasks` - Break down work into actionable tasks.
    - `/speckit.taskstoissues` - Convert the tasks in tasks.md into GitHub issues.

    > **Note**: If the '/speckit.' commands don't appear, try closing and then reopening the project in Visual Studio Code.

    **Troubleshooting**: If you encounter issues:

    - **"specify command not found"**: Ensure you completed Task 1 and installed the Specify CLI. Run `specify version` to verify installation.
    - **Permission denied errors**: On Windows, ensure you're running PowerShell with appropriate permissions. On macOS/Linux, check file permissions.
    - **Git clone errors**: Verify that you're signed in to GitHub, and that you have access to your imported repository.
    - **GitHub Spec Kit commands not appearing**: Ensure `.github/prompts/` exists in your workspace root. Try reloading Visual Studio Code.

1. Ask GitHub Copilot to explain the current project and GitHub Spec Kit files.

    For example, enter the following prompt in the Chat view:

    ```plaintext
    Review the current codebase. Explain the ContosoDashboard application features and the purpose of the GitHub Spec Kit files located under the .github\ and .specify\ directories.
    ```

1. Take a couple minutes to review GitHub Copilot's response.

    GitHub Copilot's response should summarize the application features and explain the purpose of the GitHub Spec Kit files.

    You can also review the project's README.md file for a description of the current application features, mock authentication system, and security implementation.

1. Open the **ContosoDashboard/ContosoDashboard.csproj** file in the editor.

    Notice the following:

    - The project file specifies .NET 8 as the target framework. If your development environment has a different .NET SDK version installed (.NET 9 or .NET 10), you need to update the project file to target the installed version.
    - The project file includes a reference to SQL Server LocalDB for local development. If you're using a PC with an ARM processor, you need to switch from SQL Server LocalDB to SQLite for local development.

1. Ensure that the ContosoDashboard.csproj file specifies the .NET version installed in your development environment.

    You can use GitHub Copilot to update the project file. For example, to update the ContosoDashboard.csproj file for .NET 10, enter the following prompt in the Chat view:

    ```plaintext
    I have the .NET 10 SDK installed. My project was written using .NET 8. Update the .csproj file for .NET 10 and ensure that the project builds correctly?
    ```

    > **NOTE**: The ContosoDashboard application was developed using .NET 8. If the .NET 8 SDK isn't installed in your development environment, but you have the .NET 9 or .NET 10 SDK installed, the ContosoDashboard.csproj file must be updated to target the installed .NET version before you build and run the application.

1. Ensure that you're using the correct database provider for your development environment.

    If you're using a PC with an ARM processor for your development environment, you need to switch from SQL Server LocalDB to SQLite for local development. The following files are affected by this change:

    - ContosoDashboard.csproj: Update the database provider package reference.
    - Program.cs: Update the database context configuration.
    - appsettings.json: Update the connection string.

    You can use GitHub Copilot to update the your project files (ContosoDashboard.csproj, Program.cs, and appsettings.json). For example, to update the codebase for SQLite, enter the following prompt in the Chat view:

    ```plaintext
    My PC uses an ARM64 processor. I need you to update the codebase to use SQLite rather than SQL Server LocalDB.
    ```

    > **NOTE**: If you're using a PC with an x64 processor or a Mac, you can skip this step since SQL Server LocalDB works correctly in those environments.

1. In the EXPLORER view, right-click **ContosoDashboard** and then select **Open in Integrated Terminal**.

    The terminal prompt should open in the ContosoDashboard project directory. For example:

    ```plaintext
    PS C:\TrainingProjects\ContosoDashboard\ContosoDashboard>
    ```

1. To build and run the application, enter the following commands:

    ```dotnetcli
    dotnet restore
    dotnet build
    dotnet run
    ```

    Some **warning** messages are displayed when you build and run the application, but there shouldn't be any errors.

1. Wait for the application to start, then open a browser window and navigate to the localhost URL listed in the terminal.

    You should see a URL similar to `https://localhost:5000`.

    When you open the ContosoDashboard application in the browser, you should see a login page.

1. On the ContosoDashboard login page, select **Ni Kang (Employee)** from the dropdown list, and then select **Login**.

1. Take a minute to explore the ContosoDashboard application.

    The application includes basic dashboard features such as project tracking, task management, notifications, and user profile management.

    It's important to verify that the application is working, and to explore the existing features and behaviors, before you design and develop a new feature. However, you don't need to spend too much time exploring the user interface, just take a minute to observe the basic functionality.

1. Logout and then close the browser tab.

    You can minimize the browser window, but keep it open for now.

1. In Visual Studio Code's terminal panel, to stop the running application, press **Ctrl+C** and then close the terminal.

1. Use Visual Studio Code's Source Control view to commit and then push/sync the updated project files.

    For example:

    - Select the Source Control icon in the left-hand activity bar.
    - Enter a commit message such as: "Add GitHub Spec Kit files to the ContosoDashboard project"
    - Select the checkmark icon to commit the changes (and select Yes to stage the changes if prompted).
    - Select **Sync Changes** to push the commit to GitHub (and select OK if prompted).

    Pushing the GitHub Spec Kit files to your repository enables you to track the spec-driven development process.

1. Open your GitHub repository in a browser window and verify that the push succeeded.

    You should see the GitHub Spec Kit files alongside the existing application code. You might need to refresh the page to see the latest changes.

You now have a working ContosoDashboard application with GitHub Spec Kit initialized.

## Generate a constitution based on repository files

The GitHub Spec Kit uses a constitution.md file to establish the governing principles and constraints that guide all development decisions for the ContosoDashboard project. It captures organizational policies, technical standards, security requirements, and development practices that must be followed throughout implementation.

In this task, you use GitHub Copilot's `/speckit.constitution` command to generate a comprehensive constitution based on Contoso stakeholder requirements and the existing project files.

Use the following steps to complete this task:

1. Use Visual Studio Code's EXPLORER view to expand the **.github/agents** and **.specify/memory** folders.

    These folders contain the GitHub Spec Kit resources used to create a constitution.md file. It might be helpful to familiarize yourself with these resource files before working on your constitution file.

1. In the **.github/agents** folder, open the **speckit.constitution.agent.md** file.

1. Take a minute to review the **speckit.constitution.agent.md** file.

    Notice the detailed instructions provided in this markdown file. These instructions are used by GitHub Copilot to generate the constitution.md file. The agent follows a systematic approach to generate a constitution that captures key principles and constraints.

1. In the **.specify/memory** folder, open the **constitution.md** file.

    The initial version of the constitution.md file contains the default template for a constitution.

1. Take a minute to review the **constitution.md** template.

    Notice that the template is initialized with example content that illustrates principles and constraints. The template includes examples for security, performance, quality, technical standards, etc.

    You can keep the constitution file open.

1. Ensure that the Chat view is open, then start a new chat session.

    You can start a new session by selecting the **New Chat** button (the **+** icon at the top of the Chat panel). Starting a new Chat session ensures a clean context.

1. In the Chat view, to start a constitution workflow, enter the following command:

    ```plaintext
    /speckit.constitution
    ```

    The GitHub Spec Kit supports "greenfield" and "brownfield" project types. Preliminary requirements are more significant for greenfield projects since there's no existing codebase. In this exercise, ContosoDashboard is a brownfield project with an existing codebase, so the agent analyzes the current project files to generate the constitution.

1. Monitor GitHub Copilot's response.

    GitHub Copilot uses the Chat view to communicate progress as it updates the constitution.md file.

    It can take a minute or two for GitHub Copilot to analyze the project requirements and then construct the constitution document. If the workflow updates the templates for other GitHub Spec Kit files (spec.md, plan.md, tasks.md), you can accept the updates without reviewing the changes. You generate those files in later tasks.

    > **NOTE**: If GitHub Copilot reports that it isn't able to access or edit files, open Visual Studio Code **Settings**, expand **Features**, select **Chat**, and then ensure that **Chat > Agent** is enabled.

1. Review the updated constitution.md file in the editor.

    Best practice: Always review the suggestions created by an agent.

    After GitHub Copilot updates the constitution, review the document to ensure it captures requirements accurately. This step is important when you're working in a production environment where the constitution represents your business requirements and technical governance. For a training exercise, this review is mainly to help you become familiar with the constitution content.

    Notice that GitHub Copilot recognizes the underlying principles of the ContosoDashboard project and incorporates them into the constitution. The constitution enforces a spec-driven development approach and recognizes the distinction between a training app and production code.

    Each principle should be clearly stated and actionable. For example:

    - ❌ Vague: "Use good security practices" is too general.
    - ✅ Clear: "All API endpoints must validate authentication tokens and enforce role-based permissions" is specific and actionable.

    If any critical requirements are missing or unclear, you can edit the constitution.md file directly to add or modify principles.

1. Ensure that the constitution document is complete, and then accept the changes.

    For a real-world project, it's important to review the constitution against the following criteria before saving:

    - Completeness: All major areas (security, performance, quality, technical standards) are covered.
    - Clarity: Each principle is specific and unambiguous.
    - Consistency: Principles don't contradict each other.
    - Relevance: All principles relate to the ContosoDashboard project.

1. Save and then close the **constitution.md** file.

1. Commit and push the updated files to your Git repository.

    For example, if the constitution.md file is the only file that was updated, you can use the following commands in the terminal:

    ```powershell
    git add constitution.md
    git commit -m "Add project constitution with development principles and constraints"
    git push
    ```

    You can verify the commit by checking your GitHub repository in the browser. The constitution.md file should now appear with your commit message.

The constitution serves as a "contract" between business requirements and technical implementation, ensuring consistency throughout the spec-driven development process. When you use the GitHub Spec Kit to generate the spec, plan, and tasks, it references these principles to ensure the implementation aligns with specified requirements.

## Create the feature specification using stakeholder requirements and the constitution

The specification (spec.md) defines what you're building from the user's perspective. It describes features, user stories, acceptance criteria, and business requirements without prescribing how to implement them. A well-written spec serves as the foundation for creating the implementation plan and tasks.

In this task, you use GitHub Copilot's `/speckit.specify` command to generate a detailed specification for the "document upload and management feature" based on the requirements provided by Contoso's business stakeholders.

Use the following steps to complete this task:

1. In Visual Studio Code's EXPLORER view, under the **.github/agents** folder, open the **speckit.specify.agent.md** file.

1. Take a minute to review the **speckit.specify.agent.md** file.

    Notice the detailed instructions provided to GitHub Copilot. The agent follows a systematic approach to generate a spec file that clearly defines the requirements.

1. In Visual Studio Code's EXPLORER view, expand the **StakeholderDocs** folder, and then open the **document-upload-and-management-feature.md** file.

1. Take a couple minutes to read through the **document-upload-and-management-feature.md** file.

    The document-upload-and-management-feature.md file includes detailed stakeholder requirements for the feature that you're adding to the ContosoDashboard application. Clear and detailed requirements are essential for creating an effective specification.

    The document explains that Contoso Corporation employees need a feature that allows them to upload, organize, and share work-related documents within the ContosoDashboard application. The feature must address current challenges with scattered document storage across multiple locations by providing a centralized, secure, role-based document management solution. The document indicates that the feature must work offline for training purposes while maintaining clean abstractions for future Azure cloud migration. The specification defines six core requirement areas (upload capabilities, organization and browsing, access management, integration with existing features, performance requirements, and reporting) along with detailed technical constraints ensuring the implementation follows the offline-first architecture pattern with interface-based abstractions for production deployment. Performance targets and success metrics are provided to ensure the feature meets user needs and business goals.

    It's best to prepare a comprehensive description of the feature ahead of time. However, if you didn't have a detailed requirements document like the one in the StakeholderDocs folder, you can try using a shorter description that highlights the key features, constraints, and success criteria. For example, the following (simplified) description could be used for the document upload and management feature:

    ```plaintext
    Feature: Document Upload and Management for ContosoDashboard
    
    Enable employees to upload work-related documents (PDF, Office, images, text), organize by category/project, share with team members, and search efficiently. Must integrate with existing dashboard features while maintaining security.
    
    Target Users: All 5,000 Contoso employees with role-based access (Employee, Team Lead, Project Manager, Administrator).
    
    Core Capabilities:
    1. Upload: Multiple files, max 25 MB each, supported types (PDF, Office docs, images, text), metadata (title, category, description, project, tags), progress indicator, virus scanning.
    2. Organization: My Documents view, Project Documents view, search by title/description/tags/uploader/project (results under 2 seconds).
    3. Management: Download, in-browser preview (PDF/images), edit metadata, replace files, delete documents, sharing with notifications.
    4. Integration: Attach to tasks, dashboard Recent Documents widget, notifications for sharing/new project docs.
    5. Performance: Upload in 30s (25 MB files), list load in 2s (500 docs), search in 2s, preview in 3s.
    6. Audit: Log all uploads/downloads/deletions/sharing, admin reports.
    
    Security: Azure Blob Storage encryption at rest, TLS 1.3 in transit, RBAC enforcement, virus scanning.
    
    Success Criteria: 70% adoption in 3 months, find docs under 30s, 90% properly categorized, zero security incidents.
    
    Constraints: Azure Blob Storage, ASP.NET Core integration, 8-10 week timeline, Entra ID authentication.
    
    Out of Scope: Version history, storage quotas, soft delete/trash, collaborative editing, external integrations, mobile apps.
    ```

1. Ensure that the Chat view is open.

    Notice that GitHub Copilot retains the context of previous interactions in the current chat session. If you generated the constitution.md file in the current session, GitHub Copilot provides a **Build Specification** button near the bottom of the Chat view that could be used to start generating the specification. In this case, you want to provide the requirements document explicitly, so you don't use the Build Specification button.

1. In the Chat view, to start a specify workflow that generates a specification from your stakeholders document, enter the following command:

    ```plaintext
    /speckit.specify --file StakeholderDocs/document-upload-and-management-feature.md
    ```

    If you don't specify a requirements document using the `--file` option, you're prompted to describe the feature that you want to build.

1. Monitor GitHub Copilot's response and provide assistance as needed.

    > **IMPORTANT**: GitHub Copilot asks for assistance when generating the spec.md file. For example, GitHub Copilot requests permission to create a repository branch for the new feature. Grant permission when required by responding in the Chat view.

    It can take 4-6 minutes to create and validate the spec.md file.

1. Once the specify workflow is complete, use Visual Studio Code's EXPLORER view to expand the **specs** and **checklists** folders.

1. In the EXPLORER view, select **spec.md**, and then take a couple minutes to review the spec.md file.

    The spec.md file should include a detailed specification for the document upload and management feature based on the stakeholder requirements.

    The specification should be clear, comprehensive, and well-structured. It should provide a solid foundation for creating the technical plan and tasks.

    The spec.md file is based on the template located in the **.specify/templates/spec-template.md** file. The updated spec.md file should include a detailed specification for the 'document upload and management feature' based on the stakeholder requirements that you provided.

    Ensure that the spec.md file includes the mandatory sections defined in the spec template. For example:

    - **User Scenarios & Testing**: User-focused descriptions of feature capabilities and how to test them.
    - **Requirements**: Detailed requirements that must be met, organized by category.
    - **Success Criteria**: Measurable outcomes, assumptions, and out-of-scope items.

1. Review the **spec.md** file and verify that key requirements (from your stakeholder requirements document) are captured under the Requirements section.

    For example, you should see requirements related to:

    - File size limits (25 MB per file)
    - Supported file types (PDF, Office documents, images, text files)
    - Performance targets (2-second page loads, 30-second uploads)

1. Review the **spec.md** file and verify that acceptance scenarios (associated with user scenarios) are specific and testable:

    The acceptance scenarios should follow the **Given-When-Then** format. The scenarios should provide clear conditions for success or failure. For example:

    - ✅ Good: **Given** I'm logged in as an employee, **When** I navigate to the documents page and select a PDF file under 25 MB with valid metadata (title and category), **Then** the document uploads successfully and appears in my "My Documents" list with all metadata displayed correctly

    - ✅ Good: **Given** an employee attempts to upload a 30MB file, **When** validation occurs, **Then** they see an error message stating the 25MB limit

    - ❌ Avoid: Vague criteria like "Upload should work well" or "System should be fast"

1. In the EXPLORER view, select **requirements.md**, and then take a minute to review the requirements.md file.

    Verify that no issues are reported in the **requirements.md** file. You should see that all checklist items passed successfully.

1. Accept the suggested file updates, and then save the **spec.md** and **requirements.md** files.

1. Commit the specification files and publish the new branch to your Git repository.

    For example:

    Open Visual Studio Code's SOURCE CONTROL view, stage the changes, enter a commit message like "Add specification for document upload and management feature," and then publish the new branch to your Git repository.

The specification defines the "what" without the "how." It doesn't specify programming languages, frameworks, database schemas, or code organization - those implementation details are determined in the Plan and Tasks phases based on the constitution's technical constraints. The spec focuses on user needs and business requirements, making it easier to review with nontechnical stakeholders.

## Update the specification with clarified requirements

The `/speckit.clarify` command helps identify ambiguities, gaps, and underspecified areas in your specification. GitHub Copilot analyzes the spec and asks targeted questions to ensure all requirements are clear and complete before moving to the technical planning phase.

In this task, you use the clarification process to refine the document upload and management specification.

Use the following steps to complete this task:

1. Ensure the GitHub Copilot Chat view is open.

1. In the Chat view, to start the clarification process, enter the following command:

    ```plaintext
    /speckit.clarify
    ```

1. Monitor GitHub Copilot's response and provide assistance as needed.

    GitHub Copilot analyzes the spec.md file and evaluates whether clarification questions are necessary.

    For example, you might receive questions that are similar to the following sample questions:

    - "When a user is removed from a project after uploading documents to that project, what should happen to those documents?"
    - "When a project is deleted, what should happen to all documents associated with that project?"
    - "When a shared document is deleted by the owner, what happens to recipients who had access to it?"
    - "When a user uploads a document with a filename that contains special characters (for example, Q4 Report (2025) - Finance & Ops.pdf), how should the system handle it?"
    - "When disk storage becomes full during a document upload, how should the system respond?"

    The questions are presented one at a time.

1. If clarifications are needed, consider each question appropriately before answering.

    In a production environment, your answers should reflect careful analysis of business needs, user experience considerations, and technical constraints. However, for this training, you can select the recommended option for each question.

    When you provide an answer, GitHub Copilot updates the spec.md file with clarifications.

    > **NOTE**: If GitHub Copilot presents additional rounds of questions, continue answering until it indicates there are no further clarifications needed. The clarification process typically involves 1-2 rounds of questions as GitHub Copilot refines the specification.

    Once the clarification process is complete, review the updated **spec.md** file, and then accept the changes.

    - Check that your answers are accurately reflected in the specification
    - Verify that previously ambiguous areas now have clear requirements
    - Look for any newly added acceptance criteria based on your clarifications

    You can make any manual edits if needed. For example, if GitHub Copilot interpreted an answer differently than you intended, edit the spec directly to correct it.

1. If the clarification process resulted in changes, save the updated **spec.md** file, and then commit and sync the changes.

Ensuring that specification provides clear and comprehensive guidance is important. By addressing ambiguities upfront, you reduce the risk of building the wrong solution or having to make significant changes later in the development process.

## Generate the technical plan using the specification and constitution

The technical plan bridges the gap between the "what" (specification) and the "how" (implementation). It defines the architecture, technology choices, data models, API designs, and implementation approach while adhering to the constraints defined in the constitution.

In this task, you use GitHub Copilot's `/speckit.plan` command to generate a comprehensive technical implementation plan.

Use the following steps to complete this task:

1. In Visual Studio Code's EXPLORER view, under the **.github/agents** folder, open the **speckit.plan.agent.md** file.

1. Take a minute to review the **speckit.plan.agent.md** file.

    Notice the detailed instructions provided to GitHub Copilot. The agent follows a systematic approach to generate a plan file that outlines the technical implementation strategy.

    If you're interested, you can also review the **.specify/templates/plan-template.md** file to see the structure that's used for the plan.md file.

1. Ensure the GitHub Copilot Chat view is open.

1. In the Chat view, to start the technical planning process, enter the following command:

    ```dotnetcli
    /speckit.plan
    ```

1. Monitor GitHub Copilot's response and provide assistance in the Chat view.

    GitHub Copilot analyzes the constitution.md and spec.md files to generate the plan. Provide permission and assistance when required.

    It can take 6-8 minutes for GitHub Copilot to generate the technical plan and associated markdown files.

1. Once the plan workflow is complete, ensure that the following files are listed under the **specs** folder:

    - **plan.md**
    - **research.md**
    - **quickstart.md**
    - **data-model.md**

    You might also see one or more files listed under a **contracts** folder.

1. Take a few minutes to review the **research.md**, **plan.md**, **quickstart.md**, and **data-model.md** files.

    - The research.md file captures research findings and technology decisions for the document upload and management feature.
    - The plan.md file outlines the technical implementation plan for the document upload and management feature.
    - The quickstart.md file provides setup instructions and a high-level overview of how to get started with the implementation.
    - The data-model.md file defines the data entities, properties, and relationships needed for the document upload and management feature.

    > **NOTE**: For a production scenario, you need to ensure that the plan provides a comprehensive description of the technical context and a clearly defined implementation strategy for the new feature. The research, quickstart, and data model files should complement the plan by providing additional context and details. For this exercise, focus on becoming familiar with the content associated with each of the files.

1. After reviewing the files, accept the updates.

    If the plan omits important details or makes assumptions you disagree with, you can:

    - Edit the plan.md file directly, or
    - Ask follow-up questions in GitHub Copilot Chat. For example:

    ```plaintext
    The plan should include a background job for processing virus scans. Add details about using Azure Functions with Queue Storage triggers to handle async file scanning after upload.
    ```

1. Save the files, and then commit and sync your changes.

The technical plan now serves as a blueprint for implementation. It translates business requirements into concrete technical decisions while respecting organizational constraints.

## Generate the tasks file using the specification, plan, and constitution

The tasks.md file breaks down the technical plan into specific, actionable implementation steps. Each task should be small enough to complete in a reasonable timeframe (typically a few hours to a day when implemented without AI assistance) and have clear acceptance criteria.

In this task, you use the GitHub Spec Kit's `/speckit.tasks` command to generate a comprehensive tasks list and phased implementation plan.

Use the following steps to complete this task:

1. In Visual Studio Code's EXPLORER view, under the **.github/agents** folder, open the **speckit.tasks.agent.md** file.

1. Take a minute to review the **speckit.tasks.agent.md** file.

    Notice the detailed instructions provided to GitHub Copilot. The agent follows a systematic approach to generate a tasks.md file that breaks down the implementation plan into manageable tasks.

1. Ensure the GitHub Copilot Chat view is open.

1. In the Chat view, to start generating the tasks.md file, enter the following command:

    ```dotnetcli
    /speckit.tasks
    ```

1. Monitor GitHub Copilot's response and provide assistance in the Chat view.

    GitHub Copilot analyzes the spec.md and plan.md files and generate tasks in the tasks.md file.

    It can take 3-4 minutes for GitHub Copilot to generate the tasks.md file. Provide permission and assistance when required.

1. Once the tasks workflow is complete, take a few minutes to review the **tasks.md** file.

    The tasks.md file should provide a list of tasks organized by phase and user story.

    Verify that the tasks cover the requirements from the specification and plan. For example:

    - Each functional requirement should map to one or more tasks.
    - Security requirements should have corresponding implementation tasks.
    - Performance requirements should have testing tasks.
    - Integration points should have dedicated tasks.

    Verify that tasks are ordered logically. For example:

    - Foundation tasks (database, models) come first.
    - Backend API tasks build on the foundation.
    - Frontend tasks reference backend endpoints.
    - Testing tasks come after implementation.
    - Deployment tasks come last.

1. Ensure that each task is specific and actionable:

    - ✅ Good: "Create Document entity with properties: DocumentId, Title, Description, FileName, FileSize, BlobStorageUrl"
    - ❌ Vague: "Set up database stuff"

    Verify that tasks have reasonable scope:

    - Developers should be able to complete individual tasks in a few hours to a day.
    - If a task seems too large it might need to be broken down during implementation.

    You can add task dependencies or notes if needed. For example:

    ```markdown
    - [ ] Task 12: Implement DocumentController POST /api/documents endpoint
      - Depends on: Task 11 (DocumentService)
      - Note: Include comprehensive error handling for file size limits and unsupported types
    ```

1. Accept the suggested file updates, and then save the **tasks.md** file.

1. Commit the changes and then sync the updates.

The tasks.md file now provides a clear roadmap for implementation.

## Implement the tasks required for an MVP application

With a clear specification, technical plan, and tasks document in place, you're ready to implement the document upload and management feature. The implement workflow demonstrates how spec-driven development guides implementation and how GitHub Copilot assists with code generation based on the context you established.

In this task, you review the implementation strategy and then use `speckit/implement` to implement the MVP version of the application.

Use the following steps to complete this task:

1. Open the **tasks.md** file, locate the **Implementation Strategy** section, and then review the suggested "MVP first" strategy.

    The MVP first strategy is intended to deliver working feature as quickly as possible. It should focus on completing the critical blocking phases first to establish a functional foundation before building out the first user story (US1).

    For example, the MVP implementation strategy might be similar to the following example:

    ```plaintext
    **Phases**: Setup → Foundation → US1 only  
    **Tasks**: T001 - T045 (45 tasks)  
    **Estimated Time**: 6-8 hours for developer familiar with ASP.NET Core/Blazor  
    **Deliverable**: Users can upload and view their documents
    ```

1. In the Chat view, enter a command that starts the implement workflow using the MVP first strategy:

    Create a command that specifies the range of tasks required to implement the MVP version of the feature. Use the task range specified in the Implementation Strategy section of the tasks.md file.

    > **IMPORTANT**:The command that you enter must reference the specific task range defined in your tasks.md file.

    For example (referencing the MVP implementation example from the previous step), you might enter the following command:

    ```dotnetcli
    /speckit.implement Implement the MVP first strategy (Tasks: T001 - T045)
    ```

    This command instructs GitHub Copilot to begin implementing the tasks required for the MVP version of the document upload and management feature.

1. Monitor GitHub Copilot's response and provide assistance in the Chat view.

    The agent builds the feature incrementally, task by task, following the order defined in the tasks.md file.

    > **NOTE**: GitHub Copilot is diligent about checking its work during the implementation, which is great. GitHub Copilot also keeps you involved during the implementation process. Requests for assistance occur frequently. The time required to complete the implementation can be affected by how quickly you respond to its requests for assistance.

1. If manual testing is required to verify a task, perform the steps described in the Chat view, and then report the results back to GitHub Copilot.

    You might encounter issues during manual testing. For example:

    1. GitHub Copilot tells you that manual testing is required to verify that file uploads are working correctly.
    1. The application is already running locally. The Chat view provides the URL to open in the browser (for example, `http://localhost:5000`).
    1. You open the application in the browser, login as Ni Kang, and then navigate to the My Documents page.
    1. The app appears to be unresponsive with a message "Loading documents..." displayed in the user interface.
    1. You select the Upload Document button, but nothing happens.
    1. You try logging out, but the application remains unresponsive. None of the buttons work.

    At this point you need to report the issue to GitHub Copilot:

    1. You return to Visual Studio Code's Chat view.
    1. You report the issue in the Chat view. For example:

        ```plaintext
        I opened the application in the browser at http://localhost:5000. I was able to login as Ni Kang and navigate to the My Documents page. However, I encountered an issue where the application appears unresponsive with a "Loading documents..." message displayed in the UI. When I select the Upload Document button, nothing happens. I also tried logging out, but the application remains unresponsive and none of the buttons work. Can you help me troubleshoot this issue?
        ```

    When you report an issue, GitHub Copilot uses the information you provided to begin debugging. A detailed description, including what is working, helps GitHub Copilot understand the problem better. GitHub Copilot might need extra details, such as specific error messages to resolve some issues. Provide any additional information requested by GitHub Copilot to help diagnose (and resolve) the problem.

    Continue to provide assistance until the issue is resolved. Once the issue is resolved, GitHub Copilot should ask you to resume manual testing.

1. Continue with the implement workflow until all tasks required for the MVP application are complete.

    GitHub Copilot should notify in the Chat view when the MVP implementation is complete.

1. Review and accept all changes made to the project files.

    For this lab exercise, it's okay to accept all changes made by GitHub Copilot without a detailed review. However, in a production environment, it's important to review all code changes carefully to ensure they meet quality standards and align with project requirements.

1. Take a few minutes to verify the acceptance scenarios for the MVP application.

    You can find the acceptance scenarios in the spec.md file. The acceptance scenarios listed under the **User Scenarios & Testing** section. The MVP application is usually associated with the first user story (US1) in the spec.md file.

    You can also ask GitHub Copilot for the steps required to perform manual testing of your MVP implementation. For example, you could enter the following prompt in the Chat view:

    ```plaintext
    Can you provide the steps required to manually test the MVP implementation?
    ```

    Use Visual Studio Code to run the application, and then manually test the document upload and management functionality to ensure that it works as expected.

    For example, you can use the following steps to manually test document upload functionality:

    1. Navigate to http://localhost:5000
    1. Log in as Ni Kang (Employee).
    1. Select **Documents** from the navigation menu.
    1. Use the provided interface to open a file selection dialog.
    1. Locate and select a PDF file that's less than 25 MB, then fill the Title ("Test Document") and Category ("Personal Files") fields.
    1. Select the "Upload" option to start the upload process.
    1. Verify that an upload progress indicator appears.
    1. Verify that the document appears in your uploaded documents list.

1. Report back to GitHub Copilot with the results of your manual testing.

    For example:

    - If your test succeeded, you can either continue to the next test or provide a report similar to the following example:

        "I opened the application in the browser at http://localhost:5000. I was able to login as Ni Kang and navigate to the My Documents page. I can upload a PDF file less than 25 MB with the Title 'Test Document' and Category 'Personal Files.' The upload progress indicator appeared, and the document shows up in my uploaded documents list. Task T041 passed successfully."

    - If your task failed, you need to report the issue to GitHub Copilot for assistance.

        For example: "I opened the application in the browser at http://localhost:5000. I was able to login as Ni Kang and navigate to the My Documents page. I can select a document and fill in the Title and Category fields, but there's an error when I try to upload the document. I see a progress indicator displayed on the Upload Document page, however, the My Documents page doesn't recognize that I uploaded a document. Can you help resolve the issue?

    GitHub Copilot can help you diagnose and fix issues, implement improvements to the user interface, or suggest next steps.

1. Continue manual testing and reporting results until all acceptance scenarios for the MVP application pass successfully.

1. After successfully testing your MVP application, commit and sync your implementation files.

> **NOTE**: If time permits, you can continue implementing additional tasks beyond the MVP scope. You can either instruct GitHub Copilot to proceed with the next set of tasks or manually select specific tasks to implement next.

Key Observations:

- GitHub Copilot generates code that aligns with your spec because it references the *spec.md*, *plan.md*, and *tasks.md* files in your workspace.
- Detailed comments based on specification requirements guide GitHub Copilot to produce accurate implementations.
- The spec-driven development approach ensures you don't forget requirements (file size limits, supported types, etc.) because they're explicitly documented.
- Having clear acceptance criteria makes it easy to verify that your implementation meets requirements.

In a full implementation, you would continue through all remaining tasks in the tasks.md file, using a phased approach to systematically build out the complete feature. The spec-driven development approach keeps you focused on requirements and prevents scope creep or missed functionality.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. For example, you might want to delete the ContosoDashboard repository. If you're using a local PC as your lab environment, ensure that you want to keep any tools that might have installed during the exercise. You can archive or delete the local clone of the repository that you created for this exercise.
