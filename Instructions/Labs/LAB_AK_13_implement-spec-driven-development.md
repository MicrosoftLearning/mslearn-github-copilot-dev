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

You're a software developer working for a consulting firm that's adopting spec-driven development (SDD) using GitHub Spec Kit and GitHub Copilot in Visual Studio Code. Your client, Contoso Corporation, has tasked you with adding a new feature to their internal employee dashboard application called ContosoDashboard.

Contoso's business stakeholders have provided high-level requirements for a document upload and management feature. Employees need the ability to upload work-related documents, organize them by category and project, and share them with team members. The feature must integrate seamlessly with the existing dashboard while maintaining security and compliance standards.

Your assignment is to use the spec-driven development methodology to implement this feature. Rather than jumping directly into coding, you'll create structured specifications, plans, and tasks that guide the development process. This approach ensures the implementation aligns with business requirements and organizational constraints.

You'll work with the following application:

- **ContosoDashboard**: An internal web application that provides Contoso employees with a centralized platform for managing daily work activities, including tasks, projects, team collaboration, and notifications.

**Application Context**: ContosoDashboard currently serves 5,000 Contoso employees. The application uses role-based access control (Employee, Team Lead, Project Manager, Administrator) and integrates with Contoso's Microsoft Entra ID for authentication. The existing application includes task management, project tracking, team collaboration features, and notification capabilities.

This exercise includes the following tasks:

1. Configure the GitHub Spec Kit in your development environment.
1. Import and initialize the existing ContosoDashboard application repository.
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

    - SSL certificate errors in corporate environments: Corporate networks may intercept HTTPS connections. Configure `uv` to use your organization's certificate bundle:

        ```powershell
        $env:SSL_CERT_FILE = "C:\path\to\corporate-ca-bundle.crt"
        uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
        ```

1. Open Visual Studio Code, and then ensure that GitHub Copilot Chat is enabled.

    The GitHub Spec Kit integrates with Visual Studio Code through the GitHub Copilot Chat extension.

    For GitHub Enterprise Server environments:

    - Open VS Code Settings (Ctrl+,)

    - Search for "github.enterprise"

    - Set "Github: Enterprise Uri" to your server URL (for example, `https://github.yourcompany.com`)

    - Sign in using your enterprise credentials

Your GitHub Spec Kit development environment is now configured and ready. In the next task, you'll import the existing ContosoDashboard application and initialize it for spec-driven development.

## Import and initialize the ContosoDashboard repository

In this task, you import the existing ContosoDashboard application repository to your GitHub account and initialize it with GitHub Spec Kit. This mirrors a real-world scenario where you add a new feature to an existing application rather than building from scratch.

The ContosoDashboard application is a fully functional ASP.NET Core 8.0 Blazor Server application with existing features including task management, project tracking, team collaboration, and notifications. You'll use GitHub Spec Kit to plan and implement the document upload and management feature as an addition to this working application.

### Step 1: Import the ContosoDashboard repository to your GitHub account

GitHub Importer allows you to create a complete copy of an existing repository in your own GitHub account, giving you full control over the imported copy.

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

1. Ensure that the repository is set to **Public**.

1. Select the **Begin import** button.

    GitHub uses the import process to create the new repository in your account.

    > **NOTE**: It can take a minute or two for the import process to finish.

1. Wait for the import process to complete (typically 1-2 minutes).

    GitHub will display a progress indicator and notify you when the import is complete.

1. Once complete, select **View repository** or navigate to `https://github.com/YOUR-USERNAME/ContosoDashboard` to view your imported repository.

1. Explore the repository structure to familiarize yourself with the existing application:

    - `Models/` - Contains 7 entity classes (User, TaskItem, Project, etc.)
    - `Data/` - ApplicationDbContext with EF Core configuration
    - `Services/` - 5 service implementations for business logic
    - `Pages/` - 7 Blazor pages (Dashboard, Tasks, Projects, Team, Notifications, Profile, ProjectDetails)
    - `Shared/` - Layout components and navigation
    - `README.md` and `PROJECT_SUMMARY.md` - Application documentation

### Step 2: Clone the repository and initialize GitHub Spec Kit

Now you'll clone your imported repository and initialize it with GitHub Spec Kit for spec-driven development.

1. On your ContosoDashboard repository page in GitHub, select the **Code** button and copy the HTTPS URL.

    The URL should be similar to: `https://github.com/YOUR-USERNAME/ContosoDashboard.git`

