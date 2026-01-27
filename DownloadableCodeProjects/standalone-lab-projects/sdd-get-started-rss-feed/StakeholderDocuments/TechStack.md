# Tech stack for RSS Feed Reader

Our RSS feed reader will use an ASP.NET Core Web API backend and a Blazor WebAssembly frontend. This combination allows for rapid development of the MVP while supporting future production-ready enhancements.

## Why ASP.NET Core Web API + Blazor WebAssembly?

Building an RSS feed reader with an **ASP.NET Core Web API** backend and a **Blazor WebAssembly** frontend offers several advantages:

1. **Quick Development**: Both technologies work well together with minimal setup, allowing for rapid development of the demonstration.

2. **Separation of Concerns**: The backend handles data management and (in Extended-MVP) feed operations, while the frontend focuses on user interaction.

3. **Cross-Platform**: Both ASP.NET Core and Blazor are cross-platform, allowing the application to run on Windows, macOS, and Linux.

4. **Incremental Complexity**: Start with simple subscription management (MVP), then add feed fetching (Extended-MVP), then add persistence and advanced features.

5. **Future-Ready Architecture**: While the MVP is minimal (just subscription list management), this architecture supports adding:

   - Feed fetching and parsing (`System.ServiceModel.Syndication`)
   - Database persistence (EF Core + SQLite)
   - Background processing (`BackgroundService` for polling)
   - Advanced features (read/unread, folders, etc.)

6. **Shared Code**: Blazor WebAssembly uses C#, allowing code sharing between frontend and backend if needed.

## Responsibilities

For the MVP (subscription management only):

**Backend** is responsible for:

- Exposing an API to add subscriptions
- Storing subscriptions in memory
- Returning the list of subscriptions

**Frontend** is responsible for:

- Subscription management UI (input field + add button)
- Displaying the list of subscriptions

For the Extended-MVP (add feed fetching):

**Backend** adds:

- Fetching and parsing RSS/Atom feeds when requested
- Returning feed items to the UI

**Frontend** adds:

- Manual refresh button
- Displaying items (title and link minimum)
- Basic error messages

## MVP-first implementation approach

To deliver the MVP quickly:

**MVP (subscription management only):**

- **Storage**: Use in-memory storage (List<string> or simple model). Subscriptions are lost when the app stops.
- **No feed operations**: No HTTP client, no parsing library, no feed fetching
- **Focus**: Basic UI and API communication (add subscription, get subscriptions list)

**Extended-MVP (add feed fetching):**

- **Parsing**: Add `System.ServiceModel.Syndication` for basic RSS/Atom parsing
- **HTTP client**: Add HttpClient for fetching feeds
- **Refresh**: Manual only - no background polling or scheduling
- **Error handling**: Simple "failed to load" messages, no detailed diagnostics
- **Content display**: Plain text only (title + link), no HTML rendering needed

This incremental approach makes development extremely fast while keeping the architecture clean for future enhancements.

## Local development

### Blazor project initialization

When creating a new Blazor WebAssembly project from the template, the project includes demonstration pages that must be removed to avoid conflicts with MVP features.

**⚠️ CRITICAL: This cleanup must be completed in Phase 2 (Foundational) and VERIFIED before any UI feature implementation begins. Runtime errors from incomplete cleanup will waste development time.**

**Required cleanup steps:**

1. **Remove template demo pages** from `frontend/[ProjectName].UI/Pages/`:
   - Delete `Home.razor` (conflicts with root route)
   - Delete `Counter.razor` (demo page)
   - Delete `Weather.razor` (demo page)

2. **Update navigation menu** in `frontend/[ProjectName].UI/Layout/NavMenu.razor`:
   - Remove navigation links to deleted demo pages
   - Update menu items to reflect MVP features only
   - Change root navigation link text to match your landing page (e.g., "Subscriptions")

3. **Verify routing**:
   - Ensure only ONE page uses `@page "/"` directive (your main MVP page)
   - All other pages should use unique routes (e.g., `@page "/settings"`)

4. **Verify cleanup completion** before proceeding with implementation:

   ```powershell
   # List all Razor pages - should show ONLY your MVP pages (e.g., NotFound.razor, Subscriptions.razor)
   Get-ChildItem frontend/[ProjectName].UI/Pages/ -Filter *.razor | Select-Object Name
   ```

   **STOP: Do not proceed with feature implementation until:**
   - ✗ Home.razor is GONE
   - ✗ Counter.razor is GONE  
   - ✗ Weather.razor is GONE
   - ✓ Only your MVP pages remain

