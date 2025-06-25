---
lab:
    title: 'Exercise - Get started with vibe coding using GitHub Copilot Agent'
    description: 'Learn how to create a prototype app using a vibe coding process and GitHub Copilot Agent.'

---

# Get started with vibe coding using GitHub Copilot Agent

Vibe coding is an approach to programming that uses AI tools, such as GitHub Copilot Agent, to generate software. Instead of manually writing code, a user provides a natural language description of their intended app, and the AI generates the corresponding code. This shifts the programmer's role from traditional coding to guiding, testing, and refining the AI-generated output.

In this exercise, you use a vide coding process and GitHub Copilot Agent to create a prototype version of an online shopping app. Your prototype app includes the following pages: products, product details, shopping cart, and checkout. The app includes basic navigation between pages and a limited dataset that helps to demonstrate app features. The prototype doesn't include any backend functionality, such as user authentication, payment processing, or database integration.

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

You're an entrepreneur who wants to use a vibe coding process to create a prototype shopping app. Your initial prototype needs to demonstrate the basic capabilities that a user expects from an online shopping app and specific features that you've envisioned.

You identify the following base specifications to begin your development process:

1. Create a client-side web app using HTML, CSS, and JavaScript.
2. Include the following web pages: Products, ProductDetails, ShoppingCart, and Checkout.
3. Enable navigation between pages.

This exercise includes the following tasks:

1. Define product requirements. Use GitHub Copilot to help transition your base specifications into more detailed product requirements.

1. Create an initial prototype app. Use GitHub Copilot Agent and your product requirements to create an initial prototype app.

1. Refine your prototype app. Use GitHub Copilot Agent to complete a series of iterative updates that refine the user experience and ensure your app satisfies the intended requirements.

> **NOTE**: A prototype app is an early, interactive model of an application that demonstrates its visual design and user experience. In this exercise, your prototype app should implement basic features and satisfy a small number of high-level use cases.

## Define product requirements

For an AI agent to develop your envisioned app, it needs to understand your product requirements and the intended user experience. You can communicate your intensions to the GitHub Copilot Agent using either of the following processes:

- Code first and iterate to define requirements. This approach starts the coding process using only high-level base specifications. You then begin an iterative development process where your products evolves toward you intended/envisioned product requirements and end user experience. This approach risks deviating from your original vision, for better or for worse, as you explore features implemented by the AI. The iterating-to-define-requirements process can be time-consuming and may not yield the desired results. This is especially true when the initial specifications vague or open-ended.

- Explore requirements before coding. This approach involves working with the AI to create a Product Requirements Document (PRD) that contains a detailed description of the app you want to create before coding starts. A PRD includes information about the app's purpose, target audience, features, and technical requirements. By defining the requirements upfront, you can ensure that the AI agent has a clear understanding of your vision for the app.

In this task, you use GitHub Copilot to review your base specifications and create a product requirements document for your prototype app.

Use the following steps to complete this section of the exercise:

1. Open Visual Studio Code.

1. On the File menu, select **Add folder to Workspace**.

1. In the **Add Folder to Workspace** dialog, navigate to an easy to find folder location, create a new folder named **VibeCoding-PrototypeApp**, and then select select **Add**.

    The folder location should be outside of any existing Git repository and should be easy to find. After completing this lab exercise, you may want to archive or delete the project.

    For example, if you're using a Windows PC, you can create a new folder named **VibeCoding-PrototypeApp** on your **Desktop** or in your **Documents** directory.

1. Open the Chat view.

    The Chat view can be opened by selecting the GitHub Copilot icon located near the top-center of the Visual Studio Code window, just to the right of the search textbox.

