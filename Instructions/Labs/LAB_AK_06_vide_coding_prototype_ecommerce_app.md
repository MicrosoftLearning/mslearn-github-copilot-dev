---
lab:
    title: 'Exercise - Get started with vibe coding using GitHub Copilot Agent'
    description: 'Learn how to create a prototype app using a vibe coding process and GitHub Copilot Agent.'

---

# Get started with vibe coding using GitHub Copilot Agent

Vibe coding is a programming approach that leverages AI-driven tools, such as GitHub Copilot Agent, to generate software. Instead of manually writing code, a user provides a natural language description of their intended app, and the AI generates the corresponding code. This shifts the programmer's role from traditional coding to guiding, testing, and refining the AI-generated output.

In this exercise, you use a vide coding process and GitHub Copilot Agent to create a prototype eCommerce app. Your prototype app includes the following pages: products, product details, shopping cart, and checkout. The app includes basic navigation between pages and a limited dataset that helps to demonstrate app features. The prototype doesn't include any backend functionality, such as user authentication, payment processing, or database integration.

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

You're an entrepreneur who wants to create a prototype online shopping app. You decide to use a vibe coding process. Your initial prototype needs to demonstrate the basic capabilities that a user expects from an online shopping app and specific features that you've envisioned.

You identify the following base specifications to begin your development project:

1. Create a client-side web app using HTML, CSS, and JavaScript.
2. Include the following web pages: Products, ProductDetails, ShoppingCart, and Checkout.
3. Enable navigation between pages.

This exercise includes the following tasks:

1. Define product requirements. Use GitHub Copilot to help transition your base specifications into more detailed product requirements.

1. Create an initial prototype app. Use GitHub Copilot Agent and your product requirements to create an initial prototype app.

1. Refine your prototype app. Use GitHub Copilot Agent to complete a series of iterative updates that refine the user experience and ensure your app satisfies the intended requirements.

## Define product requirements

For an AI agent to develop your envisioned app, it needs to understand your product requirements and the intended user experience. Communicating your intensions to the AI can be achieved using either of the following processes:

- Code first and iterate to define requirements. This approach starts the coding process using only high-level base specifications. You then begin an iterative development process where your products evolves toward you intended/envisioned product requirements and end user experience. This approach risks deviating from your original vision, for better or for worse, as you explore features implemented by the AI. The iterating-to-define-requirements process can be time-consuming and may not yield the desired results. This is especially true when the initial specifications vague or open-ended.

- Explore requirements before coding. This approach involves working with the AI to create a Product Requirements Document (PRD) that contains a detailed description of the app you want to create before coding starts. A PRD includes information about the app's purpose, target audience, features, and technical requirements. By defining the requirements upfront, you can ensure that the AI agent has a clear understanding of your vision for the app.

> **NOTE**: A prototype app is an early, interactive model of an application that demonstrates its visual design and user experience. In this exercise, your prototype app should implement basic features and satisfy a small number of high-level use cases.

In this task, you use GitHub Copilot to review your high-level project parameters and create a product requirements document for you prototype app.

1. Open Visual Studio Code.

1. On the File menu, select **Open Folder**.

1. In the **Open Folder** dialog, open or create a folder for your prototype app, and then select **Select Folder**.

    The folder location should be outside of any existing Git repository and should be easy to find, manage, or remove after completing this lab exercise.

    For example, you can create a new folder named **VibeCoding-PrototypeApp** in your Desktop or Documents directory.

1. Open GitHub Copilot Chat view.

1. Ensure that the chat mode is set to **Ask** and the **GPT-4.1** model is selected.

1. In the Chat view, enter the following prompt:

    ```plaintext

    I want to create a product requirements document (PRD) for an app that I'll develop using a vibe coding process. I want the PRD to include information about the app's purpose, target audience, features, and technical requirements. I've defined the following high-level parameters for my app: 1 - Create a client-side web app using HTML, CSS, and JavaScript. 2 - Include the following web pages: Products, ProductDetails, ShoppingCart, and Checkout. 3 - Enable navigation between pages. I want the prototype app to implement basic features and satisfy a small number of high-level use cases. The prototype should implement the following: basic use case functionality, simple navigation, a sample dataset, and basic styling. I'll be adding the PRD to the chat context, then asking GitHub Copilot Agent to create the prototype app. What sections should I include in the PRD to enable GitHub Copilot Agent to create my envisioned prototype?

    ```

1. Take a minute to review the response generated by GitHub Copilot.

    The response should include a list of sections that you can include in your PRD. For example, the response may include a list of sections that's similar to the following list:

    - Summary and/or Overview
    - Target Audience
    - Core Features and Functionality
    - Use Cases & Scenarios
    - Technical Requirements
    - Sample Data Structure
    - UI/UX Requirements
    - Page-Specific Requirements
    - Navigation and Routing
    - Acceptance Criteria
    - Assumptions, Constraints, Out of Scope

    > **NOTE**: AI tools generate responses that vary over time. The selected AI model, chat history, and the context provided to your chat also affect responses. Don't be alarmed if the PRD sections that you see are different from the sections listed above.

