---
Sample Product Requirements Document (PRD) Templates
---

**Template #1: Product Requirements Document (PRD) Template with examples**

    ```md
    # Product Requirements Document (PRD) Template for GitHub Copilot Agent Workflows
    
    ## 1. Project Summary
    ### Instructions:
    Provide a brief overview of the product, including its purpose, target audience, and key goals.
    
    ### Example:
    **Product:** Daily Mood Tracker Web App
    **Purpose:** To help users log their daily mood, view mood trends, and optionally share mood data with a therapist.
    **Target Audience:** Individuals seeking to track their mental health and therapists who monitor their patients' mood patterns.
    **Goals:** 
    - Enable users to log their mood quickly and easily.
    - Provide visualizations of mood trends over time.
    - Allow users to share mood data with their therapist.
    
    ## 2. Problem Overview
    ### Instructions:
    Describe the current pain points or inefficiencies that the product aims to address. Include assumptions and constraints.
    
    ### Example:
    **Current Pain Points:**
    - Users lack a simple and effective way to track their mood daily.
    - Therapists need a reliable method to monitor their patients' mood patterns remotely.
    **Assumptions:**
    - Users have access to a smartphone or computer.
    - Therapists are willing to use digital tools for monitoring.
    **Constraints:**
    - The app must be user-friendly and accessible.
    - Data privacy and security must be ensured.
    
    ## 3. Scope
    ### Instructions:
    Define what is in scope and what is explicitly out of scope for the MVP. 
    
    ### Example:
    **In Scope:**
    - Mood logging functionality.
    - Mood trend visualizations.
    - Data sharing with therapists.
    **Out of Scope:**
    - Advanced mood analysis algorithms.
    - Integration with other health tracking apps.
    
    ## 4. Use Cases & Scenarios
    ### Instructions:
    Provide real-world examples of how users will interact with the product. Include user personas and workflows.
    
    ### Example:
    **Use Case 1:** 
    - **Persona:** Jane, a 30-year-old professional experiencing stress.
    - **Scenario:** Jane logs her mood daily using the app and views her mood trends to identify patterns.
    **Use Case 2:** 
    - **Persona:** Dr. Smith, a therapist.
    - **Scenario:** Dr. Smith reviews mood data shared by his patients to monitor their progress.
    
    ## 5. Requirements
    ### Instructions:
    List the functional and non-functional requirements. Include user stories and acceptance criteria. Add mockups or wireframes if available.
    
    ### Example:
    **Functional Requirements:**
    - **User Story:** As a user, I want to log my mood daily so that I can track my mental health.
    - **Acceptance Criteria:** The mood logging feature should allow users to select their mood from predefined options and add notes.
    **Non-functional Requirements:**
    - The app should be responsive and work on both mobile and desktop devices.
    - Data should be encrypted to ensure privacy.
    
    ## 6. Dependencies
    ### Instructions:
    Identify cross-team or cross-system dependencies. List required technologies, APIs, or services.
    
    ### Example:
    **Dependencies:**
    - Integration with a secure database for storing mood data.
    - Use of charting libraries for visualizing mood trends.
    
    ## 7. Success Metrics
    ### Instructions:
    Define how success will be measured. Include adoption, performance, and satisfaction metrics.
    
    ### Example:
    **Success Metrics:**
    - Number of daily active users.
    - User satisfaction ratings.
    - Therapist adoption rate.
    
    ## 8. Competitive Analysis
    ### Instructions:
    Provide an overview of similar products in the market. Highlight strengths, weaknesses, and differentiators.
    
    ### Example:
    **Competitive Analysis:**
    - **Product A:** Offers mood tracking but lacks data sharing with therapists.
    - **Product B:** Provides advanced mood analysis but is complex to use.
    **Differentiators:** Our app focuses on simplicity and therapist integration.
    
    ## 9. Product Roadmap
    ### Instructions:
    Outline the timeline for MVP, V1.0, V2.0, etc. Include preview phases.
    
    ### Example:
    **Product Roadmap:**
    - **MVP:** Mood logging, trend visualizations, data sharing (Q1 2023)
    - **V1.0:** Enhanced visualizations, user feedback integration (Q2 2023)
    - **V2.0:** Advanced mood analysis, integration with health apps (Q3 2023)
    
    ## 10. Risks & Challenges
    ### Instructions:
    Identify technical, legal, operational, or market risks. Provide mitigation strategies.
    
    ### Example:
    **Risks & Challenges:**
    - **Technical Risk:** Ensuring data privacy and security.
    - **Mitigation:** Implement robust encryption and security protocols.
    - **Market Risk:** User adoption.
    - **Mitigation:** Conduct user testing and iterate based on feedback.
    
    ## 11. Open Questions
    ### Instructions:
    List unresolved issues or decisions pending input.
    
    ### Example:
    **Open Questions:**
    - Should the app include mood prediction features?
    - What additional mood tracking options should be provided?
    
    ## 12. Supporting Documentation
    ### Instructions:
    Provide links to research, design docs, or related specs.
    
    ### Example:
    **Supporting Documentation:**
    - [User Research Report](link)
    - [Design Mockups](link)
    
    ## 13. Sign-Off
    ### Instructions:
    Include stakeholder approvals and version history.
    
    ### Example:
    **Sign-Off:**
    - **Version 1.0:** Approved by Product Manager, Lead Developer, and UX Designer.
    
    ```

