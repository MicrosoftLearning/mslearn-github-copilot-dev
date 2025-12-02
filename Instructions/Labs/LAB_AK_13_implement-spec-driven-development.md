<!-- ---
lab:
    title: 'Exercise - Implement a product feature using GitHub Spec Kit'
    description: 'Learn how to implement a spec-driven development process using GitHub Spec Kit and GitHub Copilot in Visual Studio Code.'
--- -->

# Implement a product feature using GitHub Spec Kit

GitHub Spec Kit is an open-source toolkit that enables Spec-Driven Development (SSD) by integrating specifications with AI coding assistants like GitHub Copilot.

In this exercise, you learn how to set up a GitHub Spec Kit development environment, create constitutions, specifications, plans, and tasks, and then implement a product feature using GitHub Copilot.

This exercise should take approximately **50** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following resources: Python 3.11 or later, Git 2.48 or later, .NET SDK 8.0 or later, Visual Studio Code with the C# Dev Kit and GitHub Copilot Chat extensions, the uv package manager, and access to a GitHub account with GitHub Copilot enabled.

If you're using a local PC as a lab environment for this exercise:

- For help with configuring your local PC as your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320147" target="_blank">Configure your lab environment resources</a>.

- For help with enabling your GitHub Copilot subscription in Visual Studio Code, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

If you're using a hosted lab environment for this exercise:

- For help with enabling your GitHub Copilot subscription in Visual Studio Code, paste the following URL into a browser's site navigation bar: <a href="https://go.microsoft.com/fwlink/?linkid=2320158" target="_blank">Enable GitHub Copilot within Visual Studio Code</a>.

### Verify or install required tools