5. **Test for routing conflicts immediately** after cleanup:

   ```powershell
   # Clean build to remove cached assemblies
   dotnet clean frontend/[ProjectName].UI/[ProjectName].UI.csproj
   dotnet build frontend/[ProjectName].UI/[ProjectName].UI.csproj
   
   # Start frontend to verify no routing errors
   dotnet run --project frontend/[ProjectName].UI
   ```

   Navigate to the frontend URL in your browser. If you see an "ambiguous route" error in the browser console (F12 Developer Tools), cleanup is incomplete. **Fix the issue before implementing any features.**

**Why this matters:**

Blazor templates include demonstration pages with pre-configured routes. If you create new pages with the same routes (especially the root route `/`), you'll encounter **ambiguous route exceptions** at runtime. The error message will look like:

```
System.InvalidOperationException: The following routes are ambiguous:
'' in '[ProjectName].UI.Pages.Home'
'' in '[ProjectName].UI.Pages.YourFeature'
```

These errors only appear at runtime after you've already implemented features, making them costly to debug. The verification steps above catch this issue immediately during Phase 2 cleanup, before any feature work begins.

Cleaning up template pages before implementing MVP features prevents these conflicts and ensures a clean project structure focused on business requirements.

### Port configuration

The backend API and frontend UI run on separate localhost ports. **Port consistency is critical** - the ports must be coordinated between three locations:

1. **Backend port** (defined in `backend/RSSFeedReader.Api/Properties/launchSettings.json`):

   - Default: `http://localhost:5151`
   - This is where the API listens for requests

2. **Frontend port** (defined in `frontend/RSSFeedReader.UI/Properties/launchSettings.json`):

   - Default: `http://localhost:5213`
   - This is where the Blazor app runs

3. **API base URL** (configured in `frontend/RSSFeedReader.UI/wwwroot/appsettings.json`):

   - Must match the backend port from step 1
   - Example: `{"ApiBaseUrl": "http://localhost:5151/api/"}`

4. **CORS policy** (configured in `backend/RSSFeedReader.Api/Program.cs`):

   - Must allow the frontend port from step 2
   - Example: `.WithOrigins("http://localhost:5213", "https://localhost:7025")`

### Configuration best practices

- **Frontend Program.cs**: Read API URL from configuration, don't hardcode:

  ```csharp
  var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5151/api/";
  builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBaseUrl) });
  ```

- **Backend CORS**: Allow the actual frontend ports from launchSettings.json

- **Testing setup**: Before testing, verify:

  1. Backend is running and accessible at the configured port
  2. Frontend appsettings.json points to the correct backend port
  3. CORS allows the frontend origin

**For MVP:** Test by adding subscription URLs and verifying they appear in the list.

**For Extended-MVP:** Test with a known-good feed like <https://devblogs.microsoft.com/dotnet/feed/>

## Future enhancements (post-MVP)

When ready to extend beyond the basic demonstration, this architecture supports:

- **Database persistence**: Add EF Core + SQLite for storing subscriptions and items between sessions
- **Background polling**: Implement `BackgroundService` to automatically refresh feeds on a schedule
- **HTML sanitization**: Add `HtmlSanitizer` library to safely display rich content from feeds
- **Website-to-feed discovery**: Use `HtmlAgilityPack` to find feed URLs from website links
- **Better error handling**: Implement retry logic, timeouts, and detailed error messages
- **Testing**: Add unit and integration tests using xUnit
- **Optimization**: Implement HTTP caching (ETag/Last-Modified), de-duplication, and performance improvements

## Summary

ASP.NET Core Web API with Blazor WebAssembly provides a straightforward path to building the RSS feed reader incrementally:

- **MVP**: Subscription management only (add + list) - extremely simple, no feed operations
- **Extended-MVP**: Add feed fetching and item display - still simple with in-memory storage and manual refresh
- **Future**: Add persistence, background processing, and advanced features

The architecture is intentionally minimal to enable fast development, while the technology choices support adding production-ready features later without requiring a complete rewrite.
