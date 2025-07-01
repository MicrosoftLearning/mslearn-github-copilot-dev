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