# Project goals

Build a simple RSS/Atom feed reader. The goal is to demonstrate the most basic capability (subscribe and view items) without the complexity of a production-ready application.

## Purpose

The app exists to demonstrate how a user can subscribe to RSS feeds and view their content in one place. This is a proof-of-concept focused on showing the core functionality quickly.

## Target scope (MVP only)

This is a minimal POC application for a single user, running locally. It is designed to be developed and tested on Windows, macOS, or Linux.

The MVP includes only:

- Adding a feed subscription by URL
- Manually refreshing a subscription to see items

All other features (persistence between sessions, removing subscriptions, background polling, etc.) are deferred to post-MVP.

## Delivery approach

The focus is on rapid development of the MVP features. Build the minimal functionality first:

- Add a subscription by URL
- Manual refresh to fetch and display items

To keep development fast:

- Use a known-good feed for testing (e.g., a Microsoft DevBlog feed)
- Avoid complex error handling beyond basic "something went wrong" messages
- Skip features that add complexity (persistence, background operations, extensive validation)
- Keep the UI simple and functional rather than polished

## What "MVP working" means

The MVP is complete when:

1. A user can add a feed subscription by pasting a URL
2. A user can click a button to manually refresh the feed
3. Items from the feed are displayed (title and link minimum)

Test with a known-good RSS feed like <https://devblogs.microsoft.com/dotnet/feed/>.

### Local development checklist

Before testing the MVP, verify:

- [ ] Backend runs without errors and listens on the configured port
- [ ] Frontend runs without errors and loads in the browser
- [ ] Frontend configuration (`wwwroot/appsettings.json`) points to the correct backend URL
- [ ] Backend CORS allows the frontend origin
- [ ] Browser DevTools console shows no connection errors when loading the page

## Future enhancements (post-MVP)

Once the basic demonstration is working, these features could be added:

- **Persistence**: Save subscriptions and items between sessions (requires database implementation)
- **Remove subscriptions**: Allow users to delete feeds they no longer want
- **Background polling**: Automatically refresh feeds on a schedule
- **Better error handling**: Show detailed error messages for different failure scenarios
- **Content rendering**: Display full item content, not just title and link
- **Read/unread tracking**: Mark items as read and filter accordingly
- **Organization**: Group feeds into folders or categories

## Technology selection note

While this MVP is intentionally simple, the technology choices (ASP.NET Core + Blazor) should support future production-ready features without requiring a complete rewrite. The architecture allows for adding persistence, background operations, and enhanced UI capabilities as needed.

## How this document fits with the others

- [AppFeatures.md](AppFeatures.md) describes the specific user-facing features for the MVP
- [TechStack.md](TechStack.md) explains the technology choices and how they support the MVP goals
