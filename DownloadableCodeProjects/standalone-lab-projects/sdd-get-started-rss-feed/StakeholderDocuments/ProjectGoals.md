# Project goals

Build a simple RSS/Atom feed reader. The goal is to demonstrate the most basic capability (managing a subscription list) without the complexity of fetching and displaying feed content.

## Purpose

The app exists to demonstrate how a user can build a subscription list for RSS feeds. This is a proof-of-concept focused on the subscription management UI.

## Target scope (MVP only)

This is a minimal POC application for a single user, running locally. It is designed to be developed and tested on Windows, macOS, or Linux.

The MVP includes only:

- Adding a feed subscription by URL
- Displaying the list of subscriptions in the UI

All other features (fetching feeds, displaying items, persistence, removing subscriptions, etc.) are deferred to Extended-MVP or post-MVP.

## Delivery approach

The focus is on rapid development of the MVP feature. Build the minimal functionality first:

- Add a subscription by URL
- Display the list of subscriptions

To keep development fast:

- No feed fetching or parsing needed for MVP
- No validation of feed URLs (assume user provides valid URLs)
- Store subscriptions in memory only (simplest approach)
- Keep the UI simple and functional rather than polished

## What "MVP working" means

The MVP is complete when:

1. A user can add a feed subscription by pasting a URL
2. The UI displays the updated list of subscriptions

No actual feed fetching, parsing, or item display is required for MVP.

## Extended-MVP (next phase)

After the basic MVP is working, the Extended-MVP adds feed fetching and display capabilities:

1. A user can click a button to manually refresh the feed
2. Items from the feed are displayed (title and link minimum)

Test with a known-good RSS feed like <https://devblogs.microsoft.com/dotnet/feed/>.

### Local development checklist

Before testing the MVP, verify:

- [ ] Backend runs without errors and listens on the configured port
- [ ] Frontend runs without errors and loads in the browser
- [ ] Frontend configuration (`wwwroot/appsettings.json`) points to the correct backend URL
- [ ] Backend CORS allows the frontend origin
- [ ] Browser DevTools console shows no connection errors when loading the page

## Future enhancements (post-MVP)

Once the Extended-MVP is working (subscription management + feed fetching + item display), these features could be added:

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
