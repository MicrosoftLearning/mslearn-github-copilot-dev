<!-- ---
lab:
    title: 'Exercise - Integrate an AI Agent into existing apps using GitHub Copilot SDK'
    description: 'Learn how to integrate an AI Agent into existing applications using GitHub Copilot SDK to automate tasks and enhance functionality.'
--- -->

# Integrate an AI Agent into existing apps using GitHub Copilot SDK

The GitHub Copilot SDK exposes the same engine behind GitHub Copilot CLI as a programmable SDK. It allows you to embed agentic AI workflows in your applications — including custom tools that let the AI call your code.

In this exercise, you integrate an AI-powered customer support agent into the ContosoShop E-commerce Support Portal. By the end, the "Contact Support" page will allow a user to ask questions (for example, "Where is my order?" or "I need to return an item") and receive helpful, automated answers from an AI agent. The agent uses backend tools (like checking order status or initiating a return) to resolve queries.

This exercise should take approximately **60** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment MUST include the following resources:

- Git 2.48 or later
- .NET SDK 8.0 or later
- Visual Studio Code with the C# Dev Kit and GitHub Copilot Chat extensions
- GitHub Copilot CLI installed and in your PATH






**TODO: Update with instructions for installing GitHub Copilot CLI if not present in the environment.**
For help with configuring your lab environment, open the following link in a browser: <a href="https://go.microsoft.com/fwlink/?linkid=2345907" target="_blank">Configure your GitHub Copilot SDK lab environment</a>.






## Exercise scenario

You're a software developer working for a consulting firm. The firm developed the ContosoShop E-commerce Support Portal (a Blazor WebAssembly application with an ASP.NET Core backend) for a client named Contoso Corporation. The application includes order management, item returns, and inventory tracking. Contoso needs you to enhance the existing "Contact Support" page with an AI-powered customer support agent that can look up order details and initiate returns on behalf of customers.

The ContosoShop application uses a three-project architecture:

- **ContosoShop.Server**: ASP.NET Core Web API with Entity Framework Core, Identity authentication, and SQLite.
- **ContosoShop.Client**: Blazor WebAssembly SPA that runs in the browser and calls the server API.
- **ContosoShop.Shared**: Shared class library containing models, DTOs, and enums.

For the purposes of this lab, the application can be tested using two demo users (Mateo Gomez and Megan Bowen) with 20 sample orders across various statuses (Processing, Shipped, Delivered, and Returned).

This exercise includes the following tasks:

1. Review the starter application and verify that it runs correctly.
1. Add the GitHub Copilot SDK NuGet package and create the agent tools service.
1. Configure the Copilot SDK agent and expose an API endpoint.
1. Update the Blazor frontend to interact with the agent.
1. Test the end-to-end AI agent experience.

## Task 1: Review the starter application and verify it runs correctly

Before integrating the AI agent, you need to become familiar with the existing codebase and ensure the application runs correctly.

Use the following steps to complete this task:

1. Open a browser window and navigate to GitHub.com.

    You can log in to your GitHub account using the following URL: <a href="https://github.com/login" target="_blank">GitHub login</a>.

1. Sign in to your GitHub account, and then open your repositories tab.

    You can open your repositories tab by clicking on your profile icon in the top-right corner, then selecting **Repositories**.

1. On the Repositories tab, select the **New** button.

1. Under the **Create a new repository** section, select **Import a repository**.

1. On the **Import your project to GitHub** page, under **Your source repository details**, enter the following URL for the source repository:

    ```plaintext
    https://github.com/MicrosoftLearning/github-copilot-sdk-starter-app
    ```

1. Under the **Your new repository details** section, in the **Owner** dropdown, select your GitHub username.

1. In the **Repository name** field, enter **ContosoShop**

    GitHub automatically checks the availability of the repository name. If this name is already taken, append a unique suffix (for example, your initials or a random number) to the repository name to make it unique.

1. To create a private repository, select **Private**, and then select **Begin import**.

    GitHub uses the import process to create the new repository in your account.

    > **NOTE**: It can take a minute or two for the import process to finish. Wait for the import process to complete.

    GitHub displays a progress indicator and notify you when the import is complete.

1. Once the import is complete, open your new repository.

    A link to your repository should be displayed. Your repository should be located at: `https://github.com/YOUR-USERNAME/ContosoShop`.

    You can create a local clone of your ContosoShop repository and then initialize GitHub Spec Kit within the project directory.

1. On your ContosoShop repository page, select the **Code** button, and then copy the HTTPS URL.

    The URL should be similar to: `https://github.com/YOUR-USERNAME/ContosoShop.git`

1. Open a terminal window in your development environment, and then navigate to the location where you want to create the local clone of the repository.

    For example:

    Open a terminal window (Command Prompt, PowerShell, or Terminal), and then run:

    ```powershell
    cd C:\TrainingProjects
    ```

    Replace `C:\TrainingProjects` with your preferred location. You can use any directory where you have write permissions, and you can create a new folder location if needed.

1. To clone your ContosoShop repository, enter the following command:

    Be sure to replace `YOUR-USERNAME` with your actual GitHub username before running the command.

    ```powershell
    git clone https://github.com/YOUR-USERNAME/ContosoShop.git
    ```

    You might be prompted to authenticate using your GitHub credentials during the clone operation. You can authenticate using your browser.

1. To navigate into your ContosoShop directory and open it in Visual Studio Code, enter the following commands:

    ```powershell
    cd ContosoShop
    code .
    ```