1. Open a terminal window, and then navigate to the location where you want to clone the project:

    For example:

    Open a terminal window (Command Prompt, PowerShell, or Terminal), and then run:

    ```powershell
    cd C:\TrainingProjects
    ```

    Replace `C:\TrainingProjects` with your preferred location. You can use any directory where you have write permissions, and you can create a new folder location if needed.

1. Clone your ContosoDashboard repository:

    ```powershell
    git clone https://github.com/YOUR-USERNAME/ContosoDashboard.git
    ```

    Replace `YOUR-USERNAME` with your actual GitHub username.

1. Navigate into the cloned repository:

    ```powershell
    cd ContosoDashboard
    ```

1. Initialize GitHub Spec Kit within the existing project:

    ```powershell
    specify init --existing --ai copilot --script ps
    ```

    The `specify init` command uses the following options:

    - `--existing` - Initializes Spec Kit in the current directory (existing project)
    - `--ai copilot` - Configures the project for GitHub Copilot
    - `--script ps` - Uses PowerShell scripts (use `--script sh` for bash/zsh on macOS/Linux)

    > **Note**: If you're using macOS or Linux, replace `--script ps` with `--script sh`.

1. Wait for the initialization to complete.

    The CLI will:

    - Detect the existing Git repository
    - Add the `.github/prompts/` directory with Spec Kit commands
    - Create template files: `constitution.md`, `spec.md`, `plan.md`, `tasks.md`
    - Preserve all existing application files
    - Display a success message

1. Verify the Spec Kit files were added:

    ```powershell
    ls -Name
    ```

    You should see the new files alongside existing application files:

    ```plaintext
    constitution.md
    spec.md
    plan.md
    tasks.md
    .github/
    Models/
    Data/
    Services/
    Pages/
    Shared/
    (... other existing files ...)
    ```

### Step 3: Commit and push Spec Kit initialization

Commit the Spec Kit files to your repository to track the spec-driven development process.

1. Stage the new Spec Kit files:

    ```powershell
    git add .github/ constitution.md spec.md plan.md tasks.md
    ```

1. Create a commit:

    ```powershell
    git commit -m "Initialize GitHub Spec Kit for document management feature"
    ```

1. Push to GitHub:

    ```powershell
    git push
    ```

    If prompted for GitHub credentials, sign in to authorize the push.

1. Verify the push succeeded by refreshing your GitHub repository page in the browser. You should now see the Spec Kit files alongside the existing application code.

### Step 4: Open the project in Visual Studio Code and explore

Open the project in VS Code to familiarize yourself with the existing application and begin the spec-driven development process.

1. Open the project in Visual Studio Code:

    ```powershell
    code .
    ```

    This command opens the current directory (ContosoDashboard) in VS Code.

1. Wait for VS Code to fully load the project. Explore the EXPLORER view to see the existing application structure:

    ```plaintext
    CONTOSODASHBOARD
    ├── .github/
    │   └── prompts/          (Spec Kit commands)
    ├── constitution.md       (Spec Kit file)
    ├── spec.md               (Spec Kit file)
    ├── plan.md               (Spec Kit file)
    ├── tasks.md              (Spec Kit file)
    ├── Models/               (Existing: User, TaskItem, Project, etc.)
    ├── Data/                 (Existing: ApplicationDbContext)
    ├── Services/             (Existing: TaskService, ProjectService, etc.)
    ├── Pages/                (Existing: Index, Tasks, Projects, etc.)
    ├── Shared/               (Existing: MainLayout, NavMenu)
    ├── Program.cs            (Existing: App configuration)
    ├── README.md             (Existing: Documentation)
    └── (other application files)
    ```

1. Review the existing application documentation:

    - Open `README.md` to understand the current application features
    - Open `PROJECT_SUMMARY.md` to see the technical implementation details
    - Browse the `Models/` folder to see the existing data entities

1. Open the Copilot Chat view:

    - Press **Ctrl+Alt+I** (Windows/Linux) or **Cmd+Alt+I** (macOS)
    - Or click the **Chat** icon in the Activity Bar