1. Ensure that the chat mode is set to **Ask** and the **GPT-4.1** model is selected.

    The Set Mode dropdown menu is located in the bottom-left corner of the Chat view.

    Although their capabilities overlap, each of the chat modes is optimized for a specific purpose:

    - **Ask**: Use this mode to ask GitHub Copilot questions about your codebase. You can use Ask mode to explain code, suggest changes, or provide information about the codebase.
    - **Edit**: Use this mode to edit specific code files in your workspace. You can use Edit mode to refactor code, add comments, implement tests, or add new features to your apps.
    - **Agent**: Use this mode to run GitHub Copilot as an agent. You can use Agent mode to perform coding tasks autonomously.

1. In the Chat view, enter the following prompt:

    ```plaintext

    I want to create a product requirements document (PRD) for an app that I'll develop using a vibe coding process. I want the PRD to include information about the app's purpose, target audience, features, and technical requirements. I've defined the following high-level parameters for my app: 1 - Create a client-side web app using HTML, CSS, and JavaScript. 2 - Include the following web pages: Products, ProductDetails, ShoppingCart, and Checkout. 3 - Enable navigation between pages. I want the prototype app to implement basic features and satisfy a small number of high-level use cases. The prototype should implement the following: basic use case functionality, simple navigation, a sample dataset, and basic styling. I'll be adding the PRD to the chat context, then asking GitHub Copilot Agent to create the prototype app. What sections should I include in the PRD to enable GitHub Copilot Agent to create my envisioned prototype?

    ```

1. Review the response generated by GitHub Copilot.

    For example, your response may be similar to the following sample:

    ```md

    To enable GitHub Copilot Agent to create your envisioned prototype app, your Product Requirements Document (PRD) should include the following sections:
    
    1. App Overview / Purpose  
       - Briefly describe what the app does and its main goal.
    
    2. Target Audience  
       - Define who will use the app (e.g., general consumers, online shoppers, etc.).
    
    3. Core Features  
       - List and describe the main features (e.g., view products, view product details, add to cart, checkout).
    
    4. Page Descriptions  
       - Detail each required page (Products, ProductDetails, ShoppingCart, Checkout) and what should be displayed or possible on each.
    
    5. Navigation  
       - Explain how users move between pages (e.g., navigation bar, links, buttons).
    
    6. Sample Data  
       - Describe the sample dataset (e.g., a few example products with name, price, description, image).
    
    7. Technical Requirements  
       - Specify technologies to use (HTML, CSS, JavaScript, client-side only, no backend).
       - Mention any libraries or frameworks (if any) or state "vanilla JS".
    
    8. Styling  
       - Indicate the level of styling (e.g., basic, clean, minimal, responsive).
    
    9. Use Cases / User Flows  
       - List a few high-level use cases (e.g., browse products, view details, add to cart, proceed to checkout).
    
    10. Out of Scope  
        - Clarify what is not required (e.g., user authentication, payment processing, backend integration).
    
    Including these sections will give clear, actionable guidance for building your prototype app.

    ```

    > **NOTE**: Don't worry if your response includes different section titles or if the sections appear in a different order. The responses that AI tools generate are often slightly different from one chat session to another. The selected AI model, your chat history, and the context of your chat session can also affect responses.

1. Take a few minutes to consider the information that's required to complete each section of the PRD.

    A well-defined PRD helps ensure that GitHub Copilot Agent has a clear understanding of your vision for the app. The PRD should provide enough detail to enable the Agent to create a prototype app that meets your requirements and intended user experience. Your PRD should build upon the base specifications listed earlier in the exercise.

    If you're not sure about what information to include in a specific section, you can ask GitHub Copilot Agent to help you generate the content for that section. For example, you can ask GitHub Copilot Agent to help you define the target audience, core features, or technical requirements for your app.

    > **Tip**: You can provide a natural language description of your app's requirements and have GitHub Copilot format that information as a PRD. You can also use GitHub Copilot to help you review and update the PRD, and to ensure that it provides the level of detail required for GitHub Copilot Agent to create the prototype.

