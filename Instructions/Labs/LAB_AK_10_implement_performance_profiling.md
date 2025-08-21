<!-- ---
lab:
    title: 'Exercise - Implement performance profiling using GitHub Copilot'
    description: 'Learn how to identify and address performance bottlenecks and code inefficiencies using GitHub Copilot tools.'
--- -->

# Implement performance profiling using GitHub Copilot

Performance profiling is a critical aspect of software development that helps identify and address performance bottlenecks and code inefficiencies.

In this exercise, you review an existing project that contains performance bottlenecks and inefficient code, analyze your options for improving performance, refactor the code to address the issues, and test the refactored code to ensure it works as intended. You use GitHub Copilot in Ask mode to gain an understanding of an existing code project and explore options for refactoring the identified issues. You use GitHub Copilot in Agent mode to refactor the code and improve performance. You test the original and refactored code to measure the impact of your changes.

This exercise should take approximately **30** minutes to complete.

> **IMPORTANT**: To complete this exercise, you must provide your own GitHub account and GitHub Copilot subscription. If you don't have a GitHub account, you can <a href="https://github.com/" target="_blank">sign up</a> for a free individual account and use a GitHub Copilot Free plan to complete the exercise. If you have access to a GitHub Copilot Pro, GitHub Copilot Pro+, GitHub Copilot Business, or GitHub Copilot Enterprise subscription from within your lab environment, you can use your existing GitHub Copilot subscription to complete this exercise.

## Before you start

Your lab environment must include the following: Git 2.48 or later, .NET SDK 9.0 or later, Visual Studio Code with the C# Dev Kit extension, and access to a GitHub account with GitHub Copilot enabled.

### Configure your lab environment

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

### Download sample code project

Use the following steps to download the sample project and open it in Visual Studio Code:

1. Open a browser window in your lab environment.