**Sample 1** - Pet Adoption Web App Product Requirements Document (PRD)

    ```
    # Product Requirements Document (PRD) Template
    
    ## Executive Summary
    ### What is the product?
    The product is a web application for pet adoption. It supports users who want to give up their pets for adoption and users who want to adopt pets. The app allows users to browse available pets without logging in, but requires an account and authentication for adopting or donating pets.
    
    ### What problem does it solve?
    The app addresses the need for a streamlined and user-friendly platform for pet adoption, making it easier for pet owners to find new homes for their pets and for potential adopters to find pets that match their preferences.
    
    ### Who are the users and what are their goals?
    - **Pet Owners**: Users who need to give up their pets for adoption.
    - **Potential Adopters**: Users who are looking to adopt a pet.
    - **General Users**: Users who want to browse available pets without logging in.
    
    ## Problem Overview
    ### Description of the current pain points or inefficiencies
    Pet adoption processes can be cumbersome and inefficient, with limited online platforms that provide comprehensive information about pets available for adoption. Users often struggle to find detailed information about pets, including their medical history and care requirements.
    
    ### Assumptions and constraints
    - Only pets that can be found in a pet store (dogs, cats, hamsters, snakes, turtles) are accepted.
    - The business does not support fish or birds.
    - Users must provide credentials to donate or adopt pets.
    - Users can browse pets without logging in.
    
    ### Current vs. future state comparison
    - **Current State**: Limited online platforms with incomplete information about pets.
    - **Future State**: A comprehensive web app with detailed pet listings, user authentication, and streamlined adoption processes.
    
    ## Scope
    ### What’s in scope and what’s explicitly out of scope
    #### In Scope
    - User authentication for donating and adopting pets.
    - Browsing pets without logging in.
    - Detailed pet listings including statistics, history with previous owner, medical history, care requirements, and images.
    - Support for pets commonly found in pet stores (dogs, cats, hamsters, snakes, turtles).
    
    #### Out of Scope
    - Support for fish or birds.
    - Advanced features such as pet training or veterinary services.
    
    ### MVP definition: the smallest set of features that deliver value
    - User authentication for donating and adopting pets.
    - Browsing pets without logging in.
    - Detailed pet listings with basic statistics, history, medical info, care needs, and images.
    
    ## Use Cases & Scenarios
    ### Real-world examples of how users will interact with the product
    #### Use Case 1: Browsing Pets
    - **Scenario**: A user visits the app to browse available pets without logging in.
    - **Steps**:
      1. User navigates to the homepage.
      2. User selects the "Browse Pets" option.
      3. User views a list of available pets with basic information and images.
    
    #### Use Case 2: Donating a Pet
    - **Scenario**: A pet owner wants to give up their pet for adoption.
    - **Steps**:
      1. User logs in or creates an account.
      2. User selects the "Donate a Pet" option.
      3. User fills out a form with pet details (statistics, history, medical info, care needs, images).
      4. User submits the form, and the pet listing is created.
    
    #### Use Case 3: Adopting a Pet
    - **Scenario**: A potential adopter wants to adopt a pet.
    - **Steps**:
      1. User logs in or creates an account.
      2. User browses available pets and selects a pet for adoption.
      3. User fills out an adoption form and submits it.
      4. User receives confirmation and instructions for completing the adoption process.
    
    ## Requirements
    ### Functional Requirements: user stories and acceptance criteria
    #### User Story 1: As a user, I want to browse available pets without logging in.
    - **Acceptance Criteria**:
      - Users can view a list of available pets with basic information and images.
      - Users can filter pets by type (dog, cat, hamster, snake, turtle).
    
    #### User Story 2: As a pet owner, I want to donate my pet for adoption.
    - **Acceptance Criteria**:
      - Users must log in or create an account to donate a pet.
      - Users can fill out a form with pet details (statistics, history, medical info, care needs, images).
      - The pet listing is created and visible to other users.
    
    #### User Story 3: As a potential adopter, I want to adopt a pet.
    - **Acceptance Criteria**:
      - Users must log in or create an account to adopt a pet.
      - Users can fill out an adoption form and submit it.
      - Users receive confirmation and instructions for completing the adoption process.
    
    ### Non-functional Requirements: performance, scalability, security, etc.
    - The app must handle concurrent users efficiently.
    - User data must be securely stored and transmitted.
    - The app must be responsive and accessible on various devices.
    
    ## Dependencies
    ### Cross-team or cross-system dependencies
    - Integration with authentication services (e.g., OAuth).
    - Integration with image storage services (e.g., AWS S3).
    - Dependencies on frontend and backend frameworks (e.g., React, Node.js).
    
    ## Success Metrics
    ### How will success be measured? (e.g., adoption, performance, satisfaction)
    - **Adoption**: Number of registered users and active users.
    - **Performance**: Page load times and server response times.
    - **Satisfaction**: User feedback and ratings.
    
    ### ROI or impact metrics
    - **Adoption Rate**: Percentage of users who complete the donation or adoption process.
    - **User Engagement**: Average time spent browsing pets.
    
    ## Competitive Analysis
    ### Overview of similar products in the market
    - Comparison of features, strengths, and weaknesses of existing pet adoption platforms.
    
    ## Product Roadmap
    ### Timeline for MVP, V1.0, V2.0, etc.
    - **MVP**: Basic pet browsing, donation, and adoption features.
    - **V1.0**: Enhanced pet listings, user profiles, and search functionality.
    - **V2.0**: Advanced features such as pet training resources and veterinary services.
    
    ### Preview phases (dogfood, private/public preview)
    - **Dogfood**: Internal testing with team members.
    - **Private Preview**: Limited release to selected users.
    - **Public Preview**: Open release to all users.
    
    ## Risks & Challenges
    ### Technical, legal, operational, or market risks
    - **Technical Risks**: Scalability and performance issues.
    - **Legal Risks**: Compliance with data protection regulations.
    - **Operational Risks**: Ensuring user adoption and engagement.
    
    ### Mitigation strategies
    - Implement robust testing and monitoring.
    - Ensure compliance with legal requirements.
    - Develop user engagement strategies.
    
    ## Open Questions
    ### Unresolved issues or decisions pending input
    - What additional pet types should be considered for future versions?
    - How can we enhance user engagement and satisfaction?
    
    ## Supporting Documentation
    ### Links to research, design docs, or related specs
    - Research on pet adoption trends and user preferences.
    - Design mockups and wireframes.
    
    ## Sign-Off
    ### Stakeholder approvals and version history
    - Approval from product manager, development team, and key stakeholders.
    - Version history and change log.
    ```




