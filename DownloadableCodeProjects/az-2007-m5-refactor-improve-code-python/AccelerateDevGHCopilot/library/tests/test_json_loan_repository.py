import sys
import unittest
from pathlib import Path

# Add the parent directory to sys.path for imports
sys.path.append(str(Path(__file__).resolve().parent.parent))

from infrastructure.json_loan_repository import JsonLoanRepository
from application_core.entities.loan import Loan
from application_core.entities.book_item import BookItem
from application_core.entities.patron import Patron
from datetime import datetime, timedelta

class DummyJsonData:
    def __init__(self):
        self.loans = []
        self.save_loans_called = False

    def save_loans(self, loans):
        self.save_loans_called = True

    def load_data(self):
        pass

class TestJsonLoanRepository(unittest.TestCase):
    def setUp(self):
        self._json_data = DummyJsonData()
        test_patron = Patron(id=1, name="Test Patron", membership_end=datetime.now()+timedelta(days=30), membership_start=datetime.now()-timedelta(days=365))
        test_book_item = BookItem(id=1, book_id=1, acquisition_date=datetime.now()-timedelta(days=100))
        test_loan = Loan(id=1, book_item_id=1, patron_id=1, patron=test_patron, loan_date=datetime.now()-timedelta(days=10), due_date=datetime.now()+timedelta(days=4), return_date=None, book_item=test_book_item)
        self._json_data.loans = [test_loan]
        self._json_loan_repository = JsonLoanRepository(self._json_data)

    def test_get_loan(self):
        loan = self._json_loan_repository.get_loan(1)
        self.assertIsNotNone(loan)
        self.assertEqual(loan.id, 1)

    def test_get_loan_not_found(self):
        loan = self._json_loan_repository.get_loan(999)
        self.assertIsNone(loan)

    def test_get_loan_found(self):
        # Test case where loan with id=1 exists
        found_loan = self._json_loan_repository.get_loan(1)
        self.assertIsNotNone(found_loan)
        self.assertEqual(found_loan.id, 1)

    def test_get_loan_not_found_again(self):
        # Test case where loan with id=2 does not exist
        not_found_loan = self._json_loan_repository.get_loan(2)
        self.assertIsNone(not_found_loan)

if __name__ == "__main__":
    unittest.main()

import pytest

@pytest.fixture
def dummy_json_data_with_loans():
    from application_core.entities.loan import Loan
    from application_core.entities.book_item import BookItem
    from application_core.entities.patron import Patron
    from datetime import datetime, timedelta

    class DummyJsonData:
        def __init__(self):
            self.loans = []
            self.save_loans_called = False
        def save_loans(self, loans):
            self.save_loans_called = True
        def load_data(self):
            pass

    json_data = DummyJsonData()
    test_patron = Patron(id=1, name="Test Patron", membership_end=datetime.now()+timedelta(days=30), membership_start=datetime.now()-timedelta(days=365))
    test_book_item = BookItem(id=1, book_id=1, acquisition_date=datetime.now()-timedelta(days=100))
    test_loan = Loan(id=1, book_item_id=1, patron_id=1, patron=test_patron, loan_date=datetime.now()-timedelta(days=10), due_date=datetime.now()+timedelta(days=4), return_date=None, book_item=test_book_item)
    json_data.loans = [test_loan]
    return json_data

@pytest.mark.parametrize("loan_id,expected", [
    (1, True),
    (999, False),
])
def test_get_loan_param(dummy_json_data_with_loans, loan_id, expected):
    from infrastructure.json_loan_repository import JsonLoanRepository
    repo = JsonLoanRepository(dummy_json_data_with_loans)
    loan = repo.get_loan(loan_id)
    assert (loan is not None) == expected

def test_update_loan_raises_on_missing(dummy_json_data_with_loans):
    from infrastructure.json_loan_repository import JsonLoanRepository
    from application_core.entities.loan import Loan
    repo = JsonLoanRepository(dummy_json_data_with_loans)
    missing_loan = Loan(id=999, book_item_id=1, patron_id=1)
    # Should not raise, but let's assert update does not call save_loans
    repo.update_loan(missing_loan)
    assert not dummy_json_data_with_loans.save_loans_called

def test_add_loan_and_save(dummy_json_data_with_loans):
    from infrastructure.json_loan_repository import JsonLoanRepository
    from application_core.entities.loan import Loan
    from datetime import datetime
    repo = JsonLoanRepository(dummy_json_data_with_loans)
    new_loan = Loan(id=2, book_item_id=2, patron_id=2, loan_date=datetime.now(), due_date=datetime.now())
    repo.add_loan(new_loan)
    assert dummy_json_data_with_loans.save_loans_called
    assert any(l.id == 2 for l in dummy_json_data_with_loans.loans)

def test_get_loans_by_patron_id(dummy_json_data_with_loans):
    from infrastructure.json_loan_repository import JsonLoanRepository
    repo = JsonLoanRepository(dummy_json_data_with_loans)
    result = repo.get_loans_by_patron_id(1)
    assert len(result) == 1
    assert result[0].patron_id == 1

def test_get_loan_assertion(dummy_json_data_with_loans):
    from infrastructure.json_loan_repository import JsonLoanRepository
    repo = JsonLoanRepository(dummy_json_data_with_loans)
    with pytest.raises(AssertionError):
        # This will fail because we assert False
        assert repo.get_loan(999) is not None