1. Take a few minutes to consider the information that's required to create a PRD for your prototype app.

    > **Tip**: You can provide a natural language description of your app's requirements and have GitHub Copilot format that information as a PRD. You can also use GitHub Copilot to help you review and update the PRD to ensure that it provides the required level of detail.

1. In the Chat view, enter the following prompt:

    ```markdown
    Here's some information that should help you construct the PRD:

    My prototype app targets online shoppers interested in ordering my products. The prototype should include the following:

    - A dynamic user interface that scales automatically to appear correctly on large or small screens (desktop and phone devices).
    - A simple dataset that defines 10 fruit products. The dataset should include: product name, description, price per unit (where unit could be the number of items, ounces, pounds, etc.). If possible, I want to include a simple image that represents the product.
    - A navigation menu on the left side of the screen that allows users to navigate between the Products, Product Details, ShoppingCart, and Checkout pages.
    - Basic styling that makes the user interface visually appealing, but it doesn't need to be fully responsive or polished.

    The prototype app won't include any backend functionality, such as user authentication, payment processing, or database integration. It will be a static prototype that demonstrates the basic concepts of an eCommerce app.

    Here's a description of the user interface:

    - The Products page should display a list of products with basic information such as product name, price per unit, and image. The Products page should also provide a way to select a desired quantity of a product and an option to add selected items to the shopping cart.
    - The ProductDetails page should display detailed information about a product when the product is selected from the Products page. The ProductDetails page should display the product name, a description of the product, the price per unit, and an image of the product (if possible). The ProductDetails page should also provide a way to navigate back to the Products page.
    - The ShoppingCart page should display the list of products added to the cart. The list should include the product name, quantity, and total price for that product. The ShoppingCart page should also provide a way to update the quantity of each product that's in the cart, and an option to remove products from the cart.
    - The Checkout page should display a summary of the products being purchased, including product name, quantity, and price. The total price should be clearly displayed along with the option to "Process Order".
    - The left-side navigation menu should provide basic navigation between pages. The navigation bar should collapse down to thumbnail images when the display width drops below 300 pixels. The navigation bar should allow users to navigate between the app pages.

    ```

1. In the Chat view, select the **Agent** mode.

1. In the Chat view, enter the following prompt:

    ```markdown
    Create a markdown file named VibeCodingPRD.md using your suggested sections and the inputs that I've provided.
    ```

1. In the Chat view, to save the suggested VibeCodingPRD.md file, select **Keep**.

1. Take a couple minutes to review and update the PRD file.

    The suggested PRD should be sufficient. However, if you see any gaps or discrepancies, or if you want to add more information, you can edit the file directly in Visual Studio Code. You can also ask GitHub Copilot to help you refine the PRD by providing additional information or clarifications.

1. In the Chat view, enter the following prompt:

    ```markdown
    Can you create or propose low-fidelity wireframe diagrams (or text-based layouts) that show the user interface for my prototype app? Use the PRD that I've provided as a reference.
    ```

    GitHub Copilot Agent should generate low-fidelity text-based layouts that represent the user interface for your prototype app. These "wireframe diagrams" should help you visualize the layout of the user interface and how users interact with the app.

    > **NOTE**: There are several ways to create wireframe diagrams. For an AI-based, vibe coding type of approach, you can use Microsoft's M365 Copilot. Just provide M365 Copilot with a description of your app and ask it to create images of low-fidelity wireframe diagrams. For high-fidelity wireframe diagrams that you create manually, you can use a UI/UX design tool such as Figma.

1. In the Chat view, enter the following prompt:

    ```markdown
    Save the low-fidelity wireframe diagrams as text files.
    ```

1. Monitor the Chat view to ensure that all of the files are saved, and then select **Keep**.

    GitHub Copilot Agent can use product requirements and wireframe diagrams to develop your prototype application. Having a sufficiently detailed PRD and wireframe diagrams helps the AI agent understand your intended user experience, functionality, and design. The PRD provides detailed information about the app's purpose, target audience, features, and technical requirements, while the wireframe diagrams show the user interface and user experience.

1. Take a couple minutes to review the wireframe diagrams.

    If you see any obvious issues that you want to correct, you can edit the wireframe diagrams directly in Visual Studio Code. You can also ask GitHub Copilot Chat to help you refine the wireframe diagrams by providing additional information or clarifications.

    > **Tip**: If you're unsure how to interpret a wireframe diagram, or if you think one of the diagrams may be incorrect, ask GitHub Copilot to explain the diagram(s). For example, you can ask GitHub Copilot Agent to review the wireframe diagrams and use them to explain the layout of the user interface, and how the user interacts with the app. If the explanation doesn't match your expectations, you can ask GitHub Copilot Agent to update the wireframe diagrams to better match your intended user experience.

## Create an initial prototype app

