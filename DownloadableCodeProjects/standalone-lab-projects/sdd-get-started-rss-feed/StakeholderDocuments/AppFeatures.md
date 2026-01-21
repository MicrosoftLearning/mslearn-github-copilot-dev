# App features

This RSS feed reader is designed around three themes: making it easy to subscribe to sources, making it easy to keep up with new items, and giving you control so your feed doesn't become overwhelming.

## MVP cutline (deliver first)

The MVP is the smallest end-to-end slice that proves the core workflow (subscribe → fetch → store → read) with minimal moving parts.

For the MVP, the app MUST:

- Let a user add a feed subscription by pasting a feed URL (and remove it later).
- Let a user manually refresh a feed and see a newest-first list of items.
- Let a user open an item's original link.
- Store subscriptions and items locally so they remain available after restart.
- Show clear per-feed errors (fetch/parse/etc.) without crashing.

For the MVP, the app MAY:

- Render item content as plain text only (title + summary) to reduce early complexity.
- Treat duplicate items as a later hardening step (best-effort is acceptable initially).

## MVP behavior rules (defaults)

To keep behavior predictable and avoid "mystery waiting", the MVP follows a few simple rules:

- Refresh is manual: clicking "refresh now" fetches the selected feed immediately.
- The system should not fetch a feed repeatedly in a tight loop. If a feed was refreshed very recently, the backend should throttle additional refresh attempts.
- Network calls must have timeouts so one slow feed does not stall the system.

In MVP these values are not user-configurable; they can be hard-coded:

- Manual refresh throttle per feed: 60 seconds
- HTTP request timeout: 10 seconds
- Max concurrent feed fetches: 5

Background polling and scheduling are intentionally post-MVP.

## V1 scope (post-MVP, single-user, local-only)

At a minimum, the app will let a single user subscribe to RSS/Atom feeds, fetch updates, and read items with a clean read/unread workflow. Subscriptions can be managed directly, including removing feeds you no longer want.

The app will support adding feeds in two ways: by pasting a feed URL directly, or by pasting a website URL and letting the app discover the site's feed automatically. Users will be able to manually fetch updates with a "refresh now" action, and the backend will also poll feeds in the background on a schedule.

All data is stored locally. Feeds and feed items are persisted in a local database, so items remain available between sessions and read/unread state is remembered.

The reading experience includes an items list per feed (with newest items first) and an item view that shows the title and summary/content snippet, with an option to open the original article link. Users can mark individual items as read and also mark all items as read for a feed.

Reliability and safety are part of the must-haves. The app should handle common real-world feed issues (redirects, timeouts, and malformed XML) without crashing, and it should show clear error messages when something goes wrong (for example, if a feed moved or access is forbidden). The app should avoid storing the same item multiple times (basic de-duplication). Connections should work over HTTPS, and any HTML from feeds should be rendered safely (sanitized) so it cannot cause unsafe behavior in the UI.

Finally, the app will support basic organization by allowing feeds to be grouped into folders/collections (for example: News, Tech, Work).

### Website-to-feed discovery behavior (post-MVP)

When a user pastes a website URL (not a direct feed URL), the system should attempt to discover the site's RSS/Atom feed by:

- Following redirects.
- Looking for standard feed link tags in the HTML (for example `<link rel="alternate">` RSS/Atom entries).

If discovery fails, the UI should tell the user a feed could not be found and suggest pasting a direct feed URL.

### De-duplication behavior

The system should avoid storing the same item multiple times within a feed. For the MVP, best-effort de-duplication is acceptable; stronger guarantees are post-MVP.

When computing identity:

- Prefer a stable unique identifier from the feed (GUID/id) when present.
- Otherwise fall back to a reasonable composite (for example link + published date).

### HTTP caching optimization (optional)

If a feed returns caching headers (ETag / Last-Modified), the backend may use conditional requests to reduce bandwidth. This is an optimization and not required for the MVP.

### MVP (build this first)

For the first working version, we will focus on the MVP cutline described above.

Sequencing: deliver the manual end-to-end flow first (subscribe → refresh → items). Only once that success-path works reliably should we add discovery, background polling, read/unread, folders, and other "daily use" features.