1. Verify that GitHub Spec Kit commands are available:

    - In the Chat input field, type `/speckit`
    - You should see autocomplete suggestions appear showing available commands:
        - `/speckit.constitution`
        - `/speckit.specify`
        - `/speckit.clarify`
        - `/speckit.plan`
        - `/speckit.tasks`
        - `/speckit.implement`
        - `/speckit.analyze`
        - `/speckit.checklist`

    > **Note**: If the `/speckit` commands don't appear, reload VS Code by pressing **Ctrl+Shift+P**, typing "Reload Window", and pressing Enter. Verify you opened the `ContosoDashboard` folder (not a parent or child directory) and that `.github/prompts/` exists in your workspace root.

    **Troubleshooting**: If you encounter issues:

    - **"specify command not found"**: Ensure you completed Task 1 and installed the Specify CLI. Run `specify version` to verify installation.
    - **Permission denied errors**: On Windows, ensure you're running PowerShell with appropriate permissions. On macOS/Linux, check file permissions.
    - **Git clone errors**: Verify you're signed in to GitHub and have access to your imported repository.
    - **Spec Kit commands not appearing**: Ensure `.github/prompts/` exists in your workspace root. Try reloading VS Code.

**Understanding the Setup**: You now have a working ContosoDashboard application with GitHub Spec Kit initialized. The existing application provides:

- User authentication and role-based access control (Employee, Team Lead, Project Manager, Administrator)
- Task management with status tracking and assignments
- Project management with team members and progress tracking
- Team collaboration features
- Notification system
- User profile management

You'll use GitHub Spec Kit to plan and implement the new document upload and management feature as an addition to these existing capabilities. The spec-driven approach ensures the new feature integrates seamlessly with the existing architecture while maintaining code quality and security standards.

Your ContosoDashboard project is now ready for spec-driven development. In the next task, you'll define the project constitution to establish development principles for adding the document management feature.

## Define the project Constitution

The constitution establishes the governing principles and constraints that guide all development decisions for the ContosoDashboard project. It captures organizational policies, technical standards, security requirements, and development practices that must be followed throughout implementation.

In this task, you use GitHub Copilot's `/speckit.constitution` command to generate a comprehensive constitution based on Contoso Corporation's requirements.

### Step 1: Review the constitution template

Before generating content, familiarize yourself with the constitution structure.

1. In Visual Studio Code, open the `constitution.md` file from the EXPLORER view.

1. Observe the template structure (if present). The file may be empty or contain placeholder sections such as:

    - Project Overview
    - Development Principles
    - Technical Constraints
    - Security and Compliance Requirements
    - Quality Standards

1. Keep the file open - Copilot will populate it with generated content.

### Step 2: Generate the constitution using GitHub Copilot

Use the `/speckit.constitution` command to create the project's governing principles.

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. Start a new chat session to ensure a clean context:

    - Click the **New Chat** button (the **+** icon) at the top of the Chat panel

1. In the Chat input field, type the `/speckit.constitution` command:

    ```plaintext
    /speckit.constitution
    ```

    Press **Enter** to execute the command.

1. GitHub Copilot will prompt you for information about the project principles and constraints. Provide the following context:

    ```plaintext
    Create a constitution for the ContosoDashboard project document management feature:

    **Existing Application Context:**
    - ContosoDashboard is a working ASP.NET Core 8.0 Blazor Server application
    - Current features: task management, project tracking, team collaboration, notifications, user profiles
    - Uses Entity Framework Core 8 with SQL Server LocalDB
    - Implements role-based access (Employee, Team Lead, Project Manager, Administrator)
    - Already has 7 entity models, 5 services, and complete UI pages

    **Technology Stack (Must Match Existing):**
    - Backend: ASP.NET Core 8.0 with Entity Framework Core 8
    - UI: Blazor Server with Bootstrap 5.3
    - Cloud: Microsoft Azure (App Service, SQL Database, Blob Storage, Key Vault)
    - Authentication: Microsoft Entra ID (infrastructure already in place)
    - Development: .NET 8.0 SDK

    **Architecture Principles (Must Follow Existing Patterns):**
    - Repository pattern for data access (match existing services)
    - Service layer for business logic (follow TaskService, ProjectService patterns)
    - Dependency Injection (already configured in Program.cs)
    - Async/await for all I/O operations (existing standard)
    - Entity models in Models/ folder with proper relationships

    **Security:**
    - TLS 1.3 encryption for data in transit
    - Encryption at rest for all stored data
    - Role-based access control (RBAC) matching existing roles
    - Virus scanning for uploaded files
    - Store sensitive configuration in Azure Key Vault
    - Follow existing authentication patterns

    **Performance:**
    - Page loads under 2 seconds (match existing pages)
    - API responses under 500ms
    - Support 1,000 concurrent users
    - Database indexes on frequently queried fields (follow existing pattern)

    **Quality:**
    - 80% code coverage with unit tests
    - XML documentation for public APIs
    - WCAG 2.1 Level AA accessibility
    - Code style consistent with existing codebase
    - Integration with existing navigation and layout components
    ```

