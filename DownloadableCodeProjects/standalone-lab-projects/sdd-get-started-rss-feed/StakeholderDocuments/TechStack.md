# Tech stack for RSS Feed Reader

Our RSS feed reader will use an ASP.NET Core Web API backend (with BackgroundService poller + EF Core + SQLite) and a Blazor WebAssembly frontend.

See our reasoning below.

## Why ASP.NET Core Web API + Blazor WebAssembly?

Building an RSS feed reader with an **ASP.NET Core Web API** backend and a **Blazor WebAssembly** frontend offers several advantages:

1. **Separation of Concerns**: The backend handles feed fetching, parsing, and storage, while the frontend focuses on user interaction and presentation. This separation makes the application easier to maintain and extend.
2. **Cross-Platform**: Both ASP.NET Core and Blazor are cross-platform, allowing the application to run on Windows, macOS, and Linux without modification.
3. **Background Processing (post-MVP)**: Using `BackgroundService` in ASP.NET Core allows for efficient background polling of RSS feeds, ensuring that the application can fetch updates without blocking user interactions.
4. **Data Persistence**: EF Core with SQLite provides a lightweight and efficient way to store feed data locally, making it easy to manage subscriptions and read states.
5. **Reliable Feed Parsing**: RSS/Atom feeds vary a lot in the real world. We can use .NET’s XML capabilities along with a dedicated RSS/Atom parsing approach (library or built-in syndication APIs) to handle malformed feeds and format variations gracefully.
6. **Safe Content Rendering**: Feeds often include HTML. We will sanitize and safely render feed content so the UI does not display unsafe or unexpected markup.
7. **Search Path (Simple → Powerful)**: We can start with basic searching over stored fields (title/summary) and later add SQLite full-text search (FTS) if we need fast, scalable search.
8. **Rich UI with Blazor**: Blazor WebAssembly allows for building interactive web applications using C#. This means you can share code between the frontend and backend, reducing duplication and improving development speed.
9. **Offline-Friendly Path**: Blazor WebAssembly can later be configured as a PWA (service worker) so the app loads reliably and we can expand offline reading capabilities over time.
10. **Scalability and Future Growth**: ASP.NET Core is designed for high performance and scalability, making it suitable for handling many subscriptions and concurrent users if needed later. If we later add multi-device sync, notifications, or integrations, the same Web API can be extended (or hosted remotely) without changing the core domain model.

## Responsibilities

To keep the system maintainable, we will use a clear separation of responsibilities:

The backend is responsible for fetching feeds, parsing RSS/Atom, persistence to SQLite, and exposing APIs that the UI uses. Discovery and background polling are post-MVP capabilities.

The frontend is responsible for the subscription management experience (add/remove), triggering “refresh now”, presenting lists and item views, and displaying content safely.

## MVP-first implementation guidance

To keep the MVP deliverable and the work breakdown small, the first implementation slice should:

- Prefer manual refresh over background polling (scheduling/concurrency is post-MVP).
- Prefer direct feed URLs over website-to-feed discovery (discovery edge cases are post-MVP).
- Prefer rendering safe plain text (title/summary/link) first; add sanitized HTML rendering as a follow-on.

These are intentional “speed wins” that preserve the long-term architecture without requiring a rewrite later.

## Local development expectations

- The backend API and Blazor WebAssembly UI will run on different localhost ports.
- The UI should read an `ApiBaseUrl` (or equivalent) from configuration so developers can point it at the current backend URL without code changes.

## Source of truth

When planning work or updating requirements, treat these repo facts as the “source of truth” so documents don’t have to guess (or invent file paths).

### Repo conventions

- .NET SDK is pinned in [global.json](../global.json) (repo targets .NET 10).
- Backend solution: [backend/RssFeedReader.sln](../backend/RssFeedReader.sln)
- Backend projects live under `backend/src/` and tests under `backend/tests/`.
- Frontend project lives under `frontend/src/`.

### Local dev ports and configuration

- API default URLs are defined in [backend/src/RssFeedReader.Api/Properties/launchSettings.json](../backend/src/RssFeedReader.Api/Properties/launchSettings.json)
  - HTTP: <http://localhost:5000>
  - HTTPS: <https://localhost:5001>