1. Take a few minutes to review the project structure.

    Use Visual Studio Code's EXPLORER view to expand the project folders. You should see a folder structure that's similar to the following example:

    ```plaintext
    github-copilot-sdk-starter-app (root)
    ├── ContosoShop.Client/               (Blazor WebAssembly frontend)
    │   ├── Layout/                       (MainLayout, NavMenu)
    │   ├── Pages/                        (Home, Login, Orders, OrderDetails, Support, Inventory)
    │   ├── Services/                     (OrderService, CookieAuthenticationStateProvider)
    │   └── Shared/                       (OrderStatusBadge)
    ├── ContosoShop.Server/               (ASP.NET Core backend)
    │   ├── App_Data/                     (used for the SQLite database file)
    │   ├── Controllers/                  (AuthController, OrdersController, InventoryController)
    │   ├── Data/                         (ContosoContext, DbInitializer, Migrations)
    │   ├── Services/                     (OrderService, InventoryService, EmailServiceDev)
    │   └── Program.cs                    (App configuration and middleware)
    ├── ContosoShop.Shared/               (Shared class library)
    │   ├── Models/                       (Order, OrderItem, Product, User, etc.)
    │   └── DTOs/                         (InventorySummary, ReturnItemRequest)
    └── ContosoShopSupportPortal.slnx     (Solution file)
    ```

    > **NOTE**: Ensure that the App_Data folder in the ContosoShop.Server project is included in your local clone, as it is required for the SQLite database. If you don't see the App_Data folder, create it manually. The application will create the SQLite database file in this folder when it runs the first time.

1. Open the **ContosoShop.Server/Program.cs** file and review the application configuration.

    Notice the following key configuration areas:

    - Entity Framework Core with SQLite for data access
    - ASP.NET Core Identity for authentication with cookie-based sessions
    - Service registrations for `IEmailService`, `IInventoryService`, and `IOrderService`
    - Database seeding via `DbInitializer.InitializeAsync` at startup
    - CORS, rate limiting, CSRF protection, and security headers middleware

1. Open the **ContosoShop.Server/Controllers/OrdersController.cs** file and note the existing API endpoints.

    The orders controller provides:

    - `GetOrders` — Gets all orders for the authenticated user
    - `GetOrder` — Gets a specific order with items (verifies ownership)
    - `ReturnOrderItems` — Processes item-level returns for a delivered order

1. Open the **ContosoShop.Server/Services/OrderService.cs** file and review the `ProcessItemReturnAsync` method.

    This existing method validates that an order is in Delivered or (partially) Returned status, creates `OrderItemReturn` records, updates inventory, recalculates order status, and sends email confirmation. The AI agent you build will leverage similar logic.

1. Open the **ContosoShop.Client/Pages/Support.razor** file.

    Notice that the Support page currently displays static contact information and an "AI Chat Support Coming Soon" placeholder. This is where you will add the interactive AI chat interface.

1. Open a terminal in the **ContosoShop.Server** directory and build the solution.

    ```powershell
    cd ContosoShop.Server
    dotnet build
    ```

    > **IMPORTANT**: The project uses .NET 8 by default. If you have a later version of the .NET SDK installed (.NET 9 or .NET 10), but not .NET 8, you need to update the project to target the installed version. To update to a later version of .NET, open the GitHub Copilot Chat view and ask GitHub Copilot to update your project files to the version of .NET that you have installed in your environment. For example, you can ask: "I need you to update the project to target .NET 10. Be sure to update all related resources such as NuGet packages and project references. After completing all required updates, ensure that all projects build successfully." The AI assistant will help update your solution.

    The build should complete successfully without errors (there might be some warnings).

1. Start the server application.

    ```powershell
    dotnet run
    ```

    The server starts listening on `http://localhost:5266`.

1. Open a browser and navigate to `http://localhost:5266`.

    You should see the ContosoShop login page. Accept any certificate warnings for the localhost development certificate.

1. Sign in using the demo credentials.

    Enter `mateo@contoso.com` for the email and `Password123!` for the password, and then select **Login**.

1. Navigate to the **Orders** page and verify that orders are displayed.

    You should see 10 orders for Mateo with various statuses (Delivered, Shipped, Processing, Returned).

1. Navigate to the **Contact Support** page.

    You should see the "Interactive AI Chat Support Coming Soon" placeholder. This is the page you will enhance in subsequent tasks.

1. On the navigation menu, select **Logout**.

1. Return to the terminal where the server is running and press **Ctrl+C** to stop the application.

1. Verify that the GitHub Copilot CLI is installed and authenticated.

    ```powershell
    copilot --version
    ```

    You should see a version number (for example, `0.0.399`). If the command is not found, install the Copilot CLI by following the <a href="https://docs.github.com/en/copilot/how-tos/set-up/install-copilot-cli" target="_blank">Copilot CLI installation guide</a>.

    > **NOTE**: The GitHub Copilot SDK communicates with the Copilot CLI in server mode. The SDK manages the CLI process lifecycle automatically, but the CLI must be installed and accessible in your PATH.

## Task 2: Add the GitHub Copilot SDK and create the agent tools service

In this task, you add the GitHub Copilot SDK NuGet package to the server project and create a service class that implements the tools the AI agent will use to look up orders and process returns.

Use the following steps to complete this task:

1. Open Visual Studio Code's integrated terminal, and then navigate to the **ContosoShop.Server** directory.

1. To add the GitHub Copilot SDK NuGet package, enter the following command:

    ```powershell
    dotnet add package GitHub.Copilot.SDK --prerelease
    ```

    This installs the latest preview version of the SDK. The SDK provides `CopilotClient`, `CopilotSession`, and related types for building AI agents.

    > **NOTE**: While the GitHub Copilot SDK is in Technical Preview, the `--prerelease` flag is required to install it.

