<!-- ---
lab:
    title: 'Exercise - Develop a greenfield application using GitHub Spec Kit'
    description: 'Learn how to apply the spec-driven development methodology with GitHub Spec Kit for a greenfield application project. in Visual Studio Code.'
--- -->

# Develop a greenfield application using GitHub Spec Kit

GitHub Spec Kit is an open-source toolkit that enables Spec-Driven Development (SSD) by integrating specifications with AI coding assistants like GitHub Copilot.

In this exercise, you learn how to use the GitHub Spec Kit to develop a new greenfield application. You begin by initializing the GitHub Spec Kit for a new .NET project. You then use GitHub Spec Kit workflows to create the constitution, specification, plan, and tasks documents for the new application. Finally, you use GitHub Spec Kit's implementation workflow to implement an initial MVP version of the application.

This exercise should take approximately **75** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment MUST include the following resources: Git 2.48 or later, .NET SDK 8.0 or later, Visual Studio Code with the C# Dev Kit and GitHub Copilot Chat extensions, SQL Server LocalDB, Python 3.11 or later, the uv package manager, and access to a GitHub account with GitHub Copilot enabled.

For help with configuring your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2345907" target="_blank">Configure your GitHub Spec Kit lab environment</a>.

## Exercise scenario

You're a software developer working for a consulting firm. The firm is moving to a spec-driven development (SDD) methodology using GitHub Spec Kit and GitHub Copilot in Visual Studio Code. Your client, Contoso Corporation, needs you to develop an RSS feed reader for internal employees. Your firm needs you to use the SDD methodology and GitHub Spec Kit to ensure that an MVP version of the application is delivered quickly, and that additional features can be rolled out seamlessly after customer sign-off.

Contoso's stakeholders documented the project goals, features, and technical requirements. This information should be used to create the constitution, specification, plan, and tasks documents that guide the development process.

This exercise includes the following tasks:

1. Create a project folder and initialize GitHub Spec Kit.
1. Review the project files and prepare stakeholder documents.
1. Generate a constitution based on standards and guidelines.
1. Generate the spec.md file using stakeholder requirements.
1. Generate the plan.md file using stakeholder requirements and the specification.
1. Generate the tasks.md file using the specification, plan, and constitution.
1. Implement the tasks required for an MVP application.

## Create a project folder and initialize GitHub Spec Kit

Although you're working on a greenfield project, a project folder is required before you can initialize GitHub Spec Kit.

In this task, you create a new project folder and initialize GitHub Spec Kit in your project directory.

Use the following steps to complete this task:

1. Open a terminal window, and then navigate to the root of your C: drive.

    At the command prompt, to navigate to the root of your C: drive, enter the following command:

    ```powershell
    cd C:\
    ```

1. To create a new folder named for your RSSFeedReader project, enter the following command:

    ```powershell
    mkdir TrainingProjects\RSSFeedReader
    ```

1. To navigate to the new project folder, enter the following command:

    ```powershell
    cd TrainingProjects\RSSFeedReader
    ```

1. To initialize GitHub Spec Kit in the current directory, enter the following command:

    ```powershell
    specify init --here --ai copilot --script ps
    ```

    > **NOTE:** If you're using macOS or Linux with bash/zsh, replace `--script ps` with `--script sh`.

    This `specify init` command uses the following components:

    - `--here` - Initializes GitHub Spec Kit in the current directory (your existing RSSFeedReader project).
    - `--ai copilot` - Configures the project for GitHub Copilot.
    - `--script ps` - Uses PowerShell scripts.

    For brownfield projects, the `specify init` command recognizes that the current directory isn't empty and asks for confirmation to proceed.

    The `specify init` command completes the following actions:

    - Creates agent prompt files in the `.github/agents/` and `.github/prompts/` directories.
    - Creates template files in the `.specify/memory/` and `.specify/templates/` directories.
    - Creates script files in the `.specify/scripts/powershell/` directory.
    - Updates or creates a settings.json file in the `.vscode/` directory.
    - Preserves any existing application files.
    - Displays a success message ("Project ready").
    - Suggests some optional next steps.

1. To open the RSSFeedReader project in Visual Studio Code, enter the following command:

    ```powershell
    code .
    ```

    The `code .` command opens the current directory (RSSFeedReader) in Visual Studio Code.

1. Wait for Visual Studio Code to fully load the project.

1. Take a minute to familiarize yourself with the project structure.

    Use Visual Studio Code's EXPLORER view to expand the application folders. You should see a folder structure that's similar to the following example:

    ```plaintext
    RSSFEEDREADER (root)
    ├── .github/
    │   ├── agents/                 (GitHub Spec Kit executable workflows that can be triggered via commands)
    │   └── prompts/                (GitHub Spec Kit prompt files that provide detailed instructions for each of the agent workflows)
    ├── .specify/                   (GitHub Spec Kit configuration)
    │   ├── memory/                 (GitHub Spec Kit stores the project constitution defining core principles and governance rules that all features must follow)
    │   ├── scripts/powershell/     (GitHub Spec Kit uses automation utilities (scripts) for creating features, setting up plans, and managing the specification workflow)
    │   └── templates/              (GitHub Spec Kit provides standardized markdown formats for specs, plans, tasks, and checklists to ensure consistent documentation across all features)
    └── .vscode/                    (Visual Studio Code configuration)
    ```

1. Ensure that GitHub Copilot's Chat view is open.

    GitHub Spec Kit works with GitHub Copilot through Visual Studio Code's chat interface. When you run "specify init --ai copilot" in your project directory, the toolkit configures your workspace to recognize "/speckit.*" commands.

    > **NOTE:** This lab exercise was tested using the GPT-5.2 and Claude Sonnet 4.5 models. Results were similar between these two models. However, using GPT-4 and GPT-5 mini models produced inconsistent and unexpected results. We suggest using newer language models that are optimized for complex reasoning when running GitHub Spec Kit commands.

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

