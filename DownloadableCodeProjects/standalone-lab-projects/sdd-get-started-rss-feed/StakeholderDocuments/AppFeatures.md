# App features

This RSS feed reader demonstrates the most basic functionality: subscribing to a feed and viewing its content.

## MVP scope (proof-of-concept version)

The MVP demonstrates the minimal viable workflow: subscribe to a feed and see its items.

For the MVP, the app MUST:

- Let a user add a feed subscription by pasting a feed URL
- Let a user manually refresh the feed to fetch and display items
- Display items with at least title and link
- Show a simple error message if the feed fails to load

For the MVP, the app MAY:

- Store data only in memory (data is lost when the app closes)
- Render items as plain text only (title + link)
- Show items in the order they appear in the feed (sorting is optional)

## MVP behavior

The MVP follows simple rules:

- Users manually click "refresh" to fetch feed content
- No automatic polling or background updates
- Basic error handling: show "Failed to load feed" if something goes wrong
- No throttling or rate limiting in MVP (keep it simple)

## Post-MVP features

After developing a successful MVP app, the following features could be considered for future versions:

### Essential improvements

- **Persistence**: Store subscriptions and items in a database so they remain available after restarting the app
- **Remove subscriptions**: Allow users to delete feeds
- **Better item display**: Show item summaries/content, not just titles
- **Newest-first sorting**: Display items in chronological order

### Additional capabilities

- **Background polling**: Automatically refresh feeds on a schedule
- **Read/unread tracking**: Mark items as read and filter by read status
- **Website-to-feed discovery**: Let users paste a website URL and automatically find its RSS feed
- **Folders/organization**: Group feeds into categories
- **Better error handling**: Show specific error messages (feed moved, access denied, malformed XML, etc.)
- **De-duplication**: Ensure the same item isn't stored multiple times
- **HTML rendering**: Safely display rich content from feeds

### Practical notes for developers

- Use simple in-memory storage for MVP (List/Dictionary in C#)
- Test with known-good feeds (e.g., <https://devblogs.microsoft.com/dotnet/feed/>)
- Avoid complex parsing edge cases in MVP - handle basic RSS/Atom formats only

## Additional features (longer-term)

If the app grows beyond a basic demonstration, these features could be considered:

- **Search and filtering**: Find items by keyword, filter by date or category
- **OPML import/export**: Transfer subscriptions between feed readers
- **Advanced organization**: Tags, saved items, priorities
- **Multi-device sync**: Share subscriptions and read state across devices
- **Notifications**: Alert on new items from important feeds
- **Integrations**: Share to email, chat tools, or read-later services
- **Offline reading**: Cache full article content for offline access
- **Mobile apps**: Native apps for phones and tablets