1. To add the `Microsoft.Extensions.AI` package, enter the following command:

    ```powershell
    dotnet add package Microsoft.Extensions.AI
    ```

    The GitHub Copilot SDK uses `Microsoft.Extensions.AI` for defining custom tools. This package provides the `AIFunctionFactory` and related types for creating tools that the AI agent can call.

1. To verify the packages installed correctly, build the project:

    ```powershell
    dotnet build
    ```

    The build should succeed without errors.

1. In Visual Studio Code's EXPLORER view, right-click the **ContosoShop.Server/Services** folder, and then select **New File**.

    You'll use this file to create the SupportAgentTools service class.

1. Name the file **SupportAgentTools.cs**.

1. Add the following code to the **SupportAgentTools.cs** file:

    ```csharp
    using ContosoShop.Server.Data;
    using ContosoShop.Shared.Models;
    using ContosoShop.Shared.DTOs;
    using Microsoft.EntityFrameworkCore;

    namespace ContosoShop.Server.Services;

    /// <summary>
    /// Provides tool functions that the AI support agent can invoke
    /// to look up order information and process returns.
    /// </summary>
    public class SupportAgentTools
    {
        private readonly ContosoContext _context;
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;
        private readonly ILogger<SupportAgentTools> _logger;

        public SupportAgentTools(
            ContosoContext context,
            IOrderService orderService,
            IEmailService emailService,
            ILogger<SupportAgentTools> logger)
        {
            _context = context;
            _orderService = orderService;
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the status and details of a specific order by order ID.
        /// The AI agent calls this tool when a user asks about their order status.
        /// </summary>
        public async Task<string> GetOrderDetailsAsync(int orderId, int userId)
        {
            _logger.LogInformation("Agent tool invoked: GetOrderDetails for orderId {OrderId}, userId {UserId}", orderId, userId);

            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return $"I could not find order #{orderId} associated with your account. Please double-check the order number.";
            }

            var statusMessage = order.Status switch
            {
                OrderStatus.Processing => "is currently being processed and has not shipped yet",
                OrderStatus.Shipped => order.ShipDate.HasValue
                    ? $"was shipped on {order.ShipDate.Value:MMMM dd, yyyy} and is on its way"
                    : "has been shipped and is on its way",
                OrderStatus.Delivered => order.DeliveryDate.HasValue
                    ? $"was delivered on {order.DeliveryDate.Value:MMMM dd, yyyy}"
                    : "has been delivered",
                OrderStatus.Returned => "has been returned and a refund was issued",
                _ => "has an unknown status"
            };

            var itemSummary = string.Join(", ", order.Items.Select(i =>
                $"{i.ProductName} (qty: {i.Quantity}, ${i.Price:F2} each)"));

            return $"Order #{order.Id} {statusMessage}. " +
                   $"Order date: {order.OrderDate:MMMM dd, yyyy}. " +
                   $"Total: ${order.TotalAmount:F2}. " +
                   $"Items: {itemSummary}.";
        }

        /// <summary>
        /// Gets a summary of all orders for a given user.
        /// The AI agent calls this tool when a user asks about their orders
        /// without specifying a particular order number.
        /// </summary>
        public async Task<string> GetUserOrdersSummaryAsync(int userId)
        {
            _logger.LogInformation("Agent tool invoked: GetUserOrdersSummary for userId {UserId}", userId);

            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            if (!orders.Any())
            {
                return "You don't have any orders on file.";
            }

            var summaries = orders.Select(o =>
            {
                var status = o.Status switch
                {
                    OrderStatus.Processing => "Processing",
                    OrderStatus.Shipped => "Shipped",
                    OrderStatus.Delivered => "Delivered",
                    OrderStatus.Returned => "Returned",
                    _ => "Unknown"
                };
                return $"Order #{o.Id} - {status} - ${o.TotalAmount:F2} - Placed {o.OrderDate:MMM dd, yyyy}";
            });

            return $"You have {orders.Count} orders:\n" + string.Join("\n", summaries);
        }

        /// <summary>
        /// Processes a return for specific items in a delivered order.
        /// The AI agent calls this tool when a user wants to return items.
        /// </summary>
        public async Task<string> ProcessReturnAsync(int orderId, int userId)
        {
            _logger.LogInformation("Agent tool invoked: ProcessReturn for orderId {OrderId}, userId {UserId}", orderId, userId);

            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return $"I could not find order #{orderId} associated with your account.";
            }

            if (order.Status != OrderStatus.Delivered && order.Status != OrderStatus.Returned)
            {
                return order.Status switch
                {
                    OrderStatus.Processing => $"Order #{orderId} is still being processed and cannot be returned yet. It must be delivered first.",
                    OrderStatus.Shipped => $"Order #{orderId} is currently in transit and cannot be returned until it has been delivered.",
                    _ => $"Order #{orderId} has a status of {order.Status} and cannot be returned."
                };
            }

            // Build return items list for all unreturned items
            var returnItems = order.Items
                .Where(i => i.RemainingQuantity > 0)
                .Select(i => new ReturnItem
                {
                    OrderItemId = i.Id,
                    Quantity = i.RemainingQuantity,
                    Reason = "Customer requested return via AI support agent"
                })
                .ToList();

            if (!returnItems.Any())
            {
                return $"All items in order #{orderId} have already been returned.";
            }

            var success = await _orderService.ProcessItemReturnAsync(orderId, returnItems);

            if (!success)
            {
                return $"I was unable to process the return for order #{orderId}. Please contact our support team for assistance.";
            }

            var refundAmount = order.Items
                .Where(i => i.RemainingQuantity > 0)
                .Sum(i => i.Price * i.RemainingQuantity);

            return $"I've processed the return for order #{orderId}. " +
                   $"A refund of ${refundAmount:F2} will be issued to your original payment method within 5-7 business days. " +
                   $"You will receive a confirmation email shortly.";
        }

        /// <summary>
        /// Sends a follow-up email to the customer regarding their order.
        /// The AI agent calls this tool to send additional information by email.
        /// </summary>
        public async Task<string> SendCustomerEmailAsync(int orderId, int userId, string message)
        {
            _logger.LogInformation("Agent tool invoked: SendCustomerEmail for orderId {OrderId}", orderId);

            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return $"Could not find order #{orderId} to send an email about.";
            }

            // Get the user's email from Identity
            var user = await _context.Users.FindAsync(userId);
            var email = user?.Email ?? "customer@contoso.com";

            await _emailService.SendEmailAsync(email, $"Regarding your order #{orderId}", message);

            return $"I've sent an email to {email} with the details about order #{orderId}.";
        }
    }
    ```