1. In the Chat view, enter the following prompt:

    ```markdown
    The suggested sections look good. Here's some information that should help you construct the PRD:

    My prototype app targets online shoppers interested in ordering my products. The prototype should include the following:

    - A dynamic user interface that scales automatically to appear correctly on large or small screens (desktop and phone devices).
    - A simple dataset that defines 10 fruit products. The dataset should include: product name, description, price per unit (where unit could be the number of items, ounces, pounds, etc.). If possible, I want to include a simple image (an emoji) that represents the product.
    - A navigation menu on the left side of the screen that allows users to navigate between the Products, ProductDetails, ShoppingCart, and Checkout pages.
    - Basic styling that makes the user interface visually appealing, but it doesn't need to be fully responsive or polished.

    The prototype app won't include any backend functionality, such as user authentication, payment processing, or database integration. It will be a static prototype that demonstrates the basic concepts of an eCommerce app.

    Here's a description of the user interface:

    - The Products page should display a list of products with basic information such as product name, price per unit, and image (an emoji). The Products page should also provide a way to select a desired quantity of a product and an option to add selected items to the shopping cart.
    - The ProductDetails page should display detailed information about a product when the product is selected from the Products page. The ProductDetails page should display the product name, a description of the product, the price per unit, and an image (an emoji) representing the product. The ProductDetails page should also provide a way to navigate back to the Products page.
    - The ShoppingCart page should display the list of products added to the cart. The list should include the product name, quantity, and total price for that product. The ShoppingCart page should also provide a way to update the quantity of each product that's in the cart, and an option to remove products from the cart.
    - The Checkout page should display a summary of the products being purchased, including product name, quantity, and price. The total price should be clearly displayed along with the option to "Process Order".
    - The left-side navigation menu should provide basic navigation between pages. The navigation bar should collapse down to display a one or two letter abbreviation when the display width drops below 300 pixels. The navigation bar should allow users to navigate between the app pages.

    ```

    GitHub Copilot should generate a response that includes a suggested PRD based on the information you provided. The response should include the sections that you reviewed earlier, and it should include content for each section based on the information you provided.

1. In the Chat view, select the **Agent** mode.

1. In the Chat view, enter the following prompt:

    ```markdown
    Create a markdown file named VibeCodingPRD.md using your suggested sections and the inputs that I've provided.
    ```

1. In the Chat view, to save the suggested VibeCodingPRD.md file, select **Keep**.

1. Take a couple minutes to review the PRD file.

    The suggested PRD file should be sufficient for this exercise. However, if you see any gaps or discrepancies, or if you want to add more information, you can edit the file directly in Visual Studio Code. You can also ask GitHub Copilot to help you refine the PRD by generating additional information or implementing clarifications.