1. Verify that Git 2.48 or later is installed:

    ```powershell
    git --version
    ```

    Expected output: `git version 2.48.0` or higher

    If needed, install Git from [git-scm.com](https://git-scm.com/downloads) or your corporate application catalog.

### Configure your environment

1. To ensure that the .NET SDK is configured to use the official NuGet.org repository as a source for downloading and restoring packages:

    Open a command terminal and then run the following command:

    ```bash

    dotnet nuget add source https://api.nuget.org/v3/index.json -n nuget.org

    ```

1. To ensure that Git is configured to use your name and email address:

    Update the following commands with your information, and then run the commands:

    ```bash

    git config --global user.name "Julie Miller"

    ```

    ```bash

    git config --global user.email julie.miller@example.com

    ```

## Exercise scenario

You're a software developer working for a consulting firm that's adopting a spec-driven development (SDD) approach using GitHub Spec Kit and GitHub Copilot in Visual Studio Code. Your client, Contoso Corporation, has tasked you with adding a new feature to their internal employee dashboard application (ContosoDashboard).

Contoso's business stakeholders have provided high-level requirements for a document upload and management feature. Employees need the ability to upload work-related documents, organize them by category and project, and share them with team members. The feature must integrate seamlessly with the existing dashboard while maintaining security and compliance standards.

Your assignment is to use the spec-driven development methodology to implement this feature. Rather than jumping directly into coding, you'll create structured specifications, plans, and tasks that guide the development process. This approach ensures the implementation aligns with business requirements and organizational constraints.

The ContosoDashboard is an internal web application that provides Contoso employees with a centralized platform for managing daily work activities. The ContosoDashboard currently serves 5,000 Contoso employees. The application uses role-based access control (Employee, Team Lead, Project Manager, Administrator) and integrates with Contoso's Microsoft Entra ID for authentication. The existing application includes task management, project tracking, team collaboration features, and notification capabilities.

This exercise includes the following tasks:

1. Configure the GitHub Spec Kit in your development environment.
1. Import the ContosoDashboard repository and initialize GitHub Spec Kit.
1. Review the ContosoDashboard project and GitHub Spec Kit files.
1. Define the project Constitution (organizational constraints and development principles).
1. Create the Spec for the document upload and management feature.
1. Clarify the Spec (iterate on requirements with AI assistance).
1. Generate the Technical Plan (architecture and implementation approach).
1. Create the Task List (actionable implementation steps).
1. Implement the specified feature using GitHub Copilot.
1. Review and verify the implementation.

## Configure the GitHub Spec Kit in your development environment

The GitHub Spec Kit includes a command-line interface (CLI) tool called Specify that initializes projects for spec-driven development. It also integrates with Visual Studio Code through the GitHub Copilot Chat extension to provide AI-assisted generation of specifications, plans, and tasks.

In this task, you install the GitHub Spec Kit CLI tool and configure Visual Studio Code for spec-driven development.

Use the following steps to complete this task:

1. Open a terminal window.

    You can use a Command Prompt, PowerShell, or Terminal window.

1. Ensure that Python 3.11 or later is installed:

    Spec Kit's CLI tool is Python-based and requires Python 3.11+.

    To check the installed Python version, run the following command:

    ```powershell
    python --version
    ```

    Expected output: `Python 3.11.0` or higher

    If you need to install Python, download from [python.org](https://www.python.org/downloads/) or use your organization's software distribution system.

1. Ensure that the uv package manager is installed:

    Spec Kit uses uv for CLI installation and management.

    - You can install uv by following the instructions at [docs.astral.sh/uv](https://docs.astral.sh/uv/)

    - For Windows PowerShell:

        ```powershell
        powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"
        ```

1. To ensure that uv is in your environment PATH, restart your terminal window, and then run the following command:

    ```powershell
    uv --version
    ```

1. To install the Specify CLI tool, run the following command:

    ```powershell
    uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
    ```

    This command installs the latest version directly from the GitHub repository and makes the `specify` command available system-wide.

    The `specify` command-line tool can be used to initialize projects for spec-driven development.

1. To ensure that the `specify` command is in your environment PATH, restart your terminal window, and then run the following command:

    ```powershell
    specify version
    ```

    You should see output similar to:

    ```output
         CLI Version    0.0.22
    Template Version    0.0.85
            Released    2025-11-15
              Python    3.13.9
            Platform    Windows
        Architecture    AMD64
          OS Version    10.0.26200
    ```

    Troubleshooting installation issues:

    - Command not found: If `specify` isn't recognized after installation, the `uv` tools directory may not be in your PATH. Run `uv tool list` to verify the installation. You may need to restart your terminal or manually add the tools directory to your PATH.

    - In corporate environments with SSL interception, you may need to configure certificates. Contact your IT department for assistance.

1. Open Visual Studio Code, and then ensure that GitHub Copilot Chat is enabled.

    The GitHub Spec Kit integrates with Visual Studio Code through the GitHub Copilot Chat extension.

    For GitHub Enterprise Server environments:

    - Open Visual Studio Code Settings (Ctrl+,)

    - Search for "github.enterprise"

    - Set "Github: Enterprise Uri" to your server URL (for example, `https://github.yourcompany.com`)

    - Sign in using your enterprise credentials

Your GitHub Spec Kit development environment is now configured and ready. In the next task, you'll import the existing ContosoDashboard application and initialize it for spec-driven development.

## Import the ContosoDashboard repository and initialize GitHub Spec Kit

GitHub Importer can be used to create a copy of an existing repository in your own GitHub account, giving you full control over the imported copy.

In this task, you import the existing ContosoDashboard application repository to your GitHub account and initialize GitHub Spec Kit in your project directory.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

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

    > **NOTE**: It can take a minute or two for the import process to finish. Wait for the import process to complete (typically 1-2 minutes).

    GitHub will display a progress indicator and notify you when the import is complete.

1. Once the import is complete, open your new repository.

    A link to your repository should be displayed. Your repository should be located at: `https://github.com/YOUR-USERNAME/ContosoDashboard`.

1. On your ContosoDashboard repository page, select the **Code** button, and then copy the HTTPS URL.

    The URL should be similar to: `https://github.com/YOUR-USERNAME/ContosoDashboard.git`

1. Open a terminal window, and then navigate to the location where you want to clone the project:

    For example:

    Open a terminal window (Command Prompt, PowerShell, or Terminal), and then run:

    ```powershell
    cd C:\TrainingProjects
    ```

    Replace `C:\TrainingProjects` with your preferred location. You can use any directory where you have write permissions, and you can create a new folder location if needed.

1. To clone your ContosoDashboard repository, enter the following command:

    ```powershell
    git clone https://github.com/YOUR-USERNAME/ContosoDashboard.git
    ```

    Replace `YOUR-USERNAME` with your actual GitHub username.

1. To navigate into your ContosoDashboard directory, enter the following command:

    ```powershell
    cd ContosoDashboard
    ```

1. To initialize GitHub Spec Kit within your existing project, enter the following command:

    ```powershell
    specify init --here --ai copilot --script ps
    ```

    The command uses the following components:

    - `--here` - Initializes Spec Kit in the current directory (existing project).
    - `--ai copilot` - Configures the project for GitHub Copilot.
    - `--script ps` - Uses PowerShell scripts (use `--script sh` for bash/zsh on macOS/Linux).

    > **Note**: If you're using macOS or Linux, replace `--script ps` with `--script sh`.

1. Wait for the initialization to complete.

    The CLI will:

    - Detect the existing Git repository ("Current directory is not empty") and ask for confirmation to proceed.
    - Add the `.github/prompts/` directory with Spec Kit commands.
    - Create template files: `constitution.md`, `spec.md`, `plan.md`, `tasks.md`.
    - Preserve all existing application files.
    - Display a success message ("Project ready").
    - Suggest some optional next steps.

## Review the ContosoDashboard project and GitHub Spec Kit files

Spec Kit works with GitHub Copilot through Visual Studio Code's chat interface. After running `specify init --ai copilot`, the toolkit configures your workspace to recognize `/speckit.*` commands.

In this task, you explore the project files in Visual Studio Code, verify that GitHub Spec Kit is properly initialized, and then push the GitHub Spec Kit files to your GitHub repository.

Use the following steps to complete this task:

1. In the terminal, to open the ContosoDashboard project in Visual Studio Code, enter the following commands:

    ```powershell
    cd ContosoDashboard
    code .
    ```

    Wait for Visual Studio Code to fully load the project.

    The `code .` command opens the current directory (ContosoDashboard) in Visual Studio Code.

1. Take a minute to familiarize yourself with the project structure.

    Use Visual Studio Code's EXPLORER view to expand the application folders. You should see a folder structure that's similar to the following:

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
    │   ├── Models/                 (User, TaskItem, Project, ProjectMember, TaskComment, Notification, Announcement)
    │   ├── Data/                   (ApplicationDbContext.cs)
    │   ├── Services/               (TaskService, ProjectService, UserService, NotificationService, DashboardService, CustomAuthenticationStateProvider)
    │   ├── Pages/                  (Index, Tasks, Projects, ProjectDetails, Team, Notifications, Profile, Login, Logout, _Host)
    │   ├── Shared/                 (MainLayout, NavMenu, RedirectToLogin)
    │   ├── wwwroot/                (Static files, CSS)
    │   ├── Program.cs              (App configuration)
    │   └── ContosoDashboard.csproj (Project file)
    ├── StakeholderDocs/            (Business requirements)
    ├── README.md                   (Application documentation)
    └── LICENSE-CODE
    ```

1. Open GitHub Copilot's Chat view:

1. Ask GitHub Copilot to explain the current project and GitHub Spec Kit files.

    For example, enter the following prompt in the Chat view:

    ```plaintext
    Review the current codebase. Explain the ContosoDashboard application features and the purpose of the GitHub Spec Kit files. 
    ```

1. Take a minute to review GitHub Copilot's response.

    GitHub Copilot's response should summarize the application features and the purpose of the Spec Kit files.

    You can also review the project's README.md file for a description of the current application features, mock authentication system, and security implementation.

1. In the Chat view, to verify that GitHub Spec Kit commands are available, type **/speckit**

    You should see autocomplete suggestions that show the available commands:

    - `/speckit.analyze` - Audit implementation plans.
    - `/speckit.checklist` - Validate specification completeness.
    - `/speckit.clarify` - Refine specifications through Q&A.
    - `/speckit.constitution` - Define project governing principles.
    - `/speckit.implement` - Execute the implementation.
    - `/speckit.plan` - Generate technical implementation plans.
    - `/speckit.specify` - Create feature specifications.
    - `/speckit.tasks` - Break down work into actionable tasks.

    > **Note**: If the `/speckit` commands don't appear, try closing and then reopening the project in Visual Studio Code.

    **Troubleshooting**: If you encounter issues:

    - **"specify command not found"**: Ensure you completed Task 1 and installed the Specify CLI. Run `specify version` to verify installation.
    - **Permission denied errors**: On Windows, ensure you're running PowerShell with appropriate permissions. On macOS/Linux, check file permissions.
    - **Git clone errors**: Verify you're signed in to GitHub and have access to your imported repository.
    - **Spec Kit commands not appearing**: Ensure `.github/prompts/` exists in your workspace root. Try reloading Visual Studio Code.

1. In the EXPLORER view, right-click **ContosoDashboard** and then select **Open in Integrated Terminal**.

1. To build and run the application, run the following commands:

    ```dotnetcli
    dotnet restore
    dotnet build
    dotnet run
    ```

    There will be some Warning messages, but there shouldn't be any errors.

1. Open a browser window and then navigate to `https://localhost:5000`.

    You should see a login page for the ContosoDashboard application.

1. On the ContosoDashboard login page, select a user from the dropdown, and then select **Login**.

1. Take a minute to explore the ContosoDashboard application, and then close the browser window.

    It's good to verify that the application is working before you start developing a new feature.

1. In Visual Studio Code's terminal panel, to stop the running application, press **Ctrl+C**  and then close the terminal.

1. Use Visual Studio Code's Source Control view to commit and push the updated project files.

    For example:

    - Select the Source Control icon in the left-hand activity bar.
    - Enter a commit message such as: "Add GitHub Spec Kit files to the ContosoDashboard project"
    - Select the checkmark icon to commit the changes (and select Yes to stage the changes if prompted).
    - Select **Sync Changes** to push the commit to GitHub (and select Ok if prompted).

    Pushing the GitHub Spec Kit files to your repository enables you to track the spec-driven development process.

1. Open your GitHub repository in a browser window and verify that the push succeeded.

    You should now see the GitHub Spec Kit files alongside the existing application code.

You now have a working ContosoDashboard application with GitHub Spec Kit initialized.

## Define the project Constitution

The GitHub Spec Kit uses a "Constitution" to establish the governing principles and constraints that guide all development decisions for the ContosoDashboard project. It captures organizational policies, technical standards, security requirements, and development practices that must be followed throughout implementation.

In this task, you use GitHub Copilot's `/speckit.constitution` command to generate a comprehensive constitution based on Contoso stakeholder requirements and the existing project files.

Use the following steps to complete this task:

1. Use Visual Studio Code's EXPLORER view to expand the **.github/agents** and **.specify/memory** folders.

    These folders contain the GitHub Spec Kit resources used to create a constitution.md file. It might be helpful to familiarize yourself with these resource files before working on your constitution file.

1. In the **.github/agents** folder, open the **speckit.constitution.agent.md** file.

1. Take a minute to review the **speckit.constitution.agent.md** file.

    Notice the detailed instructions provided to GitHub Copilot. The agent follows a systematic approach to generate a constitution that captures key principles and constraints.

1. In the **.specify/memory** folder, open the **constitution.md** file.

1. Take a minute to review the constitution template.

    Notice that the template provides examples that illustrate principles and constraints for security, performance, quality, technical standards, etc.

    You can keep the constitution file open.

1. Ensure that the Chat view is open, then start a new chat session.

    Starting a new Chat session ensures a clean context.

1. In the Chat view, to start a constitution workflow, enter the following command:

    ```plaintext
    /speckit.constitution
    ```

    > **NOTE**: The GitHub Spec Kit supports "greenfield" and "brownfield" project types. Preliminary requirements are more significant for greenfield projects since there is no existing codebase. In this exercise, ContosoDashboard is a brownfield project with an existing codebase, so the agent analyzes the current project files when generating the constitution.

1. Monitor GitHub Copilot's response.

    GitHub Copilot uses the Chat view to communicate progress as it updates the constitution.md file.

    It might take 30-60 seconds for GitHub Copilot to analyze the requirements and structure the constitution document.

1. Review the updated constitution.md file in the editor.

    Always review agent suggestions. After Copilot updates a constitution, review the document carefully to ensure it captures all requirements accurately.

    Notice that GitHub Copilot recognizes the underlying principles of the ContosoDashboard project and incorporates them into the constitution. This includes enforcing a spec-driven development development approach and the distinction between a training and production code.

    Each principle should be clearly stated and actionable. For example:

    - ❌ Vague: "Use good security practices"
    - ✅ Clear: "All API endpoints must validate authentication tokens and enforce role-based permissions"

    If any critical requirements are missing or unclear, you can edit the constitution.md file directly to add or modify principles.

1. Ensure that the constitution document is complete, and then save the **constitution.md** file.

    For a real-world project, it's important to review the constitution against the following criteria before saving:

    - Completeness: All major areas (security, performance, quality, technical standards) are covered.
    - Clarity: Each principle is specific and unambiguous.
    - Consistency: Principles don't contradict each other.
    - Relevance: All principles relate to the ContosoDashboard project.

1. Commit and push the updated constitution.md file to your Git repository.

    For example:

    ```powershell
    git add constitution.md
    git commit -m "Add project constitution with development principles and constraints"
    git push
    ```

    You can verify the commit by checking your GitHub repository in the browser. The constitution.md file should now appear with your commit message.

The constitution you just created will guide all subsequent development decisions. When GitHub Copilot generates the spec, plan, and tasks, it will reference these principles to ensure the implementation aligns with Contoso's requirements. For example:

- When generating the technical plan, Copilot will ensure Azure services are specified (not AWS or GCP).
- When creating tasks for file upload, Copilot will include validation and security scanning steps.
- When suggesting code implementations, Copilot will follow .NET conventions and include proper error handling.

The constitution serves as a "contract" between business requirements and technical implementation, ensuring consistency throughout the spec-driven development process.

## Create the Spec for the document upload and management feature

The specification (spec) defines what you're building from the user's perspective. It describes features, user stories, acceptance criteria, and business requirements without prescribing how to implement them. A well-written spec serves as the foundation for creating implementation plans and tasks.

In this task, you use GitHub Copilot's `/speckit.specify` command to generate a detailed specification for the document upload and management feature based on high-level requirements from Contoso's business stakeholders.

Use the following steps to complete this task:

1. In Visual Studio Code's EXPLORER view, under the **.github/agents** folder, open the **speckit.specify.agent.md** file.

1. Take a minute to review the **speckit.specify.agent.md** file.

    Notice the detailed instructions provided to GitHub Copilot. The agent follows a systematic approach to generate a spec file that clearly defines the requirements.

1. In Visual Studio Code's EXPLORER view, expand the **StakeholderDocs** folder, and then open the **document-upload-and-management-feature.md** file:

1. Take a couple minutes to read through the requirements document.

    Pay particular attention to the following topics:

    - **Business Need**: Why Contoso needs this feature (centralized document storage, security)
    - **Target Users**: All Contoso employees with role-based permissions
    - **Core Requirements**: The 6 requirement areas covering upload, organization, access, integration, performance, and audit
    - **Success Metrics**: How feature success will be measured (e.g., 70% adoption rate, documents found in under 30 seconds)
    - **Technical Constraints**: Azure infrastructure, 8-10 week timeline, security policies
    - **Out of Scope**: Features not included in this release

    It's best to prepare a comprehensive description of the feature ahead of time. However, if you didn't have a requirements document like the one in the StakeholderDocs folder, you can construct a shorter description that highlights the key points. For example, you can provide the following description for the document upload and management feature:

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

1. Ensure that the Chat view is open, and then start a new chat session.

    You can start a new session by selecting the **New Chat** button (the **+** icon at the top of the Chat panel).

1. In the Chat view, to start a specify workflow that generates a specification from the stakeholders document, enter the following command:

    ```plaintext
    /speckit.specify --file StakeholderDocs/document-upload-and-management-feature.md
    ```

    If you don't specify a requirements document using the `--file` option, you'll be prompted to describe the feature that you want to build.

1. Monitor GitHub Copilot's response and provide assistance as needed.

    > **IMPORTANT**: GitHub Copilot asks for assistance when generating the spec.md file. For example, GitHub Copilot might request permission to create a new repository branch for the specification. Grant permission by responding in the Chat view.

    It can take a few minutes for GitHub Copilot to generate the specification in the spec.md file.

1. Once the specify workflow is complete, use Visual Studio Code's EXPLORER view to expand the **specs** and **checklists** folders.

1. In the EXPLORER view, select **spec.md**, and then take a couple minutes to review the spec.md file.

    Verify that the spec.md file includes the following sections:

    - **User Scenarios & Testing**: User-focused descriptions of feature capabilities and how to test them.
    - **Requirements**: Detailed requirements that must be met, organized by category.
    - **Success Criteria**: Measurable outcomes, assumptions, and out-of-scope items.

1. Verify that key requirements (from your feature description) are captured under the Requirements section.

    For example:

    - File size limits (25 MB per file)
    - Supported file types (PDF, Office documents, images, text files)
    - Performance targets (2-second page loads, 30-second uploads)
    - Security measures (virus scanning, encryption, RBAC)
    - Integration points (tasks, dashboard widgets, notifications)

1. Verify that acceptance scenarios (associated with user scenarios) are specific and testable:

    - ✅ Good: **Given** an employee attempts to upload a 30MB file, **When** validation occurs, **Then** they see an error message stating the 25MB limit
    - ❌ Avoid: Vague criteria like "Upload should work well" or "System should be fast"

1. In the EXPLORER view, select **requirements.md**, and then take a minute to review the requirements.md file.

    Verify that no issues are reported in the **requirements.md** file.

1. Save the **spec.md** and **requirements.md** files.

1. Commit the specification files and publish the new branch to your Git repository.

    For example:

    Open Visual Studio Code's SOURCE CONTROL view, stage the changes, enter a commit message like "Add specification for document upload and management feature," and then publish the new branch to your Git repository.

The specification defines the "what" without the "how." It doesn't specify programming languages, frameworks, database schemas, or code organization - those implementation details will be determined in the Plan and Tasks phases based on the constitution's technical constraints. The spec focuses on user needs and business requirements, making it easier to review with non-technical stakeholders.

## Clarify the Spec (iterate on requirements)

The `/speckit.clarify` command helps identify ambiguities, gaps, and underspecified areas in your specification. GitHub Copilot analyzes the spec and asks targeted questions to ensure all requirements are clear and complete before moving to the technical planning phase.

In this task, you use the clarification process to refine the document upload and management specification.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open.

1. In the Chat view, to start the clarification process, enter the following command:

    ```plaintext
    /speckit.clarify
    ```

1. Monitor GitHub Copilot's response.

    GitHub Copilot will analyze the spec.md file and generate clarification questions.

    For example, you may receive questions that are similar to the following:

    - "When storing documents in Azure Blob Storage, how should files be organized?"
    - "When a user uploads multiple files simultaneously, how should the system handle concurrent uploads?"
    - "When a user replaces a document file with an updated version (FR-026), what happens to the original file?"
    - "FR-048 states "Team Leads can view team documents" - what defines a team member's documents that a Team Lead can access?"
    - "For the upload progress indicator (FR-004), what level of detail should be shown to users?"

    The questions will be presented one at a time.

1. Consider each question appropriately before answering.

    In a production environment, your answers should reflect careful analysis of business needs, user experience considerations, and technical constraints. However, for this training, you can selected the recommended option for each question.

    When you provide an answer, GitHub Copilot updates the spec.md file with clarifications.

    > **NOTE**: If Copilot presents additional rounds of questions, continue answering until it indicates there are no further clarifications needed. The clarification process typically involves 1-2 rounds of questions as Copilot refines the specification.

1. Once the clarification process is complete, review the updated **spec.md** file, and then accept the changes.

    - Check that your answers are accurately reflected in the specification
    - Verify that previously ambiguous areas now have clear requirements
    - Look for any newly added acceptance criteria based on your clarifications

    You can make any manual edits if needed. For example, if GitHub Copilot interpreted an answer differently than you intended, edit the spec directly to correct it.

1. Save the updated **spec.md** file and commit your changes.

The clarified specification now provides comprehensive guidance for implementation. By addressing ambiguities upfront, you reduce the risk of building the wrong solution or having to make significant changes later in the development process.

## Generate the Technical Plan

The technical plan bridges the gap between the "what" (specification) and the "how" (implementation). It defines the architecture, technology choices, data models, API designs, and implementation approach while adhering to the constraints defined in the constitution.

In this task, you use GitHub Copilot's `/speckit.plan` command to generate a comprehensive technical implementation plan.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open..

1. In the Chat view, to start the technical planning process, enter the following command:

    ```dotnetcli
    /speckit.plan
    ```

1. Monitor GitHub Copilot's response and provide assistance in the Chat view.

    GitHub Copilot will analyze the constitution.md and spec.md files to generate the plan. Provide permission and assistance when required.

    It can take several minutes for GitHub Copilot to generate the technical plan in the plan.md file.

1. Once the plan workflow is complete, take a few minutes to review the following files:

    - **plan.md**
    - **research.md**
    - **quickstart.md**
    - **data-model.md**
    - **contracts/IBlobStorageService.md**
    - **contracts/IDocumentService.md**

    Verify that the plan addresses all constitutional constraints.

    Verify the plan includes implementation phases or milestones.

1. After reviewing the files, accept the updates.

    If the plan omits important details or makes assumptions you disagree with, you can:

    - Edit the plan.md file directly, or
    - Ask follow-up questions in Copilot Chat. For example:

    ```plaintext
    The plan should include a background job for processing virus scans. Add details about using Azure Functions with Queue Storage triggers to handle async file scanning after upload.
    ```

1. Save the files, and then commit and sync your changes.

The technical plan now serves as a blueprint for implementation. It translates business requirements into concrete technical decisions while respecting organizational constraints. This plan will guide the creation of actionable tasks in the next step.

## Create the task list

The task list breaks down the technical plan into specific, actionable implementation steps. Each task should be small enough to complete in a reasonable timeframe (typically a few hours to a day) and have clear acceptance criteria.

In this task, you use GitHub Copilot's `/speckit.tasks` command to generate a comprehensive task list.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open.

1. In the Chat view, to start generating the task list, enter the following command:

    ```dotnetcli
    /speckit.tasks
    ```

1. Monitor GitHub Copilot's response and provide assistance in the Chat view.

    GitHub Copilot will analyze the plan.md file and generate tasks in the tasks.md file.

    It can take several minutes for GitHub Copilot to generate the task list. Provide permission and assistance when required.

1. Once the plan workflow is complete, take a few minutes to review the **tasks.md** file.

    Review the generated task list. It should provide a list of tasks organized by phase and user story.

    Verify that the task list covers the requirements from the specification. For example:

    - Each functional requirement should map to one or more tasks
    - Security requirements should have corresponding implementation tasks
    - Performance requirements should have testing tasks
    - Integration points should have dedicated tasks

    Verify that tasks are ordered logically:

    - Foundation tasks (database, models) come first
    - Backend API tasks build on the foundation
    - Frontend tasks reference backend endpoints
    - Testing tasks come after implementation
    - Deployment tasks come last

1. Ensure each task is specific and actionable:

    - ✅ Good: "Create Document entity with properties: DocumentId, Title, Description, FileName, FileSize, BlobStorageUrl"
    - ❌ Vague: "Set up database stuff"

    Verify that tasks have reasonable scope:

    - Individual tasks should be completable in a few hours to a day
    - If a task seems too large, note that it may need to be broken down during implementation

    You can add task dependencies or notes if needed. For example:

    ```markdown
    - [ ] Task 12: Implement DocumentController POST /api/documents endpoint
      - Depends on: Task 11 (DocumentService)
      - Note: Include comprehensive error handling for file size limits and unsupported types
    ```

1. Save the `tasks.md` file, and then commit and sync your changes.

The task list now provides a clear roadmap for implementation. In the next task, you'll use GitHub Copilot to help implement these tasks systematically.

## Implement the specified feature using GitHub Copilot

With a clear specification, technical plan, and task list in place, you're ready to implement the document upload and management feature. This task demonstrates how spec-driven development guides implementation and how GitHub Copilot assists with code generation based on the context you've established.

In this task, you'll implement a subset of the feature to demonstrate the spec-driven development workflow. In a real project, you would complete all tasks, but for this exercise, you'll focus on core functionality: setting up the data model, implementing basic upload functionality, and creating a simple document list view.

Use the following steps to complete this task:

1. Review the task list in tasks.md and identify foundational tasks to implement:

    - Task 1: Create Document entity
    - Task 6: Implement upload service
    - Task 7: Create upload API endpoint
    - Task 14: Create upload UI component

1. Create the Document entity model

    In Visual Studio Code, create a new file in the existing `ContosoDashboard/Models/` folder: `ContosoDashboard/Models/Document.cs`

    The Document entity will follow the same pattern as existing entities (User, TaskItem, Project, ProjectMember, TaskComment, Notification, Announcement) in the ContosoDashboard/Models/ folder.

1. Create the `ContosoDashboard/Models/Document.cs` file and use GitHub Copilot to generate the entity:

    Type the following comment in the file:

    ```csharp
    // Document entity for storing uploaded file metadata
    // Properties: DocumentId (Guid), Title, Description, FileName, FileSize, FileType,
    // BlobStorageUrl, Category, UploadedBy, UploadDate, LastModifiedDate
    // Include data annotations for required fields and string lengths per spec requirements
    ```

    Position your cursor after the comment and press **Enter**. GitHub Copilot should generate the entity class. Review and accept the suggestion, or refine it as needed.

1. Implement the document upload service

    Create a new file in the existing `ContosoDashboard/Services/` folder: `ContosoDashboard/Services/DocumentService.cs`

    This service will follow the same pattern as existing services (TaskService, ProjectService, UserService, NotificationService, DashboardService) with authorization checks to prevent IDOR vulnerabilities.

    ```csharp
    // DocumentService for business logic
    // Constructor: inject IDocumentRepository, IBlobStorageService, ILogger
    // Method: UploadDocumentAsync(IFormFile file, DocumentUploadDto metadata, string userId)
    //   - Validate file size (max 25 MB per spec)
    //   - Validate file type (PDF, Office docs, images, text per spec)
    //   - Upload to Blob Storage
    //   - Create Document entity with metadata
    //   - Save to database via repository
    //   - Log audit trail
    //   - Return DocumentDto
    // Include comprehensive error handling and logging
    ```

1. **Create the upload API endpoint:**

    For the Blazor Server architecture used in ContosoDashboard, you'll implement upload functionality in the page's code-behind or inline code. If you choose to add Web API support for file uploads, create `ContosoDashboard/Controllers/DocumentsController.cs`:

    ```csharp
    // DocumentsController API endpoints
    // [Authorize] - require authentication
    // POST /api/documents - upload document
    //   - Accept IFormFile and DocumentUploadDto
    //   - Call DocumentService.UploadDocumentAsync
    //   - Return 201 Created with document details on success
    //   - Return 400 Bad Request for validation errors (file too large, unsupported type)
    // Include XML documentation comments for Swagger
    ```

1. **Create the upload UI component (Blazor page):**

    Create a new file in the existing `ContosoDashboard/Pages/` folder: `ContosoDashboard/Pages/Documents.razor`

    This page will follow the same pattern as existing pages (Tasks.razor, Projects.razor, ProjectDetails.razor, Team.razor, Notifications.razor, Profile.razor) and integrate with the existing MainLayout.razor and NavMenu.razor in the ContosoDashboard/Shared/ folder.

    Don't forget to:
    - Add `@page "/documents"` directive at the top
    - Add `@attribute [Authorize]` for authentication enforcement
    - Inject DocumentService using `@inject`
    - Use Bootstrap 5.3 classes for consistent styling
    - Add navigation link to ContosoDashboard/Shared/NavMenu.razor

    ```razor
    @* Document Upload Page *@
    @* Features per spec:
       - File selection input (multiple files)
       - Drag-and-drop zone
       - Metadata form: title (required), description, category dropdown, project dropdown, tags
       - Upload progress indicator
       - Success/error notifications
       - File size validation client-side (25 MB max)
    *@
    @page "/documents/upload"
    @inject HttpClient Http
    @inject NotificationService Notifications
    ```

    Use Copilot to generate the component markup and code-behind with the detailed requirements in comments.

1. **Test the implementation:**

    After implementing the core functionality, test the upload workflow:

    - Run the application locally
    - Navigate to the upload page
    - Select a file (PDF or image under 25 MB)
    - Fill in the metadata (title, category)
    - Click upload and verify:
        - Progress indicator appears
        - Success notification displays
        - File validation works correctly

1. **Mark completed tasks:**

    Open `tasks.md` and mark the tasks you've completed by changing `[ ]` to `[x]`:

    ```markdown
    - [x] Task 1: Create Document entity with EF Core model
    - [x] Task 6: Create DocumentService with upload logic
    - [x] Task 7: Implement DocumentController POST endpoint
    - [x] Task 14: Create DocumentUpload UI component
    ```

1. **Commit your implementation:**

    ```powershell
    git add .
    git commit -m "Implement core document upload and listing functionality"
    git push
    ```

**Key Observations:**

- GitHub Copilot generates code that aligns with your spec because it references the `spec.md`, `plan.md`, and `tasks.md` files in your workspace
- Detailed comments based on specification requirements guide Copilot to produce accurate implementations
- The spec-driven approach ensures you don't forget requirements (file size limits, supported types, etc.) because they're explicitly documented
- Having clear acceptance criteria makes it easy to verify that your implementation meets requirements

In a full implementation, you would continue through all remaining tasks in the task list, systematically building out the complete feature. The spec-driven development approach keeps you focused on requirements and prevents scope creep or missed functionality.

## Review and verify the implementation

The final step in spec-driven development is to verify that the implementation meets all requirements defined in the specification and that the code adheres to the principles established in the constitution.

In this task, you perform a comprehensive review of the implementation and ensure all acceptance criteria are satisfied.

Use the following steps to complete this task:

1. **Verify specification compliance:**

    Open the `spec.md` file and systematically check each requirement:

    - Open the document upload page in the running application
    - Test file upload with a PDF under 25 MB - verify it succeeds
    - Test file upload with a file over 25 MB - verify it's rejected with appropriate error message
    - Test file upload with an unsupported type (e.g., .exe) - verify it's rejected
    - Test multiple file upload if implemented
    - Verify metadata fields are captured (title, description, category)
    - Check that uploaded documents appear in the document list
    - Test sorting and filtering functionality
    - Test search if implemented

1. **Review acceptance criteria:**

    For each acceptance criterion in the spec, verify implementation:

    ```markdown
    Acceptance Criteria Review:
    - [x] User can upload PDF, Word, Excel, PowerPoint, text, and image files
    - [x] Files over 25 MB are rejected with error message
    - [x] Unsupported file types are rejected
    - [x] Upload progress is displayed during file transfer
    - [x] Success notification appears after successful upload
    - [x] Document metadata is captured and stored
    - [ ] Virus scanning is performed (may be stubbed for demo)
    - [x] Documents appear in My Documents view
    - [x] Documents can be sorted by name, date, size
    - [ ] Documents can be filtered by category and project
    - [ ] Full-text search works with results under 2 seconds
    - [ ] Storage quota is enforced
    - [ ] Audit logging captures all document actions
    ```

1. **Verify constitution compliance:**

    Open the `constitution.md` file and check that the implementation adheres to the principles:

    - **Azure Services**: Confirm Azure Blob Storage is used for file storage
    - **Authentication**: Verify Entra ID authentication is required for document endpoints
    - **Security**: Check that files are validated before storage
    - **Coding Standards**: Review code for adherence to C# conventions, XML documentation comments
    - **Error Handling**: Verify meaningful error messages without exposing sensitive information
    - **Logging**: Confirm Application Insights or logging is implemented
    - **Testing**: Check that unit tests exist for core functionality

1. **Run automated tests:**

    If you implemented tests, run them to verify functionality:

    ```powershell
    dotnet test
    ```

    Review test results and address any failures.

1. **Perform code quality review:**

    Review the code for:

    - **Readability**: Is the code easy to understand?
    - **Maintainability**: Is the code modular and well-organized?
    - **Performance**: Are there any obvious performance issues (e.g., N+1 queries)?
    - **Security**: Are there any security vulnerabilities (e.g., missing authorization checks)?

1. **Check for incomplete tasks:**

    Open `tasks.md` and identify any tasks marked as incomplete. For a production implementation, all tasks would need to be completed. For this exercise, document which tasks remain:

    ```markdown
    Remaining Tasks for Production Readiness:
    - Task 19: Document preview generation
    - Task 20: Full-text search implementation
    - Task 21-22: Sharing and version management
    - Task 26-30: Complete security hardening
    - Task 47-54: Comprehensive test coverage
    - Task 58-60: Production deployment and monitoring
    ```

1. **Document lessons learned:**

    Create a brief retrospective on the spec-driven development process. You can add this to a new file `RETROSPECTIVE.md`:

    ```markdown
    # Spec-Driven Development Retrospective
    
    ## What Worked Well:
    - Clear specification eliminated ambiguity about requirements
    - Constitution prevented scope creep and ensured Azure compliance
    - Task breakdown made implementation manageable
    - GitHub Copilot generated high-quality code aligned with spec
    - Acceptance criteria provided clear targets for implementation
    
    ## Challenges:
    - Initial specification took time to create thoroughly
    - Some clarification questions required business stakeholder input
    - Balancing detail in spec vs. flexibility in implementation
    
    ## Key Insights:
    - Investing time in specification upfront saves time during implementation
    - Having AI assist with spec/plan generation accelerates the process
    - Clear acceptance criteria make testing straightforward
    - The structured approach improves code quality and consistency
    
    ## Recommendations for Future Projects:
    - Use spec-driven development for all medium to large features
    - Involve stakeholders in specification review before planning
    - Keep constitution updated as organizational policies evolve
    - Consider templates for common specification sections
    ```

1. **Create a final summary:**

    Update the project README or create a summary of what was accomplished:

    ```markdown
    # ContosoDashboard - Document Management Feature
    
    ## Implementation Summary
    
    Implemented document upload and management capability for ContosoDashboard using spec-driven development methodology with GitHub Spec Kit.
    
    ## Features Implemented:
    - Document upload with file type and size validation
    - Metadata capture (title, description, category, tags)
    - Document listing with sorting and filtering
    - Integration with existing dashboard navigation
    - Role-based access control
    - Azure Blob Storage integration
    - Audit logging for compliance
    
    ## Features Planned (Not Yet Implemented):
    - Document sharing with granular permissions
    - Version management with 30-day history
    - In-browser preview for PDFs and images
    - Full-text search across document content
    - Storage quota enforcement and notifications
    - Advanced admin reporting and analytics
    
    ## Technical Stack:
    - ASP.NET Core 8.0
    - Entity Framework Core 8
    - Azure Blob Storage
    - Azure SQL Database
    - Blazor Server (or React/Angular)
    - Microsoft Entra ID authentication
    
    ## Artifacts:
    - `constitution.md` - Project governing principles
    - `spec.md` - Feature specification with requirements
    - `plan.md` - Technical implementation plan
    - `tasks.md` - Detailed task breakdown
    ```

1. **Commit all final changes:**

    ```powershell
    git add .
    git commit -m "Complete document management implementation with spec-driven development"
    git push
    ```

1. **Reflect on the spec-driven development process:**

    Consider how this approach differed from traditional development:

    - **Traditional Approach**: Jump directly into coding based on verbal requirements, discover ambiguities during implementation, make assumptions that may not align with business needs
    - **Spec-Driven Approach**: Invest time upfront in creating detailed specification, clarify all ambiguities before coding, use AI to accelerate spec/plan creation, implement with confidence that requirements are correct

    The key benefit is that the specification serves as a contract between stakeholders and developers, reducing rework and ensuring the final product meets actual business needs.

**Congratulations!** You've successfully completed the spec-driven development exercise. You've learned how to:

- Set up GitHub Spec Kit in a development environment
- Import an existing application repository for realistic feature development
- Create a project constitution with organizational constraints
- Generate a detailed specification from high-level requirements
- Use AI-assisted clarification to refine the specification
- Create a technical implementation plan aligned with the constitution
- Break down the plan into actionable tasks
- Implement features systematically using the structured guidance
- Verify that the implementation meets all requirements

This methodology can be applied to any software development project, especially when working with AI coding assistants like GitHub Copilot. The structured approach ensures that AI-generated code aligns with business requirements and organizational standards, resulting in higher-quality software delivered more efficiently.