GitHub Copilot Agent can use your product requirements and wireframe documents to create an initial prototype app. Your prototype app should implement the high-level features and use cases you defined. The prototype will include essential shopping features, simplified navigation, a placeholder dataset, and basic styling.

In this task, you use GitHub Copilot Agent to create an initial prototype app based on the PRD and wireframe diagrams that you created.

1. In Visual Studio Code, create a new folder named **ShoppingApp** in the VibeCoding-PrototypeApp folder.

    GitHub Copilot Agent needs an empty folder to use as a workspace for the new the app files.

    The EXPLORER view in Visual Studio Code should look similar to the following:

    ```plaintext
    VibeCoding-PrototypeApp
    ├── ShoppingApp
    ├── VibeCodingPRD.md
    ├── ProductsWireframe.txt
    ├── ProductDetailsWireframe.txt
    ├── ShoppingCartWireframe.txt
    ├── CheckoutWireframe.txt
    └── NavBarWireframe.txt
    ```

1. Add the PRD and wireframe diagrams to the chat context.

1. In the Chat view, enter the following prompt:

    ```markdown
    Create a prototype app in the ShoppingApp folder using the PRD and wireframe diagrams that I've provided. The prototype should implement the following: basic use case functionality, simple navigation, a sample dataset, and basic styling.
    ```

1. In the Chat view, select **Keep** to save the prototype app files.

1. Expand the **ShoppingApp** folder.

    The folder should contain the following files:

    - `Index.html`
    - `styles.css`
    - `app.js`

1. Take a couple minutes to review the prototype app files.

    The `Index.html` file should contain the basic structure of your prototype app, including the HTML, CSS, and JavaScript code that implements the basic functionality and user interface. The `styles.css` file should contain the basic styling for your prototype app, and the `app.js` file should contain the JavaScript code that implements the basic functionality and user interface.

1. Use Windows File Explorer to open the **Index.html** file in a browser.

    The file should contain the basic structure of your prototype app, including the HTML, CSS, and JavaScript code that implements the basic functionality and user interface.

1. Use the PRD to verify that the prototype app satisfies the specified use cases.

    For example:

    - The Products page should display a list of products with basic information such as product name, price per unit, and image.
    - The Products page should provide a way to select a quantity of a product and an option to add selected items to the shopping cart.
    - The ProductDetails page should display detailed information about a selected product, including product name, description, price per unit, and image.
    - The ProductDetails page should provide a way to navigate back to the Products page.
    - The ShoppingCart page should display a list of products added to the cart, including product name, quantity, and total price.
    - The ShoppingCart page should provide a way to update the quantity of products in the cart and remove products from the cart.
    - The Checkout page should display a summary of the products in the cart, including product name, quantity, and total price. It should also provide a way to confirm the order.
    - The prototype app should provide basic navigation between pages using a collapsible navigation bar on the left side of the screen.

1. Try resizing the browser window to see how the prototype app responds to different screen sizes.

    The prototype app should have a dynamic user interface that automatically scales to accommodate viewing on desktop and phone devices.

1. Try resizing the width of the browser window to trigger the collapsed navigation menu.

    > **NOTE**: Most desktop browsers (including Microsoft Edge) enforce a minimum window width that is greater than 300px (often around 320–400px). This means you may not be able to manually resize the browser window small enough to collapse the navigation bar.

## Update the prototype app

The initial prototype app provides a basic implementation of the product requirements, but it can be refined and improved to better meet the intended user experience and functionality. In this task, you use GitHub Copilot Agent to refine the prototype app.

1. In the Chat view, enter the following prompt:

    ```markdown
    Refactor the prototype app to use a higher breakpoint for the collapsed navigation. Change from 300 to 600px. Update the PRD to explain the updated 600px requirement.
    ```

1. In the Chat view, select **Keep** to save the updated prototype app files.

1. Retest the app in the browser.

1. In the Chat view, enter the following prompt:

    ```markdown
    Update the prototype app to display an small icon (or emoji image) in the nav bar for each of the web pages. Ensure that the icon is centered horizontally in the nav bar when the nav bar is collapsed.
    ```

1. In the Chat view, select **Keep** to save the updated prototype app files.

1. Retest the app in the browser.

1. In the Chat view, to identify additional opportunities for improvements, enter the following prompt:

    ```markdown
    #codebase Review the PRD and wireframes. Are there any features or requirements that are missing from the implementation? Are there obvious opportunities to improve the user experience?
    ```

1. Review the response from GitHub Copilot Agent.

    If you see any opportunities for improvement that look interesting and you have time for additional updates, you can ask GitHub Copilot Agent to help you implement the changes.

## Summary

In this exercise, you learned how to use GitHub Copilot Agent to create a prototype app using a vibe coding process. You defined product requirements, created an initial prototype app, and refined the prototype app to better meet the intended user experience and functionality.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. If you made any changes, revert them now. If you're using a local PC as your lab environment, you can archive or delete the prototype app folder that you created for this exercise.