1. In the Chat view, enter the following prompt:

    ```markdown
    Can you create or propose low-fidelity wireframe diagrams (or text-based layouts) that show the user interface for my prototype app? Use the PRD that I've provided as a reference.
    ```

    GitHub Copilot Agent should generate low-fidelity text-based layouts that represent the user interface for your prototype app. These "wireframe diagrams" should help you visualize the user interface and how users will interact with the app.

    For example, the response may look similar to the following:

    ```md

    Certainly! Here are text-based, low-fidelity wireframe layouts for each main page of your prototype app, based on your PRD:

    ---

    ### 1. Products Page
    

    +------------------------------------------------------+
    | [Nav] Products | ProductDetails | Cart | Checkout    |
    +----------------+-------------------------------------+
    | 🍎 Apple         $1.00/each   [Qty: - 1 +] [Add]     |
    | 🍌 Banana        $0.50/each   [Qty: - 1 +] [Add]     |
    | 🍊 Orange        $0.80/each   [Qty: - 1 +] [Add]     |
    | ... (other fruits)                                  |
    +------------------------------------------------------+
    
    ---
    
    ### 2. ProductDetails Page
    
    +------------------------------------------------------+
    | [Nav] Products | ProductDetails | Cart | Checkout    |
    +----------------+-------------------------------------+
    | 🍎 Apple                                         |
    | Fresh, crisp apples                              |
    | Price: $1.00/each                               |
    | [Qty: - 1 +] [Add to Cart]                      |
    | [Back to Products]                              |
    +------------------------------------------------------+
    
    ---
    
    ### 3. ShoppingCart Page
    
    +------------------------------------------------------+
    | [Nav] Products | ProductDetails | Cart | Checkout    |
    +----------------+-------------------------------------+
    | Cart Items:                                       |
    | 🍎 Apple   Qty: [ - 2 + ]  $2.00   [Remove]        |
    | 🍌 Banana  Qty: [ - 1 + ]  $0.50   [Remove]        |
    | -------------------------------------------------- |
    | [Update Cart]                                     |
    | [Proceed to Checkout]                             |
    +------------------------------------------------------+
    
    ---
    
    ### 4. Checkout Page
    
    +------------------------------------------------------+
    | [Nav] Products | ProductDetails | Cart | Checkout    |
    +----------------+-------------------------------------+
    | Order Summary:                                     |
    | 🍎 Apple   x2   $2.00                              |
    | 🍌 Banana  x1   $0.50                              |
    | -------------------------------------------------- |
    | Total: $2.50                                      |
    | [Process Order]                                   |
    +------------------------------------------------------+
    
    ---
    
    ### 5. Navigation Bar (Left Side, Collapsible)
    
    +-------------------+      +-----+
    | Products          |      | P   |
    | ProductDetails    |      | D   |
    | Cart              |      | C   |
    | Checkout          |      | CO  |
    +-------------------+      +-----+
    (Full width)             (Collapsed <300px)
    
    ---
    
    These wireframes provide a clear, simple structure for each page and the navigation menu, matching your requirements. If you want these added to your PRD or as a separate markdown file, let me know!

    ```

    > **NOTE**: There are many other ways to create wireframe diagrams. For an AI-based, vibe coding type of approach, you can use Microsoft's M365 Copilot. Just provide M365 Copilot with a description of your app (the contents of your PRD) and ask the AI to create images of low-fidelity wireframe diagrams. For high-fidelity wireframe diagrams that you create manually, you can use a UI/UX design tool such as Figma.

1. In the Chat view, enter the following prompt:

    ```markdown
    Save the low-fidelity wireframe diagrams as text files.
    ```

1. Monitor the Chat view to ensure that all of the files are saved, and then select **Keep**.

1. Take a couple minutes to review the wireframe diagrams.

    If you see any obvious issues that you want to correct, you can edit the wireframe diagrams directly in Visual Studio Code. You can also ask GitHub Copilot to help you refine the wireframe diagrams.

    For this exercise, your wireframe diagrams (text layouts) don't need to be exact, and the suggested wireframes should be sufficient without modification. However, if you experience issues later in the exercise that you attribute to the wireframe diagrams, you can ask GitHub Copilot Agent to help you refine them.

    > **Tip**: If you're unsure how to interpret a wireframe diagram, or if you think one of the diagrams may be incorrect, ask GitHub Copilot to explain the diagram(s). For example, you can ask GitHub Copilot Agent to "review the wireframe diagrams and use them to explain the layout of the user interface and how the user interacts with the app." If GitHub Copilot's explanation doesn't match your expectations, you can ask GitHub Copilot Agent for help updating the wireframe diagrams to better match your intended user experience.

## Create an initial prototype app

GitHub Copilot Agent can use product requirements and wireframe diagrams to develop a prototype application. Providing sufficiently detailed product requirements and wireframe diagrams helps the agent understand the user experience, app features, and design goals that you intend for your app.

- The PRD provides detailed information about the app's purpose, target audience, features, and technical requirements.
- The wireframe diagrams show the intended user interface and help to describe the user interactions.

In this task, you use GitHub Copilot Agent to create an initial prototype app based on the PRD and wireframe diagrams that you created.

Use the following steps to complete this section of the exercise:

