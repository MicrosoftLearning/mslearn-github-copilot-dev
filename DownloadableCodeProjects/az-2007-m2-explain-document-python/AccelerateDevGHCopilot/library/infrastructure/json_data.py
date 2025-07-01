import json
import os
from pathlib import Path
from application_core.entities.author import Author
from application_core.entities.book import Book
from application_core.entities.book_item import BookItem
from application_core.entities.patron import Patron
from application_core.entities.loan import Loan
from typing import List, Optional
from datetime import datetime

class JsonData:
    def __init__(self):
        # Get the absolute path to the project root
        self.project_root = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
        self.json_dir = os.path.join(self.project_root, "infrastructure", "Json")
        self.authors_path = os.path.join(self.json_dir, "Authors.json")
        self.books_path = os.path.join(self.json_dir, "Books.json")
        self.book_items_path = os.path.join(self.json_dir, "BookItems.json")  # <-- Add this line
        self.patrons_path = os.path.join(self.json_dir, "Patrons.json")
        self.loans_path = os.path.join(self.json_dir, "Loans.json")
        self.authors: List[Author] = []
        self.books: List[Book] = []
        self.book_items: List[BookItem] = []
        self.patrons: List[Patron] = []
        self.loans: List[Loan] = []
        self._loaded = False
        self.load_data()

    def _parse_datetime(self, value: Optional[str]) -> Optional[datetime]:
        if value is None:
            return None
        return datetime.fromisoformat(value)

    def load_data(self) -> None:
        try:
            with open(self.authors_path, encoding='utf-8') as f:
                authors_data = json.load(f)
                self.authors = [Author(id=a['Id'], name=a['Name']) for a in authors_data]
            with open(self.books_path, encoding='utf-8') as f:
                books_data = json.load(f)
                self.books = [Book(id=b['Id'], title=b['Title'], author_id=b['AuthorId'], genre=b['Genre'], image_name=b['ImageName'], isbn=b['ISBN']) for b in books_data]
            with open(self.book_items_path, encoding='utf-8') as f:  # <-- Fix here
                items_data = json.load(f)
                self.book_items = [BookItem(id=bi['Id'], book_id=bi['BookId'], acquisition_date=self._parse_datetime(bi['AcquisitionDate']), condition=bi.get('Condition')) for bi in items_data]
            with open(self.patrons_path, encoding='utf-8') as f:
                patrons_data = json.load(f)
                self.patrons = [Patron(id=p['Id'], name=p['Name'], membership_end=self._parse_datetime(p['MembershipEnd']), membership_start=self._parse_datetime(p['MembershipStart']), image_name=p.get('ImageName')) for p in patrons_data]
            with open(self.loans_path, encoding='utf-8') as f:
                loans_data = json.load(f)
                self.loans = [Loan(id=l['Id'], book_item_id=l['BookItemId'], patron_id=l['PatronId'], loan_date=self._parse_datetime(l['LoanDate']), due_date=self._parse_datetime(l['DueDate']), return_date=self._parse_datetime(l['ReturnDate'])) for l in loans_data]
            self._loaded = True

            # Build lookup dictionaries for fast access
            book_item_dict = {bi.id: bi for bi in self.book_items}
            book_dict = {b.id: b for b in self.books}
            author_dict = {a.id: a for a in self.authors}
            patron_dict = {p.id: p for p in self.patrons}

            # Link book_item and book to each loan
            for loan in self.loans:
                loan.book_item = book_item_dict.get(loan.book_item_id)
                if loan.book_item:
                    loan.book_item.book = book_dict.get(loan.book_item.book_id)
                    if loan.book_item.book:
                        loan.book_item.book.author = author_dict.get(loan.book_item.book.author_id)
                loan.patron = patron_dict.get(loan.patron_id)
            # Optionally, link loans to patrons
            for patron in self.patrons:
                patron.loans = [loan for loan in self.loans if loan.patron_id == patron.id]

        except (FileNotFoundError, json.JSONDecodeError) as e:
            print(f"Error loading data: {e}")
            self._loaded = False

    def save_loans(self, loans: List[Loan]) -> None:
        try:
            with open(self.loans_path, 'w', encoding='utf-8') as f:
                json.dump([
                    {
                        'Id': l.id,
                        'BookItemId': l.book_item_id,
                        'PatronId': l.patron_id,
                        'LoanDate': l.loan_date.isoformat() if l.loan_date else None,
                        'DueDate': l.due_date.isoformat() if l.due_date else None,
                        'ReturnDate': l.return_date.isoformat() if l.return_date else None
                    } for l in loans
                ], f, indent=2)
        except Exception as e:
            print(f"Error saving loans: {e}")

    def save_patrons(self, patrons: List[Patron]) -> None:
        try:
            with open(self.patrons_path, 'w', encoding='utf-8') as f:
                json.dump([
                    {
                        'Id': p.id,
                        'Name': p.name,
                        'MembershipEnd': p.membership_end.isoformat() if p.membership_end else None,
                        'MembershipStart': p.membership_start.isoformat() if p.membership_start else None,
                        'ImageName': p.image_name
                    } for p in patrons
                ], f, indent=2)
        except Exception as e:
            print(f"Error saving patrons: {e}")