**SAMPLE 2** - from Wendy on the DevDiv team (Copilot Custom Instructions for MyTodoListApp)

    ```
    ## Project Overview

    This workspace contains a .NET Aspire solution for a Todo List application, including:
    
    - **MyTodoApi**: Minimal API backend for todo items, using Entity Framework Core and SQLite.  
    - **MyNewTodoListApp**: Blazor WebAssembly frontend for managing todo items, consuming the API.  
    - **BlazorApp1**: Additional Blazor app (sample or test).  
    - **Aspire.AppHost**: Orchestrator for running the solution locally with Aspire.  
    - **Aspire.ServiceDefaults**: Shared service configuration for Aspire projects.
    
    ## Key Technologies  
    
    - .NET Aspire (for orchestration and service defaults)  
    - Blazor WebAssembly (frontend)  
    - ASP.NET Core Minimal API (backend)  
    - Entity Framework Core with SQLite (data persistence)  
    
    ## Main Features  
    
    - CRUD operations for todo items  
    - API endpoints for todo management  
    - Blazor UI for interacting with todos  
    - Local development orchestration with Aspire  
    
    ## File/Folder Highlights  
    
    - **MyTodoApi/**: API project, contains `Program.cs`, `TodoStore.cs`, `ApplicationDbContext.cs`, and `Models/`.  
    - **MyNewTodoListApp/**: Blazor WASM frontend, contains `Program.cs`, `ToDoClient.cs`, `Pages/`, and `Shared/`.  
    - **Aspire.AppHost/**: Aspire orchestration, contains `Program.cs` and `appsettings.json`.  
    
    ## Coding/Response Guidelines  
    
    - Prefer .NET 9 idioms and minimal API patterns.  
    - Use dependency injection and configuration best practices.  
    - For Blazor, use component-based design and leverage `@inject` for services.  
    - When suggesting code, reference the relevant project and file.  
    - When discussing API endpoints, refer to **MyTodoApi**.  
    - When discussing UI, refer to **MyNewTodoListApp**.  
    - For orchestration or service config, refer to **Aspire.AppHost** or **Aspire.ServiceDefaults**.
    
    ## Example Use Cases  
    
    - Add a new todo item via the Blazor frontend.  
    - Retrieve all todos from the API.  
    - Update or delete a todo item.  
    - Run the solution locally using Aspire.
    ```

