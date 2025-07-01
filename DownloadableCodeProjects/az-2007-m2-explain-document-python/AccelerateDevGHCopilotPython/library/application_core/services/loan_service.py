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