1. Press **Enter** and wait for GitHub Copilot to process the information and generate the constitution.

    This may take 30-60 seconds as Copilot analyzes the requirements and structures the constitution document.

### Step 3: Review and refine the generated constitution

After Copilot generates the constitution, review it carefully to ensure it captures all requirements.

1. Copilot will update the `constitution.md` file. Review the generated content in the editor.

1. Verify the constitution includes sections covering:

    - **Project Identity**: Name, purpose, and scope of the ContosoDashboard application
    - **Technology Stack**: Azure services, ASP.NET Core, Entity Framework Core, Entra ID
    - **Security Requirements**: Encryption, RBAC, virus scanning, secure configuration
    - **Performance Benchmarks**: Page load times, API response times, concurrent user support
    - **Quality Standards**: Test coverage, documentation, accessibility, architecture patterns

1. Check that each principle is clearly stated and actionable. For example:

    - ❌ Vague: "Use good security practices"
    - ✅ Clear: "All API endpoints must validate authentication tokens and enforce role-based permissions"

1. If any critical requirements are missing or unclear, you can edit the `constitution.md` file directly to add or modify principles. For example, you might add:

    - All uploaded files must be scanned for malware before storage
    - File size limits must be enforced (max 25 MB per file)
    - Document management features must support PDF and Microsoft Office file formats

### Step 4: Validate and save the constitution

Ensure the constitution is complete and save it for use throughout the development process.

1. Review the entire document one final time to check for:

    - Completeness: All major areas (security, performance, quality, technical standards) are covered
    - Clarity: Each principle is specific and unambiguous
    - Consistency: Principles don't contradict each other
    - Relevance: All principles relate to the ContosoDashboard project

1. Save the `constitution.md` file if it hasn't been saved automatically:

    - Press **Ctrl+S** (Windows/Linux) or **Cmd+S** (macOS)

1. Commit the constitution to your Git repository:

    Open a new terminal in VS Code (Terminal > New Terminal) and run:

    ```powershell
    git add constitution.md
    git commit -m "Add project constitution with development principles and constraints"
    git push
    ```

1. Verify the commit by checking your GitHub repository in the browser. The `constitution.md` file should now appear with your commit message.

**Understanding the Constitution's Role**: The constitution you just created will guide all subsequent development decisions. When GitHub Copilot generates the spec, plan, and tasks in the following steps, it will reference these principles to ensure the implementation aligns with Contoso's requirements. For example:

- When generating the technical plan, Copilot will ensure Azure services are specified (not AWS or GCP)
- When creating tasks for file upload, Copilot will include validation and security scanning steps
- When suggesting code implementations, Copilot will follow .NET conventions and include proper error handling

The constitution serves as a "contract" between business requirements and technical implementation, ensuring consistency throughout the spec-driven development process.

You've successfully defined the project constitution. In the next task, you'll create a detailed specification for the document upload and management feature.

## Create the Spec for the document upload and management feature

The specification (spec) defines what you're building from the user's perspective. It describes features, user stories, acceptance criteria, and business requirements without prescribing how to implement them. A well-written spec serves as the foundation for creating implementation plans and tasks.

In this task, you use GitHub Copilot's `/speckit.specify` command to generate a detailed specification for the document upload and management feature based on high-level requirements from Contoso's business stakeholders.

Use the following steps to complete this task:

1. Review the high-level requirements document located in the lab materials:

    Navigate to the following folder: spec-driven-development

1. Open the upload-manage-docs-feature-requirements-simplified.md file in Visual Studio Code or a text editor.

1. Take 1-2 minutes to read through the requirements document, paying particular attention to:

    - **Business Need**: Why Contoso needs this feature (centralized document storage, security)
    - **Target Users**: All Contoso employees with role-based permissions
    - **Core Requirements**: The 6 requirement areas covering upload, organization, access, integration, performance, and audit
    - **Success Metrics**: How feature success will be measured (e.g., 70% adoption rate, documents found in under 30 seconds)
    - **Technical Constraints**: Azure infrastructure, 8-10 week timeline, security policies
    - **Out of Scope**: Features not included in this release

