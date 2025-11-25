<!-- ---
lab:
    title: 'Exercise - Implement Spec-Driven Development with GitHub Spec Kit'
    description: 'Learn how to ???'
--- -->

# Implement Spec-Driven Development with GitHub Spec Kit

GitHub Spec Kit is a tool that helps developers implement spec-driven development (SDD) using AI coding assistants like GitHub Copilot.

In this exercise, you learn how to set up a Spec Kit development environment, create specifications, plans, and tasks, and implement a product feature using GitHub Copilot.

This exercise should take approximately **90** minutes to complete.

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

You're a software developer working for a consulting firm that's adopting spec-driven development (SDD) using GitHub Spec Kit and GitHub Copilot in Visual Studio Code. Your client, Contoso Corporation, has tasked you with adding a new feature to their internal employee dashboard application called ContosoDashboard.

Contoso's business stakeholders have provided high-level requirements for a document upload and management feature. Employees need the ability to upload work-related documents, organize them by category and project, and share them with team members. The feature must integrate seamlessly with the existing dashboard while maintaining security and compliance standards.

Your assignment is to use the spec-driven development methodology to implement this feature. Rather than jumping directly into coding, you'll create structured specifications, plans, and tasks that guide the development process. This approach ensures the implementation aligns with business requirements and organizational constraints.

You'll work with the following application:

- **ContosoDashboard**: An internal web application that provides Contoso employees with a centralized platform for managing daily work activities, including tasks, projects, team collaboration, and notifications.

**Application Context**: ContosoDashboard currently serves 5,000 Contoso employees. The application uses role-based access control (Employee, Team Lead, Project Manager, Administrator) and integrates with Contoso's Microsoft Entra ID for authentication. The existing application includes task management, project tracking, team collaboration features, and notification capabilities.

This exercise includes the following tasks:

1. Configure GitHub Spec Kit in the development environment.
1. Create and initialize a GitHub repository for the ContosoDashboard project.
1. Define the project Constitution (organizational constraints and development principles).
1. Create the Spec for the document upload and management feature.
1. Clarify the Spec (iterate on requirements with AI assistance).
1. Generate the Technical Plan (architecture and implementation approach).
1. Create the Task List (actionable implementation steps).
1. Implement the Feature using GitHub Copilot.
1. Review and verify the implementation.

## Task 1: Configure GitHub Spec Kit in the development environment

In this task, you set up a complete GitHub Spec Kit development environment with Visual Studio Code and GitHub Copilot. You install required tools, configure the environment, and verify everything is ready for spec-driven development.

### Step 1: Install prerequisites

Before installing GitHub Spec Kit, ensure your development environment includes the necessary tools.