1. In Visual Studio Code, create a new folder named **ShoppingApp** in the VibeCoding-PrototypeApp folder.

    GitHub Copilot Agent needs an empty folder to use as a workspace for the new the app files.

    The EXPLORER view in Visual Studio Code should look similar to the following:

    ```plaintext

    UNTITLED (WORKSPACE)
    └── VibeCoding-PrototypeApp
        ├── ShoppingApp
        ├── VibeCodingPRD.md
        ├── wireframe-checkout.txt
        ├── wireframe-navigation.txt
        ├── wireframe-product-details.txt
        ├── wireframe-products.txt
        └── wireframe-shopping-cart.txt

    ```

1. Add the PRD and wireframe diagrams to the chat context.

    Adding these files to the chat context tells GitHub Copilot Agent to reference them when generating a response.

    You can add files to the chat context by dragging and dropping them from the EXPLORER view onto the Chat view, or by using the **Add Context** button located in the bottom'left area of the Chat view.

1. In the EXPLORER view, select the **ShoppingApp** folder.

1. In the Chat view, enter the following prompt:

    ```markdown
    I want you to create a prototype shopping app using the information in my PRD and wireframe diagrams. Create my prototype app in the selected 'ShoppingApp' folder. Add a '.github/copilot-instructions.md' file to the workspace. Add the contents of the PRD and wireframe files to the 'copilot-instructions.md' file. Use the information in 'copilot-instructions.md' to create a prototype shopping app. The prototype should implement the following: basic use case functionality, simple navigation, a sample dataset, and basic styling.
    ```

    GitHub Copilot Agent uses this prompt to generate an initial prototype app based on the requirements that you've defined.

    - The agent creates a copilot-instructions.md file that contains your product requirements and wireframe diagrams. Visual Studio Code automatically includes the instructions from the .github/copilot-instructions.md file in every chat request and applies them for generating code.
    - The agent uses the copilot-instructions.md file to create an HTML file, a CSS file, and a JavaScript file in the **ShoppingApp** folder.

    > **TIP**: You can store custom instructions in your workspace or repository in a .github/copilot-instructions.md file. Custom instructions enable you to describe common guidelines or rules to get responses that match your specific coding practices and tech stack. Instead of manually including this context in every chat query, custom instructions automatically incorporate this information with every chat request. These instructions only apply to the workspace where the file is located.

1. Monitor the Chat view to track the agent's progress as it works on your prototype app.

    > **NOTE**: Although GitHub Copilot Agent performs tasks as an autonomous agent, it may ask for assistance when performing certain tasks. To assist the agent, respond to any prompts that appear in the Chat view. For example, if the agent asks for permission to run a command in the terminal, select **Run** to allow the agent to run the command. If the agent asks for clarification about your requirements, provide a response that helps the agent understand your requirements.

1. In the Chat view, to save the prototype app files, select **Keep**.

1. Expand the **ShoppingApp** folder.

    The folder should contain the following files:

    ShoppingApp
    ├── .github
    │   └── copilot-instructions.md
    ├── app.js
    ├── index.html
    ├── styles.css

1. Take a couple minutes to review each of the code files.

    - The **index.html** file serves as the entry point for a web application. It sets up the basic structure of the app and links the styles and scripts files.
    - The **styles.css** file provides the visual layout and responsive design for your prototype web app.
    - The **app.js** file contains the JavaScript code that manages the product catalog, shopping cart, navigation, and UI rendering.

    If time permits, consider asking GitHub Copilot to generate a detailed explanation of each file.

1. Use Windows File Explorer to open the **index.html** file in a browser.

    You can double-click the **index.html** file from the Windows File Explorer to open it in your default web browser. Alternatively, you can right-click the file and select **Open with** to choose a specific browser.