1. To download a zip file containing the sample app project, open the following URL in your browser: [GitHub Copilot lab - refactor large functions](https://github.com/MicrosoftLearning/mslearn-github-copilot-dev/raw/refs/heads/main/DownloadableCodeProjects/Downloads/GHCopilotEx10LabApps.zip)

    The zip file is named **GHCopilotEx10LabApps.zip**.

1. Extract the files from the **GHCopilotEx10LabApps.zip** file.

    For example:

    1. Navigate to the downloads folder in your lab environment.

    1. Right-click *GHCopilotEx10LabApps.zip*, and then select **Extract all**.

    1. Select **Show extracted files when complete**, and then select **Extract**.

1. Copy the **GHCopilotEx10LabApps** folder to a location that's easy to access, such as your Windows Desktop folder.

1. Open the **GHCopilotEx10LabApps** folder in Visual Studio Code.

    For example:

    1. Open Visual Studio Code in your lab environment.

    1. In Visual Studio Code, on the **File** menu, select **Open Folder**.

    1. Navigate to the Windows Desktop folder, select **GHCopilotEx10LabApps** and then select **Select Folder**.

1. In the Visual Studio Code SOLUTION EXPLORER view, verify the following project structure:

    - GHCopilotEx10LabApps\
        - ContosoOnlineStore\
        - DataAnalyzerReporter\

## Exercise scenario

You're a software developer working for a consulting firm. Your clients need help implementing performance profiling in legacy applications. Your goal is to improve code readability and maintainability while preserving the existing functionality. You're assigned to the following app:

- ContosoOnlineStore: This e-commerce app 

This exercise includes the following tasks:

1. Review the ContosoOnlineStore codebase manually.
1. Identify refactoring opportunities using GitHub Copilot Chat (Ask mode).
1. Refactor low performing and inefficient code using GitHub Copilot Chat (Agent mode).
1. Test the refactored ContosoOnlineStore code.

### Task 1: Review the ContosoOnlineStore codebase manually

**Goal**: Set up the project and observe its current performance. Before optimizing, an expert always establishes a baseline measurement. “You can’t improve what you don’t measure,” as our lead developer would say.

1. Open the Project in VS Code:

    Launch Visual Studio Code and open the ContosoOnlineStore project folder. Ensure the C# extension and GitHub Copilot Chat extension are enabled.

    Expert Tip: If you haven’t already, familiarize yourself with the project structure (check the Solution Explorer or file tree). Knowing where things are (products, orders, etc.) will save you time later.

1. Build and Run the Application:

    In VS Code’s integrated terminal, execute: dotnet run The program will simulate processing a sample order and print results to the console.

    Observe the Output: You should see the total order cost and the time taken to process the order (in milliseconds). For example, it might say “Processing Time = 2500 ms” (2.5 seconds for a simple order).

1. Note the Baseline Performance:

    Jot down the reported processing time. This is our baseline. Also note any other output (like stock levels after the order). Everything should function correctly (no bugs in results). The only issue is the speed.

    Expert Insight: A processing time of a couple of seconds for a small order is a red flag. An app like this should handle such operations in milliseconds. In real scenarios, slow performance could mean unhappy customers or lost revenue. (Recall that Amazon found a 100ms delay cost 1% in sales – performance matters!) Before we rush to fix it, we need to understand where that time is going.

1. Skim Key Parts of the Code:

    Open OrderProcessor.cs, InventoryManager.cs, and EmailService.cs. These are likely suspects for where time might be spent (as they handle calculation, updating, and an external call). Don’t change anything yet; just read the code and comments.

    What to look for: Any loops, calls to external systems (even simulated ones like Thread.Sleep), and any operation that might repeat work. The comments marked “Performance Issue” will hint at these. For instance, you might notice the Thread.Sleep(2000) in EmailService – an obvious delay.

    At this point, you have a sense of how the app behaves and which areas might be problematic. We haven’t confirmed any issues yet, but we have hypotheses. Next, we’ll gather evidence to pinpoint the bottlenecks.

### Task 2: Identify Performance and Efficiency Issues (Profiling & Copilot Ask Mode)

**Goal**: Use lightweight profiling and GitHub Copilot’s analysis capabilities to find the exact spots in code causing the slowdown.

1. Measure Performance in Segments:

    To narrow down the slow part, you can instrument the code with timers. For example, in OrderProcessor.FinalizeOrder, consider measuring how long each step takes (calculate total, update inventory, send email). You can use Stopwatch:

    ```csharp
    
    var stopwatch = Stopwatch.StartNew();
    // call CalculateOrderTotal
    stopwatch.Stop();
    Console.WriteLine($"CalculateOrderTotal took {stopwatch.ElapsedMilliseconds}ms");
    
    ```

    Do this for UpdateStockLevels and SendConfirmation as well. Then run the app again.

    Expert Insight: This is a quick-and-dirty profiling method. In real projects, I often use proper profilers, but inserting a Stopwatch is sometimes the fastest way to get answers. The code already logs the overall time; by adding these, you can see which phase is the slowest.

1. Analyze Timing Results:

    After instrumenting, suppose you see output like:

    - CalculateOrderTotal took 45ms
    - UpdateStockLevels took 30ms
    - SendConfirmation took 2000ms (as expected, ~2000ms due to the Sleep)

    In this hypothetical measurement, the email sending is clearly the biggest chunk (~2 seconds). The others are relatively minor for one order. But imagine if the order had 100 items, maybe CalculateOrderTotal would take significantly longer (we’ll test that soon).

    Expert Observation: The email delay stands out. However, 45ms for calculating totals on a small order is also higher than I’d expect. 45ms to sum up a few numbers hints that maybe a lot of lookups are happening. We should verify that.

1. Leverage GitHub Copilot Chat – Ask Mode:

    Now, turn to Copilot for insight. Open OrderProcessor.cs and highlight the loop in CalculateOrderTotal. Ask Copilot in natural language:

    “Why might this method be slow for large orders?”

    What Copilot might say: It could explain that for each item, we’re doing a lookup (GetProductById) which likely searches through the product list. It might mention this looks like an N+1 query pattern or an O(n) lookup in a loop, which could be inefficient if there are many items. Copilot might suggest caching products or retrieving them in one go.

    Expert Commentary: This is akin to doing a mini code review with Copilot. An experienced dev would think: “We’re calling GetProductById 100 times if there are 100 items – if that is not O(1), it could cost.” Copilot is confirming that hunch by pointing out the repeated lookup. Great!

1. Ask Copilot About Inventory Update:

    Open InventoryManager.cs, focus on UpdateStockLevels. Ask:

    “Is there a performance issue with how stock levels are updated?”

    Expected Copilot insight: It might note that the code is iterating over the whole stock dictionary for each order item, which is inefficient. It may explain that a direct dictionary access would be faster, and the current approach is O(n×m) which scales poorly.

    Expert Commentary: Indeed, scanning a dictionary in that manner is wasteful. A senior dev would likely shake their head at this implementation – it’s doing much more work than necessary. Copilot’s analysis here aligns with what a human expert would point out. Perhaps the original coder was trying to avoid modifying a collection while iterating (hence copying to a list), but there’s a better way.

1. Ask Copilot About EmailService:

    Open EmailService.cs. Ask:

    “How could we improve this SendConfirmation method’s design?”

    Copilot might respond: It could suggest using async/await and Task.Delay instead of Thread.Sleep, so as not to block the thread. It might explain that making the method asynchronous would allow the rest of the program to continue, improving throughput.

    Expert Commentary: In modern .NET, you’d almost never use Thread.Sleep on a server. An expert would strongly favor asynchronous I/O calls. Copilot essentially channels that best practice in its answer. It’s nice to have it validate our plan: clearly, making the email send async is on our to-do list for optimization.

1. Consider Data Volume Tests (optional):

    You might also test with a larger input to magnify issues. For example, modify Program.cs to create an order with, say, 100 items (repeat some products with varying quantities). Run it and see how the time scales. If the time jumps substantially (which it likely will), that empirically proves the looping issues (because 100 items × repeated lookups increases time roughly linearly). This kind of experiment aligns with what an expert would do: try a stress test to see where the pain grows.

    (You can skip this if you trust the analysis above, but it’s a good learning exercise.)

By the end of Task 2, you should have identified three main culprits in the E-commerce app:

- The product lookup loop in CalculateOrderTotal (likely causing an N+1 query-like inefficiency).
- The nested loop in UpdateStockLevels (unnecessarily costly inventory update).
- The synchronous SendConfirmation (blocking call causing a fixed 2-second delay).

We’ve used both measurements (timing, logging) and Copilot’s AI analysis to reach these conclusions – a powerful combination. As our expert likes to say: “Use the right tools for the job – sometimes that’s a stopwatch, sometimes it’s an AI pair programmer, often both.”

### Task 3: Refactor Code to Improve Performance (Profiling & Copilot Agent Mode)

**Goal**: Apply fixes to the identified issues, using GitHub Copilot to assist in writing the optimized code. We’ll tackle the problems one by one, and verify each improvement.

Before coding, a word of wisdom from our expert: “Always prioritize clear improvements. We want to make the code faster without breaking it or making it unmaintainable. Optimize in small steps, and test each step.” In practice, that means we’ll refactor one issue at a time and run the app to ensure we didn’t introduce bugs.

1. Optimize Product Lookup in CalculateOrderTotal:

    Problem recap: Multiple calls to _catalog.GetProductById in a loop.

    Solution approach: Fetch all needed products in advance, or cache lookups in a dictionary, so each product is only looked up once, even if it appears multiple times in the order.

    Using Copilot (Agent Mode):

    In VS Code, you could simply write a comment above the loop:

    // TODO: Optimize by reducing repeated product lookups (cache products in a Dictionary).

    Copilot may then suggest a code change. If not, try explicitly prompting in Copilot Chat: “Optimize this loop by minimizing calls to GetProductById”.

    Expected Copilot suggestion: It might propose creating a dictionary of productId -> Product for the order’s items. 

    The code could look like:

    ```csharp
    
    var productCache = new Dictionary<int, Product>();
    foreach (OrderItem item in order.Items)
    {
        Product prod;
        if (!productCache.TryGetValue(item.ProductId, out prod))
        {
            prod = _catalog.GetProductById(item.ProductId);
            productCache[item.ProductId] = prod;
        }
        if (prod != null)
        {
            total += prod.Price * item.Quantity;
        }
    }
    
    ```

    This way, each product ID is looked up at most once. If an order has multiple items of the same product, we really save time. Even if all items are different, we’ve basically turned 100 separate searches into 100 (which is the same count) but if GetProductById is expensive, this at least opens the door to batch fetching later.

    Implement the suggestion (or your variation of it) in CalculateOrderTotal.

    Test quickly: Re-run the app with the same input. The overall time should drop a bit (though if the order had all unique products loaded from memory, the difference might be small). The key is that we removed a scalability issue. For a huge order list, this would shine. And we haven’t broken anything – verify the total price is still correct.

    Expert Reflection: We basically introduced a caching mechanism. In real enterprise code, we might fetch all products with one database query (for example, using a WHERE id IN (...) clause). Since our ProductCatalog is in-memory, the dictionary cache achieves a similar effect. The principle: don’t do repetitive work if you can do it once. This is a cornerstone of performance optimization and something I emphasize often.

1. Optimize Inventory Update in UpdateStockLevels:

    Problem recap: The method uses a nested loop to find and update stock, copying the dictionary each time – very inefficient.

    Solution approach: Directly access the dictionary by key, eliminating the inner loop entirely. If the key exists, update it; if not, ignore (or handle accordingly). We can also remove the unnecessary list copy.

    Using Copilot:

    Prompt Copilot Chat with something like: “Refactor this method to update stock levels with direct dictionary access (avoid nested loops)”.

    Expected suggestion: Copilot should produce a simpler loop such as:

    ```csharp
    
    foreach (OrderItem item in order.Items)
    {
        if (stockByProductId.ContainsKey(item.ProductId))
        {
            stockByProductId[item.ProductId] -= item.Quantity;
        }
    }
    
    ```

    This does the same job but in O(m) instead of O(n×m). Possibly Copilot will also handle the case where a product isn’t in the dictionary (though in our scenario, all ordered products should be present).

    Apply this refactor. The code becomes much shorter and clearer.

    Test again: Run the app. Functionally, the outcome should be the same (inventory counts decrement correctly). Performance-wise, you might not notice a big change for a single order of a few items, but if you had a huge inventory (say 1000 products) and an order of 50 items, this change prevents a 50×1000 loop. It’s more about eliminating a potential future bottleneck.

    Expert Reflection: We just made a classic improvement: using the right data structure in the right way. We had a dictionary (which is great for fast lookups), but we weren’t taking advantage of it. As the GitHub docs on Copilot Chat note, the AI can suggest things like “use a hashmap (dictionary) instead of an array for faster lookups” – that’s exactly what we did here. The code is now both faster and simpler. A win-win: performance and readability often go hand in hand when you choose better algorithms.

1. Improve Email Sending (Asynchronous):

    Problem recap: SendConfirmation artificially blocks the thread for 2 seconds to simulate an external call. This kills performance for concurrent scenarios (though in our console app it’s just a delay).

    Solution approach: Make the email send method async. In a real system, you’d await an API call or message queue. Here we can simulate that with Task.Delay to mimic an async wait. That way, the main thread could carry on with other work (if it were a web server thread, it would be free to handle other requests).

    Using Copilot: Ask Copilot: “Rewrite SendConfirmation to use async/await (non-blocking delay)”.

    Expected suggestion: Copilot may provide something like:

    ```csharp
    
    public async Task SendConfirmationAsync(Order order)
    {
        Console.WriteLine("Sending confirmation email…");
        await Task.Delay(2000);
        Console.WriteLine("Email sent.");
    }
    
    ```

    and it will likely advise adjusting FinalizeOrder to call this with await.

    Follow the suggestion: implement SendConfirmationAsync in EmailService and change FinalizeOrder to await _emailService.SendConfirmationAsync(order). This will make FinalizeOrder itself async Task<decimal> (adjust Program to await orderProcessor.FinalizeOrder(...) as well). These are minor signature changes but important.

    Test the change:

    Run the app. You should see the same output, but note: the console app will now actually wait asynchronously (which in a single-threaded console doesn’t make a visible difference, except that now we aren’t using Thread.Sleep). To truly test the benefit of async, we’d need a scenario with concurrency. However, this refactor is about demonstrating best practice. The code base is now ready for an asynchronous environment like a web server without blocking threads.

    Expert Reflection: Even though our lab app doesn’t fully exploit async (since it just exits after sending email), I want you to get into the habit of thinking asynchronously. Non-blocking operations are essential for scalable applications. Copilot’s suggestion to use Task.Delay is spot on for our simulation. In real life, you’d be awaiting an email service call instead. After this change, our FinalizeOrder method could be easily integrated into an ASP.NET controller or API endpoint without blocking the request thread.

1. Run Full Test & Profiling:

    Build and run the app one more time with all the changes:

    - Verify that the functionality is intact (correct total, stock reduced, “Email sent” message appears).

    - Check the processing time printed. It should be lower than before. For example, if it was ~2500 ms originally, it might now be ~500 ms or less. (The email delay might still contribute if it’s awaited synchronously in the console, but in theory it shouldn’t block progress – however, our console app does still wait for it to finish before exiting, effectively still taking ~2s. That’s okay; the main point is that we wrote it in a non-blocking style for future benefit.)

    - If you want a clearer proof of improvement, you could remove the Task.Delay(2000) entirely and see that basically the code runs nearly instantaneously now. But keep it in for simulation’s sake.

    Regardless, the major CPU-bound inefficiencies (product lookup and inventory loop) are fixed. The code is cleaner and more performant. The expert in me is happier with this code now!

1. (Optional) Profile with a larger test:

    If curious, try modifying Program to create a much larger order (e.g., 200 items including repeats and some high product IDs that exist). Time it before and after the changes. You will likely see a dramatic improvement after the fixes, especially if your order had repeats (benefiting from caching) or just many unique items (benefiting from eliminating the overhead per item).

Throughout Task 3, we engaged Copilot in Agent mode – essentially directing it to make specific changes. It’s like having an assistant implement the solution under your supervision. Notice how each Copilot suggestion was reviewed and adapted by you. An experienced developer always reviews AI-generated code: “Trust, but verify,” they say. In our case, Copilot’s suggestions were solid and we integrated them with minor tweaks.

Also, by addressing one issue at a time and testing, we ensured we didn’t introduce regressions. This stepwise approach is exactly how one should tackle performance tuning: identify, fix, verify, then move on.

### Task 4: Test and Verify the Refactored Application

**Goal**: Confirm that the performance issues are resolved and that the application still works correctly. This involves re-running the app, interpreting results, and reflecting on the improvement. Validation is crucial – in a professional setting, we’d ensure that all unit tests pass and maybe even get peer code review on our changes.

1. Run the application again:

    Use dotnet run as before. It might be useful to run it a few times or with different data to ensure consistency. If you left any Stopwatch logging in the code, observe those outputs too. They should confirm faster execution of previously slow methods.

1. Check the output:

    The order total and final stock levels should be correct (same as they were initially). If anything is off (which is unlikely since we didn’t change the core logic, just how it does it), that’s a bug to fix. Assuming all is well, note how much time it reports now. For example, perhaps it went from 2500 ms down to 400 ms. That’s a massive improvement percentage-wise.

    Expert Perspective: Achieving, say, a 6x speed-up by a few lines of code change is exactly why we do profiling. We targeted the low-hanging fruit that gave maximum gain. In the real world, such wins are gold – they might mean handling 6x more traffic on a website, or reducing cloud compute costs similarly.

1. Think about what was learned:

    This is more of a mental step. Consider how we found each issue and how we fixed it. The patterns you saw here (N+1 queries, needless loops, blocking calls) are very common. By going through this exercise, you’ve gained a heuristic: next time you see a for loop making a method call that hits data, you’ll recall “Hey, this could be an N+1 problem”. Or when you see Thread.Sleep in server-side code, alarm bells will ring.

1. (Optional) Discuss with Copilot:

    You can even have a debrief with Copilot Chat. Try asking, “Summarize how we improved the performance of this app.” Copilot might recap that you introduced caching for products, removed an unnecessary loop, and made the email async. This kind of summary can reinforce your understanding and ensure you didn’t miss any other potential improvements.

    Expert Tip: I often write design docs or code comments summarizing optimizations made, so the next maintainer knows why the code is the way it is. Using Copilot to draft such a summary (and then editing it) is a neat trick to save time.

By completing this lab, you’ve not only improved the ContosoOnlineStore app but also practiced a repeatable process:

- Profile (measure) first to find real bottlenecks.
- Use tools (timers, logs, Copilot’s analysis) to zero in on issues.
- Understand why the issue causes slowness (the “science” behind it).
- Fix it in a targeted way, keeping code quality high.
- Confirm the fix with testing and measurement.

This is exactly how a performance-minded senior engineer approaches optimization. And importantly, you leveraged GitHub Copilot as a partner in the process – to explain code, to suggest changes – showing how AI can augment your expertise. As Copilot’s own documentation notes, it can help identify hotspots and propose changes like better data structures or parallelism. You saw that in action.

Wrap-Up: The ContosoOnlineStore now runs faster and more efficiently. In a real deployment, users would experience snappier responses and the system would handle more load. In the next module (or lab) you might tackle the ContosoDataCrunch, which has its own set of performance issues. The approach will be similar, and the lessons transferrable. Profiling and optimization is a skill you’ll carry to any project you work on.

To conclude, here’s a final thought from our lead developer (your mentor persona):

“Always remember, make it right, then make it fast. We start with a working program (make it right), then we use profiling to find where ‘fast’ matters. Optimizing without data is guesswork – that’s the ‘premature optimization’ you want to avoid. But once you have data, don’t hesitate to improve the code. And nowadays, you have AI tools like Copilot to help brainstorm and even implement those improvements. It’s like having a colleague who’s seen all the best practices and is ready to offer them to you. Combine that with your own judgment, and you’ll be writing efficient, clean code in no time.”
