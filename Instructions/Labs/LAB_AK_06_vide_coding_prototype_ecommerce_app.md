---
lab:
    title: 'Exercise - Get started with vibe coding using GitHub Copilot Agent'
    description: 'Learn how to create a prototype app using a vibe coding process and GitHub Copilot Agent.'

---

# Get started with vibe coding using GitHub Copilot Agent

Vibe coding is a programming approach that leverages AI-driven tools, such as GitHub Copilot Agent, to generate software. Instead of manually writing code, a user provides a natural language description of their intended app, and the AI generates the corresponding code. This shifts the programmer's role from traditional coding to guiding, testing, and refining the AI-generated output.

In this exercise, you implement a vide coding process with GitHub Copilot Agent to create a prototype eCommerce app. The app will provide a basic online shopping experience using pages for products, product details, shopping cart, and checkout. The app will also support basic page navigation and will use a limited dataset that helps demonstrate the code functionality of the app. The prototype will not include any backend functionality, such as user authentication, payment processing, or database integration.

This exercise should take approximately **30** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following: Git 2.48 or later, .NET SDK 9.0 or later, Visual Studio Code with the C# Dev Kit extension, and access to a GitHub account with GitHub Copilot enabled.

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

## Exercise scenario

You're an entrepreneur who wants to create a prototype app for an online shopping experience. You've decided to use a vibe coding process to quickly develop a functional prototype that demonstrates the basic concepts of an eCommerce app, including pages for: product listing, product details, shopping cart, and checkout.

This exercise includes the following tasks:

1. Define product requirements. Use GitHub Copilot Chat (Ask mode) to define your app requirements.

1. Create an initial prototype app. Use GitHub Copilot Agent to create an initial prototype app.

1. Review and update the prototype app. Use GitHub Copilot Agent to review and update the prototype app.

## Define product requirements

Your initial requirements for the prototype eCommerce app are as follows:

1. Develop the app using HTML, CSS, and JavaScript.
2. Implement a simple web interface that include the following pages: product listing, product details, shopping cart, and checkout.

To have an agent create your prototype app, you'll need more detailed product requirements. This is typically done by creating a Product Requirements Document (PRD) that describes the app you want to create. The PRD should include information about the app's purpose, target audience, features, and technical requirements. You can also create low-fidelity wireframe diagrams that represent the user interface and help to explain the user experience.

A product requirements document (PRD) is a detailed description of the app you want to create. It includes information about the app's purpose, target audience, features, and technical requirements. In this task, you will use GitHub Copilot Chat (Ask mode) to define your app requirements.

A prototype app implements the basic features of an app and should satisfy the required use cases. The prototype implements stub features, simplified navigation, a placeholder dataset, and basic styling.

The vibe coding process is a structured approach to software development that emphasizes collaboration, creativity, and rapid prototyping. It is designed to help developers quickly create functional prototypes of applications while maintaining a focus on user experience and design.

1. Task - Envision and plan your project

    The developer works with GitHub Copilot to brainstorm ideas, create a Product Requirements Document (PRD), and create low-fidelity wireframe diagrams that show the user interface and user experience.

    - Brainstorm ideas

        - What is the purpose of your application? What problem does your application solve?
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

    - Create low-fidelity wireframe diagrams that show the user interface and user experience:

        Give the AI tool your PRD and a prompt to create wireframes for the application. The AI tool will generate a low-fidelity wireframe diagram that shows the user interface and user experience.

        - You can use M365 Copilot to create wireframes
        - Here is a GitHub Copilot Agent project for wireframes: (https://github.com/agoldbe/wireframer)
        - You can use a tool like Figma, Miro, or draw.io to create wireframes



I need to create a prototype eCommerce app using JavaScript, HTML, and CSS. The prototype eCommerce app needs to include a dynamic user interface that scales automatically to appear correctly on large or small screens (desktop and phone devices). The prototype eCommerce app should include the following pages: products, product details, shopping cart, and checkout. The prototype eCommerce app should use a simple dataset of 10 fruit products. The dataset should include: product name, description, price per unit (where unit could be the number of items or ounces, pounds, etc.), and a simple image that represents the product. The prototype eCommerce app should include a navigation menu on the left side of the screen that allows users to navigate between the products list, product details, shopping cart, and checkout pages. The prototype eCommerce app should have basic styling to make it visually appealing, but it does not need to be fully responsive or polished. The prototype eCommerce app will not include any backend functionality, such as user authentication, payment processing, or database integration. It will be a static prototype that demonstrates the basic concepts of an eCommerce app.

Here's a description of the user interface:

- The products list page should display a list of products with basic information such as product name, price per unit, and image. The products list page should also provide a way to select a desired quantity of a product and an option to add selected items to the shopping cart.
- The product details page should display detailed information about a selected product, including product name, description, price per unit, and image. The product details page should also provide a way to navigate back to the products list page.
- The shopping cart page should display a list of products added to the cart, including product name, quantity, and total price. The shopping cart page should also provide a way to update the quantity of products in the cart, and remove products from the cart.
- The checkout page should display a summary of the products in the cart, including product name, quantity, and total price. The checkout page should also provide a way to confirm the order.
- The left side navigation menu should provide basic navigation between pages. The navigation bar should collapsible down to thumbnail images when the display width drops below 300 pixels. The navigation bar should allow users to navigate between the products list, product details, shopping cart, and checkout pages.





1. Agent uses the PRD and wireframes to develop a prototype

    The AI tool will use the PRD and wireframes to develop a prototype of the application.

    The GitHub Copilot Agent will:

    - Install necessary tools and frameworks

    The prototype will include the following:

    - Basic functionality
    - User interface
    - User experience
    - Navigation between pages
    - Stub data for testing