1. Install Python 3.11 or later:

    Spec Kit's CLI tool is Python-based and requires Python 3.11+.

    - Verify your Python version:

        ```powershell
        python --version
        ```

    - If you need to install Python, download from [python.org](https://www.python.org/downloads/) or use your organization's software distribution system.

    - Expected output: `Python 3.11.0` or higher

1. Install or verify Visual Studio Code:

    - Download from [code.visualstudio.com](https://code.visualstudio.com/) or your corporate application catalog.

    - Spec Kit works through VS Code's interface with AI coding assistants.

1. Install the GitHub Copilot Chat extension:

    - Open Visual Studio Code

    - Navigate to Extensions (Ctrl+Shift+X)

    - Search for "GitHub Copilot Chat"

    - Click Install

    - Sign in with your GitHub account when prompted

1. Install or verify Git:

    - Verify Git is installed:

        ```powershell
        git --version
        ```

    - Expected output: `git version 2.30.0` or higher

    - If needed, install Git from [git-scm.com](https://git-scm.com/downloads) or your corporate application catalog.

1. Install the uv package manager:

    Spec Kit uses uv for CLI installation and management.

    - Install uv following the instructions at [docs.astral.sh/uv](https://docs.astral.sh/uv/)

    - For Windows PowerShell:

        ```powershell
        powershell -ExecutionPolicy ByPass -c "irm https://astral.sh/uv/install.ps1 | iex"
        ```

    - Restart your terminal after installation to ensure uv is in your PATH

1. Verify GitHub Copilot access:

    - Ensure you have an active GitHub Copilot subscription

    - Check your subscription at [github.com/settings/copilot](https://github.com/settings/copilot)

    - For Microsoft employees, Copilot is typically provisioned through your organization's GitHub Enterprise account

### Step 2: Install and verify the GitHub Spec Kit (Specify CLI)

The `specify` command-line tool initializes projects for spec-driven development.

1. Install the Specify CLI using uv:

    ```powershell
    uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
    ```

    This installs the latest version directly from the GitHub repository and makes the `specify` command available system-wide.

1. Restart your terminal to ensure the `specify` command is in your PATH.

1. Verify the installation:

    ```powershell
    specify --version
    ```

    You should see output similar to:

    ```output
    specify-cli 0.3.0
    ```

    The version number may vary. If you see the version number, the installation was successful.

**Troubleshooting installation issues:**

- **Command not found**: If `specify` isn't recognized after installation, the `uv` tools directory may not be in your PATH. Run `uv tool list` to verify the installation. You may need to restart your terminal or manually add the tools directory to your PATH.

- **SSL certificate errors in corporate environments**: Corporate networks may intercept HTTPS connections. Configure `uv` to use your organization's certificate bundle:

    ```powershell
    $env:SSL_CERT_FILE = "C:\path\to\corporate-ca-bundle.crt"
    uv tool install specify-cli --from git+https://github.com/github/spec-kit.git
    ```

### Step 3: Configure Visual Studio Code for GitHub Spec Kit

Spec Kit integrates with VS Code through the GitHub Copilot Chat extension.

1. Verify Copilot Chat is installed and active:

    - Open Visual Studio Code

    - Press `Ctrl+Alt+I` to open the Copilot Chat panel

    - Look for the chat input field on the right side of the window

1. If the chat panel doesn't appear, verify the extension installation:

    - Navigate to Extensions (Ctrl+Shift+X)

    - Search for "GitHub Copilot Chat"

    - If it shows "Disable" or "Uninstall", it's already installed

    - If it shows "Install", click Install and reload VS Code

1. Authenticate to GitHub:

    - Open Visual Studio Code

    - Click the account icon in the lower-left corner

    - Select "Sign in to use GitHub Copilot"

    - Choose "Sign in with GitHub"

    - Complete the authentication flow in your browser

1. For GitHub Enterprise Server environments:

    - Open VS Code Settings (Ctrl+,)

    - Search for "github.enterprise"

    - Set "Github: Enterprise Uri" to your server URL (for example, `https://github.yourcompany.com`)

    - Sign in using your enterprise credentials

### Step 4: Create and initialize a test repository

For this exercise, you work in an isolated test repository.

1. Create a new GitHub repository:

    - Navigate to [github.com](https://github.com/) (or your GitHub Enterprise Server)

    - Click the "+" icon in the top-right corner

    - Select "New repository"

    - Name it `spec-kit-training`

    - Choose "Private" to keep experiments confidential

    - Click "Create repository"

1. Clone the repository locally:

    ```powershell
    git clone https://github.com/yourusername/spec-kit-training.git
    cd spec-kit-training
    ```

1. Initialize GitHub Spec Kit in the repository:

    ```powershell
    specify init
    ```

1. When prompted, select the appropriate options:

    - Choose "GitHub Copilot" as the AI agent

    - Choose "PowerShell" for the script variant (on Windows)

1. Observe the created files and directories:

    - `.github/prompts/` directory containing workflow templates

    - Template files: `constitution.md`, `spec.md`, `plan.md`, `tasks.md`

    - Configuration files that tell GitHub Copilot about the spec-driven workflow

### Step 5: Verify the environment is ready

Confirm your environment is properly configured.

1. Open the project in Visual Studio Code:

    ```powershell
    code .
    ```

1. Open the Copilot Chat panel (Ctrl+Alt+I)

1. Type `/speckit` in the chat input

1. Verify autocomplete shows commands like:

    - `/speckit.constitution`
    - `/speckit.specify`
    - `/speckit.clarify`
    - `/speckit.plan`
    - `/speckit.tasks`
    - `/speckit.implement`

1. Verify the project structure:

    - Confirm `.github/prompts/` directory exists

    - Confirm template files exist: `constitution.md`, `spec.md`, `plan.md`, `tasks.md`

**Troubleshooting verification issues:**

- **`/speckit` commands don't appear**: 
  - Verify `.github/prompts/` exists in your workspace root
  - Reload VS Code: Press Ctrl+Shift+P, type "Reload Window", press Enter
  - Ensure you opened the correct folder in VS Code (the folder containing `.github/`), not a parent or child directory

- **Copilot Chat shows "You don't have access to GitHub Copilot"**:
  - Check your Copilot subscription at [github.com/settings/copilot](https://github.com/settings/copilot)
  - Re-authenticate: Click the account icon in VS Code's lower-left corner, sign out, then sign back in

Your GitHub Spec Kit development environment is now configured and ready. You can proceed with the remaining tasks to implement a product feature using spec-driven development.

## Task 2: Create and initialize the ContosoDashboard repository

In this task, you create a new GitHub repository for the ContosoDashboard project and initialize it with GitHub Spec Kit. This sets up the foundation for spec-driven development by creating the necessary directory structure and template files.

### Step 1: Create a new GitHub repository

Before initializing Spec Kit locally, you need a GitHub repository to host your project.

1. Open a browser window and navigate to `https://github.com`.

1. Sign in to your GitHub account if you're not already signed in.

1. Create a new repository:

    - Click your profile icon in the top-right corner, and then select **Repositories**.
    - On the Repositories page, select the **New** button.
    - On the Create a new repository page, configure the repository as follows:
        - **Repository name**: `ContosoDashboard`
        - **Description**: `Internal employee dashboard with document management`
        - **Visibility**: Select **Public**
        - **Initialize this repository with**: Leave all checkboxes unchecked (Spec Kit will initialize the repository)
    - Select **Create repository**.

1. Keep the repository page open in your browser - you'll need the repository URL in the next steps.

    The URL should be in the format: `https://github.com/YOUR-USERNAME/ContosoDashboard.git`

### Step 2: Initialize the repository with GitHub Spec Kit

Now you'll use the Specify CLI to initialize your local repository with the Spec-Driven Development structure.

1. Open a terminal window (Command Prompt, PowerShell, or Terminal).

1. Navigate to the location where you want to create your project folder:

    ```powershell
    cd C:\Users\YourUsername\Documents
    ```

    Replace `C:\Users\YourUsername\Documents` with your preferred location.

1. Initialize a new Spec Kit project:

    ```powershell
    specify init ContosoDashboard
    ```

    The CLI will begin the initialization process and prompt you with several questions.

1. When prompted to select an AI assistant, choose **copilot** (GitHub Copilot):

    ```plaintext
    Which AI assistant will you use?
    > copilot
    ```

    Use the arrow keys to navigate and press Enter to select.

1. When prompted to select a script variant, choose **ps** (PowerShell) for Windows:

    ```plaintext
    Which script variant would you like?
    > ps (PowerShell)
    ```

    If you're using macOS or Linux, select **sh** (bash/zsh) instead.

1. Wait for the initialization to complete. The CLI will:

    - Create the `ContosoDashboard` directory
    - Set up the `.github/prompts/` directory with Spec Kit commands
    - Create template files: `constitution.md`, `spec.md`, `plan.md`, `tasks.md`
    - Initialize a Git repository
    - Display a success message with next steps

1. Observe the output and confirm that initialization completed successfully:

    ```plaintext
    ✓ Created project directory: ContosoDashboard
    ✓ Initialized Git repository
    ✓ Created Spec Kit structure
    ✓ Added template files
    
    Next steps:
    1. cd ContosoDashboard
    2. Open the project in your AI coding assistant
    3. Start with /speckit.constitution to define your project principles
    ```

### Step 3: Connect the local repository to GitHub

Link your local repository to the GitHub repository you created earlier.

1. Navigate to the newly created project directory:

    ```powershell
    cd ContosoDashboard
    ```

1. Verify the repository structure:

    ```powershell
    ls -Recurse -Depth 2
    ```

    You should see:

    - `.github/prompts/` directory with Spec Kit prompt files
    - `constitution.md`, `spec.md`, `plan.md`, `tasks.md` template files
    - `.git/` directory (hidden)

1. Connect to the remote GitHub repository:

    ```powershell
    git remote add origin https://github.com/YOUR-USERNAME/ContosoDashboard.git
    ```

    Replace `YOUR-USERNAME` with your actual GitHub username.

1. Verify the remote was added:

    ```powershell
    git remote -v
    ```

    You should see:

    ```plaintext
    origin  https://github.com/YOUR-USERNAME/ContosoDashboard.git (fetch)
    origin  https://github.com/YOUR-USERNAME/ContosoDashboard.git (push)
    ```

1. Create an initial commit and push to GitHub:

    ```powershell
    git add .
    git commit -m "Initial Spec Kit setup"
    git branch -M main
    git push -u origin main
    ```

    If prompted for GitHub credentials, sign in to authorize the push.

1. Verify the push succeeded by refreshing your GitHub repository page in the browser. You should now see the Spec Kit files in your repository.

### Step 4: Open the project in Visual Studio Code

Open the project in VS Code to begin the spec-driven development process.

1. Open the project in Visual Studio Code:

    ```powershell
    code .
    ```

    This command opens the current directory (ContosoDashboard) in VS Code.

1. Wait for VS Code to fully load the project. You should see the project files in the EXPLORER view:

    ```
    CONTOSODASHBOARD
    ├── .github
    │   └── prompts
    │       ├── (various .prompt.md files)
    ├── constitution.md
    ├── spec.md
    ├── plan.md
    └── tasks.md
    ```

1. Open the Copilot Chat view:

    - Press **Ctrl+Alt+I** (Windows/Linux) or **Cmd+Alt+I** (macOS)
    - Or click the **Chat** icon in the Activity Bar

1. Verify that Spec Kit commands are available:

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

1. If the `/speckit` commands don't appear:

    - Reload VS Code: Press **Ctrl+Shift+P**, type "Reload Window", and press Enter
    - Verify you opened the `ContosoDashboard` folder (not a parent or child directory)
    - Check that `.github/prompts/` exists in your workspace root
    - Ensure GitHub Copilot Chat extension is installed and signed in

**Troubleshooting**: If you encounter issues during initialization:

- **"specify command not found"**: Ensure you completed Task 1 and installed the Specify CLI. Run `specify --version` to verify installation.
- **Permission denied errors**: On Windows, ensure you're running PowerShell with appropriate permissions. On macOS/Linux, check file permissions.
- **Git errors during push**: Verify you're signed in to GitHub. You may need to set up Git credentials or use a personal access token.

Your ContosoDashboard project is now initialized with GitHub Spec Kit and ready for spec-driven development. You can proceed to define the project constitution in the next task.

## Task 3: Define the project Constitution

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

1. GitHub Copilot will prompt you for information about the project principles and constraints. Provide the following detailed context:

    ```plaintext
    Create a constitution for the ContosoDashboard project with the following principles and constraints:

    **Organizational Constraints:**
    - All cloud infrastructure must use Microsoft Azure services (Azure App Service, Azure SQL Database, Azure Blob Storage, Azure Key Vault)
    - Authentication must integrate with Microsoft Entra ID (formerly Azure AD)
    - Must comply with Contoso Corporation's data retention policies (3-year minimum for business data)
    - Must follow GDPR and data privacy regulations

    **Security Requirements:**
    - All data in transit must use TLS 1.3 encryption
    - All data at rest must be encrypted
    - Implement protection against OWASP Top 10 vulnerabilities (SQL Injection, XSS, CSRF, etc.)
    - Role-based access control (RBAC) must be enforced for all features
    - Sensitive configuration (connection strings, API keys) must be stored in Azure Key Vault
    - All authentication attempts and security events must be logged

    **Technical Standards:**
    - Use ASP.NET Core 8.0 or later for backend development
    - Follow Microsoft's official coding conventions and style guidelines
    - All code must include XML documentation comments for public APIs
    - Minimum 80% code coverage with unit tests
    - Use Entity Framework Core for data access
    - Implement repository pattern for data access layers

    **Performance Requirements:**
    - Page load times must not exceed 2 seconds under normal network conditions
    - API response times must not exceed 500 milliseconds for 95% of requests
    - The application must support 1,000 concurrent users
    - Database queries must be optimized with appropriate indexing

    **Quality Standards:**
    - All user-facing messages must be clear and professional
    - Error handling must provide meaningful feedback without exposing sensitive information
    - The application must be accessible and comply with WCAG 2.1 Level AA standards
    - All features must work on latest versions of Edge, Chrome, Firefox, and Safari

    **Development Practices:**
    - Use Git for version control with meaningful commit messages
    - Follow GitFlow branching strategy (main, develop, feature branches)
    - Require code reviews before merging to main branch
    - Implement CI/CD pipelines for automated testing and deployment
    - Document all architectural decisions in ADR (Architecture Decision Records) format
    ```

1. Press **Enter** and wait for GitHub Copilot to process the information and generate the constitution.

    This may take 30-60 seconds as Copilot analyzes the requirements and structures the constitution document.

### Step 3: Review and refine the generated constitution

After Copilot generates the constitution, review it carefully to ensure it captures all requirements.

1. Copilot will update the `constitution.md` file. Review the generated content in the editor.

1. Verify the constitution includes sections covering:

    - **Project Identity**: Name, purpose, and scope of the ContosoDashboard application
    - **Organizational Constraints**: Azure-only infrastructure, Entra ID integration, data retention policies
    - **Security and Compliance**: Encryption requirements, OWASP protection, RBAC, logging
    - **Technical Architecture**: Technology stack (.NET, Entity Framework), architectural patterns, coding standards
    - **Performance Benchmarks**: Load time limits, API response times, scalability targets
    - **Quality and Accessibility**: WCAG compliance, error handling, browser support
    - **Development Workflow**: Version control, branching strategy, code review process, CI/CD

1. Check that each principle is clearly stated and actionable. For example:

    - ❌ Vague: "Use good security practices"
    - ✅ Clear: "All API endpoints must validate authentication tokens and enforce role-based permissions"

1. If any critical requirements are missing or unclear, you can refine them by:

    - Editing the `constitution.md` file directly to add or modify principles
    - Or asking a follow-up question in Copilot Chat:

        ```plaintext
        Add a principle about file upload security: All uploaded files must be scanned for malware using an approved service before storage, and file size limits must be enforced (max 25 MB per file).
        ```

1. Add any Contoso-specific principles that weren't included. For example:

    - Document management features must support PDF and Microsoft Office file formats
    - Storage quotas must be enforced (2 GB per user by default)
    - All document access must be audited for compliance tracking

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

## Task 4: Create the Spec for the document upload and management feature

The specification (spec) defines what you're building from the user's perspective. It describes features, user stories, acceptance criteria, and business requirements without prescribing how to implement them. A well-written spec serves as the foundation for creating implementation plans and tasks.

In this task, you use GitHub Copilot's `/speckit.specify` command to generate a detailed specification for the document upload and management feature based on high-level requirements from Contoso's business stakeholders.

Use the following steps to complete this task:

1. Review the high-level requirements document located in the lab materials:

    Navigate to the following folder: spec-driven-development

1. Open the upload-manage-docs-feature-requirements.md file in Visual Studio Code or a text editor.

1. Take 2-3 minutes to read through the requirements document, paying particular attention to:

    - **Business Need**: Why Contoso needs this feature (centralized document storage, version control, security)
    - **Target Users**: All Contoso employees with role-based permissions
    - **High-Level Requirements**: The 18 numbered requirements covering upload, organization, management, integration, storage, and reporting
    - **Success Metrics**: How feature success will be measured (e.g., 70% adoption rate, documents found in under 30 seconds)
    - **Constraints**: Azure infrastructure, 8-10 week timeline, security policies
    - **Out of Scope**: Features not included in this release (collaborative editing, OCR, etc.)

1. In Visual Studio Code with the ContosoDashboard project open, open the Copilot Chat view (press **Ctrl+Alt+I**).

1. Start a new chat session by clicking the **New Chat** button (the **+** icon at the top of the Chat panel).

1. In the Chat input field, enter the `/speckit.specify` command and press **Enter**.

1. When prompted to describe the feature you want to build, provide a comprehensive description based on the requirements document:

    ```plaintext
    Feature: Document Upload and Management for ContosoDashboard
    
    Enable employees to upload work-related documents (PDF, Office, images, text), organize by category/project, share with team members, search efficiently, and manage versions. Must integrate with existing dashboard features while maintaining security and compliance.
    
    Target Users: All 5,000 Contoso employees with role-based access (Employee, Team Lead, Project Manager, Administrator).
    
    Core Capabilities:
    1. Upload: Multiple files, max 25 MB each (100 MB batch), supported types (PDF, Office docs, images, text), metadata (title, category, description, project, tags), progress indicator, virus scanning
    2. Organization: My Documents view, Project Documents view, Team Documents view, Shared with Me section, search by title/description/tags/uploader/project (results under 2 seconds)
    3. Management: Download with tracking, in-browser preview (PDF/images), edit metadata, version control (30-day history), soft delete (30-day trash), restore, sharing with permissions (View Only/View & Download)
    4. Integration: Attach to tasks, dashboard Recent Documents widget, notifications for sharing/new project docs
    5. Storage: 2 GB quota per user, usage display, 80% warning, admin quota adjustments
    6. Performance: Upload in 30s (25 MB files), list load in 2s (500 docs), search in 2s, preview in 3s
    7. Audit: Log all uploads/downloads/deletions/sharing, admin reports, 7-year retention for Official Records, 5-year audit logs
    
    Security: Azure Blob Storage encryption at rest, TLS 1.3 in transit, RBAC enforcement, audit logging.
    
    Success Criteria: 70% adoption in 3 months, find docs under 30s, 90% properly categorized, zero security incidents, <5% support tickets.
    
    Constraints: Azure Blob Storage, ASP.NET Core integration, 8-10 week timeline, Entra ID authentication.
    
    Out of Scope: Collaborative editing, OCR, translation, approval workflows, SharePoint integration, mobile apps, templates, e-signatures.
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

    - File size limits (25 MB per file, 100 MB batch)
    - Supported file types (PDF, Office documents, images, text files)
    - Storage quotas (2 GB per user)
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

## Task 5: Clarify the Spec (iterate on requirements)

The `/speckit.clarify` command helps identify ambiguities, gaps, and underspecified areas in your specification. GitHub Copilot analyzes the spec and asks targeted questions to ensure all requirements are clear and complete before moving to the technical planning phase.

In this task, you use the clarification process to refine the document upload and management specification.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. In the Chat input field, enter the `/speckit.clarify` command and press **Enter**.

1. GitHub Copilot will analyze the `spec.md` file and generate clarification questions. You may receive questions such as:

    - "Should drag-and-drop upload be supported in addition to the file selection dialog?"
    - "What happens to documents when a user's employment ends? Should they be transferred, archived, or deleted?"
    - "Should there be different storage quotas for different user roles (e.g., Project Managers get more space)?"
    - "How should version numbering work? Automatic incrementing (v1, v2, v3) or manual versioning?"
    - "Should users receive notifications when someone downloads their documents?"
    - "What specific virus scanning service should be used (Microsoft Defender, ClamAV, third-party)?"
    - "Should document titles be unique within a user's library, or can duplicates exist?"
    - "How should search ranking work? By relevance, date, or user-defined criteria?"

1. Answer each question thoughtfully. Here are example responses you can provide:

    For drag-and-drop support:

    ```plaintext
    Yes, support drag-and-drop upload in addition to file selection dialog for better user experience. The same validation rules apply (file type, size limits, virus scanning).
    ```

    For user termination:

    ```plaintext
    When an employee leaves, their personal documents should be retained for 90 days for knowledge transfer, then archived. Project documents remain with the project indefinitely. Team Leads are notified to review and reassign ownership of critical documents.
    ```

    For storage quotas by role:

    ```plaintext
    Use standard 2 GB quota for all users initially. Administrators can adjust quotas on a case-by-case basis. Consider implementing automatic quota increase requests for users approaching limits.
    ```

    For version numbering:

    ```plaintext
    Use automatic version numbering (v1, v2, v3...). Display version number and timestamp for each version. Allow users to add optional version notes when uploading a new version.
    ```

    For download notifications:

    ```plaintext
    No real-time notifications for downloads - this would be too noisy. Instead, provide a downloadable report showing who accessed documents in the last 30 days for audit purposes.
    ```

    For virus scanning:

    ```plaintext
    Use Microsoft Defender for Cloud integration since we're already using Azure. Files must pass scanning before being stored in Blob Storage. Quarantine failed files and notify the uploader and administrators.
    ```

    For duplicate titles:

    ```plaintext
    Allow duplicate titles - users should be able to have multiple documents with the same name in different categories or projects. Use unique document IDs internally, and display upload date/category to distinguish duplicates in the UI.
    ```

    For search ranking:

    ```plaintext
    Rank search results by relevance (title matches first, then tag matches, then description matches), with most recently uploaded as a tiebreaker. Allow users to re-sort results by date or file size after initial search.
    ```

1. After you provide each answer, GitHub Copilot updates the `spec.md` file with clarifications.

1. If Copilot presents additional rounds of questions, continue answering until it indicates there are no further clarifications needed.

    The clarification process may involve 2-3 rounds of questions as Copilot identifies dependencies and follow-up questions based on your answers.

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

## Task 6: Generate the Technical Plan

The technical plan bridges the gap between the "what" (specification) and the "how" (implementation). It defines the architecture, technology choices, data models, API designs, and implementation approach while adhering to the constraints defined in the constitution.

In this task, you use GitHub Copilot's `/speckit.plan` command to generate a comprehensive technical implementation plan.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. In the Chat input field, enter the `/speckit.plan` command and press **Enter**.

1. GitHub Copilot will analyze the `constitution.md` and `spec.md` files to generate the plan. It may prompt you for additional technical context. Provide the following information:

    ```plaintext
    Technology Stack and Architecture Context:
    
    Existing Application:
    - Backend: ASP.NET Core 8.0 Web API with C# 12
    - Frontend: Blazor Server (or specify React/Angular if different)
    - Database: Azure SQL Database with Entity Framework Core 8
    - Authentication: Microsoft Entra ID (Azure AD) with JWT tokens
    - Hosting: Azure App Service (Production), local IIS Express (Development)
    
    New Components for Document Management:
    - Storage: Azure Blob Storage with hierarchical namespace enabled
    - File Processing: Azure Functions for virus scanning (async processing)
    - Search: Azure Cognitive Search for document content indexing (optional enhancement)
    - Caching: Azure Redis Cache for frequently accessed metadata
    
    Architecture Patterns:
    - Use Repository pattern for data access
    - Implement Service layer for business logic
    - Apply Dependency Injection throughout
    - Use DTOs (Data Transfer Objects) for API contracts
    - Implement CQRS pattern for read-heavy operations like search
    
    Development Practices:
    - Follow Clean Architecture principles
    - Use async/await for all I/O operations
    - Implement comprehensive logging with Application Insights
    - Include XML documentation for all public APIs
    - Write unit tests with xUnit, integration tests with WebApplicationFactory
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

    - **Document**: DocumentId, Title, Description, FileName, FileSize, FileType, BlobStorageUrl, Category, UploadedBy, UploadDate, LastModifiedDate, IsDeleted, DeletedDate
    - **DocumentVersion**: VersionId, DocumentId, VersionNumber, BlobStorageUrl, UploadedBy, UploadDate, VersionNotes
    - **DocumentTag**: TagId, DocumentId, TagName
    - **DocumentShare**: ShareId, DocumentId, SharedBy, SharedWith, Permission (ViewOnly/ViewAndDownload), SharedDate
    - **DocumentAuditLog**: LogId, DocumentId, Action (Upload/Download/Delete/Share/Modify), UserId, Timestamp, Details

1. Review the API design section. It should include endpoints such as:

    ```plaintext
    POST   /api/documents                  - Upload new document
    GET    /api/documents                  - Get user's documents (with filtering/sorting)
    GET    /api/documents/{id}             - Get document details
    PUT    /api/documents/{id}             - Update document metadata
    DELETE /api/documents/{id}             - Soft delete document
    GET    /api/documents/{id}/download    - Download document
    GET    /api/documents/{id}/preview     - Get preview URL
    POST   /api/documents/{id}/share       - Share document with users
    GET    /api/documents/project/{id}     - Get project documents
    GET    /api/documents/search           - Search documents
    POST   /api/documents/{id}/versions    - Upload new version
    GET    /api/documents/trash            - Get deleted documents
    POST   /api/documents/{id}/restore     - Restore from trash
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

## Task 7: Create the Task List

The task list breaks down the technical plan into specific, actionable implementation steps. Each task should be small enough to complete in a reasonable timeframe (typically a few hours to a day) and have clear acceptance criteria.

In this task, you use GitHub Copilot's `/speckit.tasks` command to generate a comprehensive task list.

Use the following steps to complete this task:

1. Ensure the Copilot Chat view is open (press **Ctrl+Alt+I** if needed).

1. In the Chat input field, enter the `/speckit.tasks` command and press **Enter**.

1. GitHub Copilot will analyze the `plan.md` file and generate tasks in the `tasks.md` file. Wait 1-2 minutes for the generation to complete.

1. Open the `tasks.md` file from the EXPLORER view.

1. Review the generated task list. It should include tasks covering all aspects of implementation, for example:

    **Database and Data Model Tasks:**
    - [ ] Task 1: Create Document entity with EF Core model and DbContext configuration
    - [ ] Task 2: Create DocumentVersion entity and configure relationship with Document
    - [ ] Task 3: Create DocumentTag, DocumentShare, and DocumentAuditLog entities
    - [ ] Task 4: Generate and apply EF Core migrations for new tables
    - [ ] Task 5: Create repository interfaces (IDocumentRepository, IDocumentVersionRepository)
    - [ ] Task 6: Implement repository classes with CRUD operations and queries

    **Azure Infrastructure Tasks:**
    - [ ] Task 7: Create Azure Blob Storage container for document storage with hierarchical namespace
    - [ ] Task 8: Configure Azure Key Vault and store Blob Storage connection string
    - [ ] Task 9: Set up Azure Function for virus scanning with Queue Storage trigger
    - [ ] Task 10: Configure managed identity for secure access to Blob Storage and Key Vault

    **Backend API Tasks:**
    - [ ] Task 11: Create DocumentService class with upload logic (validation, metadata, Blob Storage)
    - [ ] Task 12: Implement DocumentController with POST /api/documents endpoint for upload
    - [ ] Task 13: Add file type and size validation middleware
    - [ ] Task 14: Implement GET /api/documents endpoint with filtering, sorting, and pagination
    - [ ] Task 15: Implement GET /api/documents/{id} endpoint for document details
    - [ ] Task 16: Implement PUT /api/documents/{id} endpoint for metadata updates
    - [ ] Task 17: Implement DELETE /api/documents/{id} endpoint for soft delete
    - [ ] Task 18: Implement GET /api/documents/{id}/download endpoint with blob streaming
    - [ ] Task 19: Implement document preview generation for PDFs and images
    - [ ] Task 20: Add document search endpoint with full-text search capability
    - [ ] Task 21: Implement document sharing endpoints (create/revoke share)
    - [ ] Task 22: Implement version management endpoints (upload version, get version history)
    - [ ] Task 23: Implement trash/restore endpoints
    - [ ] Task 24: Add storage quota checking and enforcement logic
    - [ ] Task 25: Implement audit logging for all document operations

    **Security and Authorization Tasks:**
    - [ ] Task 26: Add authorization policies for role-based access control
    - [ ] Task 27: Implement permission checks for document access (owner, shared user, project member)
    - [ ] Task 28: Add input sanitization to prevent XSS and injection attacks
    - [ ] Task 29: Configure CORS policies for frontend access
    - [ ] Task 30: Implement rate limiting for upload endpoints

    **Frontend UI Tasks:**
    - [ ] Task 31: Create DocumentUpload component with file selection and drag-drop
    - [ ] Task 32: Add upload progress indicator with file size/name display
    - [ ] Task 33: Create DocumentList component with table view and filtering
    - [ ] Task 34: Add sorting controls (by name, date, size, category)
    - [ ] Task 35: Implement search bar with autocomplete suggestions
    - [ ] Task 36: Create DocumentDetail view showing metadata and actions
    - [ ] Task 37: Implement in-browser document preview (PDF viewer, image viewer)
    - [ ] Task 38: Create document sharing dialog with user selection and permission settings
    - [ ] Task 39: Add version history display with option to download previous versions
    - [ ] Task 40: Create trash/deleted documents view with restore functionality
    - [ ] Task 41: Add storage quota indicator to user profile page
    - [ ] Task 42: Create "Recent Documents" widget for dashboard home page

    **Integration Tasks:**
    - [ ] Task 43: Add document attachment capability to task detail pages
    - [ ] Task 44: Create notification service integration for document sharing events
    - [ ] Task 45: Update dashboard summary cards to include document count
    - [ ] Task 46: Add navigation menu items for document management pages

    **Testing Tasks:**
    - [ ] Task 47: Write unit tests for DocumentService (upload, download, delete, share)
    - [ ] Task 48: Write unit tests for repository classes
    - [ ] Task 49: Write integration tests for document upload API endpoint
    - [ ] Task 50: Write integration tests for search and filtering functionality
    - [ ] Task 51: Write UI tests for upload workflow
    - [ ] Task 52: Perform security testing (file upload vulnerabilities, authorization bypass)
    - [ ] Task 53: Conduct performance testing (concurrent uploads, large file handling)
    - [ ] Task 54: Test storage quota enforcement

    **Documentation and Deployment Tasks:**
    - [ ] Task 55: Add XML documentation comments to all public APIs
    - [ ] Task 56: Create API documentation with Swagger/OpenAPI
    - [ ] Task 57: Update user documentation with document management guide
    - [ ] Task 58: Configure CI/CD pipeline to deploy Azure resources
    - [ ] Task 59: Set up monitoring and alerts in Application Insights
    - [ ] Task 60: Perform final code review and security scan

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

## Task 8: Implement the Feature using GitHub Copilot

With a clear specification, technical plan, and task list in place, you're ready to implement the document upload and management feature. This task demonstrates how spec-driven development guides implementation and how GitHub Copilot assists with code generation based on the context you've established.

In this task, you'll implement a subset of the feature to demonstrate the spec-driven development workflow. In a real project, you would complete all tasks, but for this exercise, you'll focus on core functionality: setting up the data model, implementing basic upload functionality, and creating a simple document list view.

Use the following steps to complete this task:

1. Review the task list in `tasks.md` and identify the first few foundational tasks to implement:

    - Task 1-3: Create entity models
    - Task 7-8: Set up Azure Blob Storage configuration
    - Task 11-12: Implement upload service and API endpoint
    - Task 31-32: Create upload UI component

1. **Create the Document entity model:**

    In Visual Studio Code, create a new folder structure: `ContosoDashboard/Models/`

1. Create a new file `Models/Document.cs` and use GitHub Copilot to generate the entity:

    Type the following comment in the file:

    ```csharp
    // Document entity for storing uploaded file metadata
    // Properties: DocumentId (Guid), Title, Description, FileName, FileSize, FileType,
    // BlobStorageUrl, Category, UploadedBy, UploadDate, LastModifiedDate, IsDeleted, DeletedDate
    // Include data annotations for required fields and string lengths per spec requirements
    ```

    Position your cursor after the comment and press **Enter**. GitHub Copilot should generate the entity class. Review and accept the suggestion, or refine it as needed.

1. Similarly, create entity models for `DocumentVersion.cs`, `DocumentTag.cs`, `DocumentShare.cs`, and `DocumentAuditLog.cs` using Copilot's assistance with detailed comments describing the properties and relationships.

1. **Set up database context:**

    Create `Data/ApplicationDbContext.cs` and add DbSet properties for your entities. Use Copilot to generate the configuration:

    ```csharp
    // ApplicationDbContext for ContosoDashboard
    // Include DbSets for Document, DocumentVersion, DocumentTag, DocumentShare, DocumentAuditLog
    // Configure entity relationships and indexes for optimal query performance
    ```

1. **Configure Azure Blob Storage:**

    Create `Services/BlobStorageService.cs` for handling file uploads to Azure Blob Storage:

    ```csharp
    // BlobStorageService for uploading files to Azure Blob Storage
    // Constructor: inject IConfiguration to get connection string from Azure Key Vault
    // Method: UploadFileAsync(Stream fileStream, string fileName, string containerName)
    // Method: DownloadFileAsync(string blobUrl)
    // Method: DeleteFileAsync(string blobUrl)
    // Use Azure.Storage.Blobs SDK, handle exceptions, return blob URL on success
    ```

    Let Copilot generate the implementation based on this detailed comment.

1. **Implement the document upload service:**

    Create `Services/DocumentService.cs`:

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

    Create `Controllers/DocumentsController.cs`:

    ```csharp
    // DocumentsController API endpoints
    // [Authorize] - require authentication
    // POST /api/documents - upload document
    //   - Accept IFormFile and DocumentUploadDto
    //   - Call DocumentService.UploadDocumentAsync
    //   - Return 201 Created with document details on success
    //   - Return 400 Bad Request for validation errors (file too large, unsupported type)
    //   - Return 413 Payload Too Large if file exceeds limits
    // Include XML documentation comments for Swagger
    ```

1. **Implement document listing endpoint:**

    In the same controller, add a GET endpoint:

    ```csharp
    // GET /api/documents - get user's documents with filtering and sorting
    //   - Query parameters: category, projectId, sortBy (date, name, size), page, pageSize
    //   - Call DocumentService.GetUserDocumentsAsync
    //   - Return paginated results with total count
    //   - Support filtering by category and project
    ```

1. **Create the upload UI component (Blazor example):**

    Create `Pages/Documents/Upload.razor`:

    ```razor
    @* Document Upload Page *@
    @* Features per spec:
       - File selection input (multiple files)
       - Drag-and-drop zone
       - File type filter (PDF, Office, images, text)
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

1. **Create the document list UI component:**

    Create `Pages/Documents/Index.razor`:

    ```razor
    @* My Documents Page *@
    @* Features per spec:
       - Table view with columns: Title, Category, Upload Date, File Size, Actions
       - Sorting controls (click column headers)
       - Filter controls (category dropdown, project dropdown, date range)
       - Search bar
       - Actions per row: Download, Preview, Edit, Delete, Share
       - Pagination controls (20 items per page)
    *@
    @page "/documents"
    ```

1. **Test the implementation:**

    After implementing the core functionality, test the upload workflow:

    - Run the application locally
    - Navigate to the upload page
    - Select a file (PDF or image under 25 MB)
    - Fill in the metadata (title, category)
    - Click upload and verify:
        - Progress indicator appears
        - Success notification displays
        - Document appears in the document list
        - File is stored in Azure Blob Storage (if connected) or simulated storage

1. **Mark completed tasks:**

    Open `tasks.md` and mark the tasks you've completed by changing `[ ]` to `[x]`:

    ```markdown
    - [x] Task 1: Create Document entity with EF Core model
    - [x] Task 7: Create Azure Blob Storage configuration
    - [x] Task 11: Create DocumentService with upload logic
    - [x] Task 12: Implement DocumentController POST endpoint
    - [x] Task 31: Create DocumentUpload UI component
    - [x] Task 33: Create DocumentList component
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

## Task 9: Review and verify the implementation

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
- Create a project constitution with organizational constraints
- Generate a detailed specification from high-level requirements
- Use AI-assisted clarification to refine the specification
- Create a technical implementation plan aligned with the constitution
- Break down the plan into actionable tasks
- Implement features systematically using the structured guidance
- Verify that the implementation meets all requirements

This methodology can be applied to any software development project, especially when working with AI coding assistants like GitHub Copilot. The structured approach ensures that AI-generated code aligns with business requirements and organizational standards, resulting in higher-quality software delivered more efficiently.

---

### Import the ContosoDashboard repository to your GitHub account

GitHub Importer allows you to create a copy of an existing repository in your own GitHub account, giving you full control over the imported copy.

In this task, you use your GitHub account to import the ContosoDashboard repository.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

1. Sign in to your GitHub account, and then open your repositories tab.

    You can open your repositories tab by clicking on your profile icon in the top-right corner, then selecting **Repositories**.

1. On the Repositories tab, select the **New** button.

1. Under the **Create a new repository** section, select **Import a repository**.

1. On the **Import your project to GitHub** page, under **Your source repository details**, enter the following URL for the source repository:

    ```plaintext
    https://github.com/MicrosoftLearning/ContosoDashboard.git
    ```

1. Under the **Your new repository details** section, in the **Owner** dropdown, select your GitHub username.

1. Enter **ContosoDashboard** in the **Repository name** field.

    GitHub automatically checks the availability of the repository name. If this name is already taken, append a unique suffix (for example, your initials or a random number) to the repository name to make it unique.

1. Ensure that the repository is set to **Public**.

    Secret Scanning is enabled by default for public repositories.

1. Select the **Begin import** button.

    GitHub uses the import process to create the new repository in your account.

    > **NOTE**: It can take a minute or two for the import process to finish.

1. Wait for the import process to complete, and then open the **ContosoDashboard** repository.

    The ContosoDashboard repository contains the ContosoDashboard application.
