from dataclasses import dataclass
from typing import Optional
from .author import Author

@dataclass
class Book:
    id: int
    title: str
    author_id: int
    genre: str
    image_name: str
    isbn: str
    author: Optional[Author] = None
