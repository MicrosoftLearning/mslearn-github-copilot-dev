from dataclasses import dataclass
from typing import Optional
from datetime import datetime
from .patron import Patron
from .book_item import BookItem

@dataclass
class Loan:
    id: int
    book_item_id: int
    patron_id: int
    patron: Optional[Patron] = None
    loan_date: datetime = None
    due_date: datetime = None
    return_date: Optional[datetime] = None
    book_item: Optional[BookItem] = None
