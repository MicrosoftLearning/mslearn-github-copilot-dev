import unittest
from unittest.mock import MagicMock
from application_core.services.loan_service import LoanService
from application_core.entities.loan import Loan
from application_core.enums.loan_return_status import LoanReturnStatus
from datetime import datetime, timedelta

class TestLoanService(unittest.TestCase):
    def setUp(self):
        self.mock_repo = MagicMock()
        self.service = LoanService(self.mock_repo)

    def test_return_loan_success(self):
        loan = Loan(id=1, book_item_id=1, patron_id=1, patron=None, loan_date=datetime.now()-timedelta(days=10), due_date=datetime.now()+timedelta(days=10), return_date=None, book_item=None)
        self.mock_repo.get_loan.return_value = loan
        self.mock_repo.update_loan.return_value = None
        status = self.service.return_loan(1)
        self.assertEqual(status, LoanReturnStatus.SUCCESS)

    def test_return_loan_not_found(self):
        self.mock_repo.get_loan.return_value = None
        status = self.service.return_loan(1)
        self.assertEqual(status, LoanReturnStatus.LOAN_NOT_FOUND)

if __name__ == "__main__":
    unittest.main()

import pytest

@pytest.fixture
def mock_loan_repo():
    class MockRepo:
        def __init__(self):
            self.loans = {}
            self.updated = False
        def get_loan(self, loan_id):
            return self.loans.get(loan_id)
        def update_loan(self, loan):
            self.updated = True
        def add_loan(self, loan):
            self.loans[loan.id] = loan
        def get_all_loans(self):
            return list(self.loans.values())
    return MockRepo()

@pytest.mark.parametrize("loan_id,exists,expected_status", [
    (1, True, LoanReturnStatus.SUCCESS),
    (2, False, LoanReturnStatus.LOAN_NOT_FOUND),
])
def test_return_loan_param(mock_loan_repo, loan_id, exists, expected_status):
    from application_core.services.loan_service import LoanService
    from application_core.entities.loan import Loan
    from datetime import datetime, timedelta
    repo = mock_loan_repo
    if exists:
        repo.loans[loan_id] = Loan(id=loan_id, book_item_id=1, patron_id=1, loan_date=datetime.now()-timedelta(days=2), due_date=datetime.now()+timedelta(days=2), return_date=None)
    service = LoanService(repo)
    status = service.return_loan(loan_id)
    assert status == expected_status

def test_extend_loan_membership_expired(mock_loan_repo):
    from application_core.services.loan_service import LoanService
    from application_core.entities.loan import Loan
    from application_core.entities.patron import Patron
    from application_core.enums.loan_extension_status import LoanExtensionStatus
    from datetime import datetime, timedelta
    patron = Patron(id=1, name="Expired", membership_end=datetime.now()-timedelta(days=1), membership_start=datetime.now()-timedelta(days=365))
    loan = Loan(id=1, book_item_id=1, patron_id=1, patron=patron, loan_date=datetime.now()-timedelta(days=2), due_date=datetime.now()+timedelta(days=2), return_date=None)
    mock_loan_repo.loans[1] = loan
    service = LoanService(mock_loan_repo)
    status = service.extend_loan(1)
    assert status == LoanExtensionStatus.MEMBERSHIP_EXPIRED

def test_checkout_book_raises_on_missing_patron(mock_loan_repo):
    from application_core.services.loan_service import LoanService
    from application_core.entities.book_item import BookItem
    service = LoanService(mock_loan_repo)
    with pytest.raises(AttributeError):
        # Patron is None, should raise when accessing patron.id
        service.checkout_book(None, BookItem(id=1, book_id=1, acquisition_date=datetime.now()))

def test_return_loan_assertion(mock_loan_repo):
    from application_core.services.loan_service import LoanService
    service = LoanService(mock_loan_repo)
    with pytest.raises(AssertionError):
        # This will fail because we assert False
        assert service.return_loan(999) == LoanReturnStatus.SUCCESS