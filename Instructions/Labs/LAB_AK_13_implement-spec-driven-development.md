<!-- ---
lab:
    title: 'Exercise - Implement Spec-Driven Development with GitHub Spec Kit'
    description: 'Learn how to ???'
--- -->

# Implement Spec-Driven Development with GitHub Spec Kit

GitHub Spec Kit is a tool that helps developers implement spec-driven development (SDD) using AI coding assistants like GitHub Copilot.

In this exercise, you learn how to set up a Spec Kit development environment, create specifications, plans, and tasks, and implement a product feature using GitHub Copilot.

This exercise should take approximately **45** minutes to complete.

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

You're a software developer working for a consulting firm. The firm is adopting a spec-driven development approach using GitHub Spec Kit and GitHub Copilot in Visual Studio Code. Your first assignment is to work on a client's dashboard application. You need to add a new feature to the application that enables employees to upload and manage documents. You received high-level requirements for the new feature. You need to setup GitHub Spec Kit in your development environment and then start the development project. You need to create a constitution, detailed specifications, plans, and tasks before starting implementation.

You're assigned to the following app:

- ContosoDashboard: An employee dashboard application that allows employees to view and manage their tasks and projects.

This exercise includes the following tasks:

1. Configure GitHub Spec Kit in the development environment.
1. Initialize the Spec Kit project.
1. Define the project Constitution.
1. Create the Spec for the upload feature.
1. Clarify the Spec (iterate on requirements).
1. Generate the Technical Plan.
1. Create the Task List.
1. Implement the Feature using Copilot (Automation).
1. Review the Feature and Cleanup.

## Lab Prerequisites

We list what learners need installed or prepared before using Spec Kit:

