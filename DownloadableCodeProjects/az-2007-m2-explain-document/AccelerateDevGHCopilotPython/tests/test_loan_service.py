import unittest
from unittest.mock import MagicMock
from application_core.services.loan_service import LoanService
from application_core.entities.loan import Loan
from application_core.entities.patron import Patron
from application_core.enums.loan_extension_status import LoanExtensionStatus
from application_core.enums.loan_return_status import LoanReturnStatus
from datetime import datetime, timedelta

class LoanServiceTest(unittest.TestCase):
    def setUp(self):
        self.mock_repo = MagicMock()
        self.service = LoanService(self.mock_repo)

    def test_extend_loan_success(self):
        print("Running test_extend_loan_success...")
        patron = Patron(id=1, name="John Doe", membership_end=datetime.now()+timedelta(days=1), membership_start=datetime.now()-timedelta(days=30))
        loan = Loan(id=1, book_item_id=1, patron_id=1, patron=patron, loan_date=datetime.now()-timedelta(days=2), due_date=datetime.now()+timedelta(days=1))
        self.mock_repo.get_loan.return_value = loan
        status = self.service.extend_loan(1)
        print(f"extend_loan status: {status}")
        self.assertEqual(status, LoanExtensionStatus.SUCCESS)

    def test_return_loan_not_found(self):
        print("Running test_return_loan_not_found...")
        self.mock_repo.get_loan.return_value = None
        status = self.service.return_loan(1)
        print(f"return_loan status: {status}")
        self.assertEqual(status, LoanReturnStatus.LOAN_NOT_FOUND)

if __name__ == "__main__":
    unittest.main()