1. Take a couple minutes to review the code in the `SupportAgentTools` class.

    This class provides AI support agent tools for customer service automation. Key features:

    Order Information Tools:

    - **GetOrderDetailsAsync**: Retrieves status, shipping dates, and item details for a specific order with natural language responses
    - **GetUserOrdersSummaryAsync**: Lists all orders for a user when they don't specify an order number

    Order Management Tools:

    - **ProcessReturnAsync**: Automates return processing for delivered orders, validates return eligibility based on order status, creates return records for all unreturned items, and calculates refund amounts
    - **SendCustomerEmailAsync**: Sends follow-up emails to customers with order-related information

    Design Characteristics:

    - All methods return human-readable strings formatted for AI conversation
    - Built-in validation and error handling with contextual error messages
    - Integrates with existing services (OrderService, EmailService) and database context
    - Comprehensive logging for all tool invocations
    - User-scoped operations with userId verification for security

1. Open the **ContosoShop.Server/Program.cs** file.

    You'll use the Program.cs file to register SupportAgentTools in dependency injection.

1. Locate the service registration section (after the existing `builder.Services.AddScoped<IOrderService, OrderService>();` line).

1. Add the following line to register the `SupportAgentTools` service:

    ```csharp
    // Register AI agent tools service
    builder.Services.AddScoped<SupportAgentTools>();
    ```

1. Save the two updated files.

1. In the terminal, build the project:

    ```powershell
    dotnet build
    ```

    The build should succeed. If there are errors, review the `SupportAgentTools.cs` file to ensure all `using` statements and references are correct. You can use GitHub Copilot to help debug if needed.

## Task 3: Configure the Copilot SDK agent and expose an API endpoint

In this task, you create a `CopilotClient` singleton, register it in dependency injection, and create a new API controller that accepts user questions and returns the AI agent's responses.

Use the following steps to complete this task:

1. Open the **ContosoShop.Server/Program.cs** file.

    You'll use the Program.cs file to register CopilotClient as a singleton in dependency injection.

1. Add the following `using` statement at the top of the file, after the existing `using` statements:

    ```csharp
    using GitHub.Copilot.SDK;
    ```

1. Locate the service registration section (near the other `builder.Services.Add...` lines). Add the following code to create and register a `CopilotClient` singleton:

    ```csharp
    // Register GitHub Copilot SDK client as a singleton
    builder.Services.AddSingleton<CopilotClient>(sp =>
    {
        var logger = sp.GetRequiredService<ILogger<CopilotClient>>();
        return new CopilotClient(new CopilotClientOptions
        {
            AutoStart = true,
            LogLevel = "info"
        });
    });
    ```

    The `CopilotClient` manages the Copilot CLI process lifecycle. Setting `AutoStart = true` means the CLI server starts automatically when the first session is created.

1. To ensure the `CopilotClient` is properly disposed when the application shuts down, add the following code after the `var app = builder.Build();` line (but before the database initialization block):

    ```csharp
    // Ensure CopilotClient is started
    var copilotClient = app.Services.GetRequiredService<CopilotClient>();
    await copilotClient.StartAsync();
    ```

1. Save the file.

1. In Visual Studio Code's EXPLORER view, right-click the **ContosoShop.Shared/Models** folder, and then select **New File**.

1. Name the file **SupportQuery.cs**.

1. Add the following code:

    ```csharp
    using System.ComponentModel.DataAnnotations;

    namespace ContosoShop.Shared.Models;

    /// <summary>
    /// Represents a support question submitted by the user to the AI agent.
    /// </summary>
    public class SupportQuery
    {
        /// <summary>
        /// The user's question or message for the AI support agent.
        /// </summary>
        [Required]
        [StringLength(1000, MinimumLength = 1)]
        public string Question { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents the AI agent's response to a support query.
    /// </summary>
    public class SupportResponse
    {
        /// <summary>
        /// The AI agent's answer to the user's question.
        /// </summary>
        public string Answer { get; set; } = string.Empty;
    }
    ```

1. Take a minute to review the `SupportQuery` and `SupportResponse` models.

    This file defines data transfer models for AI support agent communication:

    SupportQuery

    - Represents customer questions sent to the AI support agent
    - Contains a Question property with validation: required, 1-1000 characters
    - Used as the request payload from client to server

    SupportResponse

    - Represents AI agent responses back to the customer
    - Contains an Answer property with the agent's reply
    - Used as the response payload from server to client

    These are lightweight DTOs for the support chat interface, enabling structured communication between the Blazor client and the AI-powered support endpoint. The simple design focuses on text-based question-and-answer exchanges with basic input validation.

1. In Visual Studio Code's EXPLORER view, right-click the **ContosoShop.Server/Controllers** folder, and then select **New File**.

1. Name the file **SupportAgentController.cs**.

