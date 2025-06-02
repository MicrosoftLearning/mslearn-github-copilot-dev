from dataclasses import dataclass
from typing import Optional
from datetime import datetime
from .book import Book

@dataclass
class BookItem:
    id: int
    book_id: int
    acquisition_date: datetime
    condition: Optional[str] = None
    book: Optional[Book] = None