1. With your prototype app open in the browser, test the use cases you listed in your product requirements and verify that your prototype app delivers the expected functionality.

    The uses cases should describe basic functionality that your prototype app should implement. For example:

    - As a user, I can browse a list of fruit products.
    - As a user, I can view detailed information about a selected product.
    - As a user, I can add products to my shopping cart and adjust quantities.
    - As a user, I can review and update my cart before checkout.
    - As a user, I can view a summary of my order and "process" it (no real transaction).
    - As a user, I can navigate between the Products, ProductDetails, ShoppingCart, and Checkout pages.

1. After verifying the use cases, test the dynamic behavior of the app by resizing the browser window.

    The prototype app should have a dynamic user interface that automatically scales to accommodate viewing on desktop and phone devices.

1. Try to test the collapsed navigation menu.

    You specified that the navigation menu should collapse when the page width drops below 300 pixels. The collapsed navigation menu should display one or two letters to represent each of the web pages in the app.

    > **NOTE**: Most desktop browsers (including Microsoft Edge) enforce a minimum window width that's greater than 300px (often around 320–400px). This means you may not be able to manually resize the browser window small enough to trigger the collapse of the navigation bar.

1. (Optional) Perform additional testing to ensure that the prototype app meets your expectations.

    If you want, take notes during your testing. You can use your notes during the next task to help refine your prototype app.

## Update the prototype app

Your initial prototype app should already provide a basic implementation of the product requirements. However, it can probably be refined and improved, and it may not fully achieve the intended user experience and functionality.

In this task, you use GitHub Copilot Agent to refine the features and behavior of your prototype app.

Use the following steps to complete this section of the exercise:

1. In the Chat view, to adjust the breakpoint for the collapsed navigation bar, enter the following prompt:

    ```markdown
    #codebase Refactor the prototype app to use a higher breakpoint for the collapsed navigation bar. Change from 300 to 600px. Update the copilot-instructions.md file to explain the updated 600px requirement.
    ```

1. In the Chat view, select **Keep** to save the updated prototype app files.

1. Open the **index.html** file in the browser, and then ensure that the navigation menu collapses when the width is below 600 pixels.

1. Switch back to Visual Studio Code.

1. In the Chat view, enter the following prompt:

    ```markdown
    #codebase Update the prototype app to display an emoji in the nav bar for each of the web pages. Ensure that the icon is centered horizontally in the nav bar when the nav bar is collapsed. Update the copilot-instructions.md file to include this product requirement.
    ```

1. In the Chat view, select **Keep** to save the updated prototype app files.

1. Open the **index.html** file in the browser and test the updated navigation bar.

1. Switch back to Visual Studio Code.

1. In the Chat view, to identify additional opportunities for improvements, enter the following prompt:

    ```markdown
    #codebase Review the product requirements and wireframe diagrams in the copilot-instructions.md file. Are there any features or requirements that are missing from the implementation? Are there obvious opportunities to improve the user experience?
    ```

1. Review the response from GitHub Copilot Agent.

    Identify three or more suggested improvements that you'ld like to implement.

1. Create a prompt that describes the improvements that you want to implement.

    Use GitHub Copilot's suggestions, and any testing notes that you created, to implement improvements. For example, you can ask GitHub Copilot Agent to help you implement the following changes:

    ```markdown
    #codebase Implement the following improvements to the prototype app:

    - Replace alert() popups with in-app notification banners or toasts.
    - Add a confirmation/thank you message after processing an order.
    - Add a visual indicator (badge) for the number of items in the cart on the nav bar.

    Ensure that the copilot-instructions.md file is updated to reflect any changes to the product features, technical requirements, user experience, or other measurable characteristic.

    ```

    > **TIP**: You can copy information from GitHub Copilot's response to build your new prompt. You can also refer to sections of the response in your prompt.

## Summary

In this exercise, you learned how to use GitHub Copilot Agent to create a prototype app using a vibe coding process. You defined product requirements, created an initial prototype app, and refined the prototype app to better meet the intended user experience and functionality.

## Clean up

Now that you've finished the exercise, take a minute to ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep. If you made any changes, revert them now. If you're using a local PC as your lab environment, you can archive or delete the prototype app folder that you created for this exercise.