1. Add the following code:

    ```csharp
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.AI;
    using GitHub.Copilot.SDK;
    using ContosoShop.Server.Services;
    using ContosoShop.Shared.Models;
    using System.ComponentModel;
    using System.Security.Claims;

    namespace ContosoShop.Server.Controllers;

    /// <summary>
    /// API controller that handles AI support agent queries.
    /// Accepts user questions, creates a Copilot SDK session with custom tools,
    /// and returns the agent's response.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SupportAgentController : ControllerBase
    {
        private readonly CopilotClient _copilotClient;
        private readonly SupportAgentTools _agentTools;
        private readonly ILogger<SupportAgentController> _logger;

        public SupportAgentController(
            CopilotClient copilotClient,
            SupportAgentTools agentTools,
            ILogger<SupportAgentController> logger)
        {
            _copilotClient = copilotClient;
            _agentTools = agentTools;
            _logger = logger;
        }

        /// <summary>
        /// Accepts a support question from the user and returns the AI agent's response.
        /// POST /api/supportagent/ask
        /// </summary>
        [HttpPost("ask")]
        public async Task<IActionResult> AskQuestion([FromBody] SupportQuery query)
        {
            if (query == null || string.IsNullOrWhiteSpace(query.Question))
            {
                return BadRequest(new SupportResponse { Answer = "Please enter a question." });
            }

            // Get the authenticated user's ID from claims
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized(new SupportResponse { Answer = "Unable to identify user." });
            }

            _logger.LogInformation("Support agent query from user {UserId}: {Question}", userId, query.Question);

            try
            {
                // Define the tools the AI agent can use
                var tools = new[]
                {
                    AIFunctionFactory.Create(
                        async ([Description("The order ID number")] int orderId) =>
                            await _agentTools.GetOrderDetailsAsync(orderId, userId),
                        "get_order_details",
                        "Look up the status and details of a specific order by its order number. Returns order status, items, dates, and total amount."),

                    AIFunctionFactory.Create(
                        async () =>
                            await _agentTools.GetUserOrdersSummaryAsync(userId),
                        "get_user_orders",
                        "Get a summary list of all orders for the current user. Use this when the user asks about their orders without specifying an order number."),

                    AIFunctionFactory.Create(
                        async ([Description("The order ID number to return")] int orderId) =>
                            await _agentTools.ProcessReturnAsync(orderId, userId),
                        "process_return",
                        "Process a return for a delivered order. Returns all unreturned items in the order and initiates a refund. Only works for orders with Delivered status."),

                    AIFunctionFactory.Create(
                        async (
                            [Description("The order ID number")] int orderId,
                            [Description("The email message content")] string message) =>
                            await _agentTools.SendCustomerEmailAsync(orderId, userId, message),
                        "send_customer_email",
                        "Send a follow-up email to the customer with additional information about their order.")
                };

                // Create a Copilot session with the system prompt and tools
                await using var session = await _copilotClient.CreateSessionAsync(new SessionConfig
                {
                    Model = "gpt-4.1",
                    SystemMessage = new SystemMessageConfig
                    {
                        Mode = SystemMessageMode.Replace,
                        Content = @"You are ContosoShop's AI customer support assistant. Your role is to help customers with their order inquiries.
                        
                            CAPABILITIES:
                            - Look up order status and details using the get_order_details tool
                            - List all customer orders using the get_user_orders tool
                            - Process returns for delivered orders using the process_return tool
                            - Send follow-up emails using the send_customer_email tool
                            
                            RULES:
                            - ALWAYS use the available tools to look up real data. Never guess or make up order information.
                            - Be friendly, concise, and professional in your responses.
                            - If a customer asks about an order, use get_order_details with the order number they provide.
                            - If a customer asks about their orders without specifying a number, use get_user_orders to list them.
                            - If a customer wants to return an order, confirm the order number first, then use process_return.
                            - Only process returns when the customer explicitly requests one.
                            - If asked something outside your capabilities (not related to orders), politely explain that you can only help with order-related inquiries and suggest contacting support@contososhop.com or calling 1-800-CONTOSO for other matters.
                            - Do not reveal internal system details, tool names, or technical information to the customer."
                            },
                    Tools = tools,
                    InfiniteSessions = new InfiniteSessionConfig { Enabled = false }
                });

                // Collect the agent's response
                var responseContent = string.Empty;
                var done = new TaskCompletionSource();

                session.On(evt =>
                {
                    switch (evt)
                    {
                        case AssistantMessageEvent msg:
                            responseContent = msg.Data.Content;
                            break;
                        case SessionIdleEvent:
                            done.TrySetResult();
                            break;
                        case SessionErrorEvent err:
                            _logger.LogError("Agent session error: {Message}", err.Data.Message);
                            done.TrySetException(new Exception(err.Data.Message));
                            break;
                    }
                });

                // Send the user's question
                await session.SendAsync(new MessageOptions { Prompt = query.Question });

                // Wait for the response with a timeout
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(30));
                var completedTask = await Task.WhenAny(done.Task, timeoutTask);

                if (completedTask == timeoutTask)
                {
                    _logger.LogWarning("Agent session timed out for user {UserId}", userId);
                    return Ok(new SupportResponse
                    {
                        Answer = "I'm sorry, the request took too long. Please try again or contact our support team."
                    });
                }

                // Rethrow if the task faulted
                await done.Task;

                _logger.LogInformation("Agent response for user {UserId}: {Answer}", userId, responseContent);

                return Ok(new SupportResponse { Answer = responseContent });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing support agent query for user {UserId}", userId);
                return StatusCode(500, new SupportResponse
                {
                    Answer = "I'm sorry, I encountered an error processing your request. Please try again or contact our support team at support@contososhop.com."
                });
            }
        }
    }
    ```