## Download and review the stakeholder documents

In this task, you download the RSSFeedReader documents provided by the Contoso stakeholders, add them to your project, and then evaluate how the documents relate to GitHub Spec Kit commands.

Use the following steps to complete this task:

1. To download the stakeholder documents, open the following link in a browser: [RSSFeedReader - stakeholder documents](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHSpecKitEx14StakeholderDocuments.zip).

1. Open the folder containing the downloaded ZIP file.

1. Extract the contents of the downloaded ZIP file to a temporary folder.

1. In Visual Studio Code's EXPLORER view, right-click the RSSFeedReader project root folder, and then select **Reveal in File Explorer** (or **Reveal in Finder** on macOS).

1. In File Explorer (or Finder), open the temporary folder where you extracted the ZIP file.

1. Select all files in the temporary folder, copy them, and then paste them into the RSSFeedReader project root folder.

    The extracted file folder contains the following:

    ```plaintext
    GHSpecKitEx14StakeholderDocuments (root)
    ├── StakeholderDocuments        (folder containing stakeholder supplied documents)
    └── README.md                   (a readme file describing the project)
    ```

1. Switch back to Visual Studio Code.

1. In Visual Studio Code's EXPLORER view, expand the **StakeholderDocuments** folder.

    The StakeholderDocuments folder includes the following files:

    - **Project Goals.md** - High-level project goals, purpose, scope, delivery approach, rollout plan, quality goals, and standards/guidelines.
    - **App Features.md** - Detailed user-facing feature requirements.
    - **Tech Stack.md** - Technology choices and architectural rationale.
    - **MVP System Rules.md** - MVP system behavior rules that inform specs.

1. Take a few minutes to open and review each of the stakeholder documents.

    These documents include natural language descriptions of the project's goals, features, technical requirements, and constraints. Understanding this context is essential for creating an effective specification, plan, and tasks. The level of detail varies between the documents, but the overall mix is typical of what you might find in many real-world projects.

    The number of documents and the details provided by the documents can vary greatly depending on company policies and project complexity. The GitHub Spec Kit commands are designed to work with the files and details available, and use that information to create the constitution, spec, plan, and tasks documents required for a successful spec-driven development process.

1. Take a few minutes to consider how each document relates to GitHub Spec Kit commands.

    Each of the stakeholder documents provide information that helps guide different aspects of the spec-driven development process.

    For example:

    - **Project Goals.md**: This document provides high-level goals and standards that will inform the constitution.md file.
    - **App Features.md**: This document contains detailed user-facing requirements that will help to create the spec.md file.
    - **Tech Stack.md**: This document outlines technology choices and architectural rationale that will influence the plan.md file.
    - **MVP System Rules.md**: This document defines system behavior rules that will guide the implementation tasks in tasks.md.

## Generate a constitution based on standards and guidelines

The GitHub Spec Kit uses a constitution.md file to establish the governing principles and constraints that guide all development decisions for the RSSFeedReader project. It captures organizational policies, technical standards, security requirements, and development practices that must be followed throughout implementation.

In this task, you use GitHub Copilot's `/speckit.constitution` command to generate a comprehensive constitution, first with user-supplied inline text input and then using the stakeholder documents.

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

1. Take a moment to consider the options for running the `/speckit.constitution` command.

    You can run the /speckit.constitution command with the following options:

    - `/speckit.constitution --text "..."`: Use inline text to describe the standards, guidelines, principles, and constraints that should be included in the constitution.
    - `/speckit.constitution --files ...`: Specify project documents that provide context for creating the constitution.

    > **NOTE**: The /speckit.constitution command can be run multiple times in the same project to refine or expand the constitution.md file. In this case, you first run the command using inline text input, and then you run it again using the stakeholder documents.

1. In the Chat view, to start a constitution workflow using inline text that provides a broad scope of general coding principles, enter the following command:

    ```plaintext
    /speckit.constitution --text "Code projects emphasize security, privacy, accessibility, performance, reliability, observability, release management, documentation, dependency management, and code quality. Ensure that all principles are specific, actionable, and relevant to the project context."
    ```

    The GitHub Spec Kit supports "greenfield" and "brownfield" project types. When running GitHub Spec Kit commands, the inputs that you specify for greenfield projects can be more impactful since there's no existing codebase.

1. Monitor GitHub Copilot's response in the Chat view.

1. Take a minute to review the updated constitution.md file in the editor.

    Notice that GitHub Copilot has updated the constitution.md file to include principles based on the inline text you provided. The principles should be clearly stated and actionable.

    For a real-world project, it's important to review the constitution against the following criteria before saving:

    - Completeness: All major areas (security, performance, quality, technical standards) are covered.
    - Clarity: Each principle is specific and unambiguous.
    - Consistency: Principles don't contradict each other.
    - Relevance: All principles relate to the RSSFeedReader project.

1. If the `/speckit.constitution` command updated files in the **templates** folder, take a minute to review those files as well.

    The constitution workflow might update the templates for other GitHub Spec Kit files (spec.md, plan.md, tasks.md). The updates should reflect the principles defined in the constitution.md file.

1. To accept the changes to all updated files, select the **Keep** button in the Chat view.

    You can also accept changes to individual files, or individual changes within a file, by selecting a **Keep** button in the editor.

1. In the Chat view, to start a second constitution workflow using the stakeholder documents, enter the following command:

    ```plaintext
    /speckit.constitution --files StakeholderDocuments/Project\ Goals.md StakeholderDocuments/App\ Features.md StakeholderDocuments/Tech\ Stack.md StakeholderDocuments/MVP\ System\ Rules.md
    ```

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
