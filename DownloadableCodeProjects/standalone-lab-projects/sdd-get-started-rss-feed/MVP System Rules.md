# MVP system rules (for specs)

This is a compact reference for system behavior decisions that support the MVP described in [App Features.md](App%20Features.md). It is meant to be used as input when writing GitHub Spec Kit specs.

## Scope

The MVP is a single-user, local-only RSS/Atom feed reader.

It must support the full workflow end-to-end: subscribe → fetch → store → read.

## Out of scope (for MVP)

The MVP intentionally does not include:

- Accounts, authentication, or multi-user support
- Multi-device sync
- Mobile-first UX or native mobile apps
- Full offline mode (beyond “items already stored locally remain readable”)
- Full-text fetching/readability mode
- Notifications
- OPML import/export
- Advanced filtering/rules engine

Implementation responsibilities are described at a high level in [Tech Stack.md](Tech%20Stack.md).

## Polling and refresh rules

To keep behavior predictable, the MVP should follow simple polling rules:

- Manual refresh (“refresh now”) fetches the selected feed immediately.
- Background polling fetches subscribed feeds periodically while the backend is running.
- Feeds should not be fetched repeatedly in a tight loop; if a feed was fetched very recently, the backend should throttle additional fetch attempts.
- Network calls should have timeouts so a single slow feed does not stall the system.

In MVP these values are not user-configurable; they can be hard-coded:

- Background polling interval: 30 minutes
- Manual refresh throttle per feed: 60 seconds
- HTTP request timeout: 10 seconds
- Max concurrent feed fetches: 5

If a feed returns caching headers (ETag / Last-Modified), the backend may use conditional requests to reduce bandwidth, but this is an optimization and not required for MVP.

## Feed discovery expectations

When a user pastes a website URL, discovery should attempt to find the site’s RSS/Atom feed by:

- Looking for standard feed link tags in the HTML (RSS/Atom alternates)
- Handling redirects

If discovery fails, the UI should tell the user that a feed could not be found and suggest pasting a feed URL directly.

## De-duplication expectations

The system should avoid storing the same feed item multiple times within a feed.

- If the feed provides a stable unique identifier (GUID/id), that should be used to detect duplicates.
- If not, the system can fall back to link-based or a reasonable composite identity (for example, link + published date).

## Data portability (MVP)

In MVP, portability is local-first:

- The SQLite database file is the primary storage, so a user can back up or move their data by copying that file.
- Formal export formats (such as OPML) are planned for later iterations.

## Safety and rendering expectations

- HTTPS feeds must be supported.
- Feed content that includes HTML must be rendered safely (sanitized) so it cannot execute scripts or inject unsafe markup.
- Malformed feeds and unexpected XML should not crash the backend; failures should be captured as a per-feed error state.

Completion criteria for the MVP live in the “MVP definition of done” section of [App Features.md](App%20Features.md).

## Notes for future iterations

When moving beyond MVP, this document should be expanded (or replaced by additional specs) to cover:

- Search and filtering (and whether SQLite FTS is used)
- OPML import/export and data portability
- Smarter throttling/backoff strategies
- Offline/PWA behavior
- Sync and multi-device support