1. Take a few minutes to review the `SupportAgentController` code.

    This controller implements an AI-powered customer support agent using the GitHub Copilot SDK. Key features:

    AI Agent Integration:

    - Uses CopilotClient to create AI sessions with GPT-4.1
    - Configures a custom system prompt defining the agent's role as ContosoShop support assistant
    - Provides explicit rules and capabilities for the AI agent

    Custom Tools (Function Calling):

    - get_order_details - Looks up specific orders by ID
    - get_user_orders - Lists all orders for the current user
    - process_return - Automates return processing for delivered orders
    - send_customer_email - Sends follow-up emails to customers

    Security & User Context:

    - Requires [Authorize] - only authenticated users can access
    - Extracts userId from claims to ensure user-scoped operations
    - All tool calls are bound to the authenticated user's ID

    Session Management:

    - Creates event-driven sessions with the Copilot SDK
    - Handles AssistantMessageEvent, SessionIdleEvent, and SessionErrorEvent
    - 30-second timeout protection for long-running requests
    - Comprehensive error handling and logging

    API Design:

    - Single endpoint: POST /api/supportagent/ask
    - Accepts SupportQuery, returns SupportResponse
    - Returns friendly error messages on failures

    This enables conversational AI support where customers can ask natural language questions and the agent autonomously decides which tools to invoke to help them.

1. Open the **ContosoShop.Server/Program.cs** file.

1. Verify that the CORS configuration section allows the `GET` and `POST` methods required by the API endpoint you just created.

   The existing configuration allows `GET` and `POST` methods, which is sufficient.

    ```csharp
    .WithMethods("GET", "POST") // Only required methods
    ```

1. To build the project, enter the following command in the terminal:

    ```powershell
    dotnet build
    ```

    The build should succeed without errors. If you see errors related to `GitHub.Copilot.SDK` types, verify that the NuGet package was installed correctly.

## Task 4: Update the Blazor frontend to interact with the agent

In this task, you create a client-side service to call the agent API and update the Support.razor page with an interactive chat interface.

Use the following steps to complete this task:

1. In Visual Studio Code's EXPLORER view, right-click the **ContosoShop.Client/Services** folder, and then select **New File**.

1. Name the file **SupportAgentService.cs**.

1. Add the following code:

    ```csharp
    using System.Net.Http.Json;
    using ContosoShop.Shared.Models;

    namespace ContosoShop.Client.Services;

    /// <summary>
    /// Client-side service for communicating with the AI support agent API.
    /// </summary>
    public class SupportAgentService
    {
        private readonly HttpClient _http;

        public SupportAgentService(HttpClient http)
        {
            _http = http;
        }

        /// <summary>
        /// Sends a question to the AI support agent and returns the response.
        /// </summary>
        /// <param name="question">The user's question</param>
        /// <returns>The agent's response text</returns>
        public async Task<string> AskAsync(string question)
        {
            var query = new SupportQuery { Question = question };

            var response = await _http.PostAsJsonAsync("api/supportagent/ask", query);

            if (!response.IsSuccessStatusCode)
            {
                var errorText = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Support agent returned {response.StatusCode}: {errorText}");
            }

            var result = await response.Content.ReadFromJsonAsync<SupportResponse>();
            return result?.Answer ?? "I'm sorry, I didn't receive a response. Please try again.";
        }
    }
    ```

1. Take a minute to review the `SupportAgentService` code.

    This is a client-side HTTP service that interfaces with the AI support agent backend. Key features:

    Simple API Wrapper:

    - Single method AskAsync(string question) - sends user questions to the support agent API endpoint
    - Posts to POST /api/supportagent/ask on the server

    Communication Handling:

    - Wraps the question in a SupportQuery DTO
    - Uses HttpClient.PostAsJsonAsync for automatic JSON serialization
    - Deserializes the response into a SupportResponse object

    Error Management:

    - Checks HTTP status codes for failures
    - Throws HttpRequestException with detailed error information on non-success responses
    - Provides fallback message if response parsing fails

    Design Pattern:

    - Thin client wrapper following the service layer pattern
    - Injected HttpClient for testability and proper lifetime management
    - Used by Blazor components (like Support.razor) to interact with the AI agent without handling HTTP details directly

    This service abstracts away the HTTP communication complexity, providing a clean interface for Blazor components to ask questions to the AI support agent.

1. Open the **ContosoShop.Client/Program.cs** file.

1. Add the following `using` statement at the top of the file:

    ```csharp
    using ContosoShop.Client.Services;
    ```

1. Locate the service registration section (near the existing `builder.Services.AddScoped...` lines). Add the following line:

    ```csharp
    builder.Services.AddScoped<SupportAgentService>(sp =>
        new SupportAgentService(sp.GetRequiredService<HttpClient>()));
    ```

1. Save the file.

### Step 4.3: Update the Support.razor page with the chat interface

1. Open the **ContosoShop.Client/Pages/Support.razor** file.

