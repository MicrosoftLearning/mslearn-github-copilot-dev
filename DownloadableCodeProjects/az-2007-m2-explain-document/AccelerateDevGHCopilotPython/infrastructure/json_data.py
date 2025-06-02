import json
from pathlib import Path
from application_core.entities.author import Author
from application_core.entities.book import Book
from application_core.entities.book_item import BookItem
from application_core.entities.patron import Patron
from application_core.entities.loan import Loan
from typing import List
from datetime import datetime

class JsonData:
    def __init__(self, base_path='PythonApp/console/Json'):
        self.base_path = Path(base_path)
        self.authors: List[Author] = []
        self.books: List[Book] = []
        self.book_items: List[BookItem] = []
        self.patrons: List[Patron] = []
        self.loans: List[Loan] = []
        self._loaded = False
        self.load_data()

    def _parse_datetime(self, value):
        if value is None:
            return None
        return datetime.fromisoformat(value)

    def load_data(self):
        # Authors
        with open(self.base_path / 'Authors.json', encoding='utf-8') as f:
            authors_data = json.load(f)
            self.authors = [Author(id=a['Id'], name=a['Name']) for a in authors_data]
        # Books
        with open(self.base_path / 'Books.json', encoding='utf-8') as f:
            books_data = json.load(f)
            self.books = [Book(id=b['Id'], title=b['Title'], author_id=b['AuthorId'], genre=b['Genre'], image_name=b['ImageName'], isbn=b['ISBN']) for b in books_data]
        # BookItems
        with open(self.base_path / 'BookItems.json', encoding='utf-8') as f:
            items_data = json.load(f)
            self.book_items = [BookItem(id=bi['Id'], book_id=bi['BookId'], acquisition_date=self._parse_datetime(bi['AcquisitionDate']), condition=bi.get('Condition')) for bi in items_data]
        # Patrons
        with open(self.base_path / 'Patrons.json', encoding='utf-8') as f:
            patrons_data = json.load(f)
            self.patrons = [Patron(id=p['Id'], name=p['Name'], membership_end=self._parse_datetime(p['MembershipEnd']), membership_start=self._parse_datetime(p['MembershipStart']), image_name=p.get('ImageName')) for p in patrons_data]
        # Loans
        with open(self.base_path / 'Loans.json', encoding='utf-8') as f:
            loans_data = json.load(f)
            self.loans = [Loan(id=l['Id'], book_item_id=l['BookItemId'], patron_id=l['PatronId'], loan_date=self._parse_datetime(l['LoanDate']), due_date=self._parse_datetime(l['DueDate']), return_date=self._parse_datetime(l['ReturnDate'])) for l in loans_data]
        self._loaded = True

    def save_loans(self, loans: List[Loan]):
        # Save loans to JSON
        with open(self.base_path / 'Loans.json', 'w', encoding='utf-8') as f:
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

    def save_patrons(self, patrons: List[Patron]):
        # Save patrons to JSON
        with open(self.base_path / 'Patrons.json', 'w', encoding='utf-8') as f:
            json.dump([
                {
                    'Id': p.id,
                    'Name': p.name,
                    'MembershipEnd': p.membership_end.isoformat() if p.membership_end else None,
                    'MembershipStart': p.membership_start.isoformat() if p.membership_start else None,
                    'ImageName': p.image_name
                } for p in patrons
            ], f, indent=2)