**SAMPLE 3** - for MyMoodTrackerApp

    ```
    
    ## Project Overview
    
    Daily Mood Tracker: A web application for users to log their daily mood and view mood trends.

    Scenario: A wellness startup wants a prototype for users to log their mood and see trends.

    ## Key Features
    - User authentication (optional)
    - Mood logging
        - Mood selection UI (e.g., emojis or dropdown)
        - Text input for notes
        - Submit entries (tagged with entry date and time)
    - Mood trend visualization 
        - e.g., charts based on date range, weekday, or time of day
        - View past entries
    - Local storage - Entity Framework Core with SQLite (data persistence)
    - CRUD operations for mood entries

    ## Project Structure

    - MyMoodTrackerApi/
        - Minimal API backend for mood entries
        - Entity Framework Core with SQLite
        - Contains Program.cs, MoodStore.cs, ApplicationDbContext.cs, Models/
        - Endpoints for mood entry management
    - MyMoodTrackerApp/
        - Blazor WebAssembly frontend for mood logging and visualization
        - Contains Program.cs, MoodClient.cs, Pages/, Shared/
        - UI for mood entry submission and trend visualization
        - Uses HttpClient to interact with the API

    ```

**SAMPLE 4** - for MyMoodTrackerApp

    ```md

    1. Project Summary
    
        Product: Daily Mood Tracker Web App  
    
        Purpose: To help users log their daily mood, view mood trends.  
        Target Audience: Individuals seeking to track their mental health.  
    
        Goals:
    
        - Enable users to log their mood quickly and easily.
        - Provide visualizations of mood trends over time.
    
    2. Problem Overview
    
        Current Pain Points:
    
        - Users lack a simple and effective way to track their mood daily.
    
        Assumptions:
    
        - Users have access to a smartphone or computer.
    
        Constraints:
    
        - The app must be user-friendly and accessible.
        - Data privacy and security must be ensured.
    
    3. Scope
    
        In Scope:
    
        - Mood logging functionality.
        - Mood trend visualizations.
    
        Out of Scope:
    
        - Data sharing with therapists.
        - Advanced mood analysis algorithms.
        - Integration with other health tracking apps.
    
    4. Use Cases & Scenarios
    
        Use Case 1:
    
        - Persona: A 30-year-old professional experiencing stress.
        - Scenario: The user logs their mood daily using the app and views their mood trends to identify patterns.
    
    5. Requirements
    
        Functional Requirements:
    
        - User Story: As a user, I want to log my mood one or more times daily so that I can track my mental health.
        - Acceptance Criteria: The mood logging feature should allow users to select their mood from predefined options and add notes. The app should store the date/time stamp with notes for charting.
    
        Non-functional Requirements:
    
        - The app should be responsive and work on both mobile and desktop devices.
        - Data should be encrypted to ensure privacy.
    
    6. Dependencies
    
        Dependencies:
    
        - Integration with a secure SQLite database for storing mood data.
        - Use of charting libraries for visualizing mood trends.
    
    7. Success Metrics
    
        Success Metrics:
    
        - Number of daily active users.
        - User satisfaction ratings.
    
    8. Competitive Analysis
    
        Competitive Analysis:
    
        - Product A: Offers mood entry but poor tracking.
        - Product B: Provides advanced mood analysis but is complex to use.
    
        Differentiators: Our app focuses on simplicity and mood analysis tools.
    
    9. Product Roadmap
    
        Product Roadmap:
    
        - MVP: Mood logging, trend visualizations (Q1 2023)
        - V1.0: Enhanced visualizations, user feedback integration (Q2 2023)
        - V2.0: Advanced mood analysis, integration with health apps (Q3 2023)
    
    10. Risks & Challenges
    
        Risks & Challenges:
    
        - Technical Risk: Ensuring data privacy and security.
        - Mitigation: Implement robust encryption and security protocols.
        - Market Risk: User adoption.
        - Mitigation: Conduct user testing and iterate based on feedback.
    
    11. Open Questions
    
        Open Questions:
    
        - Should the app include mood prediction features?
        - What additional mood tracking options should be provided?
    
    12. Supporting Documentation

    ```