1. Replace the entire content of the file with the following code:

    ```cshtml
    @page "/support"
    @using ContosoShop.Shared.Models
    @using ContosoShop.Client.Services
    @attribute [Microsoft.AspNetCore.Authorization.Authorize]
    @inject SupportAgentService AgentService

    <PageTitle>Contact Support - ContosoShop Support Portal</PageTitle>

    <div class="container mt-4">
        <div class="row">
            <div class="col-lg-8 mx-auto">
                <h2 class="mb-4">Contact Support</h2>

                <!-- AI Chat Card -->
                <div class="card mb-4 border-info">
                    <div class="card-header bg-info text-white">
                        <h5 class="mb-0">
                            <i class="bi bi-robot me-2"></i>AI Chat Support
                        </h5>
                    </div>
                    <div class="card-body">
                        <!-- Chat Messages Area -->
                        <div class="border rounded p-3 mb-3" style="min-height: 300px; max-height: 500px; overflow-y: auto;" id="chatMessages">
                            @if (!conversations.Any())
                            {
                                <div class="text-center text-muted py-4">
                                    <i class="bi bi-chat-dots display-4 mb-2"></i>
                                    <p>Ask me about your orders! For example:</p>
                                    <ul class="list-unstyled">
                                        <li><em>"What is the status of order #1001?"</em></li>
                                        <li><em>"Show me all my orders"</em></li>
                                        <li><em>"I want to return order #1005"</em></li>
                                    </ul>
                                </div>
                            }
                            @foreach (var entry in conversations)
                            {
                                <div class="mb-3">
                                    <div class="d-flex align-items-start mb-1">
                                        <span class="badge bg-primary me-2">You</span>
                                        <span>@entry.Question</span>
                                    </div>
                                    @if (!string.IsNullOrEmpty(entry.Answer))
                                    {
                                        <div class="d-flex align-items-start ms-2">
                                            <span class="badge bg-info me-2">Agent</span>
                                            <span style="white-space: pre-line;">@entry.Answer</span>
                                        </div>
                                    }
                                </div>
                            }
                            @if (isLoading)
                            {
                                <div class="d-flex align-items-start ms-2">
                                    <span class="badge bg-info me-2">Agent</span>
                                    <span class="text-muted"><em>Thinking...</em></span>
                                </div>
                            }
                        </div>

                        <!-- Input Area -->
                        <div class="input-group">
                            <input type="text"
                                   class="form-control"
                                   placeholder="Type your question..."
                                   @bind="currentQuestion"
                                   @bind:event="oninput"
                                   @onkeydown="HandleKeyDown"
                                   disabled="@isLoading" />
                            <button class="btn btn-info text-white"
                                    @onclick="SubmitQuestion"
                                    disabled="@(isLoading || string.IsNullOrWhiteSpace(currentQuestion))">
                                <i class="bi bi-send me-1"></i>Send
                            </button>
                        </div>

                        @if (!string.IsNullOrEmpty(errorMessage))
                        {
                            <div class="alert alert-danger mt-2 mb-0">
                                <i class="bi bi-exclamation-triangle me-1"></i>@errorMessage
                            </div>
                        }
                    </div>
                </div>

                <!-- Contact Information Card -->
                <div class="card mb-4">
                    <div class="card-header bg-primary text-white">
                        <h5 class="mb-0">
                            <i class="bi bi-headset me-2"></i>Get in Touch
                        </h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6 mb-3">
                                <h6 class="text-muted">Email Support</h6>
                                <p class="mb-0">
                                    <i class="bi bi-envelope me-2"></i>
                                    <a href="mailto:support@contososhop.com">support@contososhop.com</a>
                                </p>
                                <small class="text-muted">Response time: 24-48 hours</small>
                            </div>
                            <div class="col-md-6 mb-3">
                                <h6 class="text-muted">Phone Support</h6>
                                <p class="mb-0">
                                    <i class="bi bi-telephone me-2"></i>
                                    <a href="tel:1-800-266-8676">1-800-CONTOSO</a>
                                </p>
                                <small class="text-muted">Mon-Fri 9AM-5PM EST</small>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Quick Links -->
                <div class="card">
                    <div class="card-header">
                        <h5 class="mb-0">
                            <i class="bi bi-question-circle me-2"></i>Need Help With Your Order?
                        </h5>
                    </div>
                    <div class="card-body">
                        <ul class="list-unstyled mb-0">
                            <li class="mb-2">
                                <a href="/orders" class="text-decoration-none">
                                    <i class="bi bi-box-seam me-2"></i>View Your Orders
                                </a>
                            </li>
                            <li class="mb-2">
                                <i class="bi bi-arrow-return-left me-2"></i>
                                <span>Return a delivered order from the Order Details page</span>
                            </li>
                            <li class="mb-0">
                                <i class="bi bi-info-circle me-2"></i>
                                <span>Track shipment status and delivery updates</span>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    @code {
        private class ConversationEntry
        {
            public string Question { get; set; } = string.Empty;
            public string Answer { get; set; } = string.Empty;
        }

        private List<ConversationEntry> conversations = new();
        private string currentQuestion = string.Empty;
        private bool isLoading = false;
        private string errorMessage = string.Empty;

        private async Task HandleKeyDown(KeyboardEventArgs e)
        {
            if (e.Key == "Enter" && !isLoading && !string.IsNullOrWhiteSpace(currentQuestion))
            {
                await SubmitQuestion();
            }
        }

        private async Task SubmitQuestion()
        {
            if (string.IsNullOrWhiteSpace(currentQuestion) || isLoading)
                return;

            errorMessage = string.Empty;
            var question = currentQuestion.Trim();
            currentQuestion = string.Empty;

            var entry = new ConversationEntry { Question = question };
            conversations.Add(entry);

            try
            {
                isLoading = true;
                StateHasChanged();

                var answer = await AgentService.AskAsync(question);
                entry.Answer = answer;
            }
            catch (Exception ex)
            {
                errorMessage = "Sorry, something went wrong. Please try again or contact our support team.";
                Console.Error.WriteLine($"Agent error: {ex.Message}");
            }
            finally
            {
                isLoading = false;
                StateHasChanged();
            }
        }
    }
    ```