1. In Visual Studio Code with the ContosoDashboard project open, open the Copilot Chat view (press **Ctrl+Alt+I**).

1. Start a new chat session by clicking the **New Chat** button (the **+** icon at the top of the Chat panel).

1. In the Chat input field, enter the `/speckit.specify` command and press **Enter**.

1. When prompted to describe the feature you want to build, provide a comprehensive description based on the requirements document:

    ```plaintext
    Feature: Document Upload and Management for ContosoDashboard
    
    Enable employees to upload work-related documents (PDF, Office, images, text), organize by category/project, share with team members, and search efficiently. Must integrate with existing dashboard features while maintaining security.
    
    Target Users: All 5,000 Contoso employees with role-based access (Employee, Team Lead, Project Manager, Administrator).
    
    Core Capabilities:
    1. Upload: Multiple files, max 25 MB each, supported types (PDF, Office docs, images, text), metadata (title, category, description, project, tags), progress indicator, virus scanning
    2. Organization: My Documents view, Project Documents view, search by title/description/tags/uploader/project (results under 2 seconds)
    3. Management: Download, in-browser preview (PDF/images), edit metadata, replace files, delete documents, sharing with notifications
    4. Integration: Attach to tasks, dashboard Recent Documents widget, notifications for sharing/new project docs
    5. Performance: Upload in 30s (25 MB files), list load in 2s (500 docs), search in 2s, preview in 3s
    6. Audit: Log all uploads/downloads/deletions/sharing, admin reports
    
    Security: Azure Blob Storage encryption at rest, TLS 1.3 in transit, RBAC enforcement, virus scanning.
    
    Success Criteria: 70% adoption in 3 months, find docs under 30s, 90% properly categorized, zero security incidents.
    
    Constraints: Azure Blob Storage, ASP.NET Core integration, 8-10 week timeline, Entra ID authentication.
    
    Out of Scope: Version history, storage quotas, soft delete/trash, collaborative editing, external integrations, mobile apps.
    ```

1. Press **Enter** and wait 1-2 minutes for GitHub Copilot to generate the specification in the `spec.md` file.

1. Open the `spec.md` file from the EXPLORER view and verify it includes these sections:

    - **Feature Summary**: High-level overview of the document upload and management feature
    - **User Stories**: User-centric descriptions (e.g., "As an Employee, I want to upload documents...")
    - **Functional Requirements**: Detailed capabilities organized by category
    - **User Interface Requirements**: Description of the expected user experience
    - **Security and Compliance**: Authentication, authorization, data protection requirements
    - **Performance Requirements**: Specific benchmarks for load times and response times
    - **Acceptance Criteria**: Testable conditions that must be met

1. Check that key requirements from your input are captured:

    - File size limits (25 MB per file)
    - Supported file types (PDF, Office documents, images, text files)
    - Performance targets (2-second page loads, 30-second uploads)
    - Security measures (virus scanning, encryption, RBAC)
    - Integration points (tasks, dashboard widgets, notifications)

1. Verify acceptance criteria are specific and testable:

    - ✅ Good: "When an Employee uploads a PDF under 25 MB, the system accepts it and displays a success message"
    - ✅ Good: "When a file exceeds 25 MB, the system rejects it with error message 'File size exceeds 25 MB limit'"
    - ❌ Avoid: Vague criteria like "Upload should work well" or "System should be fast"

1. Save the `spec.md` file (press **Ctrl+S** or **Cmd+S**).

1. Commit the specification to your Git repository:

    ```powershell
    git add spec.md
    git commit -m "Add specification for document upload and management feature"
    git push
    ```

The specification defines the "what" without the "how." It doesn't specify programming languages, frameworks, database schemas, or code organization - those implementation details will be determined in the Plan and Tasks phases based on the constitution's technical constraints. The spec focuses on user needs and business requirements, making it easier to review with non-technical stakeholders.

## Clarify the Spec (iterate on requirements)

The `/speckit.clarify` command helps identify ambiguities, gaps, and underspecified areas in your specification. GitHub Copilot analyzes the spec and asks targeted questions to ensure all requirements are clear and complete before moving to the technical planning phase.