- Programming runtime as appropriate (for example Python 3 environment or the uvx tool to install the Specify CLI, since the Specify CLI is Python-based).
- The target tech stack runtime if known (for instance, Node.js or .NET SDK, depending on what kind of project they'll be working on – in our scenario they might need .NET 6 if the backend is .NET).
- Visual Studio Code with the GitHub Copilot (Chat) extension, since we'll be using VS Code as the primary IDE to trigger commands and review artifacts.
- Git and access to a repository (the lab scenario will assume either an empty repo or folder where they initialize a project).
- GitHub Copilot access (or whichever AI agent the user intends to use with Spec Kit). Since this is internal Microsoft training, we assume they have Copilot enabled or we provide one in a lab environment.

We also mention any Microsoft internal dev environment specifics: for example, if using a corporate machine, ensure you can install VS Code extensions; if there's an internal Dev VM image that already has these tools, they could use that. The idea is to preempt any setup hurdles so that when we get to the hands-on part, everyone is ready to go.

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

## Task 2: Initialize the Spec Kit project.

1. Open a new VS Code terminal in the project folder.

1. Run the command specify init.

1. When prompted by the CLI, select the appropriate options.

    - Choose GitHub Copilot as the AI agent (since we're using Copilot Chat)
    - Pick the project language/environment (for example, "JavaScript/Node.js" if our internal app is Node-based, or "C#/.NET" if using .NET backend – choose whichever fits the scenario as instructed by the trainer).

1. Observe that specify init creates a Spec Kit structure.

    You should see new files/folders such as .github/prompts/, a specs/ or similar folder, and key files like spec.md, plan.md, tasks.md, and constitution.md (some may be empty templates initially).

1. Confirm creation of constitution.md, spec.md, plan.md, tasks.md files.

    In VS Code, open the README or instructions that specify init might output for next steps (the CLI often prints a summary).

    This confirms the project is ready for Spec Kit usage.

## Task 3: Define the project Constitution

1. Open the constitution.md file created by the init.

    It might be empty or have a template structure.

1. In VS Code's Copilot Chat pane, type the command /speckit.constitution and press Enter.

    Copilot will likely ask or require some input about the principles.

1. Provide input describing internal guidelines:

    For example: "Principles: (1) Use Azure services for all cloud needs; (2) Ensure only authenticated users can access features; (3) Prioritize performance for large file uploads; (4) Follow Microsoft coding standards." You can type this as a message after the command.

    Copilot will generate content in constitution.md listing these principles in a structured way.

1. Review the generated constitution.

    - Ensure it captured the intended rules (for example, it lists an item about Azure use, one about auth, etc.).
    - If something is missing or phrased incorrectly, feel free to edit the markdown directly. For instance, add a rule for "No use of preview/beta libraries" if that's an internal policy.

1. Save constitution.md.

    This will serve as the guiding constraints for our project.

## Task 4: Create the Spec for the upload feature

1. In Copilot Chat, run the command /speckit.specify.

    Copilot will prompt for a high-level description of the feature. 

1. Provide a detailed description of the Document Upload feature, including key requirements.

    For example:

    "Feature: Document Upload in Internal Employee Dashboard. Description: Allow users to select a document (PDF or Word) from their computer and upload it via the dashboard. The file should be stored in an Azure Blob Storage container. After upload, the user sees the file listed in a ‘My Documents' section. Only users with the role ‘Contributor' can upload. File size limit: 50 MB. Show an error message if the file is too large or an unsupported type."

1. Press Enter and let Copilot generate the specification.

    It will create or update spec.md with sections such as summary, user stories, acceptance criteria, etc., based on your input.

1. Open spec.md to review the content.

    You should see a structured draft:

    - Verify the Acceptance Criteria includes points like "Uploading a PDF or DOCX adds it to the user's documents list" and "Files >50MB are rejected with an error".
    - Check Security/Permissions are mentioned (only certain users can upload).
    - Make note of any obvious questions (for example, did it mention how the UI looks? If not, that might be clarified next).

## Task 5: Clarify the Spec (iterate on requirements)

1. Still in Copilot Chat, run /speckit.clarify.

    Copilot will analyze spec.md and likely ask a series of clarification questions. For example, you might get:

    - "What file extensions besides PDF and DOCX (if any) should be allowed?"
    - "Should there be a confirmation after successful upload, or just the list update?"
    - "Do uploaded documents need virus scanning or any additional processing (since this is an internal tool)?"

1. Answer each question one by one.

    You can type answers or choose from options if provided. For instance:

    - Allowed extensions: "Only .pdf and .docx as initially stated."
    - Confirmation: "Show a brief success banner and update the document list."
    - Virus scanning: "Not required for this internal scenario."

    After answering, Copilot will update the spec.md content to include these details.

    For example, it might add an acceptance criterion like "Success message is displayed for 3 seconds upon upload" or note "No antivirus integration – assumed internal network is secure".

1. If Copilot presents multiple rounds of questions, continue until it says there are no further clarifications needed.

    Each round refines the spec.

1. Once done, open spec.md again and do a final review.

    Ensure it comprehensively covers the feature (what the UI will do, what the backend does, all limits and roles). You now have a finalized spec.

## Task 6: Generate the Technical Plan

1. Run /speckit.plan in Copilot Chat.

    The assistant will use spec.md and constitution.md to draft a plan. It might ask if you have preferences or context to add.

1. Provide tech stack context.

    You can mention:

    "This dashboard's backend is in C# .NET 6, frontend is React; prefer using Azure SDK for storage."

    (Or if Node.js: "The app will be a Node.js Express server with a simple HTML form." – choose the stack you intend to use for implementation.)

1. Copilot produces plan.md. Open this file and read through it.

    It should outline:

    - For example, Architecture: "Add an Upload API in the existing .NET Web API project; add a new page or component in React for upload form; use Azure Blob Storage SDK in the API to store files; update database or in-memory list of files for the user; ensure Azure AD token is required for the API call."
    - Steps: The plan might enumerate steps like "Update data model", "Implement API endpoint", "Integrate frontend with API", etc., with some details for each.
    - Checks: It may mention compliance with the constitution (like confirming it's using Azure, enforcing auth).
    - Assumptions: for example, "Assumes an Azure Blob container named ‘docs' is available; assumes frontend already has user auth context."

    **NOTE**: If something in the plan is inconsistent or not as desired (say it assumed a SQL database but you wanted to keep it simple with in-memory for this lab), you can edit those parts or clarify in chat and re-run the plan. However, for the lab, we'll generally accept the plan as-is if it looks reasonable.

1. Save the changes to plan.md.

## Task 7: Create the Task List

1. Run /speckit.tasks in Copilot Chat.

    This will generate tasks.md from the plan.

1. Open tasks.md.

    You should see a checklist of all the tasks to implement the upload feature. For example:

    - Task 1: "Set up Azure Blob Storage connection in configuration (get connection string, container name)".
    - Task 2: "Back-end: Create POST /api/upload endpoint to receive file and metadata; use Azure SDK to upload file; return result".
    - Task 3: "Front-end: Create an Upload form with file input and upload button".
    - Task 4: "Integrate front-end with /api/upload (call API, handle responses)".
    - Task 5: "Display uploaded files in the UI (update list dynamically)".
    - Task 6: "Error handling: file too large message on front-end; handle API errors".
    - Task 7: "Test the full flow with a sample file".

1. Verify the task list covers everything outlined in the plan.

    All acceptance criteria from the spec should trace to a task (explicitly or implicitly). The order should also make sense (for example, back-end tasks might come before front-end integration in the list).

This tasks.md acts as our to-do list. In Copilot Chat or the VS Code interface, you may see these tasks listed as checkboxes.

## Task 8: Implement the Feature using Copilot (Automation)

Now you'll proceed to execute the tasks, writing code with Copilot's help. You can either tackle tasks one by one or have Spec Kit automate the sequence. For the lab, we'll demonstrate the sequential approach:

### Task 1 (Azure config)

1. Open the project's configuration file or create one if needed (for a .NET project, maybe appsettings.json; for Node, maybe a config section in code).

1. Using Copilot, add the placeholder for an Azure Blob Storage connection string or any setup needed.

    If this is a new project, you might initialize a simple app structure first – for example, create a basic Express server or a minimal .NET Web API project. Spec Kit doesn't do this initial scaffolding, so you might use a template or dotnet new webapi if needed. The lab can simplify by assuming some base project exists or instruct quickly how to make one.

1. Ask Copilot in the editor to add code.

    For example, in a Node project, create a file index.js with a basic Express server listening on port 3000. If using .NET, ensure the project is created and open Program.cs/Startup.cs ready to add a controller.

1. Mark Task 1 as complete.

    You can literally check it off in tasks.md if you like, for tracking.

### Task 2 (Back-end API): Implement the upload endpoint

Backend can be .NET or Node.js based on your earlier choice.

If .NET:

1. Create a new controller (for example, UploadController) with a `[HttpPost] /upload` action.

1. Use Copilot to generate the method stub.

1. Inside the stub method, use Copilot to write code that reads the uploaded file from the request, uses Azure Blob client (initialized with connection from config) to save the file, and returns a success/failure response.

    (Because this is a lab, you might not actually connect to Azure – you could simulate success – but if you have an Azure connection string, it could really upload if online.)

If Node:

1. Use Copilot to add a POST /upload route.

    Possibly use a middleware like multer for file handling (Copilot might suggest it).

1. Save the file to a local folder or simulate upload.

    (Alternatively, to stay focused, instruct Copilot to just accept a dummy file content and pretend to store it, as the main goal is the Spec Kit flow, not Azure integration intricacies.)

In both cases:

1. Ensure the code enforces the conditions:

    1. file type check
    1. file size check.

    Copilot will base this on spec – for example, it might generate an if-statement to reject files over 50MB with a 400 error.

1. Test quickly (if possible) by sending a sample request.

    This might be done later in integrated testing. Mark Task 2 done.

### Task 3 (Front-end form): Implement the upload form UI

1. If React: Create a new component UploadDocument.js

    (Copilot can stub a functional component).

    Within it, have an `<%= product.image %>" /> ?page=<%= currentPage - 1 %><% if(sortParam) { %>\&sort=<%= sortParam %><% } %>Previous <% } %> Page <%= currentPage %> of <%= totalPages %> <% if (currentPage < totalPages) { %> ?page=<%= currentPage + 1 %><% if(sortParam) { %>\&sort=<%= sortParam %><% } %>Next <% } %>`

    This will generate Previous/Next links. The `href` includes the sort param if one is active, so that sorting is preserved when switching pages.

    We use simple anchors; a more sophisticated approach could be a page number list, but for brevity, Previous/Next is fine.

1. Save products.ejs.

1. To verify, restart the server (node index.js) and navigate to <http://localhost:3000/> in a browser.

    You should see the product listing page with products displayed. Check that:

    1. The products are listed (likely 20 of them on the first page, nicely formatted with image, name, price, rating).

    1. The "Next" link appears (since we have 30 products, page 2 should have the remaining 10). Click "Next" to ensure it goes to page=2 and shows those products, and that "Previous" appears.

    1. Change the sort dropdown to "Price: Low to High" and see the page reload with products sorted by price (lowest first). Try "Price: High to Low" and "Rating" to confirm those work. Note that after sorting, if you navigate pages, the sort is preserved.

    1. Test responsiveness quickly by shrinking the browser window: the grid should drop to fewer columns (eventually 1 column on very narrow width).

1. If anything is not working as expected:

    - If the page doesn't load, check the terminal for errors (perhaps a variable not defined, etc.).

    - If sorting or pagination isn't working right, inspect the query params in URL and the logic in index.js. For example, ensure you sort the full list before slicing for page. If you realize a mistake (like slicing then sorting), fix that in index.js and refresh.

    - If the layout is messy, adjust the CSS in the template. Copilot can assist if you write a comment like `/* TODO: improve mobile layout */`.

    This iterative fix/test process is normal and part of development – importantly, our spec and plan guided us well, so likely only minor tweaks are needed.

## Task 9: Final Review and Cleanup

1. Review spec.md and verify that everything in the spec is now visible in the running application:

    - The spec said 20 per page – yes, that's what we did.
    - Sorting by price and rating – implemented.
    - Responsive layout – implemented via CSS.
    - (If something from spec is not done, note it and discuss how one would add it; for example, if spec said "include product description text" but we skipped it, mention how easy it would be to add since the framework is in place.)

1. In a real project, this is where you'd run any automated tests.

    We did manual testing in the browser. If time, one could quickly demonstrate adding a simple unit test or two (but not necessary for this lab's scope).

1. Highlight the Spec Kit artifacts in the project.

    Open spec.md, plan.md, tasks.md to show they are all fulfilled. It's often satisfying to check off tasks.md items if not already done.

1. Reflect on how having a clear spec and plan made the coding part more straightforward and how Copilot's suggestions were on point mostly because it had the context (the spec in particular) to reference.

    For instance, Copilot might have used the word "20 products per page" in code comments because it saw that in spec.md.

Lab Conclusion: The learner successfully built a feature using Spec Kit's spec-driven approach. The key outcome is that the code was developed in alignment with a pre-written specification, with the AI assisting at each step but under strong guidance. This demonstrates to the learner the practical value of SDD: even if the AI wasn't perfect at every step, the structure we imposed ensured the final product meets the requirements.

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