1. Take a few minutes to review the updated `Support.razor` code.

    This Blazor page provides an AI-powered chat support interface for customers. Key features:

    Chat Interface:

    - Real-time conversational UI with message history showing user questions and AI agent responses
    - Loading indicator ("Thinking...") during agent processing
    - Auto-scrolling chat area with 300-500px height
    - Enter key support for quick message submission
    - Helpful example prompts when chat is empty

    User Interaction:

    - Input field with send button (disabled during loading)
    - Shows conversation history with badge-labeled messages (You/Agent)
    - Clears input after submission
    - Error handling with user-friendly messages

    Additional Support Options:

    - Contact Information Card - Email (support@contososhop.com) and phone number (1-800-CONTOSO) with response time details
    - Quick Links Card - Links to view orders, return information, and tracking details

    State Management:

    - Maintains conversation history as a list of Q&A pairs
    - Tracks loading state to prevent duplicate submissions
    - Preserves chat history during the session

    Security:

    - [Authorize] attribute - requires authentication
    - Integrates with SupportAgentService for secure backend communication

    UI/UX:

    - Bootstrap styling with info/primary color scheme
    - Robot icon for AI chat, headset icon for contact info
    - Responsive layout with centered column on large screens
    - Pre-formatted text support for multi-line agent responses

    This page serves as the complete customer support hub combining autonomous AI assistance with traditional contact methods.

1. Open the ContosoShop.Server directory in the terminal, and then enter the following command:

    ```powershell
    dotnet build
    ```

    The build should succeed without errors.

## Task 5: Test the end-to-end AI agent experience

In this task, you run the application and test the AI agent with various support queries to verify it functions correctly.

Use the following steps to complete this task:

1. To start the server application from the terminal, enter the following command:

    ```powershell
    dotnet run
    ```

    Watch the console output for any errors during startup. You should see the application listening on the HTTPS and HTTP ports.

1. Open a browser and navigate to `http://localhost:5266`.

1. Sign in with the demo credentials.

    Enter `mateo@contoso.com` for the email and `Password123!` for the password, and then select **Login**.

1. Navigate to the **Contact Support** page.

    You should now see the interactive AI Chat Support interface instead of the "Coming Soon" placeholder. The chat area displays example prompts to help you get started.

1. To test the agent's ability to **Check order status**, enter the following question and select **Send** (or press Enter):

    ```plaintext
    What's the status of order #1001?
    ```

    The agent should respond with details about order #1001, including its status, order date, items, and total amount. The response should reflect the actual data in the database.

    Verify the response matches what you see on the Orders page for order #1001.

1. To test the agent's ability to **List all orders**, enter the following question:

    ```plaintext
    Show me all my orders
    ```

    The agent should use the `get_user_orders` tool and return a summary list of all 10 of Mateo's orders with their statuses and amounts.

1. To test the agent's ability to **Process a return**, enter the following question:

    ```plaintext
    I want to return order #1005
    ```

    The agent should process the return for order #1005 (which has Delivered status) and confirm the refund amount. After the response:

    - Navigate to the **Orders** page and verify that order #1005 now shows a "Returned" status.
    - Check the server console output for the email notification log from `EmailServiceDev`.

1. To test the agent's ability to **Handle an order that can't be returned**, enter the following question:

    ```plaintext
    Can I return order #1010?
    ```

    Order #1010 has "Processing" status and cannot be returned. The agent should explain that the order must be delivered before it can be returned.

1. To test the agent's ability to **Handle a non-existent order**, enter the following question:

    ```plaintext
    Where is my order #9999?
    ```

    The agent should respond that it could not find order #9999 associated with the user's account.

1. To test the agent's ability to **Handle an off-topic question**, enter the following question:

    ```plaintext
    What's the weather like today?
    ```

    The agent should politely explain that it can only help with order-related inquiries and suggest contacting support through other channels.

1. When you are done testing, return to the terminal and press **Ctrl+C** to stop the application.

**Troubleshooting tips:**

- If the agent's responses seem odd or it doesn't use the tools, check the server console for error messages. Common issues include:
  - **CopilotClient connection failures**: Ensure the Copilot CLI is installed and you are signed in. Run `copilot --version` to verify.
  - **Authentication errors**: Ensure you are logged in to the Copilot CLI. Run `copilot auth status` to check.
  - **Timeout errors**: The agent has a 30-second timeout. If responses are slow, check your network connection.
  - **Tool invocation errors**: Check the server logs for messages from `SupportAgentTools`. The log output shows which tools are being called and what data they return.

## Clean up

Now that you've finished the exercise, take a minute to clean up your environment:

- Stop the server application if it's still running (press **Ctrl+C** in the terminal).
- Ensure that you haven't made changes to your GitHub account or GitHub Copilot subscription that you don't want to keep.
- Optionally archive or delete the local clone of the repository.

## Summary

In this exercise, you successfully integrated an AI-powered customer support agent into the ContosoShop E-commerce Support Portal using the GitHub Copilot SDK. You:

- **Created backend tools** (`SupportAgentTools`) that the AI agent can invoke to look up orders and process returns, leveraging the existing application services.
- **Configured the Copilot SDK** with a `CopilotClient` singleton and created sessions with a custom system prompt and tool definitions using `AIFunctionFactory.Create`.
- **Built an API endpoint** (`SupportAgentController`) that accepts user questions, creates agent sessions, and returns AI-generated responses.
- **Updated the Blazor frontend** with an interactive chat interface on the Support page.
- **Tested the integration** with real-world scenarios including order lookups, returns, error handling, and off-topic deflection.

This pattern — defining business logic as tools, registering them with an AI agent runtime, and exposing the agent via an API — is applicable to many domains beyond e-commerce support. You can apply the same approach to IT helpdesk automation, CRM assistants, or any scenario where an AI agent needs to take actions on behalf of users.