- UI default URLs are defined in [frontend/src/RssFeedReader.Ui/Properties/launchSettings.json](../frontend/src/RssFeedReader.Ui/Properties/launchSettings.json)
  - HTTP: <http://localhost:5240>
  - HTTPS: <https://localhost:7222>
- UI → API wiring uses `ApiBaseUrl` from [frontend/src/RssFeedReader.Ui/wwwroot/appsettings.Development.json](../frontend/src/RssFeedReader.Ui/wwwroot/appsettings.Development.json) and applies it in [frontend/src/RssFeedReader.Ui/Program.cs](../frontend/src/RssFeedReader.Ui/Program.cs).
- Dev-only CORS is configured in [backend/src/RssFeedReader.Api/Program.cs](../backend/src/RssFeedReader.Api/Program.cs) (allows localhost/127.0.0.1 origins).

### Persistence and data location

- Storage is SQLite via EF Core.
- Default DB location (relative to backend content root): `app_data/rssfeedreader.db` (see [backend/src/RssFeedReader.Api/Program.cs](../backend/src/RssFeedReader.Api/Program.cs)).
- Database schema is managed via EF Core migrations and applied on startup (also in [backend/src/RssFeedReader.Api/Program.cs](../backend/src/RssFeedReader.Api/Program.cs)).
- Practical backup/portability for MVP: copy the SQLite DB file.

### Key dependencies (what we actually use)

- Feed parsing: `System.ServiceModel.Syndication` (see [backend/src/RssFeedReader.Infrastructure/RssFeedReader.Infrastructure.csproj](../backend/src/RssFeedReader.Infrastructure/RssFeedReader.Infrastructure.csproj)).
- HTML sanitization: `HtmlSanitizer` via `HtmlSanitizerService` (see [backend/src/RssFeedReader.Infrastructure/Sanitization/HtmlSanitizerService.cs](../backend/src/RssFeedReader.Infrastructure/Sanitization/HtmlSanitizerService.cs)).
- HTTP fetching: typed client `FeedHttpClient` (see [backend/src/RssFeedReader.Infrastructure/Http/FeedHttpClient.cs](../backend/src/RssFeedReader.Infrastructure/Http/FeedHttpClient.cs)).
  - Optional optimization: conditional requests using ETag / Last-Modified when available.
- Website-to-feed discovery (post-MVP): `HtmlAgilityPack` via `FeedDiscoveryService` (see [backend/src/RssFeedReader.Infrastructure/Discovery/FeedDiscoveryService.cs](../backend/src/RssFeedReader.Infrastructure/Discovery/FeedDiscoveryService.cs)).

### Testing conventions

- Tests are xUnit-based.
- API integration tests use `WebApplicationFactory` and avoid public internet by injecting `FakeHttpMessageHandler`:
  - [backend/tests/RssFeedReader.Api.Tests/TestInfrastructure/TestWebApplicationFactory.cs](../backend/tests/RssFeedReader.Api.Tests/TestInfrastructure/TestWebApplicationFactory.cs)
  - [backend/tests/RssFeedReader.Api.Tests/TestDoubles/FakeHttpMessageHandler.cs](../backend/tests/RssFeedReader.Api.Tests/TestDoubles/FakeHttpMessageHandler.cs)
- Default test command: `dotnet test backend/RssFeedReader.sln`

### API surface area (where endpoints live)

- Subscriptions endpoints: [backend/src/RssFeedReader.Api/Endpoints/SubscriptionsEndpoints.cs](../backend/src/RssFeedReader.Api/Endpoints/SubscriptionsEndpoints.cs)
- Items endpoints: [backend/src/RssFeedReader.Api/Endpoints/ItemsEndpoints.cs](../backend/src/RssFeedReader.Api/Endpoints/ItemsEndpoints.cs)

## Version alignment note (Blazor)

The project targets .NET 10. When customizing routing (for example, `App.razor`), prefer the default .NET 10 Blazor template patterns and avoid introducing Router parameters/components that are not supported by the target framework.

## Summary

In summary, using ASP.NET Core Web API with Blazor WebAssembly provides a powerful, flexible, and efficient architecture for building an RSS feed reader that can meet both current and future needs.
