from ..interfaces.iloan_service import ILoanService
from ..interfaces.iloan_repository import ILoanRepository
from ..enums.loan_return_status import LoanReturnStatus
from ..enums.loan_extension_status import LoanExtensionStatus
from datetime import datetime, timedelta

class LoanService(ILoanService):
    EXTEND_BY_DAYS = 14

    def __init__(self, loan_repository: ILoanRepository):
        self._loan_repository = loan_repository

    def return_loan(self, loan_id: int) -> LoanReturnStatus:
        loan = self._loan_repository.get_loan(loan_id)
        if loan is None:
            return LoanReturnStatus.LOAN_NOT_FOUND
        if loan.return_date is not None:
            return LoanReturnStatus.ALREADY_RETURNED
        loan.return_date = datetime.now()
        try:
            self._loan_repository.update_loan(loan)
            return LoanReturnStatus.SUCCESS
        except Exception:
            return LoanReturnStatus.ERROR

    def extend_loan(self, loan_id: int) -> LoanExtensionStatus:
        loan = self._loan_repository.get_loan(loan_id)
        if loan is None:
            return LoanExtensionStatus.LOAN_NOT_FOUND
        if loan.patron and loan.patron.membership_end < datetime.now():
            return LoanExtensionStatus.MEMBERSHIP_EXPIRED
        if loan.return_date is not None:
            return LoanExtensionStatus.LOAN_RETURNED
        if loan.due_date < datetime.now():
            return LoanExtensionStatus.LOAN_EXPIRED
        try:
            loan.due_date = loan.due_date + timedelta(days=self.EXTEND_BY_DAYS)
            self._loan_repository.update_loan(loan)
            return LoanExtensionStatus.SUCCESS
        except Exception:
            return LoanExtensionStatus.ERROR

    def checkout_book(self, patron, book_item, loan_id=None) -> None:
        from ..entities.loan import Loan
        from datetime import datetime, timedelta
        # Generate a new loan ID if not provided
        if loan_id is None:
            all_loans = getattr(self._loan_repository, 'get_all_loans', lambda: [])()
            max_id = 0
            for l in all_loans:
                if l.id > max_id:
                    max_id = l.id
            loan_id = max_id + 1 if all_loans else 1
        now = datetime.now()
        due = now + timedelta(days=14)
        new_loan = Loan(
            id=loan_id,
            book_item_id=book_item.id,
            patron_id=patron.id,
            patron=patron,
            loan_date=now,
            due_date=due,
            return_date=None,
            book_item=book_item
        )
        self._loan_repository.add_loan(new_loan)
        return new_loan
