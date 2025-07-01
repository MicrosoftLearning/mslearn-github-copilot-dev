class BookRepository:
    def __init__(self, json_data):
        self.books = json_data.get('books', [])
        self.authors = json_data.get('authors', [])
        self.book_items = json_data.get('book_items', [])

    def get_book_by_title(self, title):
        for book in self.books:
            if book.title.lower() == title.lower():
                return book
        return None

class BookItemRepository:
    def __init__(self, json_data):
        self.books = json_data.get('books', [])
        self.authors = json_data.get('authors', [])
        self.book_items = json_data.get('book_items', [])

    def get_book_by_title(self, title):
        for book in self.books:
            if book.title.lower() == title.lower():
                return book
        return None

    def get_items_by_book_id(self, book_id):
        items = []
        for item in self.book_items:
            if item.book_id == book_id:
                items.append(item)
        return items