In this task, you use the clarification process to refine the document upload and management specification.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. In the Chat input field, enter the `/speckit.clarify` command and press **Enter**.

1. GitHub Copilot will analyze the `spec.md` file and generate clarification questions. You may receive questions such as:

    - "Should drag-and-drop upload be supported in addition to the file selection dialog?"
    - "What happens to documents when a user's employment ends?"
    - "What specific virus scanning service should be used?"
    - "Should document titles be unique within a user's library, or can duplicates exist?"
    - "How should search results be ranked?"

1. Answer each question thoughtfully. Here are example responses you can provide:

    For drag-and-drop support:

    ```plaintext
    Yes, support drag-and-drop upload for better user experience. The same validation rules apply (file type, size limits, virus scanning).
    ```

    For user termination:

    ```plaintext
    When an employee leaves, personal documents are deleted after 90 days. Project documents remain with the project. Team Leads are notified to review critical documents.
    ```

    For virus scanning:

    ```plaintext
    Use Microsoft Defender for Cloud since we're using Azure. Files must pass scanning before storage. Quarantine failed files and notify the uploader.
    ```

    For duplicate titles:

    ```plaintext
    Allow duplicate titles. Use unique document IDs internally, and display upload date/category to distinguish duplicates in the UI.
    ```

    For search ranking:

    ```plaintext
    Rank by relevance (title matches first, then tags, then description), with recent uploads as tiebreaker. Allow users to re-sort by date or file size.
    ```

1. After you provide each answer, GitHub Copilot updates the `spec.md` file with clarifications.

1. If Copilot presents additional rounds of questions, continue answering until it indicates there are no further clarifications needed.

    The clarification process typically involves 1-2 rounds of questions as Copilot refines the specification.

1. Once the clarification process is complete, review the updated `spec.md` file:

    - Check that your answers are accurately reflected in the specification
    - Verify that previously ambiguous areas now have clear requirements
    - Look for any newly added acceptance criteria based on your clarifications

1. Make any manual edits if needed. For example, if Copilot interpreted an answer differently than you intended, edit the spec directly to correct it.

1. Save the updated `spec.md` file and commit your changes:

    ```powershell
    git add spec.md
    git commit -m "Clarify specification with detailed requirements for edge cases and workflows"
    git push
    ```

The clarified specification now provides comprehensive guidance for implementation. By addressing ambiguities upfront, you reduce the risk of building the wrong solution or having to make significant changes later in the development process.

## Generate the Technical Plan

The technical plan bridges the gap between the "what" (specification) and the "how" (implementation). It defines the architecture, technology choices, data models, API designs, and implementation approach while adhering to the constraints defined in the constitution.

In this task, you use GitHub Copilot's `/speckit.plan` command to generate a comprehensive technical implementation plan.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. In the Chat input field, enter the `/speckit.plan` command and press **Enter**.

1. GitHub Copilot will analyze the `constitution.md` and `spec.md` files to generate the plan. It may prompt you for additional technical context. Provide the following information:

    ```plaintext
    Technology Stack Context:
    
    Backend: ASP.NET Core 8.0 Web API with C#, Entity Framework Core 8
    Database: Azure SQL Database
    Storage: Azure Blob Storage for document files
    Authentication: Microsoft Entra ID with JWT tokens
    Hosting: Azure App Service
    
    Architecture:
    - Repository pattern for data access
    - Service layer for business logic
    - Dependency Injection
    - DTOs for API contracts
    
    Development:
    - Async/await for all I/O operations
    - Unit tests with xUnit
    - XML documentation for public APIs
    - Application Insights for logging
    ```

1. Wait 2-3 minutes for GitHub Copilot to generate the technical plan in the `plan.md` file.

1. Open the `plan.md` file from the EXPLORER view and verify it includes these sections:

    - **Architecture Overview**: High-level system design and component interactions
    - **Technology Stack**: Specific versions and frameworks to be used
    - **Data Model**: Database schema, entities, and relationships
    - **API Design**: RESTful endpoints, request/response formats
    - **Azure Resources**: Required cloud services and configurations
    - **Security Implementation**: Authentication, authorization, encryption details
    - **File Processing Workflow**: Upload pipeline, virus scanning, storage
    - **Frontend Components**: UI pages, components, and user flows
    - **Integration Points**: How the feature connects with existing dashboard features
    - **Testing Strategy**: Unit tests, integration tests, and performance tests
    - **Deployment Approach**: CI/CD pipeline steps and environment configuration

