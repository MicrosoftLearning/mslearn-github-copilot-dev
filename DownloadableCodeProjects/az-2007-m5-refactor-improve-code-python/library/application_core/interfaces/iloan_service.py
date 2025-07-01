from abc import ABC, abstractmethod
from ..enums.loan_return_status import LoanReturnStatus
from ..enums.loan_extension_status import LoanExtensionStatus

class ILoanService(ABC):
    @abstractmethod
    def return_loan(self, loan_id: int) -> LoanReturnStatus:
        pass

    @abstractmethod
    def extend_loan(self, loan_id: int) -> LoanExtensionStatus:
        pass

    @abstractmethod
    def checkout_book(self, patron, book_item, loan_id=None) -> None:
        pass
