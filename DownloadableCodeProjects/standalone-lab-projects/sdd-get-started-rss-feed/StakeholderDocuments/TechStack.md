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

- The backend API and frontend UI run on separate localhost ports
- The UI needs to know the API URL (configure via `ApiBaseUrl` setting)
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
