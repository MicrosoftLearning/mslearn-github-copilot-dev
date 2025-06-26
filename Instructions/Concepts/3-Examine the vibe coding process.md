# Examine the Vibe Coding process

Vibe coding is a collaborative process between developers and AI tools, enabling rapid prototyping and iterative development.

The vibe coding process typically involves the following steps:

1. Envision and plan your project

    The developer will work with an AI tool on the following steps to envision and plan the application:

    - Brainstorm ideas

        - What problem does your application solve? What is the purpose of your application?
        - Who is your target audience?
        - What platforms will your application run on (web, mobile, desktop)?
        - What features will your application have?
        - What technologies will you use to build it?
        - What are the data requirements for your application?
        - Is there any sensitive or PI data that needs to be protected?

    - Create Product Requirements Document (PRD)

        - Project Summary
        - Problem Overview
        - Scope
        - Use Cases & Scenarios
        - Requirements
            - Functional Requirements: user stories and acceptance criteria
            - Non-functional Requirements: performance, scalability, security, etc.
            - Dependencies
        - Success Metrics
        - Competitive Analysis
        - Product Roadmap
        - Risks & Challenges
        - Open Questions
        - Supporting Documentation
        - Sign-Off

    - Create low-fidelity wireframe diagrams that show the user interface and help explain the user experience:

        Give the AI tool your PRD and a prompt to create wireframe diagrams for the application. The AI tool will generate low-fidelity wireframe diagrams (text-based layouts) that represent the user interface and help to define the user experience.

        - You can use M365 Copilot to create images of wireframe diagrams
        - Here is a GitHub Copilot Agent project for wireframe diagrams: [https://github.com/agoldbe/wireframer](https://github.com/agoldbe/wireframer)
        - You can use a tool like Figma to create high-fidelity wireframe diagrams, or you can use tools like Miro or draw.io to create medium-fidelity wireframe diagrams.

1. Agent uses the PRD and wireframe diagrams to develop a prototype

    The AI tool will use the PRD and wireframe diagrams to develop a prototype of the application.

    The GitHub Copilot Agent will:

    - Install necessary tools and frameworks

    The prototype will include the following:

    - Basic functionality
    - User interface
    - User experience
    - Navigation between pages
    - Stub data for testing

    You can also have the Agent help with the following:

    - Configure your IDE or code editor
    - Set up version control (e.g., Git)

1. Implement fully-functional features (iterate, iterate, iterate)

    GitHub Copilot Agent helps you iterate on the prototype in the following ways:

    - Replacing prototype/stub code with code that implements completed features
    - Developing UI elements that enable richer interactions
    - Implementing sample data that helps demonstrate basic functionality
    - Code review and bug fixing
    - Develop unit tests to ensure code quality
    - Developing new features that enhance the app
    - Refactoring code

1. Language and framework conversion

    If you want to convert your application to a different language or framework, you can use the AI tool to help with the conversion. The AI tool will:

    - Analyze the existing codebase
    - Generate equivalent code in the target language or framework
    - Ensure that the new code maintains the same functionality and user experience

1. Documentation generation

    The AI tool can generate documentation for the codebase, including function descriptions, usage examples, and API documentation. This helps maintain clarity and understanding of the code, especially in collaborative environments.

1. Code review and improvement

    The AI tool can help with code review and improvement by:

    - Analyzing the code for potential bugs and errors
    - Suggesting improvements to the code structure and design
    - Identifying areas for optimization and performance enhancements
    - Ensuring adherence to coding standards and best practices
    - Providing feedback on code readability and maintainability
    - Assisting with refactoring to improve code quality
