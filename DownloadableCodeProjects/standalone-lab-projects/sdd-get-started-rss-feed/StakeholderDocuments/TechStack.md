# Tech stack for RSS Feed Reader

Our RSS feed reader will use an ASP.NET Core Web API backend and a Blazor WebAssembly frontend. This combination allows for rapid development of the MVP while supporting future production-ready enhancements.

## Why ASP.NET Core Web API + Blazor WebAssembly?

Building an RSS feed reader with an **ASP.NET Core Web API** backend and a **Blazor WebAssembly** frontend offers several advantages:

1. **Quick Development**: Both technologies work well together with minimal setup, allowing for rapid development of the demonstration.
2. **Separation of Concerns**: The backend handles feed fetching and parsing, while the frontend focuses on user interaction. This keeps the code organized even in a simple app.
3. **Cross-Platform**: Both ASP.NET Core and Blazor are cross-platform, allowing the application to run on Windows, macOS, and Linux.
4. **Future-Ready Architecture**: While the MVP is simple (in-memory storage, manual refresh only), this architecture supports adding:
   - Database persistence (EF Core + SQLite)
   - Background processing (`BackgroundService` for polling)
   - Advanced features (read/unread, folders, etc.)
5. **Feed Parsing**: .NET's `System.ServiceModel.Syndication` provides built-in RSS/Atom parsing capabilities.
6. **Shared Code**: Blazor WebAssembly uses C#, allowing code sharing between frontend and backend if needed.

## Responsibilities

For the MVP:

**Backend** is responsible for:

- Exposing an API to add subscriptions
- Fetching and parsing RSS/Atom feeds when requested
- Returning feed items to the UI

**Frontend** is responsible for:

- Subscription management UI (add feed by URL)
- Manual refresh button
- Displaying items (title and link minimum)
- Basic error messages

## MVP-first implementation approach

To deliver the demonstration quickly:

- **Storage**: Use in-memory storage (no database) for MVP. Subscriptions and items are lost when the app stops.
- **Refresh**: Manual only - no background polling or scheduling
- **Parsing**: Use `System.ServiceModel.Syndication` for basic RSS/Atom parsing
- **Error handling**: Simple "failed to load" messages, no detailed diagnostics
- **Content display**: Plain text only (title + link), no HTML rendering needed

These choices make development fast while keeping the architecture clean for future enhancements.

## Local development

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
  
- Test with a known-good feed like <https://devblogs.microsoft.com/dotnet/feed/>

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

ASP.NET Core Web API with Blazor WebAssembly provides a straightforward path to building the RSS feed reader MVP quickly. The architecture is intentionally simple (in-memory storage, manual operations) to enable fast development, while the technology choices support adding production-ready features (persistence, background processing, advanced UI) later without requiring a complete rewrite.
