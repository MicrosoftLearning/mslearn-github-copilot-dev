# App features

This RSS feed reader demonstrates subscription management as the foundation for a feed reader application.

## MVP scope (proof-of-concept version)

The MVP demonstrates the minimal viable functionality: managing a subscription list.

For the MVP, the app MUST:

- Let a user add a feed subscription by pasting a feed URL
- Display the list of subscriptions in the UI

For the MVP, the app MAY:

- Store data only in memory (data is lost when the app closes)
- Accept any URL without validation (assume valid RSS/Atom feed URLs)
- Display subscriptions in a simple list format

## MVP behavior

The MVP follows simple rules:

- Users can add subscriptions by entering a URL
- The subscription list updates immediately when a subscription is added
- No feed fetching, parsing, or validation
- No error handling needed (no network operations)

## Extended-MVP features

After the basic MVP (subscription management) is working, the Extended-MVP adds feed fetching and display:

- **Manual refresh**: Users can click "refresh" to fetch feed content
- **Item display**: Show items with title and link
- **Basic error handling**: Show "Failed to load feed" if something goes wrong
- **No automatic polling**: Manual refresh only, no background updates

## Post-MVP features

After developing a successful Extended-MVP app, the following features could be considered for future versions:

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

**For MVP (subscription management only):**

- Use simple in-memory storage (List in C#)
- No need for feed parsing libraries yet
- No HTTP client needed for MVP
- Focus on basic UI and state management

**For Extended-MVP (add feed fetching):**

- Use `System.ServiceModel.Syndication` for parsing
- Test with known-good feeds (e.g., <https://devblogs.microsoft.com/dotnet/feed/>)
- Avoid complex parsing edge cases - handle basic RSS/Atom formats only

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
