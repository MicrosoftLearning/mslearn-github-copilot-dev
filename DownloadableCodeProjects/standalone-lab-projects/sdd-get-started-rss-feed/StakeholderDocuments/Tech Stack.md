# Tech stack for RSS Feed Reader

Our RSS feed reader will use an ASP.NET Core Web API backend (with BackgroundService poller + EF Core + SQLite) and a Blazor WebAssembly frontend. 

See our reasoning below.

## Why ASP.NET Core Web API + Blazor WebAssembly?

Building an RSS feed reader with an **ASP.NET Core Web API** backend and a **Blazor WebAssembly** frontend offers several advantages:

1. **Separation of Concerns**: The backend handles feed fetching, parsing, and storage, while the frontend focuses on user interaction and presentation. This separation makes the application easier to maintain and extend.
2. **Cross-Platform**: Both ASP.NET Core and Blazor are cross-platform, allowing the application to run on Windows, macOS, and Linux without modification.
3. **Background Processing**: Using `BackgroundService` in ASP.NET Core allows for efficient background polling of RSS feeds, ensuring that the application can fetch updates without blocking user interactions.
4. **Data Persistence**: EF Core with SQLite provides a lightweight and efficient way to store feed data locally, making it easy to manage subscriptions and read states.
5. **Reliable Feed Parsing**: RSS/Atom feeds vary a lot in the real world. We can use .NET’s XML capabilities along with a dedicated RSS/Atom parsing approach (library or built-in syndication APIs) to handle malformed feeds and format variations gracefully.
6. **Safe Content Rendering**: Feeds often include HTML. We will sanitize and safely render feed content so the UI does not display unsafe or unexpected markup.
7. **Search Path (Simple → Powerful)**: We can start with basic searching over stored fields (title/summary) and later add SQLite full-text search (FTS) if we need fast, scalable search.
8. **Rich UI with Blazor**: Blazor WebAssembly allows for building interactive web applications using C#. This means you can share code between the frontend and backend, reducing duplication and improving development speed.
9. **Offline-Friendly Path**: Blazor WebAssembly can later be configured as a PWA (service worker) so the app loads reliably and we can expand offline reading capabilities over time.
10. **Scalability and Future Growth**: ASP.NET Core is designed for high performance and scalability, making it suitable for handling many subscriptions and concurrent users if needed later. If we later add multi-device sync, notifications, or integrations, the same Web API can be extended (or hosted remotely) without changing the core domain model.

## Responsibilities

To keep the system maintainable, we will use a clear separation of responsibilities:

The backend is responsible for feed discovery, polling and fetching feeds, parsing RSS/Atom, de-duplication, persistence to SQLite, and exposing APIs that the UI uses.

The frontend is responsible for the subscription management experience (add/remove), triggering “refresh now”, presenting lists and item views, and displaying content safely.

## Summary

In summary, using ASP.NET Core Web API with Blazor WebAssembly provides a powerful, flexible, and efficient architecture for building an RSS feed reader that can meet both current and future needs.