For the MVP, we are intentionally keeping scope tight. We will not build accounts, sync, notifications, OPML import/export, advanced filtering rules, background polling, discovery, folders, or read/unread until the core reader experience is stable.

### MVP definition of done

The MVP is complete when the following are true:

- A user can add a feed by URL and remove it later.
- A user can refresh a feed on demand and see newly fetched items.
- Items are stored locally and remain available after restarting the app.
- The UI shows items per feed in newest-first order.
- A user can open the original article link.
- Feed failures show clear errors and do not crash the app.

The following are explicitly post-MVP (V1) features:

- Background polling
- Website-to-feed discovery
- Read/unread and mark-all-read
- Folders/collections
- Strong de-duplication guarantees

## Practical notes for running the MVP (developer-facing)

- The UI and API run on different localhost ports; that is expected. The UI must be configured with the API base URL.
- Items appear only after a refresh has successfully run at least once for that subscription.
- Feed URLs should be treated as user input: trim whitespace and normalize before storing to avoid hard-to-debug issues (for example, a trailing space).

### MVP-first safety fallback

- If HTML sanitization introduces dependency/tooling friction, MVP is allowed to render **plain text only** (title + summary + original link) and defer HTML rendering until the dependency story is stable.

### Newest-first ordering note (SQLite)

- Newest-first should be defined as: order by `PublishedAt` (if present) then fallback to the stored `CreatedAt`.
- When implementing with EF Core + SQLite, be mindful of provider translation limitations for timestamp types (especially `DateTimeOffset`) in `ORDER BY`.

## Practical verification flow (developer-facing)

Use at least one known-good feed for the first success-path verification before trying stricter or rate-limited sources.

Example "known-good" feeds:

- <https://devblogs.microsoft.com/dotnet/feed/>
- <https://devblogs.microsoft.com/visualstudio/feed/>
- <https://devblogs.microsoft.com/powershell/feed/>

Golden path:

1. Start the API and UI.
2. Add the feed URL.
3. Click refresh.
4. Verify items appear newest-first.
5. Open an item's original link.

Some popular sources may block or rate-limit automated clients (commonly returning 403/429 or HTML instead of RSS). Treat those as robustness tests, not the first success-path test.

## Nice to have features

Once the core reader experience is stable, we can improve organization and reading flow with tags/labels, saved/starred ("read later") items, and strong keyboard shortcuts. We can also offer multiple layouts (compact list, magazine/cards, etc.) so users can choose how dense the UI feels.

Search and filtering is another high-impact upgrade. This includes fast searching across titles and summaries (and potentially full text later), along with rule-based filtering such as including/excluding keywords, authors, or categories. Users may want "mute" rules to hide items containing certain terms, and priority/highlighting rules to surface important items.

We can also improve freshness controls by allowing configurable refresh intervals, a clearer "refresh now" experience, and smarter backoff/throttling when feeds are failing or rate limiting.

For portability, we can add OPML import/export so users can move subscriptions between readers, and we can detect duplicate feeds and help merge them.

## Features that could be added later

If the app grows beyond a single-device local-only reader, multi-device sync can be added so subscriptions, read state, and saved items follow the user across devices. Offline reading can be expanded by explicitly caching items and content for use without connectivity.

This would also open the door to first-class web and mobile support, where the same account/data can be used from a browser and mobile devices.

Content quality can also be improved with full-text fetching (readability mode) when feeds only provide excerpts, and better media handling for images and enclosures such as podcasts or YouTube feeds.

Notifications can be added later for high-priority feeds or keyword matches.

Integrations and sharing features can come later as well. This includes sharing items to email and chat tools (for example Slack/Teams), sending items to read-later services (for example Pocket/Instapaper/OneNote), and offering quick actions like copying a clean link. It can also include opening links in the system browser or in an in-app browser view. For some workflows, RSS-to-email could be added so selected feeds (or filtered items) are forwarded to an email inbox.

Finally, as privacy and data ownership features mature, we can explicitly focus on minimal tracking and clear data ownership/export (for example, ensuring users can always export their data in a usable form).