1. Review the data model section. It should include entities such as:

    - **Document**: DocumentId, Title, Description, FileName, FileSize, FileType, BlobStorageUrl, Category, UploadedBy, UploadDate, LastModifiedDate
    - **DocumentTag**: TagId, DocumentId, TagName
    - **DocumentShare**: ShareId, DocumentId, SharedBy, SharedWith, SharedDate
    - **DocumentAuditLog**: LogId, DocumentId, Action (Upload/Download/Delete/Share), UserId, Timestamp

1. Review the API design section. It should include endpoints such as:

    ```plaintext
    POST   /api/documents                  - Upload new document
    GET    /api/documents                  - Get user's documents (with filtering/sorting)
    GET    /api/documents/{id}             - Get document details
    PUT    /api/documents/{id}             - Update document metadata or replace file
    DELETE /api/documents/{id}             - Delete document
    GET    /api/documents/{id}/download    - Download document
    GET    /api/documents/{id}/preview     - Get preview URL
    POST   /api/documents/{id}/share       - Share document with users
    GET    /api/documents/project/{id}     - Get project documents
    GET    /api/documents/search           - Search documents
    ```

1. Check that the plan addresses all constitutional constraints:

    - Uses Azure services (Blob Storage, SQL Database, Functions)
    - Implements Entra ID authentication
    - Includes encryption at rest and in transit
    - Follows ASP.NET Core coding conventions
    - Includes logging and audit trails
    - Specifies Entity Framework Core with repository pattern

1. Verify the plan includes implementation phases or milestones. For example:

    - **Phase 1**: Database schema and models (Week 1)
    - **Phase 2**: Azure Blob Storage integration and file upload API (Week 2-3)
    - **Phase 3**: Document listing, search, and filtering (Week 4)
    - **Phase 4**: Version management and sharing features (Week 5-6)
    - **Phase 5**: Dashboard integration and notifications (Week 7)
    - **Phase 6**: Testing, security review, and deployment (Week 8-10)

1. If the plan omits important details or makes assumptions you disagree with, you can:

    - Edit the `plan.md` file directly, or
    - Ask follow-up questions in Copilot Chat. For example:

    ```plaintext
    The plan should include a background job for processing virus scans. Add details about using Azure Functions with Queue Storage triggers to handle async file scanning after upload.
    ```

1. Save the `plan.md` file and commit your changes:

    ```powershell
    git add plan.md
    git commit -m "Add technical implementation plan for document management feature"
    git push
    ```

The technical plan now serves as a blueprint for implementation. It translates business requirements into concrete technical decisions while respecting organizational constraints. This plan will guide the creation of actionable tasks in the next step.

## Create the Task List

The task list breaks down the technical plan into specific, actionable implementation steps. Each task should be small enough to complete in a reasonable timeframe (typically a few hours to a day) and have clear acceptance criteria.

In this task, you use GitHub Copilot's `/speckit.tasks` command to generate a comprehensive task list.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. In the Chat input field, enter the `/speckit.tasks` command and press **Enter**.

1. GitHub Copilot will analyze the `plan.md` file and generate tasks in the `tasks.md` file. Wait 1-2 minutes for the generation to complete.

1. Open the `tasks.md` file from the EXPLORER view.

1. Review the generated task list. It should include tasks such as:

    **Data Model (5 tasks):**
    - [ ] Task 1: Create Document entity with EF Core model
    - [ ] Task 2: Create DocumentTag, DocumentShare, and DocumentAuditLog entities
    - [ ] Task 3: Generate and apply EF Core migrations
    - [ ] Task 4: Create repository interfaces
    - [ ] Task 5: Implement repository classes

    **Backend API (8 tasks):**
    - [ ] Task 6: Create DocumentService with upload logic
    - [ ] Task 7: Implement POST /api/documents endpoint for upload
    - [ ] Task 8: Implement GET /api/documents endpoint with filtering/sorting
    - [ ] Task 9: Implement GET /api/documents/{id}/download endpoint
    - [ ] Task 10: Implement document preview generation
    - [ ] Task 11: Implement document search endpoint
    - [ ] Task 12: Implement sharing endpoints
    - [ ] Task 13: Implement audit logging

    **Frontend UI (5 tasks):**
    - [ ] Task 14: Create DocumentUpload component with drag-drop
    - [ ] Task 15: Create DocumentList component with table view
    - [ ] Task 16: Create document preview viewer
    - [ ] Task 17: Create sharing dialog
    - [ ] Task 18: Create "Recent Documents" dashboard widget

    **Testing (4 tasks):**
    - [ ] Task 19: Write unit tests for DocumentService
    - [ ] Task 20: Write integration tests for upload API
    - [ ] Task 21: Write UI tests for upload workflow
    - [ ] Task 22: Perform security testing

    **Deployment (3 tasks):**
    - [ ] Task 23: Configure CI/CD pipeline
    - [ ] Task 24: Set up Application Insights monitoring
    - [ ] Task 25: Perform final code review

1. Verify that the task list covers all requirements from the specification:

    - Each functional requirement should map to one or more tasks
    - Security requirements should have corresponding implementation tasks
    - Performance requirements should have testing tasks
    - Integration points should have dedicated tasks

1. Check that tasks are ordered logically:

    - Foundation tasks (database, models) come first
    - Backend API tasks build on the foundation
    - Frontend tasks reference backend endpoints
    - Testing tasks come after implementation
    - Deployment tasks come last

1. Ensure each task is specific and actionable:

    - ✅ Good: "Create Document entity with properties: DocumentId, Title, Description, FileName, FileSize, BlobStorageUrl"
    - ❌ Vague: "Set up database stuff"

1. Verify that tasks have reasonable scope:

    - Individual tasks should be completable in a few hours to a day
    - If a task seems too large, note that it may need to be broken down during implementation

1. Add task dependencies or notes if needed. For example:

    ```markdown
    - [ ] Task 12: Implement DocumentController POST /api/documents endpoint
      - Depends on: Task 11 (DocumentService)
      - Note: Include comprehensive error handling for file size limits and unsupported types
    ```

1. Save the `tasks.md` file and commit your changes:

    ```powershell
    git add tasks.md
    git commit -m "Add comprehensive task list for document management implementation"
    git push
    ```

The task list now provides a clear roadmap for implementation. In the next task, you'll use GitHub Copilot to help implement these tasks systematically.

## Implement the specified feature using GitHub Copilot

With a clear specification, technical plan, and task list in place, you're ready to implement the document upload and management feature. This task demonstrates how spec-driven development guides implementation and how GitHub Copilot assists with code generation based on the context you've established.

In this task, you'll implement a subset of the feature to demonstrate the spec-driven development workflow. In a real project, you would complete all tasks, but for this exercise, you'll focus on core functionality: setting up the data model, implementing basic upload functionality, and creating a simple document list view.

Use the following steps to complete this task:

1. Review the task list in `tasks.md` and identify foundational tasks to implement:

    - Task 1: Create Document entity
    - Task 6: Implement upload service
    - Task 7: Create upload API endpoint
    - Task 14: Create upload UI component

1. **Create the Document entity model:**

    In Visual Studio Code, create a new file in the existing `Models/` folder: `Models/Document.cs`

    The Document entity will follow the same pattern as existing entities (User, TaskItem, Project, etc.) in the Models folder.

1. Create the `Models/Document.cs` file and use GitHub Copilot to generate the entity:

    Type the following comment in the file:

    ```csharp
    // Document entity for storing uploaded file metadata
    // Properties: DocumentId (Guid), Title, Description, FileName, FileSize, FileType,
    // BlobStorageUrl, Category, UploadedBy, UploadDate, LastModifiedDate
    // Include data annotations for required fields and string lengths per spec requirements
    ```

    Position your cursor after the comment and press **Enter**. GitHub Copilot should generate the entity class. Review and accept the suggestion, or refine it as needed.

1. **Implement the document upload service:**

    Create a new file in the existing `Services/` folder: `Services/DocumentService.cs`

    This service will follow the same pattern as existing services (TaskService, ProjectService, etc.).

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

    If using Blazor Server (like the existing ContosoDashboard), you may create a code-behind file for a page. If adding Web API support, create `Controllers/DocumentsController.cs`:

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

1. **Create the upload UI component (Blazor example):**

    Create a new file in the existing `Pages/` folder: `Pages/Documents.razor`

    This page will follow the same pattern as existing pages (Tasks.razor, Projects.razor, etc.) and integrate with the existing MainLayout and NavMenu.

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
